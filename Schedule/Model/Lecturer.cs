using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Model
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
        /// <param name="t"></param>
        public void AddConstraint(TimeConstraint t)
        {
            foreach (var item in Constraints)
            {
                
            }
        }

        /// <summary>
        /// Get list of constrains
        /// </summary>
        /// <returns></returns>
        public LinkedList<TimeConstraint> GetConstraints()
        {
            return Constraints;
        }

        public int DifferentDays()
        {
            return Constraints.Select(x => x.Day).Distinct().Count();
        }

        public int Hours()
        {
            int sum = 0;
            foreach (var item in Constraints)
                sum += item.Hours();
            return sum;
        }
    }
}
