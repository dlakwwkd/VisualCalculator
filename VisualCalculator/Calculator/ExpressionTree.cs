using System;
using System.Collections.Generic;
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
            public Node     Left { get; set; }
            public Node     Right { get; set; }
        }

        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public async Task Run(Panel _panel, List<IObject> _postfixExpr)
        {
            panel_ = _panel;
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
            var node = new Node() { Data = _postfixExpr.Pop() };

            if (node.Data is IOperator)
            {
                if (node.Data is IBinaryOper)
                {
                    node.Right = await BuildTree(_postfixExpr);
                    node.Left = await BuildTree(_postfixExpr);
                }
                else
                {
                    node.Right = await BuildTree(_postfixExpr);
                }
            }
            return node;
        }

        private async Task Evaluate()
        {
            await Task.Delay(10);
        }



        Panel   panel_;
        Node    rootNode_;
    }
}
