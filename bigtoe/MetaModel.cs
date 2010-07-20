namespace bigtoe
{
    using System.Reflection;

    public class MetaModel
    {
        public static Metadata Build<T>()
        {
            var t = typeof(T);
            var result = new Metadata(t.Name, EntityType.Class);

            ProcessProperties<T>(result);
            
            
            var constructors = typeof (T).GetConstructors();
            foreach (var constructor in constructors)
            {
                result.AddConstructor(constructor);
            }


            ProcessMethods<T>(result);
            return result;
        }

        static void ProcessMethods<T>(Metadata result)
        {
            var methods = typeof(T).GetMethods();
            foreach (var info in methods)
            {
                if(result.HasProperty(info) || result.IsMethodOnObject(info)) continue;
                if(info.IsConstructor) continue;

                var meth = result.AddMethod(info);
                foreach (var parameter in info.GetParameters())
                {
                    meth.AddParameter(parameter);
                }
            }
        }

        static void ProcessProperties<T>(Metadata result)
        {
            var properties = typeof(T).GetProperties();
            foreach (var info in properties)
            {
                result.AddProperty(info);
            }
        }
    }

    public static class Ext
    {

        public static Metadata AddProperty(this Metadata m, PropertyInfo info)
        {
            var x = new Metadata(info.Name, new EntityType(info.PropertyType.Name));
            m.Relationships.Add(Relationship.BuildHasA(EntityType.Property, x));

            return x;
        }
        public static Metadata AddMethod(this Metadata m, MethodInfo methodInfo)
        {
            var x = new Metadata(methodInfo.Name, new EntityType(methodInfo.ReturnType.Name));
            m.Relationships.Add((Relationship.BuildHasA(EntityType.Method, x)));

            return x;
        }
        public static Metadata AddParameter(this Metadata m, ParameterInfo info)
        {
            var x = new Metadata(info.Name, new EntityType(info.ParameterType.Name));
            m.Relationships.Add((Relationship.BuildHasA(EntityType.Parameter, x)));

            return x;
        }
        public static Metadata AddConstructor(this Metadata m, ConstructorInfo info)
        {
            var x = new Metadata(info.Name, new EntityType(info.DeclaringType.Name));

            m.Relationships.Add(Relationship.BuildHasA(EntityType.Constructor, x));

            return x;
        }
    }
}