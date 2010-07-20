namespace bigtoe
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}:{EntityType.Name}:{With.Name}")]
    public class Relationship :
        Metadata
    {
        //name
        //from
        //to?
        public Relationship(string name, EntityType type) : base(name, type)
        {
        }

        public Metadata With { get; set; }

        public override string ToString()
        {
            return "Relationship: " + With.Name;
        }

        public static Relationship BuildHasA(EntityType type, Metadata with)
        {
            return new Relationship("has a", type)
                       {
                           With = with
                       };
        }
    }
}