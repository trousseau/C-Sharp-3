using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Tester
{
    class Program
    {
        public static void ReflectionTest()
        {
            Assembly asm = Assembly.Load("SharedLib");
            PrintModules(asm);
        }

        public static void PrintModules(Assembly asm)
        {
            var modules = asm.GetModules();

            foreach (var mod in modules)
            {
                Console.WriteLine($"Module: {mod.Name}");
                PrintTypes(mod);
            }
        }

        public static void PrintTypes(Module m)
        {
            var types = m.GetTypes();
            foreach (var t in types)
            {
                Console.WriteLine($"  Type: {t.Name}");
                PrintConstructors(t);
                PrintFields(t);
                PrintMethods(t);
                PrintProperties(t);
                PrintEvents(t);
            }
        }

        public static void PrintConstructors(Type t)
        {
            var ctors = t.GetConstructors();
            foreach (var c in ctors)
            {
                Console.WriteLine($"    Constructor: {c.Name}");
                PrintParameters(c);

            }
        }

        public static void PrintParameters(ConstructorInfo c)
        {
            var param = c.GetParameters();
            if (param.Count() > 0)
            {
                Console.WriteLine($"      Parameters:");
                foreach (var p in param)
                {
                    Console.WriteLine($"        {p.Name} {p.ParameterType}");

                }
            }

        }

        public static void PrintParameters(MethodInfo c)
        {
            var param = c.GetParameters();

            if (param.Count() > 0)
            {
                Console.WriteLine($"      Parameters:");
                foreach (var p in param)
                {
                    Console.WriteLine($"        {p.ParameterType} {p.Name}");
                }
            }
        }

        public static void PrintFields(Type t)
        {
            var fields = t.GetFields();

            if (fields.Count() > 0)
            {
                Console.WriteLine($"     Fields: ");
                foreach (var f in fields)
                {
                    Console.WriteLine($"        {f.FieldType} {f.Name}");
                }
            }

        }

        public static void PrintMethods(Type t)
        {
            var methods = t.GetMethods();
            foreach (var m in methods)
            {
                Console.WriteLine($"    Method: {m.Name}() returns {m.ReturnType}");
                PrintParameters(m);
            }
        }

        public static void PrintProperties(Type t)
        {
            var props = t.GetProperties();
            if (props.Count() > 0)
            {
                Console.WriteLine($"    Properites:");
                foreach (var p in props)
                {
                    Console.WriteLine($"        {p.PropertyType} {p.Name}");
                }
            }
        }

        public static void PrintEvents(Type t)
        {
            var events = t.GetEvents();
            if (events.Count() > 0)
            {
                Console.WriteLine($"    Events: ");
                foreach (var e in events)
                {
                    Console.WriteLine($"        {e.EventHandlerType} {e.Name}");
                }
            }
        }

        public static void TestPerson()
        {
            Assembly asm = Assembly.Load("SharedLib");
            dynamic p1 = asm.CreateInstance("SharedLib.Person");

            p1.FirstName = "Daenarys";
            p1.LastName = "Targaryn";
            p1.DOB = DateTime.Parse("11-9-1984");

            Type enumType = asm.GetType("SharedLib.Person+Genders");
            p1.Gender = (dynamic)Enum.Parse(enumType, "Female");

            Console.WriteLine(p1);

            dynamic p2 = asm.CreateInstance("SharedLib.Person", true,
                BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance, null,
                new object[] { "Smith", "John", DateTime.Parse("1/1/2000"),
                (dynamic)Enum.Parse(enumType, "Male") }, null, null);

            Console.WriteLine(p2);
        }

        public static void MathStuffTest()
        {
            Assembly asm = Assembly.Load("SharedLib");
            Type mathType = asm.GetType("SharedLib.MathStuff");

            var areaMethod = mathType.GetMethod("CircleArea", BindingFlags.Public |
            BindingFlags.Static);            double area = (double)areaMethod.Invoke(null, new object[] { 12.34 });
            Console.WriteLine(area);

        }

        public static void CustomAttributeTest()
        {
            Assembly asm = Assembly.Load("SharedLib");
            Type mathType = asm.GetType("SharedLib.MathStuff");
            Type personType = asm.GetType("SharedLib.Person");
            Type specialType = asm.GetType("SharedLib.SpecialClassAttribute");
            Type shapeType = asm.GetType("SharedLib.Shape");
            Type rectangleType = asm.GetType("SharedLib.Rectangle");
            Type triangleType = asm.GetType("SharedLib.Triangle");
            Type utilsType = asm.GetType("SharedLib.Utils");

            var attrs = mathType.GetCustomAttributes(specialType);

            foreach (dynamic attr in attrs)
            {
                Console.WriteLine($"{mathType.Name} has the special class ID of {attr.ID}");
            }

            var persAttrs = personType.GetCustomAttributes(specialType);

            foreach (dynamic persAttr in persAttrs)
            {
                Console.WriteLine($"{personType.Name} has the special class ID of {persAttr.ID}");
            }

            var shapeAttrs = shapeType.GetCustomAttributes(specialType);

            foreach (dynamic shapeAttr in shapeAttrs)
            {
                Console.WriteLine($"{shapeType.Name} has the special class ID of {shapeAttr.ID}");
            }

            var rectangleAttrs = rectangleType.GetCustomAttributes(specialType);

            foreach (dynamic rectangleAttr in rectangleAttrs)
            {
                Console.WriteLine($"{rectangleType.Name} has the special class ID of {rectangleAttr.ID}");
            }

            var triangleAttrs = triangleType.GetCustomAttributes(specialType);

            foreach (dynamic triangleAttr in triangleAttrs)
            {
                Console.WriteLine($"{triangleType.Name} has the special class ID of {triangleAttr.ID}");
            }

            var utilsAttrs = utilsType.GetCustomAttributes(specialType);

            foreach (dynamic utilsAttr in utilsAttrs)
            {
                Console.WriteLine($"{utilsType.Name} has the special class ID of {utilsAttr.ID}");
            }

        }

        static void Main(string[] args)
        {
            ReflectionTest();
            TestPerson();
            MathStuffTest();
            CustomAttributeTest();

            Console.ReadLine();
        }
    }
}
