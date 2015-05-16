using System;

namespace Schedule.Domain.Model
{

    public enum ConstraintDayOfWeek
    {
        Sunday =1,
        Monday,
        Tuesday,
        Wednesday,
        Thursday
    };

    public class TimeConstraint
    {
        private string _day;
        public string Day
        {
            get { return _day; }
            set
            {
                ConstraintDayOfWeek day;
                if (Enum.TryParse(value, out day))
                    _day = value;
            }
        }

        private int _beginHour;
        public int BeginHour
        {
            get { return _beginHour; }
            set
            {
                if (value >= 8 && value <= 20)
                    _beginHour = value;
            }
        }

        private int _endHour;
        public int EndHour
        {
            get { return _endHour; }
            set
            {
                if (value >= 8 && value <= 20)
                    _endHour = value;
            }
        }

        public TimeConstraint(string day, int begin, int end)
        {
            this.Day = day;
            this.BeginHour = begin;
            this.EndHour = end;
        }

        public TimeConstraint(ConstraintDayOfWeek day, int begin, int end)
        {
            this.Day = day.ToString();
            this.BeginHour = begin;
            this.EndHour = end;
        }

        /// <summary>
        /// Time range
        /// </summary>
        public int Hours()
        {
            return this.EndHour - this.BeginHour;
        }

        /// <summary>
        /// Check if constraints overlaps
        /// </summary>
        /// <param name="other"></param>
        public bool Contains(TimeConstraint other)
        {
            // different day of week
            if (!this.Day.Equals(other.Day))
                return false;
            // this start later or end befor other
            if (this.BeginHour >= other.BeginHour && this.EndHour >= other.EndHour)
                return true;
            return false;
        }

        /// <summary>
        /// Union 2 constraints
        /// </summary>
        /// <param name="other"></param>
        public TimeConstraint Union(TimeConstraint other)
        {
            // different day of week
            if (!this.Day.Equals(other.Day))
                return null;
            // this ends before other
            if (this.EndHour < other.BeginHour)
                return null;
            // other ends before this
            if (other.EndHour < this.BeginHour)
                return null;
            // get begin and end hours
            int start = this.BeginHour <= other.BeginHour ? this.BeginHour : other.BeginHour;
            int end = this.EndHour >= other.EndHour ? this.EndHour : other.EndHour;

            return new TimeConstraint(this.Day, start, end);
        }

        /// <summary>
        /// Constraint string representation
        /// Example: "Sunday, from 9:00 to 12:00"
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}, from {1}:00 to {2}:00", _day, _beginHour, _endHour);
        }
    }
}
