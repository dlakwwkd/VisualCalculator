using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualCalculator.Operator;
using VisualCalculator.Operator.Binary;

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
        public async Task Run(Panel _panel, List<IObject> _postfixExpr)
        {
            _panel.Controls.Clear();
            panel_ = _panel;
            createPos_ = new Point(panel_.Width / 2, 0);
            gabX_ = createPos_.X;
            gabY_ = 50;
            rootNode_ = await BuildTree(new Stack<IObject>(_postfixExpr));
            await Task.Delay(200);
            await Evaluate();
        }



        //------------------------------------------------------------------------------------
        // Private Field
        //------------------------------------------------------------------------------------
        private async Task<Node> BuildTree(Stack<IObject> _postfixExpr)
        {
            // _postfixExpr의 마지막 요소는 무조건 operand이기 때문에 다음 재귀를 호출하지 않는다.
            // 따라서 여기에 들어왔다는건 요소가 반드시 하나 이상 있다는 의미이다.
            var node = new Node()
            {
                Data = _postfixExpr.Pop(),
                Item = new Label()
            };
            node.Item.Anchor = AnchorStyles.Top;
            node.Item.AutoSize = true;
            node.Item.BackColor = Color.SkyBlue;
            node.Item.Padding = new Padding(3);
            node.Item.Location = createPos_;
            node.Item.Text = node.Data.Name;
            panel_.Controls.Add(node.Item);

            await Task.Delay(300);

            gabX_ /= 2;
            if (node.Data is IOperator)
            {
                if (node.Data is IBinaryOper)
                {
                    createPos_.X += gabX_;
                    createPos_.Y += gabY_;
                    node.Right = await BuildTree(_postfixExpr);
                    gabX_ *= 2;
                    createPos_.X -= gabX_ * 2;
                    node.Left = await BuildTree(_postfixExpr);
                    gabX_ *= 2;
                    createPos_.X += gabX_;
                    createPos_.Y -= gabY_;
                }
                else
                {
                    createPos_.Y += gabY_;
                    node.Right = await BuildTree(_postfixExpr);
                    gabX_ *= 2;
                    createPos_.Y -= gabY_;
                }
            }
            return node;
        }

        private async Task Evaluate()
        {
            await Task.Delay(10);
        }



        Panel   panel_;
        Point   createPos_;
        int     gabX_;
        int     gabY_;

        Node    rootNode_;
    }
}
