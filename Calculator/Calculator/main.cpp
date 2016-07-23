#include "stdafx.h"
#include "Calculator.h"
#include <iostream>
#include <sstream>


int main()
{
    Calculator calc;
    for (int i = 1; i < 9; ++i)
    {
        std::ostringstream oss;
        oss << "TestCase" << i << ".txt";
        if (calc.ReadFile(oss.str()))
        {
            std::cout << "[File_" << i << "]" << std::endl;
            calc.ParseExpressionToPostfix();
            calc.PrintParsedData();
            calc.BuildEvaluationTree();
            calc.Calculate();
            std::cout << std::endl;
        }
    }
}
