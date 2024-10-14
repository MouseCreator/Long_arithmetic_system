#pragma once

#include <iostream>
#include "AST.h"
#include "Token.h"
namespace parse {
	class Parser {
		virtual AST parse(std::vector<Token> tokens) = 0;
	};
	enum PARSE_STATE {
		BEGIN_EXPESSION
	};
	enum LAYER_TYPE {
		TOP, ADDITIVE, FACTOR, EXPRESSION
	};
	class LayerItem {
	private:
		std::vector<Token> tokens;
		std::string operation;
	public:
		LayerItem(std::vector<Token> tokens,
			std::string operation) {
			this->tokens = tokens;
			this->operation = operation;
		}

		std::string getOperation() {
			return operation;
		}
		std::vector<Token> getTokens() {
			return tokens;
		}

	};

	std::vector<LayerItem> sample_split(std::vector<Token> current_layer, std::vector<TokenError>& errors, std::vector<std::string> matches) {
		int bracket_level = 0;
		std::vector<LayerItem> layer_items;
		std::vector<Token> current = std::vector<Token>();
		for (std::size_t i = 0; i < current_layer.size(); i++) {
			Token current_token = current_layer[i];
			if (bracket_level == 0) {
				if (current_token.value == ")") {
					std::string pos = std::to_string(current_token.position);
					errors.push_back(TokenError(UNEXPECTED_TOKEN, "Bracket ')' at " + pos + " has no matching bracket"));
					return std::vector<LayerItem>();
				}
				else if (current_token.value == "(") {
					bracket_level++;
					current.push_back(current_token);
				}
				else {
					bool match = false;
					for (std::string m : matches) {
						if (current_token.value == m) {
							match = true;
							break;
						}
					}
					if (match) {
						if (current.empty()) {
							errors.push_back(TokenError(EMPTY_INPUT, "Empty input"));
							return std::vector<LayerItem>();
						}
						layer_items.push_back(LayerItem(current, current_token.value));
						current = std::vector<Token>();
					}
				}
			}
			else {
				if (current_token.value == ")") {
					bracket_level--;
				}
				else if (current_token.value == "(") {
					bracket_level++;
				}
				current.push_back(current_token);
			}	
		}
		if (bracket_level > 0) {
			std::string missing_brackets = std::to_string(bracket_level);
			errors.push_back(TokenError(UNEXPECTED_TOKEN, missing_brackets + " brackets opened but not closed!"));
		}
		else if (current.empty()) {
			errors.push_back(TokenError(EMPTY_INPUT, "Empty input"));
		}
		return layer_items;
	}

	

	class Layer {	
	public:
		virtual ASTNode build(std::vector<Token> tokens, std::vector<TokenError>& errors) = 0;
	};
	class UnaryMinusLayer;
	

	ASTNode to_next_layer(Layer* layer, std::vector<LayerItem> items, std::vector<TokenError>& errors) {
		if (!errors.empty()) {
			return ErrorNode();
		}
		LayerItem item = items[0];
		ASTNode left = layer->build(item.getTokens(), errors);
		std::size_t i = 0;
		while (item.getOperation() != "") {
			LayerItem next = items[i + 1];
			ASTNode right = layer->build(item.getTokens(), errors);
			left = OperationNode(left, item.getOperation(), right);
			i++;
			item = items[i];
		}
		return left;
	}

	class ExpressionLayer : public Layer {
	private:
		UnaryMinusLayer topLayer;

		std::string token_error_desc(std::vector<Token> tokens, std::size_t begin = 1) {
			std::string s = "[ ";
			std::size_t size = tokens.size();
			for (std::size_t i = begin; i < size; i++) {
				s += tokens[i].value;
				s += " ";
			}
			s += "]";
			return s;
		}
	public:
		ASTNode build(std::vector<Token> tokens, std::vector<TokenError>& errors) override {
			std::size_t s = tokens.size();
			if (tokens[0].type == FUNCTION) {
				if (tokens[1].value != "(" || tokens[s - 1].value != ")") {
					errors.push_back(TokenError(UNEXPECTED_TOKEN, "Function should be covered in brackets"));
					return ErrorNode();
				}
				std::string func_name = tokens[0].value;

				std::vector<Token> updated_tokens;
				for (int i = 2; i < s - 1; i++) { //func_name( <updated_tokens> )
					updated_tokens.push_back(tokens[i]);
				}

				std::vector<LayerItem> items = sample_split(tokens, errors, { "," });
				std::vector<ASTNode> arguments;
				for (LayerItem item : items) {
					ASTNode node = topLayer.build(item.getTokens(), errors);
					arguments.push_back(node);
				}
				return FunctionNode(func_name, arguments);
			}
			else if (tokens[0].type == VARIABLE) {
				VariableNode node = VariableNode(tokens[0].value);
				if (tokens.size() > 1) {
					std::string extra_tokens_message = token_error_desc(tokens);
					errors.push_back(TokenError(COMPLEX_EXPRESSION, "Expression has extra tokens, cannot match: " + extra_tokens_message));
				}
				return node;
			}
			else if (tokens[0].type == NUMBER) {
				NumberNode number = NumberNode(tokens[0].value);
				if (tokens.size() > 1) {
					std::string extra_tokens_message = token_error_desc(tokens);
					errors.push_back(TokenError(COMPLEX_EXPRESSION, "Expression has extra tokens, cannot match: " + extra_tokens_message));
				}
				return number;
			}
			else if (tokens[0].type == BRACKET) {
				if (tokens[1].value != "(" || tokens[s - 1].value != ")") {
					errors.push_back(TokenError(UNEXPECTED_TOKEN, "Function should be covered in brackets"));
					return ErrorNode();
				}
				std::string func_name = tokens[0].value;

				std::vector<Token> updated_tokens;
				for (int i = 2; i < s - 1; i++) { //( <updated_tokens> )
					updated_tokens.push_back(tokens[i]);
				}

				return topLayer.build(updated_tokens, errors);
			}
			else {
				std::string extra_tokens_message = token_error_desc(tokens, 0);
				errors.push_back(TokenError(UNEXPECTED_TOKEN, "Cannot process expression: " + extra_tokens_message));
				return ErrorNode();
			}

		}
	};

