using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.Domain.Helpers;

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
            // add and sort
            Constraints.AddFirst(t);
            Constraints = new LinkedList<TimeConstraint>(Constraints.OrderBy(x => ((int)Enum.Parse(typeof(ConstraintDayOfWeek), x.Day))).ThenBy(x => x.BeginHour));

            for (LinkedListNode<TimeConstraint> node = Constraints.First; node != null; node = node.Next)
            {
                if (!node.Value.Day.Equals(t.Day))          // different day
                    continue;
                var before = node.Previous;
                if (before == null)                         // first node
                    continue;

                var tc = node.Value.Union(before.Value);   // try to merge nodes
                if (tc != null)                            // merge succeded
                {
                    Constraints.AddAfter(before, tc);
                    Constraints.Remove(node.Value);
                    Constraints.Remove(before.Value);
                    node = Constraints.First;
                }
            }
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
            return Constraints.Sum(x => x.Hours());
        }

    }
}
