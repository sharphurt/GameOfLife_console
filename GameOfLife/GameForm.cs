using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public sealed partial class GameForm : Form
    {
        public const int CellSize = 10;
        public static readonly Game Game = new Game(new HashSet<Point>());

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
                    new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize));
            }
        }
    }
}