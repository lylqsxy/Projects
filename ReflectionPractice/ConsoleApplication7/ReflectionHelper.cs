using System;
using System.Reflection;

namespace ConsoleApplication7
{
    public static class ReflectionHelper
    {
        public static T CreateInstance<T>(Type TClass)
        {
            object o = Assembly.Load(TClass.Assembly.FullName).CreateInstance(TClass.FullName);
            return (T)o;
        }
    }
}
