/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCHQ;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            List<Piece> pieces = new List<Piece>
            {
                new Piece(0, 0, 2),
                new Piece(0, 0, 2),
                new Piece(0, 0, 2)
            };


            LeftAlign(pieces);
            pieces.Reverse();

            string data = "";
            Recurse(1, ref data);


             data = Write(pieces);

            File.WriteAllText(@"C:\temp\data.txt", data);
        }

        private void Recurse(int count, ref string data)
        {
            for(int i = 0; i < count; ++i)
            {
                data += i;

                if (count == 10)
                    break;

                Recurse(count + 1, ref data);
            }
            data += Environment.NewLine;

        }

        private string Write(List<Piece> pieces)
        {
            string data = "";

            for (int x = 1; x < 10;)
            {
                var piece = pieces.SingleOrDefault(p => p.X == x);

                if (piece == null)
                {
                    data += " ";
                    x += 1;
                }
                else
                {
                    data += new string('X', piece.Length);
                    x += piece.Length;
                }
            }
            data += "|";


            return data;
        }

        private static void LeftAlign(List<Piece> pieces)
        {
            int x = 0;
            foreach (Piece piece in pieces)
            {
                piece.X = x;
                x += piece.Length + 1;
            }
        }
    }
}
*/
