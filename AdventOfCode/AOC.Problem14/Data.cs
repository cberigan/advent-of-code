namespace AOC.Problem14
{
    internal class Data
    {
        public int IndexFound { get; set; }
        public string Trip { get; set; }
        public string Quint { get; set; }
        public string Hash { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Data;
            if (other == null) return false;
            return IndexFound.Equals(other.IndexFound) && Hash.Equals(other.Hash);

        }

        public override int GetHashCode()
        {
            return IndexFound.GetHashCode() ^ Hash.GetHashCode();
        }
    }
}