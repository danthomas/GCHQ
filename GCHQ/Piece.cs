using System.Windows.Forms;

namespace GCHQ
{
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