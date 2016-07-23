#include "stdafx.h"
#include "Operator.h"


const std::string Operator::operTypes = "+-*/^()";


Operator::Operator(char _name)
: Object(_name)
, isLeftAssociative_(true)
, precedence_(0)
{
}


Operator::~Operator()
{
}

std::shared_ptr<Operator> Operator::CreateOper(char _operName)
{
    switch (_operName)
    {
    case '(':
    case ')': return std::make_shared<Parenthesis>(_operName);
    case '+': return std::make_shared<Plus>();
    case '-': return std::make_shared<Minus>();
    case '*': return std::make_shared<Mult>();
    case '/': return std::make_shared<Div>();
    case '^': return std::make_shared<Pow>();
    case '~': return std::make_shared<Negasion>();
    case 'S': return std::make_shared<Sin>();
    case 'C': return std::make_shared<Cos>();
    case 'T': return std::make_shared<Tan>();
    default:
        return nullptr;
    }
}
