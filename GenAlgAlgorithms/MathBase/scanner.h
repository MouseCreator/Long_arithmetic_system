#pragma once

#include "Token.h"
namespace parse {
    struct ScanResult {
        std::vector<Token> tokens;
        std::vector<TokenError> errors;

        bool hasErrors() {
            return !errors.empty();
        }
        ScanResult(std::vector<Token> tokens, std::vector<TokenError> errors) {
            this->tokens = tokens;
            this->errors = errors;
        }
    };

    class Scanner {
    public:
        virtual ScanResult scan(std::string input) = 0;
        virtual ~Scanner() {}
    };


    enum State {
        INITIAL, DIGITS, FUNCTIONAL
    };

    class ScanSession {
    private:
        std::string input;
        int position;
        int lookahead;
        bool finished;
        std::vector<Token> tokens;
        std::vector<TokenError> errors;
        State state = INITIAL;
        std::string currentWord;
        char current;
        char next;
        std::size_t size;

        void _scan() {
            for (; position < size; _increment()) {
                current = input[position];
                next = lookahead < size ? input[lookahead] : ' ';
                switch (state) {
                case INITIAL:
                    on_initial();
                    break;
                case FUNCTIONAL:
                    on_functional();
                    break;
                case DIGITS:
                    on_digits();
                    break;
                }
            }
        }
        void append(SCAN_TOKEN_TYPE type, char value) {
            append(type, std::string(1, value));
        }
        void appendWord(SCAN_TOKEN_TYPE type) {
            Token token = Token();
            token.position = position - currentWord.length() + 1;
            token.type = type;
            token.value = currentWord;
            tokens.push_back(token);
        }
        void append(SCAN_TOKEN_TYPE type, std::string value) {
            Token token = Token();
            token.position = position;
            token.type = type;
            token.value = value;
            tokens.push_back(token);
        }
        void _increment() {
            position++;
            lookahead++;
        }
        void on_initial() {
            switch (current)
            {
            case '(':
            case ')':
                append(BRACKET, current);
                break;
            case '+':
            case '-':
            case '/':
            case '%':
            case '^':
                append(OPERATOR, current);
                break;
            case '*':
                if (next == '*') {
                    append(OPERATOR, '^');
                    _increment();
                }
                else {
                    append(OPERATOR, '*');
                }
                break;
            case ',':
                append(COMMA, current);
                break;
            case ' ':
            case '\r':
            case '\t':
            case '\n':
                break;
            case 'x':
            case 'X':
                append(VARIABLE, "x");
                break;
            default:
                if (_is_digit(current)) {
                    currentWord = std::string(1, current);
                    set_state_digits();
                }
                else if (_is_alpha(current)) {
                    currentWord = std::string(1, current);
                    state = FUNCTIONAL;
                }
                else {
                    std::string p = std::string(1, current);
                    std::string pos_str = std::to_string(position);
                    std::string message = "Unexpected symbol: '" + p + "' Near position: " + pos_str;
                    append_error(UNEXPECTED_SYMBOL, message);
                }
                break;
            }
        }
        void append_error(ERROR_TYPE type, std::string message) {
            errors.push_back(TokenError(type, message));
        }
        void on_digits() {
            currentWord += current;
            if (_is_digit(next)) {
                return;
            }
            appendWord(NUMBER);
            state = INITIAL;
        }
        void on_functional() {
            currentWord += current;
            if (_is_alpha(next)) {
                return;
            }
            appendWord(FUNCTION);
            state = INITIAL;
        }
        void set_state_digits() {
            if (_is_digit(next)) {
                state = DIGITS;
                return;
            }
            append(NUMBER, current);
            state = INITIAL;
        }
        bool _is_digit(char ch) {
            return '0' <= ch && ch <= '9';
        }
        bool _is_alpha(char ch) {
            return ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z') && ch != 'x' && ch != 'X';
        }

        void set_state_functional() {
            if (_is_alpha(next)) {
                state = FUNCTIONAL;
            }
            append(FUNCTION, current);
            state = INITIAL;
        }


    public:
        ScanSession(std::string in) {
            input = in;
            position = 0;
            lookahead = 1;
            finished = false;
            currentWord = "";
            current = ' ';
            next = ' ';
            size = in.size();
        }
        void scanInput() {
            if (finished) {
                return;
            }
            _scan();
            finished = true;
        }
        ScanResult getResult() {
            return ScanResult(tokens, errors);
        }
    };

    class IterativeScanner : public Scanner {
    public:
        IterativeScanner() : Scanner() {
        }
        ScanResult scan(std::string input) override {
            ScanSession scanSession = ScanSession(input);
            scanSession.scanInput();
            ScanResult result = scanSession.getResult();
            return result;
        }
    };
}