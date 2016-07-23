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
        std::getline(ifs, expression_); // ���� ����

        std::string line;
        std::getline(ifs, line);                        // ù��° ���̽��� �� ���� �۾��� ����
        ParseVariable(line);                            // 1.����Ÿ�� �Ľ�
        caseList_.emplace_back(CopyVarList(varList_));  // 2.���̽��� ������ ����

        while (std::getline(ifs, line)) // �� �������ʹ� �� ���̽��� �������� �����Ѵ�.
        {
            VarList varList = CopyVarList(varList_);

            std::istringstream iss(line);
            int idx = 0;
            while (!iss.eof())
            {
                // ������ �̿��� ���ڸ� ��ŵ�ϱ� ���� 1�� for��
                for (char temp = 0; temp != '='; iss >> temp);

                // ������ �Է�
                if (!(iss >> *(varList[idx++])))
                {
                    std::cout << "�� ���̽��� ����Ÿ���� ��ġ���� �ʽ��ϴ�." << std::endl;
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

    bool triFlag = false;   // �ﰢ�Լ� �Ǻ��� ���� �÷���
    bool negFlag = true;    // ���� �����ڿ� ���� ������ ������ ���� (true�� �� ����ȭ ������)

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
                negFlag = false; // ���� �ٷ� �ڿ��� ���� �����ڸ� �´�.
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
        negFlag = true; // ������ �ڿ��� ����ȭ �����ڸ� �´�.

        // ��Ģ�� ���� ������ �Ľ�
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

        // ���� ���̽��� �������� ������ �� ������ �����Ѵ�.
        int size = varList_.size();
        for (int i = 0; i < size; ++i)
        {
            auto& pVar = varList_[i];
            pVar->SetValue(curCase[i]);
        }

        PrintCalcExpr();

        // ���Ե� �������� ������ ����� �����Ѵ�.
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
        if (name == '@') // ���
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
        // ��� �Ľ�
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
        --iter; // ���� �Ľ��� ���� ��ġ�� �ٽ� �������´�.

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
        // ���� �Ľ�
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
    
    // ���� �̸��� ������ �� ������ �������̰�, �ؽ�Ʈ ���� ���뿡 ������ ���ٰ�
    // �����Ǿ� �ֱ� ������, �̿� ����Ͽ� ������.
    char temp;
    char varName;
    iss >> varName;
    while (!iss.eof())
    {
        iss >> temp; // '='�� ������ �뵵

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
            varName = checkDecimal; // checkDecimal�� ���� varName�� �ԷµǾ� �ֱ� ����
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
