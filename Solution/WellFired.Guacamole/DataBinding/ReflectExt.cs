using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.DataBinding
{
    public class ReflectionCache
    {
        public MemberInfo[] Members { get; }

        private ReflectionCache(Type t)
        {
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
        [UsedImplicitly]
        public static bool HasAttribute<T>(this MemberInfo m) where T : Attribute
        {
            return Attribute.IsDefined(m, typeof(T));
        }

        [UsedImplicitly]
        public static T GetAttribute<T>(this MemberInfo m) where T : Attribute
        {
            return m.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
        }

        [UsedImplicitly]
        public static T GetAttribute<T>(this object m, string memberName) where T : Attribute
        {
            var member = ReflectionCache.Get<T>().GetMember(memberName);
            return member?.GetAttribute<T>();
        }

        [UsedImplicitly]
        public static Type GetMemberType(this MemberInfo member)
        {
            var info = member as MethodInfo;
            if (info != null)
                return info.ReturnType;

            var propertyInfo = member as PropertyInfo;
            if (propertyInfo != null)
                return propertyInfo.PropertyType;

            return ((FieldInfo)member).FieldType;
        }

        [UsedImplicitly]
        public static Type GetParamaterType(this MemberInfo member)
        {
            var methodInfo = member as MethodInfo;
            if (methodInfo != null)
            {
                var p = methodInfo.GetParameters().FirstOrDefault();
                return p?.ParameterType;
            }

            var propertyInfo = member as PropertyInfo;
            if (propertyInfo != null)
                return propertyInfo.PropertyType;

            var memberInfo = member as FieldInfo;
            return memberInfo?.FieldType;
        }

        [UsedImplicitly]
        public static void SetMemberValue(this MemberInfo member, object instance, object value)
        {
            var memberInfo = member as MethodInfo;
            var propertyInfo = member as PropertyInfo;
            if (memberInfo != null)
                memberInfo.Invoke(instance, memberInfo.GetParameters().Any() ? new[] {value} : null);
            else if(propertyInfo != null)
                propertyInfo.SetValue(instance, value, null);
            else
                ((FieldInfo)member).SetValue(instance, value);
        }

        [UsedImplicitly]
        public static object GetMemberValue(this MemberInfo member, object instance)
        {
            var memberInfo = member as MethodInfo;
            if (memberInfo != null)
                return memberInfo.Invoke(instance, null);
            var propertyInfo = member as PropertyInfo;
            return propertyInfo != null ? propertyInfo.GetValue (instance, null) : ((FieldInfo)member).GetValue(instance);
        }

        [UsedImplicitly]
        public static object GetMemberValue(this object instance, string propertyName)
        {
            var member = ReflectionCache.Get(instance.GetType()).GetMember(propertyName); 

            if (member == null)
                return null;

            var memberInfo = member as MethodInfo;
            if (memberInfo != null)
                return memberInfo.Invoke(instance, null);
            var propertyInfo = member as PropertyInfo;
            return propertyInfo != null ? propertyInfo.GetValue(instance, null) : ((FieldInfo)member).GetValue(instance);
        }

        [UsedImplicitly]
        public static T GetMemberValue<T>(this MemberInfo member, object instance)
        {
            return (T)GetMemberValue(member, instance);

        }

        [UsedImplicitly]
        public static MemberInfo[] GetRuntimeMembers(this Type t)
        {
            return ReflectionCache.Get(t).Members;
        }

        [UsedImplicitly]
        public static MemberInfo GetRuntimeMember(this Type t, string name)
        {
            return ReflectionCache.Get(t).GetMember(name);
        }

        [UsedImplicitly]
        public static bool IsEnum(this Type t)
        {
            return t.IsEnum;
        }

        [UsedImplicitly]
        public static bool IsAssignable(this Type desiredType, object param)
        {
            return desiredType.IsInstanceOfType(param);
        }
    }
}