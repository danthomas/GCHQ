using System;
using System.Collections.Generic;
using System.Linq;

namespace GCHQ.Core
{
    public class Board
    {
        private readonly int _cellSize;
        private readonly int[][] _horizontal;
        private readonly int[][] _vertical;
        private int _downX;

        public Board(int cellSize, int[][] horizontal, int[][] vertical)
        {
            _cellSize = cellSize;
            _horizontal = horizontal;
            _vertical = vertical;

            Initialise();
        }

        private void Initialise()
        {
            Height = _horizontal.Length;
            Width = _vertical.Length;

            Pieces = _horizontal
             .Select((xs, y) => new Temp { y = y, xs = xs })
             .SelectMany(x => x.xs, (temp, length) => new Piece(0, temp.y, length))
             .ToList();

            for (int y = 0; y < Height; ++y)
            {
                int x = 0;
                foreach (var piece in Pieces.Where(piece => piece.Y == y))
                {
                    piece.X = x;
                    x += piece.Length + 1;
                }
            }
        }

        public List<Piece> Pieces { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public class Temp
        {
            public int y { get; set; }
            public int[] xs { get; set; }
        }

        public bool CellOccupied(int x, int y)
        {
            return Pieces.Any(p => x >= p.X && x < p.X + p.Length && y == p.Y);
        }

        public void MouseDown(int x, int y)
        {
            CurrentPiece = Pieces.SingleOrDefault(p => CellHitTest(x, y, p));

            if ()
            _downPieceX = CurrentPiece.X
            _downX = x;
        }

        private bool CellHitTest(int x, int y, Piece p)
        {
            var ret = x >= p.X * _cellSize
                   && x < (p.X + p.Length) * _cellSize
                   && y >= p.Y * _cellSize
                   && y < (p.Y + 1) * _cellSize;

            return ret;
        }

        public Piece CurrentPiece { get; set; }

        public void MouseMove(int x, int y)
        {
            int xDiff = x - _downX;

            int diff = (int)Math.Floor(xDiff / 1.0 / _cellSize);
        }
    }
}
