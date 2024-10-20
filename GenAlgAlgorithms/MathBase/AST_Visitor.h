#pragma once

#include <sstream>
#include <iostream>
#include <optional>

#include "AST.h"
#include "SignedNumber.h"
#include "AST_Generic_Visitor.h"


class InfNumberFieldInterpreter : public ASTVisitor {
private:
	SignedNumber buffer;
public:
	SignedNumber visit(NumberNode number) {
		std::string digits = number.digits;
		return SignedNumber(digits);
	}
	void visitVariable(VariableNode& variable) override{
		setError("Variable is not allowed here: " + variable.var_name);
	}
	void visitOperation(OperationNode& operation) override {
		
	}
	void visitFunction(FunctionNode& func) {
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
	void visitUnary(UnaryNode& operation) override {

	}
	void visitNumber(NumberNode& number) override {

	}
	void writeBuffer(SignedNumber s) {
		this->buffer = s;
	}
	SignedNumber readBuffer() {
		return buffer;
	}
	std::optional <SignedNumber> safeReadBuffer() {
		if (hasError()) {
			return {};
		}
		return buffer;
	}
};