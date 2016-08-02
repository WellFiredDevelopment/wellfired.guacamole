using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WellFired.Guacamole.Databinding
{
    public class ReflectionCache
    {
        public Type Type;
        public MemberInfo[] Members;

        public ReflectionCache(Type t)
        {
            Type = t;
            Members = t.GetMembers().ToArray();
        }

        public MemberInfo GetMember(string name)
        {
            return Members.FirstOrDefault(o => o.Name == name);
        }

        private static readonly Dictionary<Type, ReflectionCache> Cache = new Dictionary<Type, ReflectionCache>();

        public static ReflectionCache Get<T>()
        {
            return Get(typeof(T));
        }

        public static ReflectionCache Get(Type type)
        {
            if (Cache.ContainsKey(type))
                return Cache[type];

            Cache.Add(type, new ReflectionCache(type));
            return Cache[type];
        }
    }

    public static class ReflectionExt
    {
        public static bool HasAttribute<T>(this MemberInfo m) where T : Attribute
        {
            return Attribute.IsDefined(m, typeof(T));
        }
			
        public static T GetAttribute<T>(this MemberInfo m) where T : Attribute
        {
            return m.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
        }

        public static T GetAttribute<T>(this object m, string memberName) where T : Attribute
        {
            var member = ReflectionCache.Get<T>().GetMember(memberName);
            if (member == null)
                return null;

            return member.GetAttribute<T>();

        }

        public static Type GetMemberType(this MemberInfo member)
        {
            if (member is MethodInfo)
                return ((MethodInfo)member).ReturnType;

            if (member is PropertyInfo)
                return ((PropertyInfo)member).PropertyType;

            return ((FieldInfo)member).FieldType;
        }

        public static Type GetParamaterType(this MemberInfo member)
        {
            if (member is MethodInfo)
            {
                var p = ((MethodInfo)member).GetParameters().FirstOrDefault();
                if (p == null)
                    return null;
                return p.ParameterType;
            }

            if (member is PropertyInfo)
                return ((PropertyInfo)member).PropertyType;

            if (member is FieldInfo)
                return ((FieldInfo)member).FieldType;

            return null;
        }

        public static void SetMemberValue(this MemberInfo member, object instance, object value)
        {
            if (member is MethodInfo)
            {
                var method = ((MethodInfo)member);

                if (method.GetParameters().Any())
                {
                    method.Invoke(instance, new[] { value });
                }
                else
                {
                    method.Invoke(instance, null);
                }
            }
            else if (member is PropertyInfo)
            {
                ((PropertyInfo)member).SetValue(instance, value, null);
            }
            else
            {
                ((FieldInfo)member).SetValue(instance, value);
            }
        }

        public static object GetMemberValue(this MemberInfo member, object instance)
        {
            if (member is MethodInfo)
                return ((MethodInfo)member).Invoke(instance, null);
			if (member is PropertyInfo)
				return ((PropertyInfo)member).GetValue (instance, null);

            return ((FieldInfo)member).GetValue(instance);

        }

        public static object GetMemberValue(this object instance, string propertyName)
        {
            var member = ReflectionCache.Get(instance.GetType()).GetMember(propertyName); 

            if (member == null)
                return null;

            if (member is MethodInfo)
                return ((MethodInfo)member).Invoke(instance, null);
            if (member is PropertyInfo)
                return ((PropertyInfo)member).GetValue(instance, null);

            return ((FieldInfo)member).GetValue(instance);

        }

        public static T GetMemberValue<T>(this MemberInfo member, object instance)
        {
            return (T)GetMemberValue(member, instance);

        }

        public static MemberInfo[] GetRuntimeMembers(this Type t)
        {
            return ReflectionCache.Get(t).Members;
        }

        public static MemberInfo GetRuntimeMember(this Type t, string name)
        {
            return ReflectionCache.Get(t).GetMember(name);
        }

        public static bool IsEnum(this Type t)
        {
            return t.IsEnum;
        }

        public static bool IsAssignable(this Type desiredType, object param)
        {
            return desiredType.IsInstanceOfType(param);
        }
    }
}