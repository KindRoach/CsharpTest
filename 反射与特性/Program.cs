using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 反射与特性
{
    public class NotNullAttribute : Attribute
    {
        public static bool IsPropertyNull(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] ps = type.GetProperties();
            foreach (var item in ps)
            {
                if (item.IsDefined(typeof(NotNullAttribute)))
                {
                    var value = item.GetValue(obj);
                    if (value == null) return true;
                }
            }
            return false;                                  
        }

    }

    public class MyClass
    {
        [NotNull]
        public string MyProperty { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass mc = new MyClass() { MyProperty="123" };
            Console.WriteLine(NotNullAttribute.IsPropertyNull(mc));
        }
    }
}
