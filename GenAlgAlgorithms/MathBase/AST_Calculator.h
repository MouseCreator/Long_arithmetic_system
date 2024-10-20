#pragma once
#include <iostream>
#include "AST.h"
#include "SignedNumber.h"
#include "AST_Visitor.h"
template <typename NodeType>
class AST_Calculator {
public:
	virtual void calculate(NodeType node) = 0;
};

class AST_Operator_Calculator : public AST_Calculator<OperationNode> {
public:
	virtual std::string forOperation() = 0;
	virtual void calculate(OperationNode node) = 0;
};
template<typename T>
class LROperands {
	T left;
	T right;

public:
	LROperands(T left, T right) {
		this->left = left;
		this->right = right;
	}
	T getLeft() {
		return left;
	}
	T getRight() {
		return right;
	}
};

class LROperands_Signed : public LROperands<SignedNumber> {
public:
	LROperands_Signed(SignedNumber left, SignedNumber right) : LROperands(left, right) {}
};
class AST_INF_Operator_Calculator : public AST_Operator_Calculator {
protected:
	InfNumberFieldInterpreter calculator;

	std::optional<LROperands_Signed> getOperands(OperationNode node) {
		node.left->accept(calculator);
		auto val = calculator.safeReadBuffer();
		if (!val) {
			return {};
		}
		SignedNumber left = val.value();
		node.right->accept(calculator);
		val = calculator.safeReadBuffer();
		if (!val) {
			return {};
		}
		SignedNumber right = val.value();
		return LROperands_Signed(left, right);
	}
public:
	virtual std::string forOperation() = 0;
	virtual void calculate(OperationNode node) = 0;
	AST_INF_Operator_Calculator(InfNumberFieldInterpreter calculator) {
		this->calculator = calculator;
	}
};

class AST_INF_Plus_Calculator : public AST_INF_Operator_Calculator {
public:
	std::string forOperation() {
		return "+";
	}
	void calculate(OperationNode node) {
		auto op = getOperands(node);
		if (!op) {
			return;
		}
		LROperands_Signed lr = op.value();
		SignedNumber sum = lr.getLeft() + lr.getRight();
		calculator.writeBuffer(sum);
	}
};

class AST_INF_Minus_Calculator : public AST_INF_Operator_Calculator {
private:
	InfNumberFieldInterpreter calculator;
public:
	std::string forOperation() {
		return "-";
	}
	void calculate(OperationNode node) {
		auto op = getOperands(node);
		if (!op) {
			return;
		}
		LROperands_Signed lr = op.value();
		SignedNumber sum = lr.getLeft() - lr.getRight();
		calculator.writeBuffer(sum);
	}
};

class AST_INF_Multiply_Calculator : public AST_INF_Operator_Calculator {
private:
	InfNumberFieldInterpreter calculator;
public:
	std::string forOperation() {
		return "*";
	}
	void calculate(OperationNode node) {
		auto op = getOperands(node);
		if (!op) {
			return;
		}
		LROperands_Signed lr = op.value();
		SignedNumber sum = lr.getLeft() * lr.getRight();
		calculator.writeBuffer(sum);
	}
};

class AST_INF_Divide_Calculator : public AST_INF_Operator_Calculator {
private:
	InfNumberFieldInterpreter calculator;
public:
	std::string forOperation() {
		return "/";
	}
	void calculate(OperationNode node) {
		auto op = getOperands(node);
		if (!op) {
			return;
		}
		LROperands_Signed lr = op.value();
		SignedNumber sum = lr.getLeft() / lr.getRight();
		calculator.writeBuffer(sum);
	}
};
