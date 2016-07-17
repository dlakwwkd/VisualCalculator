﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using VisualCalculator.Operand;
using VisualCalculator.Operator;
using VisualCalculator.Operator.Unary;
using VisualCalculator.Operator.Binary;

namespace VisualCalculator.Calculator
{
    class ShuntingYardAlgorithm
    {
        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public async Task<List<IObject>> Run(Panel _panel, List<IObject> _infixExpr)
        {
            Init(_panel);
            infixExpr_ = _infixExpr;
            postfixExpr_.Clear();
            operStack_.Clear();

            await InputSetting();
            await Task.Delay(500);

            foreach (var obj in infixExpr_)
            {
                var item = inputPanel_.Controls[0];
                inputPanel_.Controls.Remove(item);
                infixPanel_.Controls.Add(item);
                await Task.Delay(200);

                if (obj is IOperand)
                {
                    await MoveToPostfix(obj);
                    continue;
                }

                var oper = obj as IOperator;
                if (oper is BracketL)
                {
                    await PushStackFromInfix(oper);
                }
                else if (oper is BracketR)
                {
                    while (!(operStack_.Peek() is BracketL))
                    {
                        await PopStackToPostfix();
                    }
                    await PopStackAndPopInfix();
                }
                else
                {
                    while (operStack_.Any())
                    {
                        var topOper = operStack_.Peek();
                        if (topOper is BracketL)
                            break;

                        if (oper is IUnaryOper)
                        {
                            if (topOper is IUnaryOper)
                            {
                                await PopStackToPostfix();
                            }
                            break;
                        }
                        if (topOper is IUnaryOper)
                        {
                            await PopStackToPostfix();
                            continue;
                        }
                        var curOper = oper as IBinaryOper;
                        if (curOper.IsLeftAssociative())
                        {
                            if (curOper.GetPrecedence() <= (topOper as IBinaryOper).GetPrecedence())
                            {
                                await PopStackToPostfix();
                                continue;
                            }
                        }
                        else if (curOper.GetPrecedence() < (topOper as IBinaryOper).GetPrecedence())
                        {
                            await PopStackToPostfix();
                            continue;
                        }
                        break;
                    }
                    await PushStackFromInfix(oper);
                }
            }
            while (operStack_.Any())
            {
                await PopStackToPostfix();
            }

            _panel.Controls.Remove(inputPanel_);
            _panel.Controls.Remove(infixPanel_);
            _panel.Controls.Remove(stackPanel_);

            return postfixExpr_;
        }



        //------------------------------------------------------------------------------------
        // Private Field
        //------------------------------------------------------------------------------------
        private void Init(Panel _panel)
        {
            _panel.Controls.Remove(inputPanel_);
            _panel.Controls.Remove(infixPanel_);
            _panel.Controls.Remove(postfixPanel_);
            _panel.Controls.Remove(stackPanel_);

            inputPanel_.Controls.Clear();
            inputPanel_.FlowDirection = FlowDirection.LeftToRight;
            inputPanel_.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            inputPanel_.Location = new Point(_panel.Width - 5, 5);
            inputPanel_.Padding = new Padding(3);
            inputPanel_.Size = new Size(5, 5);
            inputPanel_.AutoSize = true;
            inputPanel_.BackColor = Color.Brown;

            infixPanel_.Controls.Clear();
            infixPanel_.FlowDirection = FlowDirection.LeftToRight;
            infixPanel_.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            infixPanel_.Location = new Point(_panel.Width - 5, 30);
            infixPanel_.Padding = new Padding(3);
            infixPanel_.Size = new Size(5, 5);
            infixPanel_.AutoSize = true;
            infixPanel_.BackColor = Color.Brown;

            postfixPanel_.Controls.Clear();
            postfixPanel_.FlowDirection = FlowDirection.LeftToRight;
            postfixPanel_.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            postfixPanel_.Location = new Point(5, 30);
            postfixPanel_.Padding = new Padding(3);
            postfixPanel_.Size = new Size(5, 5);
            postfixPanel_.AutoSize = true;
            postfixPanel_.BackColor = Color.Brown;

            stackPanel_.Controls.Clear();
            stackPanel_.FlowDirection = FlowDirection.BottomUp;
            stackPanel_.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            stackPanel_.Location = new Point(_panel.Width / 2, _panel.Height);
            stackPanel_.Padding = new Padding(3);
            stackPanel_.Size = new Size(5, 5);
            stackPanel_.AutoSize = true;
            stackPanel_.BackColor = Color.Brown;

            _panel.Controls.Add(inputPanel_);
            _panel.Controls.Add(infixPanel_);
            _panel.Controls.Add(postfixPanel_);
            _panel.Controls.Add(stackPanel_);
        }

        private async Task InputSetting()
        {
            foreach (var obj in infixExpr_)
            {
                var label = new Label();
                label.BackColor = Color.Snow;
                label.Margin = new Padding(1);
                label.AutoSize = true;
                label.Text = obj.Name;
                inputPanel_.Controls.Add(label);
                await Task.Delay(200);
            }
        }

