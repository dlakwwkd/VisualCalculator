#pragma once
#include "Object.h"
#include <memory>
#include <iostream>

class Int;
class Float;


class Operand : public Object
{
public:
    Operand(char _name);
    virtual ~Operand();

    virtual bool            IsOperator() const override { return false; }
    virtual bool            IsInteger() const = 0;
    virtual void            SetValue(const std::shared_ptr<Operand>& _source) = 0;
    virtual std::istream&   SetValue(std::istream& _input) = 0;
    double                  GetValue() const;

    friend std::istream& operator>>(std::istream& _input, Operand& _var)
    {
        return _var.SetValue(_input);
    }
};


class Int : public Operand
{
public:
    Int(char _name, int _value);
    Int(const std::shared_ptr<Operand>& _obj);
    virtual ~Int();
    
    virtual bool            IsInteger() const override { return true; }
    virtual void            SetValue(const std::shared_ptr<Operand>& _source) override;
    virtual std::istream&   SetValue(std::istream& _input) override;
    int                     GetValue() const { return value_; }

private:
    int value_;
};


class Float : public Operand
{
public:
    Float(char _name, float _value);
    Float(const std::shared_ptr<Operand>& _obj);
    virtual ~Float();

    virtual bool            IsInteger() const override { return false; }
    virtual void            SetValue(const std::shared_ptr<Operand>& _source) override;
    virtual std::istream&   SetValue(std::istream& _input) override;
    float                   GetValue() const { return value_; }

private:
    float value_;
};
