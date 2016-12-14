using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem13
{
    class Space
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsWall { get; private set; }
        public bool IsOpen {
            get { return !IsWall; }
        }

        public Space ParentSpace { get; private set; }

        private int favorite = 1350;

        public Space(int x, int y)
        {
            X = x;
            Y = y;
            IsWall = DetermineWall();
        }

        public Space(int x, int y, Space parent) : this(x,y)
        {
            ParentSpace = parent;
        }

        private bool DetermineWall()
        {
            var value = (X * X) + (3 * X) + (2 * X * Y) + Y + (Y * Y) + favorite;
            var binary = Convert.ToString(value, 2);
            return binary.Count(c => c.Equals('1')) % 2 == 1 || X < 0 || Y < 0;
        }

        public int GetDistance(Space to)
        {
            return Math.Abs(to.X - X) + Math.Abs(to.Y - Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        internal int GetStepsFromStart()
        {
            if (ParentSpace == null) return 0;
            return ParentSpace.GetStepsFromStart() + 1;
        }

        internal List<Space> GetPointsAround()
        {
            List<Space> points = new List<Space>();
            points.Add(new Space(X + 1, Y, this));
            points.Add(new Space(X - 1, Y, this));
            points.Add(new Space(X, Y + 1, this));
            points.Add(new Space(X, Y - 1, this));
            return points;

        }

        public override bool Equals(object obj)
        {
            var other = obj as Space;
            if (other == null) return false;

            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
}
