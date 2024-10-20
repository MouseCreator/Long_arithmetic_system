#pragma once
#include <iostream>
#include <vector>
class Errors {
private:
	std::vector<std::string> errors;

	std::string formatOutputError(const std::string& input) {
		std::string result;
		size_t length = input.length();
		size_t i = 0;

		while (i < length) {
			if (input[i] == ';') {
				size_t start = i;
				while (i < length && input[i] == ';') {
					i++;
				}
				size_t count = i - start;
				result.append(count + 1, ';');
			}
			else {
				result += input[i];
				i++;
			}
		}

		return result;
	}
public:
	void addError(std::string s) {
		errors.emplace_back(s);
	}
	bool hasError() {
		return !errors.empty();
	}
	std::vector<std::string> getAllErrors() {
		return errors;
	}
	void clear() {
		errors.clear();
	}
	std::string concat() {
		if (!hasError()) {
			return "";
		}
		std::size_t size = errors.size();
		std::string origin = formatOutputError(errors[0]);
		for (std::size_t i = 1; i < size; i++) {
			std::string err = errors[i];
			std::string formatted = formatOutputError(err);
			origin += ";";
			origin += formatted;
		}
		return origin;
	}
};