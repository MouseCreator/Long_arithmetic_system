#pragma once
#include "AST.h"
#include <sstream>
class ASTVisitor {
	virtual void visit(ASTNode node) = 0;
	virtual void visit(NumberNode number) = 0;
	virtual void visit(VariableNode variable) = 0;
	virtual void visit(OperationNode operation) = 0;
	virtual void visit(FunctionNode func) = 0;
};

class PrettyPrinterVisitor : public ASTVisitor {
	std::stringstream stream;
	virtual void visit(ASTNode node) {
	}
	virtual void visit(NumberNode number) {
		stream << number.digits;
	}
	virtual void visit(VariableNode variable) {
		stream << variable.var_name;
	}
	virtual void visit(OperationNode operation) {
		visit(operation.left);
		stream << operation.operation;
		visit(operation.right);
	}
	virtual void visit(FunctionNode func) {
		stream << func.functionName << "(";
		std::size_t num_arguments = func.num_argumnets();
		if (num_arguments == 0) {
			stream << ")";
			return;
		}
		visit(func.arguments[0]);
		for (std::size_t i = 1; i < num_arguments; i++) {
			stream << ",";
			visit(func.arguments[i]);
		}		
	}
};