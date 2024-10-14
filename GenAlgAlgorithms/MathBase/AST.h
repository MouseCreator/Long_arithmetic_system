#pragma once
#include <iostream>
#include "AST_Visitor.h"
#include <vector>


class ASTNode {
	virtual void accept(ASTVisitor visitor);
};


class AST {
private:
	ASTNode root;
public:
	ASTNode getRoot() {
		return root;
	}
};

class OperationNode : public ASTNode {
public:
	ASTNode left;
	ASTNode right;
	std::string operation;
	OperationNode(ASTNode left, std::string operation, ASTNode right) {
		this->left = left;
		this->right = right;
		this->operation = operation;
	}
};

class UnaryNode : public ASTNode {
public:
	ASTNode target;
	std::string operation;
	UnaryNode(std::string operation, ASTNode target) {
		this->target = target;
		this->operation = operation;
	}
};

class ErrorNode : public ASTNode {
};
class ValueNode : public ASTNode {
};
class NumberNode : public ValueNode {
public:
	std::string digits;
	NumberNode(std::string digits) {
		this->digits = digits;
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
};