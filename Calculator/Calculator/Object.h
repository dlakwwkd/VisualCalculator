#pragma once


class Object
{
public:
    Object(char _name);
    virtual ~Object();
    
    virtual bool    IsOperator() const = 0;
    char            GetName() const { return name_; }

protected:
    char name_;
};
