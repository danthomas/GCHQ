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
        private int _size;
        private int _mouseDownX;
        private int _initialPieceX;
        private int _diffX;
        private bool[,] _fixed;
        private int[][] _vertical;
        private bool[] _highlight;

        public Form1()
        {
            _size = 25;

            InitializeComponent();

            _highlight = new bool[25];

            this.SetStyle(
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer,
               true);

            int[][] horizontal = {
                new[] {7, 3, 1, 1, 7},
                new[] {1, 1, 2, 2, 1, 1},
                new[] {1, 3, 1, 3, 1, 1, 3, 1},
                new[] {1, 3, 1, 1, 6, 1, 3, 1},
                new[] {1, 3, 1, 5, 2, 1, 3, 1},
                new[] {1, 1, 2, 1, 1},
                new[] {7, 1, 1, 1, 1, 1, 7},
                new[] {3, 3},
                new[] {1, 2, 3, 1, 1, 3, 1, 1, 2},
                new[] {1, 1, 3, 2, 1, 1},
                new[] {4, 1, 4, 2, 1, 2},
                new[] {1, 1, 1, 1, 1, 4, 1, 3},
                new[] {2, 1, 1, 1, 2, 5},
                new[] {3, 2, 2, 6, 3, 1},
                new[] {1, 9, 1, 1, 2, 1},
                new[] {2, 1, 2, 2, 3, 1},
                new[] {3, 1, 1, 1, 1, 5, 1},
                new[] {1, 2, 2, 5},
                new[] {7, 1, 2, 1, 1, 1, 3},
                new[] {1, 1, 2, 1, 2, 2, 1},
                new[] {1, 3, 1, 4, 5, 1},
                new[] {1, 3, 1, 3, 10, 2},
                new[] {1, 3, 1, 1, 6, 6},
                new[] {1, 1, 2, 1, 1, 2},
                new[] {7, 2, 1, 2, 5},
            };

            _vertical = new[]
            {
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

            string result = "";
            for (int x = 0; x < 25; ++x)
            {
                result += String.Join(", ", _vertical[x].Select(z => z.ToString())) + Environment.NewLine;

            }

            label2.Text = result;

            _pieces = horizontal
                .Select((xs, y) => new Temp { y = y, xs = xs })
                .SelectMany(x => x.xs, (temp, length) => new Piece(Orientation.Horizontal, 0, temp.y * _size, length * _size))
                .ToList();

            for (int y = 0; y < 25; ++y)
            {
                int x = 25 * _size;
                foreach (var piece in _pieces.Where(piece => piece.Orientation == Orientation.Horizontal && piece.Y == y * 25).Reverse())
                {
                    piece.X = x - piece.Length;
                    x -= piece.Length + _size;
                }
            }

            var rightBoard = GetBoard();


            for (int y = 0; y < 25; ++y)
            {
                int x = 0;
                foreach (var piece in _pieces.Where(piece => piece.Orientation == Orientation.Horizontal && piece.Y == y * 25))
                {
                    piece.X = x;
                    x += piece.Length + _size;
                }
            }

            var leftBoard = GetBoard();
            


            SetFixed(leftBoard, rightBoard);


            Check();
        }

        private void SetFixed(bool[,] leftBoard, bool[,] rightBoard)
        {
            _fixed = new bool[25, 25];

           /* for (int x = 0; x < 25; ++x)
            {
                for (int y = 0; y < 25; ++y)
                {
                    _fixed[x, y] = leftBoard[x, y] && rightBoard[x, y];
                }
            }
*/

            _fixed[3, 3] = true;
            _fixed[4, 3] = true;
            _fixed[12, 3] = true;
            _fixed[13, 3] = true;
            _fixed[21, 3] = true;

            _fixed[6, 8] = true;
            _fixed[7, 8] = true;
            _fixed[10, 8] = true;
            _fixed[14, 8] = true;
            _fixed[15, 8] = true;
            _fixed[18, 8] = true;

            _fixed[6, 17] = true;
            _fixed[11, 17] = true;
            _fixed[16, 17] = true;
            _fixed[20, 17] = true;

            _fixed[3, 22] = true;
            _fixed[4, 22] = true;
            _fixed[9, 22] = true;
            _fixed[10, 22] = true;
            _fixed[15, 22] = true;
            _fixed[20, 22] = true;
            _fixed[21, 22] = true;

        }

        public class Temp
        {
            public int y { get; set; }
            public int[] xs { get; set; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush grey = new SolidBrush(Color.Gray);
            Brush blue = new SolidBrush(Color.Blue);
            Pen black = new Pen(Color.Black, 3);
            Pen red = new Pen(Color.Red, 3);
            Pen yellow = new Pen(Color.Yellow, 3);

            float[] dashValues = { 5, 5, 5, 5 };
            yellow.DashPattern = dashValues;


            var rectangle = new Rectangle(0, 0, 25 * _size, 25 * _size);

            e.Graphics.DrawRectangle(black, rectangle);


            foreach (var piece in _pieces)
            {
                rectangle = new Rectangle(piece.X + (_piece == piece ? _diffX : 0), piece.Y, piece.Length, _size);

                e.Graphics.FillRectangle(grey, rectangle);
                e.Graphics.DrawRectangle(_piece == piece ? red : black, rectangle);
            }


             for (int x = 0; x < 25; ++x)
             {
                 for (int y = 0; y < 25; ++y)
                 {
                     if ( _fixed[x, y])
                     {
                         rectangle = new Rectangle((x * 25) + 10, (y * 25) + 10, 12, 12);
                         e.Graphics.FillRectangle(blue, rectangle);
                     }
                 }
             }


            for (int x = 0; x < 25; ++x)
            {
                if (_highlight[x])
                {
                    e.Graphics.DrawLine(yellow, (x * 25) + 11, 0, (x * 25) + 11, 25 * 25);
                }
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _piece = _pieces.SingleOrDefault(x =>
                e.X >= x.X
                && e.X < (x.X + x.Length)
                && e.Y >= x.Y
                && e.Y < (x.Y + _size));

                if (_piece != null)
                {
                    _initialPieceX = _mouseDownX = e.Location.X;

                     _pieces.Where(x => x.Y == _piece.Y && x.X > _piece.X).Min(x => x.X);

                    Invalidate();
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_piece != null)
                {
                    var x = _piece.X + _diffX;

                    _piece.X = (int)(Math.Round(1.0 * x / _size, 0) * _size);

                    _diffX = 0;
                    _piece = null;

                    Check();
                }
            }
            else
            {
                _highlight[(int) (Math.Round(1.0*e.X/_size, 0)*_size) / 25] =! _highlight[(int) (Math.Round(1.0*e.X/_size, 0)*_size) / 25];
            }

            Invalidate();
        }

        private void Check()
        {
            int[][] actual = new int[25][];

            var board = GetBoard();

            for (int x = 0; x < 25; ++x)
            {
                List<Piece> pieces = new List<Piece>();
                Piece piece = null;


                for (int y = 0; y < 25; ++y)
                {
                    if (board[x, y])
                    {
                        if (piece == null)
                        {
                            piece = new Piece(Orientation.Vertical, 0, 0, 1);
                            pieces.Add(piece);
                        }
                        else
                        {
                            piece.Length += 1;
                        }
                    }
                    else
                    {
                        piece = null;
                    }
                }

                actual[x] = pieces.Select(z => z.Length).ToArray();

                //result += String.Join(", ", pieces.Select(z => z.Length.ToString())) + Environment.NewLine;
            }


            string result = "";
            for (int x = 0; x < 25; ++x)
            {
                result += String.Join(", ", actual[x].Select(z => z.ToString()));

                if (String.Join(", ", actual[x].Select(z => z.ToString())) == String.Join(", ", _vertical[x].Select(z => z.ToString())))
                {
                    result += "************";
                }

                result += Environment.NewLine;

            }

            label1.Text = result;
        }

        private bool[,] GetBoard()
        {
            bool[,] board = new bool[25, 25];

            foreach (Piece piece in _pieces)
            {
                int x = piece.X / 25;
                int y = piece.Y / 25;

                for (int i = 0; i < piece.Length / 25; ++i)
                {
                    board[x + i, y] = true;
                }
            }
            return board;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_piece != null)
            {
                _diffX = e.X - _mouseDownX;


                if (_piece.X + _piece.Length + _diffX > (25 * 25))
                {
                    _diffX = (25*25) - _piece.X - _piece.Length;
                }

                Invalidate();
            }
        }
    }
}
