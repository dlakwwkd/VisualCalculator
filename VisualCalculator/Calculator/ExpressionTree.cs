using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualCalculator.Operand;
using VisualCalculator.Operator;
using VisualCalculator.Operator.Binary;
using VisualCalculator.Operator.Unary;

namespace VisualCalculator.Calculator
{
    class ExpressionTree
    {
        class Node
        {
            public IObject  Data { get; set; }
            public Label    Item { get; set; }
            public Node     Left { get; set; }
            public Node     Right { get; set; }
        }

        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public async Task BuildTree(Panel _panel, List<IObject> _postfixExpr)
        {
            _panel.Controls.Clear();
            _panel.Invalidate();

            panel_ = _panel;
            createPos_ = new Point(panel_.Width / 2, 0);
            gabX_ = panel_.Width / 2;
            gabY_ = 60;
            rootNode_ = await BuildTree(new Stack<IObject>(_postfixExpr));
        }

        public async Task<double> Evaluate()
        {
            return await Evaluate(rootNode_);
        }

        public async Task SetVariable(char _name, double _value)
        {
            await SetVariable(_name, _value, rootNode_);
        }



        //------------------------------------------------------------------------------------
        // Private Field
        //------------------------------------------------------------------------------------
        private async Task<Node> BuildTree(Stack<IObject> _postfixExpr)
        {
            // _postfixExpr의 마지막 요소는 무조건 operand이기 때문에 다음 재귀를 호출하지 않는다.
            // 따라서 여기에 들어왔다는건 요소가 반드시 하나 이상 있다는 의미이다.
            var node = CreateNode(_postfixExpr.Pop());

            await Task.Delay(500);

            gabX_ /= 2;
            if (node.Data is IOperator)
            {
                if (node.Data is IBinaryOper)
                {
                    createPos_.Y += gabY_;
                    createPos_.X += (int)gabX_;

                    node.Right = await BuildTree(_postfixExpr);
                    await DrawLine(node, node.Right);

                    createPos_.X -= (int)(gabX_ * 2);

                    node.Left = await BuildTree(_postfixExpr);
                    await DrawLine(node, node.Left);

                    createPos_.X += (int)gabX_;
                    createPos_.Y -= gabY_;
                }
                else
                {
                    createPos_.Y += gabY_;

                    node.Right = await BuildTree(_postfixExpr);
                    await DrawLine(node, node.Right);

                    createPos_.Y -= gabY_;
                }
            }
            gabX_ *= 2;


            return node;
        }

        private Node CreateNode(IObject _data)
        {
            var label = new Label();
            label.Anchor = AnchorStyles.Top;
            label.AutoSize = true;
            label.BackColor = Color.SkyBlue;
            label.Padding = new Padding(3);
            label.Location = createPos_;
            label.Text = _data.Name;
            panel_.Controls.Add(label);
            return new Node() { Data = _data, Item = label };
        }

        private async Task DrawLine(Node _parent, Node _child)
        {
            if (_parent == null || _child == null)
                return;

            var g = panel_.CreateGraphics();
            var p = new Pen(Color.DarkBlue);
            var sizeP = _parent.Item.Size;
            var sizeC = _child.Item.Size;
            sizeP.Width /= 2;
            sizeP.Height /= 2;
            sizeC.Width /= 2;
            sizeC.Height /= 2;
            g.DrawLine(p, _parent.Item.Location + sizeP, _child.Item.Location + sizeC);
            await Task.Delay(300);
        }

        private async Task<double> Evaluate(Node _node)
        {
            if (_node == null)
                return 0.0;

            if (_node.Data is IOperator)
            {
                var oper = _node.Data as IOperator;
                if (oper is IBinaryOper)
                {
                    double leftResult = await Evaluate(_node.Left);
                    double rightResult = await Evaluate(_node.Right);
                    return (oper as IBinaryOper).Calc(leftResult, rightResult);
                }
                else
                {
                    double result = await Evaluate(_node.Right);
                    return (oper as IUnaryOper).Calc(result);
                }
            }
            else
            {
                return (_node.Data as IOperand).Value;
            }
        }

        private async Task SetVariable(char _name, double _value, Node _node)
        {
            if (_node == null)
                return;

            if (_node.Data is Variable)
            {
                var var = _node.Data as Variable;
                if (var.Name == _name.ToString())
                {
                    var.Value = _value;
                    _node.Item.Text = _name + ": " + _value;
                }
            }
            await SetVariable(_name, _value, _node.Left);
            await SetVariable(_name, _value, _node.Right);
        }



        Panel   panel_;
        Point   createPos_;
        float   gabX_;
        int     gabY_;
        Node    rootNode_;
    }
}
