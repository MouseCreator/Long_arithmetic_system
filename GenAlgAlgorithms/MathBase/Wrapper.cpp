#include "FiniteNumber.h"
#include "FiniteField.h"
#include "Message.h"
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
        FiniteNumber result = field.calculate(expr);  

        resStr = new char[MAX_MESSAGE_LENGTH];
        std::string resultString = result.toString();
        strcpy(resStr, resultString.c_str());
    }
    catch (const std::exception& ex)
    {
        strcpy(errorStr, ex.what());
    }

    return resStr;
}


extern "C" char*
addition(char* a, char* b, char* mod, char* errorStr)
{
    char* resStr = nullptr;
    try
    {
        PositiveNumber modNumber = PositiveNumber(mod);
        FiniteNumber number1 = FiniteNumber(a, modNumber);
        FiniteNumber number2 = FiniteNumber(b, modNumber);
        FiniteNumber result = number1 + number2;

        char* resStr = new char[MESSAGE_LEN];
        std::string resultString = result.toString();
        strcpy_s(resStr, MESSAGE_LEN, resultString.c_str());
    }
    catch (const std::exception& ex)
    {
        strcpy_s(errorStr, MESSAGE_LEN, ex.what());
    }

    return resStr;
}