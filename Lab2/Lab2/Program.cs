using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void DynamicArrayTest()
        {
            using (DynamicArray<int> a = new DynamicArray<int>(5))
            {
                a[0] = 123;
                foreach (int x in a)
                {
                    Console.WriteLine(x);
                }
            }

            DynamicArray<int> b = new DynamicArray<int>(5);
            b[0] = 123;
            foreach (int x in b)
            {
                Console.WriteLine(x);
            }
        }

        static void StudentTest()
        {
            List<Student> students = new List<Student>();
            Student s = new Student("Smtih", "John", 3);
            s.Scores[0] = 90;
            s.Scores[1] = 85;
            s.Scores[2] = 97;
            students.Add(s);

            s = new Student("Williams", "Jane", 2);
            s.Scores[0] = 95;
            s.Scores[1] = 91;
            s.Scores.Resize(3);
            s.Scores[2] = 89;
            students.Add(s);

            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }
        }


        static void Main(string[] args)
        {
            //swap tests
            int x = 12;
            int y = 34;
            Console.WriteLine($"x = {x}, y = {y}");
            Utilities.Swap(ref x, ref y);
            Console.WriteLine($"x = {x}, y = {y}");

            double d1 = 123.45;
            double d2 = 543.21;
            Console.WriteLine($"d1 = {d1}, d2 = {d2}");
            Utilities.Swap(ref d1, ref d2);
            Console.WriteLine($"d1 = {d1}, d2 = {d2}");

            DynamicArrayTest();
            Console.WriteLine();
            StudentTest();

            Console.WriteLine("Press <ENTER> to start GC...");
            Console.ReadLine();
            GC.Collect();

            Console.WriteLine("Press <ENTER> to quit...");
            Console.ReadLine();
        }
    }
}
