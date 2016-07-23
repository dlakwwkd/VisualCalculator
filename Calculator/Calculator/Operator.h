#pragma once
#include "Object.h"
#include <memory>
#include <string>


class Operator : public Object
{
    const static std::string operTypes;
public:
    Operator(char _name);
    virtual ~Operator();

    virtual bool    IsOperator() const override { return true; }
    virtual bool    IsBinaryOper() const = 0;

    bool            IsLeftAssociative() const { return isLeftAssociative_; }
    int             GetPrecedence() const { return precedence_; }

    static bool     IsOperType(char _char)
    {
        return operTypes.find(_char) != std::string::npos;
    }
    static std::shared_ptr<Operator> CreateOper(char _operName);
    
protected:
    bool    isLeftAssociative_;
    int     precedence_;
};



class BinaryOper : public Operator
{
public:
    BinaryOper(char _name)
    : Operator(_name)
    {
    }
    virtual bool    IsBinaryOper() const override { return true; }
    virtual double  Calc(double _left, double _right) const = 0;
};


class UnaryOper : public Operator
{
public:
    UnaryOper(char _name)
    : Operator(_name)
    {
    }
    virtual bool    IsBinaryOper() const override { return false; }
    virtual double  Calc(double _source) const = 0;
};


class Parenthesis : public Operator
{
public:
    Parenthesis(char _name)
    : Operator(_name)
    {
    }
    virtual bool    IsBinaryOper() const override { return false; }
};


#pragma region BinaryOper
class Plus : public BinaryOper
{
public:
    Plus() : BinaryOper('+')
    {
        isLeftAssociative_ = true;
        precedence_ = 2;
    }
    virtual double Calc(double _left, double _right) const override { return _left + _right; }
};

class Minus : public BinaryOper
{
public:
    Minus() : BinaryOper('-')
    {
        isLeftAssociative_ = true;
        precedence_ = 2;
    }
    virtual double Calc(double _left, double _right) const override { return _left - _right; }
};

class Mult : public BinaryOper
{
public:
    Mult() : BinaryOper('*')
    {
        isLeftAssociative_ = true;
        precedence_ = 3;
    }
    virtual double Calc(double _left, double _right) const override { return _left * _right; }
};

class Div : public BinaryOper
{
public:
    Div() : BinaryOper('/')
    {
        isLeftAssociative_ = true;
        precedence_ = 3;
    }
    virtual double Calc(double _left, double _right) const override { return _left / _right; }
};

class Pow : public BinaryOper
{
public:
    Pow() : BinaryOper('^')
    {
        isLeftAssociative_ = false;
        precedence_ = 4;
    }
    virtual double Calc(double _left, double _right) const override { return std::pow(_left, _right); }
};
#pragma endregion BinaryOper


#pragma region UnaryOper
class Negasion : public UnaryOper
{
public:
    Negasion() : UnaryOper('~')
    {
        isLeftAssociative_ = true;
        precedence_ = 5;
    }
    virtual double Calc(double _source) const override { return -(_source); }
};

class Sin : public UnaryOper
{
public:
    Sin() : UnaryOper('S')
    {
        isLeftAssociative_ = true;
        precedence_ = 6;
    }
    virtual double Calc(double _source) const override { return std::sin(_source); }
};

class Cos : public UnaryOper
{
public:
    Cos() : UnaryOper('C')
    {
        isLeftAssociative_ = true;
        precedence_ = 6;
    }
    virtual double Calc(double _source) const override { return std::cos(_source); }
};

class Tan : public UnaryOper
{
public:
    Tan() : UnaryOper('T')
    {
        isLeftAssociative_ = true;
        precedence_ = 6;
    }
    virtual double Calc(double _source) const override { return std::tan(_source); }
};
#pragma endregion UnaryOper
