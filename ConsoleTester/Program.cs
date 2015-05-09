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
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Tuesday, 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday.ToString(), 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Thursday.ToString(), 8, 10));

           
            foreach (var item in l.Constraints)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
