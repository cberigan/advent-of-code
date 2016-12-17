using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem11
{
    class State
    {
        public List<Pair> Pairs { get; private set; }
        public List<Item> Items { get; private set; }
        public int ElevatorPos { get; private set; }

        public State PreviousState { get; private set; }
        public Move MoveFromPrevious { get; private set; }

        public State(int elevator, List<Item> items)
        {
            this.Items = items;
            this.Pairs = GetPairs(items);
            this.ElevatorPos = elevator;
        }

        private List<Item> GetItems(List<Pair> pairs)
        {
            List<Item> items = new List<Item>();
            foreach (var p in pairs)
            {
                items.Add(new Item(p.Type, MaterialType.generator, p.GenFloor));
                items.Add(new Item(p.Type, MaterialType.microchip, p.MicFloor));
            }
            return items;
        }

        private List<Pair> GetPairs(List<Item> items)
        {
            List<Pair> pairs = new List<Pair>();
            Dictionary<string, int> genFloors = new Dictionary<string, int>();
            Dictionary<string, int> micFloors = new Dictionary<string, int>();
            foreach (var i in items)
            {
                if (i.Type.Equals(MaterialType.generator))
                {
                    genFloors.Add(i.Material, i.Floor);
                }
                else
                {
                    micFloors.Add(i.Material, i.Floor);
                }
            }
            foreach (var m in micFloors)
            {
                pairs.Add(new Pair(m.Key, m.Value, genFloors[m.Key]));
            }
            pairs.Sort(new PairComparer());
            return pairs;
        }

        public State(int elevator, List<Item> items, State previous, Move move) : this(elevator,items)
        {
            this.PreviousState = previous;
            this.MoveFromPrevious = move;
        }

        internal int GetRank()
        {
            int sum = 0;
            foreach (var i in Items)
            {
                sum += (4 - i.Floor);
            }
            return sum;
        }

        public HashSet<State> GenerateNextStates()
        {
            List<int> possibleFloors = GetPossibleElevatorFloors();
            var itemCombos = GetPossibleItemsOnElevator(Items.Where(i => i.Floor == ElevatorPos).ToList());

            Dictionary<Move,State> moves = new Dictionary<Move, State>();
            foreach (var newFloor in possibleFloors)
            {
                foreach (var pair in itemCombos)
                {
                    List<Item> copy = Items.ToList();
                    Item old1 = pair.Item1;
                    Item old2 = pair.Item2;

                    Item new1 = old1.MoveToFloor(newFloor);
                    Item new2 = old2 != null ? old2.MoveToFloor(newFloor) : null;

                    copy.Remove(old1);
                    copy.Add(new1);
                    if (old2 != null)
                    {
                        copy.Remove(old2);
                        copy.Add(new2);
                    }
                    var newState = new State(newFloor,copy);

                    if (newState.IsValid())
                    {
                        var direction = ElevatorPos > newFloor ? Direction.Down : Direction.Up;
                        var move = new Move(direction, Tuple.Create<Item, Item>(new1, new2));
                        moves.Add(move, new State(newFloor, copy,this,move));
                    }

                }
            }
            //if we can bring two items upstairs remove all moves we bring one item upstairs
            var movesCopy = moves.Keys.ToList();
            if (movesCopy.Count(m => m.ElevatorDirection.Equals(Direction.Up) && m.Items.Item1 != null && m.Items.Item2 != null) > 0)
            {
                var remove = movesCopy.Where(m => m.ElevatorDirection.Equals(Direction.Up) && m.Items.Item1 != null && m.Items.Item2 == null);
                foreach (var r in remove)
                {
                    moves.Remove(r);
                }
            }

            //if we can bring one item downstairs remove all moves we bring two items downstairs
            if (movesCopy.Count(m => m.ElevatorDirection.Equals(Direction.Down) && m.Items.Item1 != null && m.Items.Item2 == null) > 0)
            {
                var remove = movesCopy.Where(m => m.ElevatorDirection.Equals(Direction.Down) && m.Items.Item1 != null && m.Items.Item2 != null);
                foreach (var r in remove)
                {
                    moves.Remove(r);
                }
            }
            return new HashSet<State>(moves.Values.ToList());
        }

        internal int GetStepsFromStart()
        {
            int steps = 0;
            State currentState = this;
            while(currentState.PreviousState != null){
                steps++;
                currentState = currentState.PreviousState;
            }
            return steps;
        }

        private bool IsValid()
        {
            bool valid = true;
            for (int floor = 1; floor <= 4; floor++)
            {
                if (!IsFloorValid(ElevatorPos, floor)) valid = false;
            }

            return valid;
            
        }

        private bool IsFloorValid(int elevator, int floor)
        {
            //check for gen-chip pairs
            var chips = Items.Where(it => it.Type.Equals(MaterialType.microchip) && it.Floor.Equals(floor)).ToList();
            var gens = Items.Where(it => it.Type.Equals(MaterialType.generator) && it.Floor.Equals(floor)).ToList();

            if (chips.Count == 0 || gens.Count == 0)
                return true;

            //cancel out generators
            foreach (var c in chips.ToList())
            {
                var genPair = gens.FirstOrDefault(it => it.Material.Equals(c.Material));
                if (genPair != null)
                {
                    gens.Remove(genPair);
                    chips.Remove(c);
                }
            }
            
            //check for exposed chips
            if(chips.Count > 0) return false;
            else return true;
        }

        public bool IsGoal()
        {
            return Items.All(i => i.Floor == 4);
        }

        private List<Tuple<Item, Item>> GetPossibleItemsOnElevator(List<Item> items)
        {
            List<Tuple<Item, Item>> combos = new List<Tuple<Item, Item>>();
            for (int i = 0; i < items.Count; i++)
            {
                combos.Add(Tuple.Create<Item,Item>(items[i],null));
                for (int j = i + 1; j < items.Count; j++)
                {
                    combos.Add(Tuple.Create<Item, Item>(items[i], items[j]));
                }
            }
            return combos;
        }

        private List<int> GetPossibleElevatorFloors()
        {
            List<int> floors = new List<int>();
            var up = ElevatorPos + 1;
            var down = ElevatorPos - 1;

            //if no items are on the floors below don't go down
            if(down >= 1 && Items.Count(i => i.Floor <= down) > 0)
                floors.Add(down);

            if (up <= 4) floors.Add(up);
            return floors;
        }

        public override bool Equals(object obj)
        {
            var other = obj as State;
            if (other == null) return false;

            return this.ElevatorPos.Equals(other.ElevatorPos) && this.Pairs.SequenceEqual(other.Pairs);
        }
        
        public override int GetHashCode()
        {
            return ElevatorPos.GetHashCode() ^ Items.GetHashCode();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("===================");
            sb.AppendLine(string.Format("Elevator Position: {0}", ElevatorPos));
            for (int i = 4; i >= 1; i--)
            {
                var itemsOnFloor = Items.Where(it => it.Floor == i);
                sb.AppendLine(string.Format("Floor {0}: {1}", i, string.Join(", ", itemsOnFloor.Select(s => s.ToString()).ToArray())));
            }
            sb.AppendLine("===================");
            return sb.ToString();
        }
    }
}
