using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem11
{
    class State
    {
        public HashSet<Item> Items { get; private set; }
        public int ElevatorPos { get; private set; }

        public State PreviousState { get; private set; }
        public Move MoveFromPrevious { get; private set; }

        public State(int elevator, List<Item> items)
        {
            this.Items = new HashSet<Item>(items);
            this.ElevatorPos = elevator;
        }

        public State(int elevator, List<Item> items, State previous, Move move) : this(elevator,items)
        {
            this.PreviousState = previous;
            this.MoveFromPrevious = move;
        }

        public List<State> GenerateNextStates()
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
            if(movesCopy.Count(m => m.ElevatorDirection.Equals(Direction.Up) && m.Items.Item1 != null && m.Items.Item2 != null) > 0)
            {
                var remove = movesCopy.Where(m => m.ElevatorDirection.Equals(Direction.Up) && m.Items.Item1 != null && m.Items.Item2 == null);
                foreach (var r in remove)
                {
                    moves.Remove(r);
                }
            }

            //if we can bring one item downstairs remove all moves we bring two items upstairs
            if (movesCopy.Count(m => m.ElevatorDirection.Equals(Direction.Down) && m.Items.Item1 != null && m.Items.Item2 == null) > 0)
            {
                var remove = movesCopy.Where(m => m.ElevatorDirection.Equals(Direction.Down) && m.Items.Item1 != null && m.Items.Item2 != null);
                foreach (var r in remove)
                {
                    moves.Remove(r);
                }
            }
            return moves.Values.ToList();
        }

        private List<Move> GenerateAllPossibleMoves()
        {
            throw new NotImplementedException();
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
                if (!IsFloorValid(ElevatorPos, floor))
                { 
                    valid = false;
                }
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
            if(chips.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
            if(down >= 1)
            {
                if(Items.Count(i => i.Floor <= down) > 0)
                    floors.Add(down);
            }

            if(up <= 4) floors.Add(up);
            return floors;
        }

        public override bool Equals(object obj)
        {
            var other = obj as State;

            if (other == null)
            {
                return false;
            }

            return this.ElevatorPos.Equals(other.ElevatorPos) && this.Items.SetEquals(other.Items);
        }

        public override int GetHashCode()
        {
            return ElevatorPos.GetHashCode() ^ Items.GetHashCode();
        }
    }
}
