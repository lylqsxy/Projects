using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
