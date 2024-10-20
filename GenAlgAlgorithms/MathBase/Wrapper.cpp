#include "FiniteNumber.h"
#include "FiniteField.h"
#include "Message.h"
#include "Executor.h"
#include <cstdlib>
#include <cstring>
#include <string>
#include <stdexcept>
#include <cmath>

const int MAX_MESSAGE_LENGTH = 4096;

extern "C" char* random_prime(const char* min, const char* max, char* errorStr)
{
    char* resStr = nullptr;
    try
    {
        long min_val = std::strtol(min, nullptr, 10);
        long max_val = std::strtol(max, nullptr, 10);

        if (min_val < 2 || max_val <= min_val) {
            strcpy(errorStr, "Invalid range;;min;;max;;;");
            return nullptr;
        }

        long prime = min_val + rand() % (max_val - min_val + 1);
        while (prime < max_val) {
            bool is_prime = true;
            for (long i = 2; i <= std::sqrt(prime); ++i) {
                if (prime % i == 0) {
                    is_prime = false;
                    break;
                }
            }
            if (is_prime) break;
            prime++;
        }

        resStr = new char[MAX_MESSAGE_LENGTH];
        std::snprintf(resStr, MAX_MESSAGE_LENGTH, "%ld", prime);
    }
    catch (const std::exception& ex)
    {
        strcpy(errorStr, ex.what());
    }

    return resStr;
}

extern "C" char* finite_field(const char* expression, const char* n, char* errorStr)
{
    char* resStr = nullptr;
    try
    {
        PositiveNumber modNumber = PositiveNumber(n);
        std::string expr(expression);
        FiniteField field(modNumber);
        
        Executor executor = Executor();
        Errors errors;
        std::string resultString;
        if (modNumber.isZero()) {
            SignedNumber result;
            executor.infinite_field(expr, &result, &errors);
            resultString = result.toString();
        }
        else {
            FiniteNumber result;
            executor.finite_field(expr, &modNumber, &result, &errors);
            resultString = result.toString();
        }
        if (errors.hasError()) {
            strcpy(errorStr, errors.concat().c_str());
            return resStr;
        }
        resStr = new char[MAX_MESSAGE_LENGTH];
        
        strcpy(resStr, resultString.c_str());
    }
    catch (const std::exception& ex)
    {
        strcpy(errorStr, ex.what());
    }

    return resStr;
}