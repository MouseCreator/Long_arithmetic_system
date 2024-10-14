#pragma once
#include "AST.h"
#include <sstream>
#include "SignedNumber.h"
template <typename T>
class ASTVisitor {
	virtual T visit(ASTNode node) = 0;
	virtual T visit(NumberNode number) = 0;
	virtual T visit(VariableNode variable) = 0;
	virtual T visit(OperationNode operation) = 0;
	virtual T visit(FunctionNode func) = 0;
};


class InfNumberFieldInterpreter : public ASTVisitor<SignedNumber> {

private:
	SignedNumber result = SignedNumber();
public:
	virtual SignedNumber visit(ASTNode node) = 0;
	virtual SignedNumber visit(NumberNode number) {
		std::string digits = number.digits;
		return SignedNumber(digits);
	}
	virtual SignedNumber visit(VariableNode variable) {

	}
	virtual SignedNumber visit(OperationNode operation) {
		if (operation.operation == "+") {

		}
		else if (operation.operation == "-") {

		}
		else if (operation.operation == "*") {

		}
		else if (operation.operation == "/") {

		}
		else if (operation.operation == "^") {

		}
		else if (operation.operation == "%") {

		}
		else {

		}
	}
	virtual SignedNumber visit(FunctionNode func) {
		if (func.isNamed("sqrt")) {
			// square root
		}
		else if (func.isNamed("log")) {
			// discrete logarithm
		}
		else if (func.isNamed("eul")) {
			// euler
		}
		else if (func.isNamed("kar")) {
			// karmichael
		}
		else {
			// error
		}
	}
};