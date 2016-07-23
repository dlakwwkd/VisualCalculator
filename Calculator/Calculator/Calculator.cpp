#include "stdafx.h"
#include "Calculator.h"
#include <iostream>
#include <fstream>
#include <sstream>
#include <stack>
#include "Operand.h"
#include "Operator.h"
#include "EvaluationTree.h"


Calculator::Calculator()
: evalTree_(std::make_unique<EvaluationTree>())
{
    varList_.reserve(DEF_VAR_NUM);
    caseList_.reserve(DEF_CASE_NUM);
}


Calculator::~Calculator()
{
}

bool Calculator::ReadFile(const std::string& _filePath)
{
    Release();
    std::ifstream ifs(_filePath);
    if (ifs.is_open())
    {
        std::getline(ifs, expression_); // 계산식 저장

        std::string line;
        std::getline(ifs, line);                        // 첫번째 케이스는 두 가지 작업을 진행
        ParseVariable(line);                            // 1.변수타입 파싱
        caseList_.emplace_back(CopyVarList(varList_));  // 2.케이스의 변수값 저장

        while (std::getline(ifs, line)) // 그 다음부터는 각 케이스의 변수값만 저장한다.
        {
            VarList varList = CopyVarList(varList_);

            std::istringstream iss(line);
            int idx = 0;
            while (!iss.eof())
            {
                // 변수값 이외의 문자를 스킵하기 위한 1줄 for문
                for (char temp = 0; temp != '='; iss >> temp);

                // 변수값 입력
                if (!(iss >> *(varList[idx++])))
                {
                    std::cout << "각 케이스의 변수타입이 일치하지 않습니다." << std::endl;
                    ifs.close();
                    return false;
                }
            }
            caseList_.emplace_back(varList);
        }
        ifs.close();
        return true;
    }
    return false;
}

void Calculator::Release()
{
    evalTree_->Release();
    expression_.clear();
    postfixExpr_.clear();
    varList_.clear();
    caseList_.clear();
}

void Calculator::ParseExpressionToPostfix()
{
    std::stack<std::shared_ptr<Operator>> operStack;

    auto start = expression_.cbegin();
    auto end = expression_.cend();

    bool triFlag = false;   // 삼각함수 판별을 위한 플래그
    bool negFlag = true;    // 뺄셈 연산자와 음수 연산자 구분을 위해 (true일 때 음수화 연산자)

    for (auto iter = start; iter != end; ++iter)
    {
        auto& curChar = *iter;
        if (curChar == ' ' || curChar == '\t')
            continue;

        if (!Operator::IsOperType(curChar))
        {
            if (curChar == 's' && *(iter + 1) == 'i'
                || curChar == 'c' && *(iter + 1) == 'o'
                || curChar == 't' && *(iter + 1) == 'a')
            {
                triFlag = true;
            }
            else
            {
                ParseDigit(iter);
                negFlag = false; // 숫자 바로 뒤에는 뺄셈 연산자만 온다.
                continue;
            }
        }

        char operName = curChar;

        if (triFlag)
        {
            triFlag = false;
            iter += 2;
            operName = toupper(operName);
        }
        else if (negFlag && operName == '-')
        {
            operName = '~';
        }
        negFlag = true; // 연산자 뒤에는 음수화 연산자만 온다.

        // 규칙에 따라 연산자 파싱
        auto curOper = Operator::CreateOper(operName);
        switch (operName)
        {
        case '(': operStack.push(curOper); break;
        case ')':
            while (operStack.top()->GetName() != '(')
            {
                postfixExpr_.push_back(operStack.top());
                operStack.pop();
            }
            operStack.pop();
            break;
        default:
            while (!operStack.empty())
            {
                auto& topOper = operStack.top();
                if (curOper->IsLeftAssociative())
                {
                    if (curOper->GetPrecedence() <= topOper->GetPrecedence())
                    {
                        postfixExpr_.push_back(topOper);
                        operStack.pop();
                        continue;
                    }
                }
                else if (curOper->GetPrecedence() < topOper->GetPrecedence())
                {
                    postfixExpr_.push_back(topOper);
                    operStack.pop();
                    continue;
                }
                break;
            }
            operStack.push(curOper);
        }
    }

    while (!operStack.empty())
    {
        postfixExpr_.push_back(operStack.top());
        operStack.pop();
    }
}

void Calculator::BuildEvaluationTree() const
{
    evalTree_->BuildTree(postfixExpr_);
    evalTree_->BuildResultPrint();
}

