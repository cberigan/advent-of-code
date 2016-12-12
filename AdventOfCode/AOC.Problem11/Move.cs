using System;

namespace AOC.Problem11
{
    internal class Move
    {
        public Direction ElevatorDirection { get; private set; }
        public Tuple<Item, Item> Items { get; private set; }

        public Move(Direction dir, Tuple<Item,Item> items)
        {
            Items = items;
            ElevatorDirection = dir;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Move;

            if (other == null)
            {
                return false;
            }
            return this.ElevatorDirection.Equals(other.ElevatorDirection) && this.Items.Equals(other.Items);
        }

        public override int GetHashCode()
        {
            return this.ElevatorDirection.GetHashCode() ^ this.Items.GetHashCode();
        }
    }
}