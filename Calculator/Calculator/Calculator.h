#pragma once
#include <string>
#include <vector>
#include <memory>

class EvaluationTree;
class Object;
class Operand;


class Calculator
{
    enum
    {
        DEF_VAR_NUM     = 10,
        DEF_CASE_NUM    = 5,
    };
    using PostfixExpr   = std::vector<std::shared_ptr<Object>>;
    using VarList       = std::vector<std::shared_ptr<Operand>>;
    using CaseList      = std::vector<VarList>;
    using EvalTree      = std::unique_ptr<EvaluationTree>;
public:
    Calculator();
    ~Calculator();

    bool    ReadFile(const std::string& _filePath);
    void    Release();
    void    ParseExpressionToPostfix();
    void    BuildEvaluationTree() const;
    void    Calculate() const;
    void    PrintParsedData() const;

private:
    void    PrintCalcExpr() const;
    void    ParseDigit(std::string::const_iterator& iter);
    void    ParseVariable(const std::string& _firstCase);
    VarList CopyVarList(const VarList& _varList) const;

private:
    std::string expression_;
    PostfixExpr postfixExpr_;
    VarList     varList_;
    CaseList    caseList_;
    EvalTree	evalTree_;
};
