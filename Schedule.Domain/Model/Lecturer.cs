using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.Domain.Model
{
    public class Lecturer
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
        }

        private LinkedList<TimeConstraint> _constraints;
        public LinkedList<TimeConstraint> Constraints
        {
            get { return _constraints; }
            set { _constraints = value; }
        }

        public Lecturer(string first, string last)
        {
            this._firstName = first;
            this._lastName = last;
            Constraints = new LinkedList<TimeConstraint>();
        }

        /// <summary>
        /// Add constraint to the list
        /// </summary>
        /// <param name="t">TimeConstraint</param>
        public void AddConstraint(TimeConstraint t)
        {
            var list = Constraints.Where(x => x.Day == t.Day);
            if (list.Count() == 0)
                Constraints.AddLast(t);
            foreach (var item in Constraints.Where(x=>x.Day ==t.Day))
            {

            }

            Constraints = new LinkedList<TimeConstraint>(Constraints.OrderBy(x => ((int)Enum.Parse(typeof(ConstraintDayOfWeek), x.Day))));
        }

        /// <summary>
        /// Get list of constrains
        /// </summary>
        public LinkedList<TimeConstraint> GetConstraints()
        {
            return Constraints;
        }

        /// <summary>
        /// Get number of different days for all canstaints 
        /// </summary>
        public int DifferentDays()
        {
            return Constraints.Select(x => x.Day).Distinct().Count();
        }

        /// <summary>
        /// Get total number of hours for all canstaints 
        /// </summary>
        public int Hours()
        {
            int sum = 0;
            foreach (var item in Constraints)
                sum += item.Hours();
            return sum;
        }
    }
}
