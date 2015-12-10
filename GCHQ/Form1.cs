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
            Pen black = new Pen(Color.Black, 2);
            Pen white = new Pen(Color.White, 2);
            
            //e.Graphics.DrawRectangle(white, 0, 0, 5 * 25, 5 * 25);
            e.Graphics.DrawRectangle(white, 0, 0, 5 * 25, 5 * 25);

            //foreach (var piece in _pieces)
            //{
            //    e.Graphics.DrawRectangle(black, piece.X * 5, piece.Y * 5, 5, 5);
            //}
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
