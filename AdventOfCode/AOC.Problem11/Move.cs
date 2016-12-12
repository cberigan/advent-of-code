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
    }
}