using System;
using GCHQ.Core;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class BoardTests
    {
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
            Board board = new Board(25, new[]
            {
                new[] {2, 1},
                new[] {2},
                new[] {2},
                new[] {2, 1},
                new[] {1, 2},
            }, new[]
            {
                new [] { 2 },
                new [] { 1, 2 },
                new [] { 3, 1 },
                new [] { 1, 1 },
                new [] { 1, 1 },
            });

            Assert.That(BoardToString(board), Is.EqualTo(@"
XX X |
XX   |
XX   |
XX X |
X XX |"));

            for(int x = 0; x < 50; ++x)
            {
                for (int y = 0; y < 25; ++y)
                {
                    board.MouseDown(x, y);
                    Assert.That(board.CurrentPiece, Is.EqualTo(board.Pieces[0]));
                }
            }

            for (int x = 50; x < 75; ++x)
            {
                for (int y = 0; y < 25; ++y)
                {
                    board.MouseDown(x, y);
                    Assert.That(board.CurrentPiece, Is.Null);
                }
            }
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
