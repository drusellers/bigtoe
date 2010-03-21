namespace bigtoe
{
    using System.Reflection;

    public class MetaModel
    {
        public static Metadata Build<T>()
        {
            var t = typeof(T);
            var classMeta = new EntityType("Class");
            var result = new Metadata(t.Name, classMeta);

            foreach (var info in typeof(T).GetProperties())
            {
                result.AddProperty(info);
            }
            foreach (var info in typeof(T).GetMethods())
            {
                var meth = result.AddMethod(info);
                foreach (var parameter in info.GetParameters())
                {
                    meth.AddParameter(parameter);
                }
            }
            return result;
        }
    }

    public static class Ext
    {
        private static EntityType _propertyRelationship = new EntityType("Property");
        private static EntityType _methodRelationship = new EntityType("Method");
        private static EntityType _parameterRelationship = new EntityType("Parameter");
        public static Metadata AddProperty(this Metadata m, PropertyInfo info)
        {
            var x = new Metadata(info.Name, new EntityType(info.PropertyType.Name));
            m.Relationships.Add(new Relationship("has a", _propertyRelationship)
                                {
                                    With = x
                                });
            return x;
        }
        public static Metadata AddMethod(this Metadata m, MethodInfo methodInfo)
        {
            var x = new Metadata(methodInfo.Name, new EntityType(methodInfo.ReturnType.Name));
            m.Relationships.Add(new Relationship("has a", _methodRelationship)
                                {
                                    With = x
                                });
            return x;
        }
        public static Metadata AddParameter(this Metadata m, ParameterInfo info)
        {
            var x = new Metadata(info.Name, new EntityType(info.ParameterType.Name));
            m.Relationships.Add(new Relationship("has a", _parameterRelationship)
                                {
                                    With = x
                                });
            return x;
        }
    }
}