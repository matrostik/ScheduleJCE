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
           // l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Thursday.ToString(), 8, 10));
            //l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 8, 11));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 12, 14));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 18, 19));
            //l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 8, 9));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 15, 16));
            //l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Tuesday, 8, 10));

            Console.WriteLine("***** Output test: *****");
            foreach (var item in l.Constraints)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Different Days : " + l.DifferentDays());
            Console.WriteLine("Total Hours: " +l.Hours());
            Console.ReadKey();
        }
    }
}
