namespace bigtoe
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Linq;

    [DebuggerDisplay("{Name} {EntityType}")]
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

        public bool HasProperty(MethodInfo info)
        {
            bool result = false;
            foreach (var relationship in Relationships)
            {
                if(relationship.Name == "has a")
                {
                    if(relationship.EntityType == EntityType.Property)
                    {
                        if(info.Name.StartsWith("get_") ||info.Name.StartsWith("set_"))
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
        public bool IsMethodOnObject(MethodInfo info)
        {
            return new[] {"GetType", "ToString", "GetHashCode", "Equals"}.Contains(info.Name);
        }
    }
}