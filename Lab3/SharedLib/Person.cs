using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(1)]
    public class Person
    {
        public enum Genders
        {
            Unknown,
            Male,
            Female,
            Other
        };

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public Genders Gender { get; set; }

        public Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DOB = DateTime.MinValue;
            Gender = Genders.Unknown;
        }

        public Person(string firstName, string lastName, DateTime dOB, Genders gender)
        {
            FirstName = firstName;
            LastName = lastName;
            DOB = dOB;
            Gender = gender;
        }

        public override string ToString()
        {
            return string.Format($"{FirstName} {LastName} {DOB:dd-MM-yyyy} {Gender}");
        }
    }
}
