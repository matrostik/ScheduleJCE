using System.Collections.Generic;
using System.Linq;

namespace Schedule.Domain.Model
{
    public class Staff
    {
        // properties
        private LinkedList<Lecturer> _lecturers;
        public LinkedList<Lecturer> Lecturers
        {
            get { return _lecturers; }
            set { _lecturers = value; }
        }

        // ctors
        public Staff()
        {
            _lecturers = new LinkedList<Lecturer>();
        }

        /// <summary>
        /// Get constrain for the selected lecturer
        /// </summary>
        /// <param name="first">First name</param>
        /// <param name="last">Last name</param>
        /// <returns></returns>
        public LinkedList<TimeConstraint> GetConstraints(string first, string last)
        {
            var lecturer = _lecturers.FirstOrDefault(l => l.FirstName.Equals(first) && l.LastName.Equals(last));
            return lecturer != null ? lecturer.Constraints : null;
        }

        /// <summary>
        /// Add constraints to the lecturer's list of constraints
        /// </summary>
        /// <param name="first">First name</param>
        /// <param name="second">Last name</param>
        /// <param name="list">list of constraints</param>
        public void AddConstraints(string first, string second, LinkedList<TimeConstraint> list)
        {
            var lecturer = _lecturers.FirstOrDefault(l => l.FirstName.Equals(first) && l.LastName.Equals(second));
            if (lecturer == null)
                return;
            foreach (var item in list)
            {
                lecturer.AddConstraint(item);
            }
        }

        /// <summary>
        /// Add new lecturer
        /// </summary>
        /// <param name="first">First name</param>
        /// <param name="second">Last name</param>
        public void AddLecturer(string first, string last)
        {
            var lecturer = new Lecturer(first, last);
            this._lecturers.AddLast(lecturer);
        }

        public void AddLecturer(Lecturer lecturer)
        {
            this._lecturers.AddLast(lecturer);
        }
    }
}
