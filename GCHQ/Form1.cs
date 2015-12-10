using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCHQ
{
    public partial class Form1 : Form
    {
        private List<Piece> _pieces;
        private Piece _piece;

        public Form1()
        {
            InitializeComponent();

            int[][] pieces = {
                new [] { 7, 2, 1, 1, 7 },
                new [] { 1, 1, 2, 2, 1, 1 },
                new [] { 1, 3, 1, 3, 1, 3, 1, 3, 1 },
                new [] { 1, 3, 1, 1, 5, 1, 3, 1 },
                new [] { 1, 3, 1, 1, 4, 1, 3, 1 },
                new [] { 1, 1, 1, 2, 1, 1 },
                new [] { 7, 1, 1, 1, 1, 1, 7 },
                new [] { 1, 1, 3 },
                new [] { 2, 1, 2, 1, 8, 2, 1 },
                new [] { 2, 2, 1, 2, 1, 1, 1, 2 },
                new [] { 1, 7, 3, 2, 1 },
                new [] { 1, 2, 3, 1, 1, 1, 1, 1 },
                new [] { 4, 1, 1, 2, 6 },
                new [] { 3, 3, 1, 1, 1, 3, 1 },
                new [] { 1, 2, 5, 2, 2 },
                new [] { 2, 2, 1, 1, 1, 1, 1, 2, 1 },
                new [] { 1, 3, 3, 2, 1, 8, 1 },
                new [] { 6, 2, 1 },
                new [] { 7, 1, 4, 1, 1, 3 },
                new [] { 1, 1, 1, 1, 4 },
                new [] { 1, 3, 1, 3, 7, 1 },
                new [] { 1, 3, 1, 1, 1, 2, 1, 1, 4 },
                new [] { 1, 3, 1, 4, 3, 3 },
                new [] { 1, 1, 2, 2, 2, 6, 1 },
                new [] { 7, 1, 3, 2, 1, 1 }
            };

            _pieces = pieces
                .Select((xs, y) => new Temp { y = y, xs = xs })
                .SelectMany(x => x.xs, (temp, length) => new Piece(Orientation.Horizontal, 0, temp.y, length))
                .ToList();

            for (int y = 0; y < 25; ++y)
            {
                int x = 0;
                foreach (var piece in _pieces.Where(piece => piece.Orientation == Orientation.Horizontal && piece.Y == y))
                {
                    piece.X = x;
                    x += piece.Length + 1;
                }
            }
        }

        public class Temp
        {
            public int y { get; set; }
            public int[] xs { get; set; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush black = new SolidBrush(Color.Black);
            Brush red = new SolidBrush(Color.Red);

            foreach (var piece in _pieces)
            {
                e.Graphics.FillRectangle(_piece == piece ? red : black, new Rectangle(piece.X * 10, piece.Y * 10, 10 * piece.Length, 10));
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _piece = _pieces.SingleOrDefault(x =>
            e.X >= x.X * 10
            && e.X < (x.X + x.Length) * 10
            && e.Y >= x.Y * 10
            && e.Y < (x.Y + 1) * 10);

            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _piece = null;
            Invalidate();
        }
    }

    public class Piece
    {
        public Orientation Orientation { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Length { get; set; }

        public Piece(Orientation orientation, int x, int y, int length)
        {
            Orientation = orientation;
            X = x;
            Y = y;
            Length = length;
        }
    }
}
