using System.Reflection;
using System.Text;

namespace Lesson6
{

    internal class Program
    {
        public static TestClass MakeTestclass() {
            Type testclass = typeof(TestClass);
            return Activator.CreateInstance(testclass) as TestClass;
        }

        public static TestClass MakeTestclass(int i)
        {
            Type testclass = typeof(TestClass);
            return Activator.CreateInstance(testclass, new object[] { i}) as TestClass;
        }

        public static TestClass MakeTestclass(int i, string s, decimal d, char[] c)
        {
            Type testclass = typeof(TestClass);
            return Activator.CreateInstance(testclass, new object[] {i, s, d, c}) as TestClass;
        }

        public static PropertyInfo FindFieldByAttr(Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo res = null;
            if (properties.Length == 0)
            {
                return res;
            }
            foreach (var property in properties)
            {
                var attrs = property.GetCustomAttributes(true);
                foreach (var item in properties)
                {
                    if (item is CustomNameAttribute)
                    {
                        res = property; break;
                    }
                }
            }
            return res;
        }
        public static string ObjectToString(object o)
        {
            Type type = o.GetType();
            StringBuilder res = new StringBuilder();
            //res.Append(type.Namespace + ':');
            res.Append(type.AssemblyQualifiedName + ":");
            res.Append(type.Name + '|');
            var prop = type.GetProperties();
            
            foreach (var item in prop)
            {
                var temp = item.GetValue(o);

                var attrs = item.GetCustomAttributes(true);
                bool hasAlias = false;
                foreach (var i in attrs)
                {
                    if (i is CustomNameAttribute)
                    {
                        hasAlias = true;
                        res.Append((i as CustomNameAttribute).CustomName);
                    }
                }
                if (!hasAlias) { res.Append(item.Name); }
                res.Append(':');                
                if (item.PropertyType == typeof(char[]))
                {
                    res.Append(new string(temp as char[]) + '|');
                }
                else {
                    res.Append(temp);
                    res.Append('|');
                }
                 
            }
            return res.ToString();
        }

        public static object StringToObject(string s) {
            string[] arr = s.Split("|");
            string[] arr1 = arr[0].Split(':');
            object some = Activator.CreateInstance(null, arr1[0].Split(",")[0]);
            
            if(arr.Length > 1 && some != null)
            {
                var type = some.GetType();
                for (int i = 1; i < arr.Length; i++)
                {
                    string[] nameAndValue = arr[i].Split(":");
                    
                    var p = type.GetProperty(nameAndValue[0]);
                    if (p == null) {
                        continue;
                    }
                    if (p.PropertyType == typeof(int)) {
                        p.SetValue(some, int.Parse(nameAndValue[1]));
                    } 
                    else if (p.PropertyType == typeof(string)) { 
                        p.SetValue(some, nameAndValue[1]);
                    }
                    else if(p.PropertyType == typeof(decimal)) {
                        p.SetValue(some, decimal.Parse(nameAndValue[1]));
                    }
                    else if(p.PropertyType == typeof(char[])) {
                        p.SetValue(some, nameAndValue[1].ToCharArray());
                    }

                }
            }
            return some;
        }
        static void Main(string[] args)
        {
            //var n1 = MakeTestclass();
            //var n2 = MakeTestclass(5);
            char[] somearr = new char[] { 'a', 'g', 'f' };
            var n3 = MakeTestclass(6, "что-то", 1, somearr);

            string some = ObjectToString(n3);
            Console.WriteLine(some);

            var some1 = StringToObject(some);
            Console.WriteLine(ObjectToString(some1) );

        }
    }
}
