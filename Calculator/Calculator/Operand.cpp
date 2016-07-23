#include "stdafx.h"
#include "Operand.h"


Operand::Operand(char _name)
: Object(_name)
{
}


Operand::~Operand()
{
}

double Operand::GetValue() const
{
    auto number = const_cast<Operand*>(this);
    if (number->IsInteger())
        return static_cast<Int*>(number)->GetValue();
    else
        return static_cast<Float*>(number)->GetValue();
}



Int::Int(char _name, int _value)
: Operand(_name)
, value_(_value)
{
}

Int::Int(const std::shared_ptr<Operand>& _obj)
: Operand(_obj->GetName())
{
    auto obj = dynamic_cast<Int*>(_obj.get());
    value_ = obj->value_;
}


Int::~Int()
{

}

void Int::SetValue(const std::shared_ptr<Operand>& _source)
{
    if (_source->IsInteger())
        value_ = static_cast<Int*>(_source.get())->GetValue();
    else
        value_ = static_cast<int>(static_cast<Float*>(_source.get())->GetValue());
}

std::istream& Int::SetValue(std::istream& _input)
{
    return _input >> value_;
}



Float::Float(char _name, float _value)
: Operand(_name)
, value_(_value)
{
}

Float::Float(const std::shared_ptr<Operand>& _obj)
: Operand(_obj->GetName())
{
    auto obj = dynamic_cast<Float*>(_obj.get());
    value_ = obj->value_;
}


Float::~Float()
{

}

void Float::SetValue(const std::shared_ptr<Operand>& _source)
{
    if (_source->IsInteger())
        value_ = static_cast<float>(static_cast<Int*>(_source.get())->GetValue());
    else
        value_ = static_cast<Float*>(_source.get())->GetValue();
}

std::istream& Float::SetValue(std::istream& _input)
{
    return _input >> value_;
}
