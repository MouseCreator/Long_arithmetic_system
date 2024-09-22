#include "FiniteNumber.h"
#include "FiniteField.h"
#include "Message.h"


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
        strcpy(resStr, resultString.c_str());
    }
    catch (const std::exception& ex)
    {
        strcpy(errorStr, ex.what());
    }

    return resStr;
}