namespace PowerCfgToggle
{
    public class PowerConfigPair
    {
        public PowerConfigPair(string name, string guid)
        {
            Name = name;
            Guid = guid;
        }
        
        public string Name { get; }
        
        public string Guid { get; }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Guid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PowerConfigPair pair) || this.GetType() != obj.GetType())
            {
                return false;
            }
            
            return Name.Equals(pair.Name) && Guid.Equals(pair.Guid);
        }

        public override string ToString()
        {
            return $"{Name}: {Guid}";
        }
    }
}