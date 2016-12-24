using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem24
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Point Prev { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(int x, int y, Point prev) : this(x, y)
        {
            Prev = prev;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public int GetStepsFromStart()
        {
            if (Prev == null) return 0;
            return 1 + Prev.GetStepsFromStart();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Point;
            if (other == null) return false;
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        internal int GetDistance(Point goal)
        {
            return Math.Abs(X - goal.X) + Math.Abs(Y - goal.Y);
        }

        internal List<Point> GetPointsAround()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(X + 1, Y, this));
            points.Add(new Point(X - 1, Y, this));
            points.Add(new Point(X, Y + 1, this));
            points.Add(new Point(X, Y - 1, this));
            return points;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
}