void Calculator::Calculate() const
{
    std::cout << " <CalcResult>" << std::endl;
    int i = 0;
    for (auto& curCase : caseList_)
    {
        std::cout << "   - Result" << ++i << " : ";

        // 현재 케이스의 변수값을 계산식의 각 변수에 대입한다.
        int size = varList_.size();
        for (int i = 0; i < size; ++i)
        {
            auto& pVar = varList_[i];
            pVar->SetValue(curCase[i]);
        }

        PrintCalcExpr();

        // 대입된 변수값을 가지고 계산을 수행한다.
        auto result = evalTree_->Evaluate();
        std::cout << result << std::endl;
    }
    std::cout << std::endl;
}

void Calculator::PrintParsedData() const
{
    std::cout << " <ParsedData>" << std::endl;
    std::cout << "   - Expression\t: " << expression_ << std::endl;
    std::cout << "   - Postfix\t: ";
    for (auto& pObj : postfixExpr_)
    {
        char name = pObj->GetName();
        if (name == '@') // 상수
        {
            auto value = dynamic_cast<Operand*>(pObj.get());
            std::cout << value->GetValue() << " ";
        }
        else
        {
            std::cout << name << " ";
        }
    }
    std::cout << std::endl;
    std::cout << "   - VarList\t: ";
    for (auto& pVar : varList_)
    {
        std::cout << pVar->GetName() << ", ";
    }
    std::cout << '\b' << '\b' << ' ' << std::endl;
    std::cout << "   - CaseList" << std::endl;
    int i = 0;
    for (auto& varList : caseList_)
    {
        std::cout << "     > Case" << ++i << " : ";
        for (auto& pVar : varList)
        {
            std::cout << pVar->GetValue() << ", ";
        }
        std::cout << '\b' << '\b' << ' ' << std::endl;
    }
    std::cout << std::endl;
}



void Calculator::PrintCalcExpr() const
{
    std::string temp(expression_);
    for (auto& pVar : varList_)
    {
        auto pos = temp.find(pVar->GetName());
        if (pos == std::string::npos)
            continue;

        auto iter = temp.begin() + pos;
        if (*iter == 's' && *(iter + 1) == 'i'
            || *iter == 'c' && *(iter + 1) == 'o'
            || *iter == 't' && *(iter + 1) == 'a')
            continue;

        std::ostringstream oss;
        oss << pVar->GetValue();
        temp.replace(iter, iter + 1, oss.str());
    }
    std::cout << temp << " = ";
}

void Calculator::ParseDigit(std::string::const_iterator& iter)
{
    auto& curChar = *iter;
    if (isdigit(curChar))
    {
        // 상수 파싱
        std::string buf;
        bool isFloat = false;
        auto end = expression_.cend();
        for (; iter != end; ++iter)
        {
            char c = *(iter);
            if (isdigit(c))
            {
                buf.push_back(c);
            }
            else if (c == '.')
            {
                isFloat = true;
                buf.push_back(c);
            }
            else
            {
                buf.push_back('\0');
                break;
            }
        }
        --iter; // 다음 파싱을 위해 위치를 다시 돌려놓는다.

        if (isFloat)
        {
            float value = std::stof(buf);
            postfixExpr_.push_back(std::make_shared<Float>('@', value));
        }
        else
        {
            int value = std::stoi(buf);
            postfixExpr_.push_back(std::make_shared<Int>('@', value));
        }
    }
    else
    {
        // 변수 파싱
        for (auto& pVar : varList_)
        {
            if (curChar == pVar->GetName())
            {
                postfixExpr_.push_back(pVar);
                break;
            }
        }
    }
}

void Calculator::ParseVariable(const std::string& _firstCase)
{
    std::istringstream iss(_firstCase);
    
    // 변수 이름은 무조건 한 글자의 영문자이고, 텍스트 파일 내용에 오류가 없다고
    // 가정되어 있기 때문에, 이에 기반하여 구현함.
    char temp;
    char varName;
    iss >> varName;
    while (!iss.eof())
    {
        iss >> temp; // '='을 버리는 용도

        int value;
        iss >> value;

        char checkDecimal;
        iss >> checkDecimal;
        if (checkDecimal == '.')
        {
            int underNum;
            iss >> underNum;

            std::string buf(std::to_string(value));
            buf += "." + std::to_string(underNum);

            float floatValue = std::stof(buf);
            varList_.push_back(std::make_shared<Float>(varName, floatValue));

            iss >> varName;
        }
        else
        {
            varList_.push_back(std::make_shared<Int>(varName, value));
            varName = checkDecimal; // checkDecimal에 다음 varName이 입력되어 있기 때문
        }
    }
}

Calculator::VarList Calculator::CopyVarList(const VarList& _varList) const
{
    VarList retVarList;
    retVarList.reserve(_varList.size());
    for (auto& pVar : _varList)
    {
        if (pVar->IsInteger())
            retVarList.push_back(std::make_shared<Int>(pVar));
        else
            retVarList.push_back(std::make_shared<Float>(pVar));
    }
    return retVarList;
}
