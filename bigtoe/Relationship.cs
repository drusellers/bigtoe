namespace bigtoe
{
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
    }
}