namespace GCHQ.Core
{
    public class Piece
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Length { get; set; }

        public Piece(int x, int y, int length)
        {
            X = x;
            Y = y;
            Length = length;
        }
    }
}