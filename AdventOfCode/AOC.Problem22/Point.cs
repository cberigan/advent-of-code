using System;

namespace AOC.Problem22
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Point;
            if (other == null) return false;
            return other.X == X && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        internal Point ToCopy()
        {
            return new Point(X, Y);
        }
    }
}