namespace bigtoe
{
    using System;
    using System.Reflection;

    public class MetaModel
    {
        public static Metadata Build(Type t)
        {
            var result = new Metadata(t.Name, EntityType.Class);

            ProcessProperties(t, result);
            
            
            var constructors = t.GetConstructors();
            foreach (var constructor in constructors)
            {
                result.AddConstructor(constructor);
            }


            ProcessMethods(t, result);
            return result;
        }
        public static Metadata Build<T>()
        {
            return Build(typeof (T));
        }

        static void ProcessMethods(Type t, Metadata result)
        {
            var methods = t.GetMethods();
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

        static void ProcessProperties(Type t, Metadata result)
        {
            var properties = t.GetProperties();
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