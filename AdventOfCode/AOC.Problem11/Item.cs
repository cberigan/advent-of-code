using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem11
{
    class Item
    {
        public string Material { get; private set; }
        public MaterialType Type { get; private set; }
        public int Floor { get; set; }

        public Item(string material, MaterialType type, int floor)
        {
            Material = material;
            Type = type;
            Floor = floor;
        }

        public Item MoveToFloor(int floor)
        {
            return new Item(this.Material, this.Type, floor);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Item;

            if (other == null)
            {
                return false;
            }

            return this.Floor.Equals(other.Floor) && this.Material.Equals(other.Material) && this.Type.Equals(other.Type);
        }

        public override int GetHashCode()
        {
            return Floor.GetHashCode() ^ Type.GetHashCode() ^ Material.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Material,Type.ToString());
        }
    }
}
