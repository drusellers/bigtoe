namespace bigtoe
{
    using System;
    using System.Reflection;
    using Magnum.Extensions;

    public class MetaModel
    {
        public static Metadata BuildMessage(Type t)
        {
            var result = new Metadata(t.Name, EntityType.Message);
            result.Relationships.Add(Relationship.BuildAssembly(t));
//            if(t.HasAttribute<ObsoleteAttribute>())
//            {
//                result.Relationships.Add(Relationship.Obsolete(t));
//            }

            ProcessProperties(t, result);

            ProcessMethods(t, result);
            return result;
        }
        public static Metadata BuildMessage<T>()
        {
            return BuildMessage(typeof (T));
        }

        public static Metadata BuildClass(Type t)
        {
            var result = new Metadata(t.Name, EntityType.Class);
            result.Relationships.Add(Relationship.BuildAssembly(t));

//            if (t.HasAttribute<ObsoleteAttribute>())
//            {
//                result.Relationships.Add(Relationship.Obsolete(t));
//            }

            ProcessProperties(t, result);


            var constructors = t.GetConstructors();
            foreach (var constructor in constructors)
            {
                result.AddConstructor(constructor);
            }


            ProcessMethods(t, result);
            return result;
        }
        public static Metadata BuildClass<T>()
        {
            return BuildClass(typeof(T));
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
                if (info.PropertyType.IsUserType())
                {
                    var r = BuildMessage(info.PropertyType);
                    result.Relationships.Add(Relationship.BuildHasA(EntityType.Property, r));
                }
                else
                {
                    result.AddProperty(info);
                }
            }
        }
    }

    public static class Ext
    {
        public static bool IsUserType(this Type t)
        {
            if (t == null) return false;

            try
            {
                if (t.IsPrimitive || t.Is<int>() || t.Is<string>() || t.Is<decimal>() || t.Is<Guid>() ||
                    t.Is<DateTime>() || t.IsGenericType || t.Is<Type>())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool IsNullable(this Type t)
        {
            if(t.IsGenericType)
            {
                var def = t.GetGenericTypeDefinition();
                return def.Equals(typeof (Nullable<>));
            }

            return false;
        }



        public static Metadata AddProperty(this Metadata m, PropertyInfo info)
        {
            var t = info.PropertyType;
            
            //TODO: Handle lists
            Metadata x;
            if (t.IsNullable())
            {
                x = new Metadata(info.Name, new EntityType(info.PropertyType.GetGenericArguments()[0].Name));
                x.Relationships.Add(new Relationship("Nullable", EntityType.Note));

//                if(info.HasAttribute<ObsoleteAttribute>())
//                {
//                    x.Relationships.Add(Relationship.Obsolete(t));
//                }

                m.Relationships.Add(Relationship.BuildHasA(EntityType.Property, x));
            }
            else
            {
                x = new Metadata(info.Name, new EntityType(t.Name));
//                if (info.HasAttribute<ObsoleteAttribute>())
//                {
//                    x.Relationships.Add(Relationship.Obsolete(t));
//                }
                m.Relationships.Add(Relationship.BuildHasA(EntityType.Property, x));
            }

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
        public static bool Is<T>(this Type t)
        {
            return t.IsAssignableFrom(typeof (T));
        }
    }
}