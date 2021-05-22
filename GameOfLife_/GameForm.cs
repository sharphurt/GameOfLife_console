using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Session2;

namespace GameOfLife_
{
    public partial class GameForm : Form
    {
        public int CellSize = 10;

        public readonly Game Game = new Game(new HashSet<Point>
        {
            new Point(1, 1), new Point(2, 1), new Point(3, 1)
        });

        public bool IsUserDrawing;
        public bool ControlPressed;
        public bool ShiftPressed;

        private Point PrevMousePosition;
        private Size PositionDelta;

        private Point RectFillStartPoint;

        public Point PositionOffset;

        public GameForm()
        {
            InitializeComponent();
            Size = new Size(800, 500);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            var graphics = paintEventArgs.Graphics;

            for (var x = 0; x < Size.Width / CellSize; x++)
            for (var y = 0; y < Size.Height / CellSize; y++)
            {
                graphics.FillRectangle(Game.Field.Contains(new Point(x, y)) ? Brushes.Cornsilk : Brushes.Black,
                    new Rectangle(x * CellSize, y * CellSize, CellSize,
                        CellSize));
            }

            if (!IsUserDrawing)
                Game.MakeTurn();

            Invalidate();
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0 && IsUserDrawing && !ControlPressed)
            {
                if (ShiftPressed)
                {
                    for (var x = -2; x <= 2; x++)
                    for (var y = -2; y <= 2; y++)
                        Game.Field.Add(new Point(e.X / CellSize + x, e.Y / CellSize + y));
                }
                else
                {
                    var point = new Point(e.X / CellSize, e.Y / CellSize);
                    Game.Field.Add(point);
                }
            }

            if ((e.Button & MouseButtons.Left) != 0 && IsUserDrawing && ControlPressed)
            {
                Text = e.X.ToString();
                for (var x = RectFillStartPoint.X / CellSize; x < e.X / CellSize; x++)
                for (var y = RectFillStartPoint.Y / CellSize; y < e.Y / CellSize; y++)
                    Game.Field.Add(new Point(x, y));
            }

            if ((e.Button & MouseButtons.Right) != 0)
            {
                PositionDelta = new Size(e.X - PrevMousePosition.X, e.Y - PrevMousePosition.Y);
                PositionOffset += PositionDelta;
                Game.SetOffset(new Point(PositionDelta.Width / CellSize, PositionDelta.Height / CellSize));
            }

            PrevMousePosition = e.Location;
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            IsUserDrawing = true;
            if (ControlPressed)
                RectFillStartPoint = new Point(e.X, e.Y);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            IsUserDrawing = false;
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            ControlPressed = e.Control;
            ShiftPressed = e.Shift;
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            ControlPressed = e.Control;
            ShiftPressed = e.Shift;
            base.OnKeyDown(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            CellSize += e.Delta / 120;
            if (CellSize < 3)
                CellSize = 3;

            base.OnMouseWheel(e);
        }
    }
}