#pragma once
#include <iostream>
#include <vector>


#include "AST_Generic_Visitor.h"


class OperationNode : public ASTNode {
public:
	ASTNode* left = nullptr;
	ASTNode* right = nullptr;
	std::string operation;
	OperationNode(ASTNode* left, std::string operation, ASTNode* right) {
		this->left = left;
		this->right = right;
		this->operation = operation;
	}
	void accept(ASTVisitor& visitor) {
		visitor.visitOperation(*this);
	}
};

class UnaryNode : public ASTNode {
public:
	ASTNode* target;
	std::string operation;
	UnaryNode(std::string operation, ASTNode* target) {
		this->target = target;
		this->operation = operation;
	}
	void accept(ASTVisitor visitor) {
		visitor.visitUnary(*this);
	}
};

class ErrorNode : public ASTNode {
	void accept(ASTVisitor visitor) {
		visitor.setError("Visited error node");
	}
};
class ValueNode : public ASTNode {
};
class NumberNode : public ValueNode {
public:
	std::string digits;
	NumberNode(std::string digits) {
		this->digits = digits;
	}
	void accept(ASTVisitor visitor) {
		visitor.visitNumber(*this);
	}
};
class VariableNode : public ValueNode {
public:
	std::string var_name;
	VariableNode(std::string var_name) {
		this->var_name = var_name;
	}
};
class FunctionNode : public ASTNode {
public:
	std::string functionName;
	std::vector<ASTNode> arguments;

	std::size_t num_argumnets() {
		return arguments.size();
	}
	FunctionNode(std::string name, std::vector<ASTNode> arguments) {
		this->functionName = name;
		this->arguments = arguments;
	}
	bool isNamed(std::string expected_name) {
		return expected_name == functionName;
	}
};