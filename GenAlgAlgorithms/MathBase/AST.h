#pragma once
#include <iostream>
#include "AST_Visitor.h"
#include <vector>
class ASTNode {
	virtual void accept(ASTVisitor visitor);
};

class OperationNode : public ASTNode {
public:
	ASTNode left;
	ASTNode right;
	std::string operation;
};

class UnaryNode : public ASTNode {
public:
	ASTNode left;
	std::string operation;
};

class ValueNode : public ASTNode {
};
class NumberNode : public ValueNode {
public:
	std::string digits;
};
class VariableNode : public ValueNode {
public:
	std::string var_name;
};
class FunctionNode : public ASTNode {
public:
	std::string functionName;
	std::vector<ASTNode> arguments;

	std::size_t num_argumnets() {
		return arguments.size();
	}
};