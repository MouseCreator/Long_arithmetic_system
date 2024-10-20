#pragma once
#include <iostream>
#include <vector>
namespace parse {
    enum ERROR_TYPE {
        UNEXPECTED_SYMBOL, UNEXPECTED_TOKEN, EMPTY_INPUT, COMPLEX_EXPRESSION
    };
    enum SCAN_TOKEN_TYPE {
        NUMBER, BRACKET, OPERATOR, FUNCTION, COMMA, VARIABLE
    };
    struct Token {
        int position;
        SCAN_TOKEN_TYPE type;
        std::string value;

        bool operator==(const Token& other) const {
            return (position == other.position) &&
                (type == other.type) &&
                (value == other.value);
        }
    };
    struct TokenError {
        ERROR_TYPE type;
        std::string message;
        TokenError(ERROR_TYPE t, std::string m) {
            type = t;
            message = m;
        }
    };
}

