using System.Collections.Generic;
using System.Drawing;
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
            public IObject  Data { get; set; }  = null;
            public Label    Item { get; set; }  = null;
            public Node     Left { get; set; }  = null;
            public Node     Right { get; set; } = null;
        }

        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public async Task BuildTree(Panel panel, List<IObject> postfixExpr)
        {
            panel.Controls.Clear();
            panel.Invalidate();

            panel_ = panel;
            createPos_ = new Point(panel_.Width / 2, 0);
            gabX_ = panel_.Width / 2;
            gabY_ = 60;
            rootNode_ = await BuildTree(new Stack<IObject>(postfixExpr));
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
        private async Task<Node> BuildTree(Stack<IObject> postfixExpr)
        {
            // _postfixExpr의 마지막 요소는 무조건 operand이기 때문에 다음 재귀를 호출하지 않는다.
            // 따라서 여기에 들어왔다는건 요소가 반드시 하나 이상 있다는 의미이다.
            var node = CreateNode(postfixExpr.Pop());

            await Task.Delay(500);

            gabX_ /= 2;
            if (node.Data is IOperator)
            {
                if (node.Data is IBinaryOper)
                {
                    createPos_.Y += gabY_;
                    createPos_.X += (int)gabX_;

                    node.Right = await BuildTree(postfixExpr);
                    await DrawLine(node, node.Right);

                    createPos_.X -= (int)(gabX_ * 2);

                    node.Left = await BuildTree(postfixExpr);
                    await DrawLine(node, node.Left);

                    createPos_.X += (int)gabX_;
                    createPos_.Y -= gabY_;
                }
                else
                {
                    createPos_.Y += gabY_;

                    node.Right = await BuildTree(postfixExpr);
                    await DrawLine(node, node.Right);

                    createPos_.Y -= gabY_;
                }
            }
            gabX_ *= 2;


            return node;
        }

        private Node CreateNode(IObject data)
        {
            var label = new Label()
            {
                Anchor = AnchorStyles.Top,
                AutoSize = true,
                BackColor = Color.SkyBlue,
                Padding = new Padding(3),
                Location = createPos_,
                Text = data.Name
            };
            panel_.Controls.Add(label);
            return new Node() { Data = data, Item = label };
        }

        private async Task DrawLine(Node parent, Node child)
        {
            if (parent == null || child == null)
                return;

            var g = panel_.CreateGraphics();
            var p = new Pen(Color.DarkBlue);
            var sizeP = parent.Item.Size;
            var sizeC = child.Item.Size;
            sizeP.Width /= 2;
            sizeP.Height /= 2;
            sizeC.Width /= 2;
            sizeC.Height /= 2;
            g.DrawLine(p, parent.Item.Location + sizeP, child.Item.Location + sizeC);
            await Task.Delay(300);
        }

        private async Task<double> Evaluate(Node node)
        {
            if (node == null)
                return 0.0;

            switch (node.Data)
            {
            case IOperator oper:
                {
                    switch (oper)
                    {
                    case IBinaryOper binary:
                        {
                            double leftResult = await Evaluate(node.Left);
                            double rightResult = await Evaluate(node.Right);
                            return binary.Calc(leftResult, rightResult);
                        }
                    case IUnaryOper unary:
                        {
                            double result = await Evaluate(node.Right);
                            return unary.Calc(result);
                        }
                    case null:
                    default:
                        return 0.0;
                    }
                }
            case IOperand operand:
                {
                    return operand.Value;
                }
            case null:
            default:
                return 0.0;
            }
        }

        private async Task SetVariable(char name, double value, Node node)
        {
            if (node == null)
                return;

            if (node.Data is Variable var)
            {
                if (var.Name == name.ToString())
                {
                    var.Value = value;
                    node.Item.Text = name + ": " + value;
                }
            }
            await SetVariable(name, value, node.Left);
            await SetVariable(name, value, node.Right);
        }

        Panel   panel_;
        Point   createPos_;
        float   gabX_;
        int     gabY_;
        Node    rootNode_;
    }
}
