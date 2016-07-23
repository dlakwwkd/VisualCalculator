#include "stdafx.h"
#include "EvaluationTree.h"
#include "Operator.h"
#include "Operand.h"


EvaluationTree::EvaluationTree()
{
}


EvaluationTree::~EvaluationTree()
{
}

void EvaluationTree::BuildTree(const PostfixExpr& _postfixExpr)
{
    // 원본도 유지하기 위해서
    PostfixExpr postfixExpr(_postfixExpr);
    Release();
    BuildTree(postfixExpr, rootNode_);

}

void EvaluationTree::Release()
{
    rootNode_.reset();
}

double EvaluationTree::Evaluate()
{
    return Evaluate(rootNode_);
}

void EvaluationTree::BuildResultPrint() const
{
    std::cout << " <TreeBuildResult>" << std::endl;
    std::cout << "   - InOrder\t: ";
    InOrderPrint(rootNode_);
    std::cout << std::endl;
    std::cout << "   - PostOrder\t: ";
    PostOrderPrint(rootNode_);
    std::cout << '\n' << std::endl;
}



void EvaluationTree::BuildTree(PostfixExpr& _postfixExpr, Node::NodePtr& _node)
{
    auto token = _postfixExpr.back();
    _postfixExpr.pop_back();

    _node = std::make_unique<Node>(token);

    if (token->IsOperator())
    {
        auto oper = dynamic_cast<Operator*>(token.get());
        if (oper->IsBinaryOper())
        {
            BuildTree(_postfixExpr, _node->right_);
            BuildTree(_postfixExpr, _node->left_);
        }
        else
        {
            BuildTree(_postfixExpr, _node->right_);
        }
    }
}

double EvaluationTree::Evaluate(Node::NodePtr& _node)
{
    if (!_node)
        return 0.0;

    auto& data = _node->data_;
    if (data->IsOperator())
    {
        auto oper = dynamic_cast<Operator*>(data.get());
        if (oper->IsBinaryOper())
        {
            auto leftResult = Evaluate(_node->left_);
            auto rightResult = Evaluate(_node->right_);

            auto binay = dynamic_cast<BinaryOper*>(oper);
            return binay->Calc(leftResult, rightResult);
        }
        else
        {
            auto result = Evaluate(_node->right_);

            auto unary = dynamic_cast<UnaryOper*>(oper);
            return unary->Calc(result);
        }
    }
    else
    {
        auto value = dynamic_cast<Operand*>(data.get());
        return value->GetValue();
    }
}



void EvaluationTree::NodePrint(const Node::NodePtr& _node) const
{
    char name = _node->data_->GetName();
    if (name == '@') // 상수
    {
        auto value = dynamic_cast<Operand*>(_node->data_.get());
        std::cout << value->GetValue();
    }
    else
    {
        std::cout << name;
    }
}

void EvaluationTree::InOrderPrint(const Node::NodePtr& _node) const
{
    if (!_node)
        return;

    std::cout << "(";
    InOrderPrint(_node->left_);
    NodePrint(_node);
    InOrderPrint(_node->right_);
    std::cout << ")";
}

void EvaluationTree::PostOrderPrint(const Node::NodePtr& _node) const
{
    if (!_node)
        return;

    PostOrderPrint(_node->left_);
    PostOrderPrint(_node->right_);
    NodePrint(_node);
    std::cout << " ";
}