        private async Task MoveToPostfix(IObject _obj)
        {
            var item = infixPanel_.Controls[0] as Label;
            infixPanel_.Controls.Remove(item);

            await MoveAction(item, async () =>
            {
                item.Location = infixPanel_.Location;
                var goalPos = postfixPanel_.Location;
                goalPos.X += postfixPanel_.Width;

                int spot = goalPos.X + (item.Location.X - goalPos.X) / 2;
                var speed = 0.0f;
                while (item.Location.X > goalPos.X)
                {
                    var curPos = item.Location;
                    if (curPos.X > spot)
                        speed += 0.3f;
                    else
                        speed -= 0.3f;
                    item.Location = new Point(curPos.X - (int)(speed + 3), curPos.Y);
                    await Task.Delay(10);
                }
            });

            postfixPanel_.Controls.Add(item);
            postfixExpr_.Add(_obj);
        }

        private async Task PushStackFromInfix(IOperator _oper)
        {
            var item = infixPanel_.Controls[0] as Label;
            infixPanel_.Controls.Remove(item);

            await MoveAction(item, async () =>
            {
                item.Location = infixPanel_.Location;
                var goalPos = stackPanel_.Location;
                goalPos.Y -= stackPanel_.Height;

                var speedX = 13.0f;
                var speedY = 0.0f;
                while (item.Location.Y < goalPos.Y)
                {
                    var curPos = item.Location;
                    speedX -= 0.3f;
                    speedY += 0.2f;
                    item.Location = new Point(curPos.X - (int)(speedX), curPos.Y + (int)(speedY));
                    await Task.Delay(10);
                }
            });

            stackPanel_.Controls.Add(item);
            operStack_.Push(_oper);
        }

        private async Task PopStackToPostfix()
        {
            var item = stackPanel_.Controls[stackPanel_.Controls.Count - 1] as Label;
            stackPanel_.Controls.Remove(item);

            await MoveAction(item, async () =>
            {
                item.Location = stackPanel_.Location;
                var goalPos = postfixPanel_.Location;
                goalPos.X += postfixPanel_.Width;

                var speedX = 0.0f;
                var speedY = 9.0f;
                while (item.Location.X > goalPos.X)
                {
                    var curPos = item.Location;
                    speedX += 0.2f;
                    speedY -= 0.2f;
                    item.Location = new Point(curPos.X - (int)(speedX), curPos.Y - (int)(speedY));
                    await Task.Delay(10);
                }
            });

            postfixPanel_.Controls.Add(item);
            postfixExpr_.Add(operStack_.Pop());
        }

        private async Task PopStackAndPopInfix()
        {
            var item1 = infixPanel_.Controls[0];
            var item2 = stackPanel_.Controls[stackPanel_.Controls.Count - 1];
            infixPanel_.Controls.Remove(item1);
            stackPanel_.Controls.Remove(item2);

            var syaPanel = infixPanel_.Parent;
            syaPanel.Controls.Add(item1);
            syaPanel.Controls.Add(item2);
            item1.Location = infixPanel_.Location;
            item2.Location = new Point(stackPanel_.Location.X, stackPanel_.Location.Y - stackPanel_.Height / 2);
            for (int i = 0; i < 50; ++i)
            {
                item1.Location = new Point(item1.Location.X - 1, item1.Location.Y + 1);
                item2.Location = new Point(item2.Location.X + 1, item2.Location.Y - 1);
                await Task.Delay(10);
            }
            syaPanel.Controls.Remove(item1);
            syaPanel.Controls.Remove(item2);
            operStack_.Pop();
        }

        private async Task MoveAction(Label _item, Func<Task> _action)
        {
            var syaPanel = infixPanel_.Parent;
            syaPanel.Controls.Add(_item);

            var orgF = _item.Font;
            var orgP = _item.Padding;
            var orgBS = _item.BorderStyle;
            _item.Font = new Font(_item.Font, FontStyle.Bold);
            _item.Padding = new Padding(3);
            _item.BorderStyle = BorderStyle.FixedSingle;

            await _action();

            _item.Font = orgF;
            _item.Padding = orgP;
            _item.BorderStyle = orgBS;

            syaPanel.Controls.Remove(_item);
        }



        private FlowLayoutPanel     inputPanel_     = new FlowLayoutPanel();
        private FlowLayoutPanel     infixPanel_     = new FlowLayoutPanel();
        private FlowLayoutPanel     postfixPanel_   = new FlowLayoutPanel();
        private FlowLayoutPanel     stackPanel_     = new FlowLayoutPanel();
        private List<IObject>       infixExpr_      = new List<IObject>();
        private List<IObject>       postfixExpr_    = new List<IObject>();
        private Stack<IOperator>    operStack_      = new Stack<IOperator>();
    }
}
