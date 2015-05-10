using Schedule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {


            Lecturer l = new Lecturer("Bruce", "Lee");
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 19, 20));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Thursday.ToString(), 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 8, 11));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 11, 14));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 11, 19));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 11, 18));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 11, 17));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Tuesday, 8, 10));

            Console.WriteLine("***** Output test: *****");
            foreach (var item in l.Constraints)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Different Days : " + l.DifferentDays());
            Console.WriteLine("Total Hours: " + l.Hours());
            Console.WriteLine("index: " + (int)ConstraintDayOfWeek.Monday);
            //ConstraintDayOfWeek.Wednesday

            Console.ReadKey();
        }
    }
}
