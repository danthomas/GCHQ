using System;
using GCHQ.Core;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;
        private int _cellSize;

        [Test]
        public void Test()
        {
            Board board = new Board(25, new[]
            {
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
            }, new[]
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
            });

            var s = BoardToString(board);

        }

        [Test]
        public void Test2()
        {
            Setup();

            Assert.That(BoardToString(_board), Is.EqualTo(@"
XX X |
XX   |
XX   |
XX X |
X XX |"));

            var ranges = new[]
            {
                new[] {0, 0, 0, 2},
                new[] {-1, 2, 0, 1},
                new[] {1, 3, 0, 1},
                new[] {-1, 4, 0, 1},

                new[] {2, 0, 1, 2},
                new[] {-1, 2, 1, 3}
            };

            foreach (var range in ranges)
            {
                for (int x = _cellSize*range[1]; x < _cellSize*(range[1] + range[3]); ++x)
                {
                    for (int y = _cellSize*range[2]; y < _cellSize*(range[2] + 1); ++y)
                    {
                        _board.MouseDown(x, y);

                        if (range[0] == -1)
                        {
                            Assert.That(_board.CurrentPiece, Is.Null);
                        }
                        else
                        {
                            Assert.That(_board.CurrentPiece, Is.EqualTo(_board.Pieces[range[0]]));
                        }
                    }
                }
            }
        }

        [Test]
        public void Test3()
        {
            Setup();

            var x = (_board.Pieces[2].X + _cellSize) + 10;
            var y = (_board.Pieces[2].Y + _cellSize) + 10;

            _board.MouseDown(x, y);

            _board.MouseMove(x + 24, y);

        }

        private void Setup()
        {
            _cellSize = 25;

            _board = new Board(_cellSize, new[]
            {
                new[] {2, 1},
                new[] {2},
                new[] {2},
                new[] {2, 1},
                new[] {1, 2},
            }, new[]
            {
                new[] {2},
                new[] {1, 2},
                new[] {3, 1},
                new[] {1, 1},
                new[] {1, 1},
            });
        }

        private string BoardToString(Board board)
        {
            var ret = "";
            for (int y = 0; y < board.Height; ++y)
            {
                ret += Environment.NewLine;
                for (int x = 0; x < board.Width; ++x)
                {
                    ret += board.CellOccupied(x, y) ? "X" : " ";
                }
                ret += "|";
            }

            return ret;
        }
    }
}
