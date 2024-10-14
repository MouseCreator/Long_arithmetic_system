#pragma once
#include "ErrorManager.h"
#include "SignedNumber.h"
#include "FiniteNumber.h"
class Executor {
public:
    void random_prime(PositiveNumber* min, PositiveNumber* max, SignedNumber* result, Errors* errors);
    void finite_field(std::string expression, PositiveNumber* n, FiniteNumber* result, Errors* errors);
    void infinite_field(std::string expression, SignedNumber* result, Errors* errors);
};