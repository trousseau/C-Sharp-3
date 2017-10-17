using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    public class Student : IDisposable
    {
        private bool _Disposed = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DynamicArray<int> Scores { get; set; }

        public Student(string lastName, string firstName, int numScores)
        {
            LastName = lastName;
            FirstName = firstName;
            Scores = new DynamicArray<int>(numScores);
        }

        public override string ToString()
        {
            return $"{LastName,-12} {FirstName,12} {Scores.Count()} {Scores.Average(),-12:###.###}";
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing Student from thread {Thread.CurrentThread.ManagedThreadId}");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                Scores?.Dispose();
                Scores = null;
                return;
            }

            _Disposed = true;
        }

        ~Student()
        {
            Console.WriteLine($"Finalizing Student from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
