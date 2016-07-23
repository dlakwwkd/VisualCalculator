#pragma once
#include <memory>
#include <vector>

class Object;


class EvaluationTree
{
    struct Node
    {
        using DataPtr = std::shared_ptr<Object>;
        using NodePtr = std::unique_ptr<Node>;
        DataPtr data_;
        NodePtr left_;
        NodePtr right_;

        Node(const DataPtr& _data) 
        : data_(_data)
        {
        }
    };
    using PostfixExpr = std::vector<Node::DataPtr>;
public:
    EvaluationTree();
    ~EvaluationTree();

    void    BuildTree(const PostfixExpr& _postfixExpr);
    void    Release();
    double  Evaluate();
    void    BuildResultPrint() const;

private:
    void    BuildTree(PostfixExpr& _postfixExpr, Node::NodePtr& _node);
    double  Evaluate(Node::NodePtr& _node);

    void    NodePrint(const Node::NodePtr& _node) const;
    void    InOrderPrint(const Node::NodePtr& _node) const;
    void    PostOrderPrint(const Node::NodePtr& _node) const;

private:
    Node::NodePtr   rootNode_;
};
