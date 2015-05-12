using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Domain.Model;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Lecturer l = new Lecturer("Bruce", "Lee");
            Assert.IsNotNull(l);
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Wednesday, 19, 20));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Thursday, 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 8, 10));
            Assert.AreEqual(3, l.Constraints.Count);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Lecturer l = new Lecturer("Bruce", "Lee");
            Assert.IsNotNull(l);
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 19, 20));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 8, 10));
            l.AddConstraint(new TimeConstraint(ConstraintDayOfWeek.Monday, 8, 10));
            Assert.AreEqual(2, l.Constraints.Count);
        }
    }
}