	class ExponentModuloLayer : public Layer {
	private:
		ExpressionLayer expLayer;
	public:
		ASTNode build(std::vector<Token> tokens, std::vector<TokenError>& errors) override {
			std::vector<LayerItem> items = sample_split(tokens, errors, { "^", "%" });
			return to_next_layer(&expLayer, items, errors);
		}
	};
	class MultiplyDivideLayer : public Layer {
	private:
		ExpressionLayer nextLayer;
		std::vector<Token> add_skipped_multiply_signs(std::vector<Token> tokens) {
			std::size_t size = tokens.size();
			std::vector<Token> tokensExtra;

			for (std::size_t i = 0; i < size - 1; i++) {
				bool addMultiply = false;
				if (tokens[i].type == VARIABLE && tokens[i + 1].type == NUMBER) {
					addMultiply = true;
				}
				if (tokens[i].type == NUMBER && tokens[i + 1].type == VARIABLE) {
					addMultiply = true;
				}
				tokensExtra.push_back(tokens[i]);
				if (addMultiply) {
					Token newToken = Token();
					newToken.position = tokens[i].position;
					newToken.type = OPERATOR;
					newToken.value = "*";
					tokensExtra.push_back(newToken);
				}
			}
			return tokensExtra;
		}
	public:
		ASTNode build(std::vector<Token> tokens, std::vector<TokenError>& errors) override {
			std::vector<Token> tokensExtra = add_skipped_multiply_signs(tokens);
			std::vector<LayerItem> items = sample_split(tokensExtra, errors, { "*", "/" });
			return to_next_layer(&nextLayer, items, errors);
		}
	};

	class PlusMinusLayer : public Layer {
	private:
		MultiplyDivideLayer nextLayer = MultiplyDivideLayer();
	public:
		ASTNode build(std::vector<Token> tokens, std::vector<TokenError>& errors) override {
			std::vector<LayerItem> items = sample_split(tokens, errors, { "+", "-" });
			return to_next_layer(&nextLayer, items, errors);
		}
	};

	class UnaryMinusLayer : public Layer {
	private:
		PlusMinusLayer layer = PlusMinusLayer();
	public:
		ASTNode build(std::vector<Token> tokens, std::vector<TokenError>& errors) override {
			std::vector<Token> remainingTokens;
			bool unaries_end = false;
			bool is_minus = false;
			for (std::size_t i = 0; i < tokens.size(); i++) {
				if (unaries_end) {
					remainingTokens.push_back(tokens[i]);
				}
				else {
					if (tokens[i].value == "-") {
						is_minus = !is_minus;						
					}
					else {
						remainingTokens.push_back(tokens[i]);
					}
				}
			}
			ASTNode next_layer = layer.build(remainingTokens, errors);
			if (is_minus) {
				return UnaryNode("-", next_layer);
			}
			return next_layer;
		}
	};

	class ParseSession {
	private:
		int current_token_index;
		int lookeahead_token_index;
		Token* current;
		Token* lookahead;

		std::size_t size;
		std::vector<Token> tokens;
		std::vector<TokenError> errors;

		PARSE_STATE state;

		bool forced_failure;
		bool finished;

		std::vector<Token> stack;

		void _parse() {
			for (; current_token_index < size; ) {
				
			}
		}

		bool check_stack_top(std::string type) {

		}

		
	public:
		ParseSession(std::vector<Token> tokens) {
			this->tokens = tokens;
			current = tokens.size() > 0 ? &tokens[0] : nullptr;
			lookahead = tokens.size() > 1 ? &tokens[1] : nullptr;
			current_token_index = 0;
			lookeahead_token_index = 1;
			state = BEGIN_EXPESSION;
			forced_failure = false;
			finished = false;
		}
	};

	class LRParser : public Parser {
		AST parse(std::vector<Token> tokens) override {

		}
	};
}