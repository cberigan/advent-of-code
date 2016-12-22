using System;
using System.Collections.Generic;

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

        internal int GetDistance(Point other)
        {
            return Math.Abs(this.Y - other.Y) + Math.Abs(this.X - other.X);
        }

        internal List<Point> GetPointsAround()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(X + 1, Y));
            points.Add(new Point(X - 1, Y));
            points.Add(new Point(X, Y - 1));
            points.Add(new Point(X, Y + 1));
            return points;
        }
    }
}