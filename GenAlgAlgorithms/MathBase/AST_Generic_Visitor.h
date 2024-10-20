#pragma once
#include "AST.h"

class ASTNode;

class ASTVisitor {
protected:
	std::string error = "";
public:
	virtual void visitNumber(NumberNode& number) = 0;
	virtual void visitVariable(VariableNode& variable) = 0;
	virtual void visitOperation(OperationNode& operation) = 0;
	virtual void visitUnary(UnaryNode& operation) = 0;
	virtual void visitFunction(FunctionNode& func) = 0;
	void setError(std::string error) {
		this->error = error;
	}
	bool hasError() {
		return error != "";
	}
	void clearError() {
		error = "";
	}
	std::string getError() {
		return error;
	}
};

class ASTNode {
public:
	virtual void accept(ASTVisitor& visitor) = 0;
};

class AST {
private:
	ASTNode* root = nullptr;
public:
	ASTNode* getRoot() {
		return root;
	}
};