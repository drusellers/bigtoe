namespace bigtoe
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}")]
    public class EntityType : 
        Meta
    {
        public static   EntityType Null = new EntityType("null");
        public static EntityType Property = new EntityType("Property");
        public static EntityType Method = new EntityType("Method");
        public static EntityType Parameter = new EntityType("Parameter");
        public static EntityType Class = new EntityType("Class");
        public static EntityType Constructor = new EntityType("Constructor");

        public EntityType(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return "EntityType: "+Name;
        }
    }
}