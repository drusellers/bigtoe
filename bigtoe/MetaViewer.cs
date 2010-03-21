namespace bigtoe
{
    using System;

    public class MetaViewer
    {
        public static void View(Metadata data)
        {
            Console.WriteLine("a '{0}' named '{1}' ", data.EntityType.Name, data.Name);
            foreach (var rel in data.Relationships)
            {
                Console.WriteLine("  '{0}' '{1}' '{2}' is a '{3}'", rel.Name,rel.EntityType.Name, rel.With.Name, rel.With.EntityType.Name);
                foreach (var r in rel.With.Relationships)
                {
                    Console.WriteLine("    '{0}' '{1}' '{2}' '{3}'", r.Name, r.EntityType.Name, r.With.Name, r.With.EntityType.Name);
                }
            }
        }
    }
}