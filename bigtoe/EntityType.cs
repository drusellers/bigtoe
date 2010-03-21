namespace bigtoe
{
    public class EntityType : 
        Meta
    {
        public static   EntityType Null = new EntityType("null");
        public EntityType(string name)
        {
            Name = name;
        }
    }
}