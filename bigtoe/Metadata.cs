namespace bigtoe
{
    using System.Collections.Generic;

    public class Metadata : Meta
    {
        public  static readonly Metadata Root;
        static Metadata()
        {
            Root = new Metadata("root", new EntityType("null"));
        }

        public Metadata(string name, EntityType type)
        {
            Name = name;
            EntityType = type;
            Relationships = new List<Relationship>();
        }

        public EntityType EntityType { get; set; }
        public IList<Relationship> Relationships { get; set; }
    }
}