using NUnit.Framework;
using System;
using TLCLogin.Business;

namespace LoginTest
{
    [TestFixture]
    public class BusinessLogicTest
    {

        #region Student
            

        [Test]
        public void TestStudent_ValidStudentID_Low()
        {
            Student s = new Student();
            s.StudentID = 0;
            Assert.AreEqual(s.StudentID, 0);
        }

        [Test]
        public void TestStudent_ValidStudentID_High()
        {
            Student s = new Student();
            s.StudentID = 999999999;
            Assert.AreEqual(s.StudentID, 999999999);
        }

        [Test]
        public void TestStudent_InvalidStudentID_High()
        {
            Student s = new Student();
            Assert.Throws<ArgumentException>(() => s.StudentID = 1000000000);
        }

        [Test]
        public void TestStudent_InvalidStudentID_Low()
        {
            Student s = new Student();
            Assert.Throws<ArgumentException>(() => s.StudentID = -1);
        }


        [Test]
        public void TestStudent_ValidFirstName()
        {
            Student s = new Student();
            string n = "A";
            s.FirstName = n;
            Assert.AreEqual(s.FirstName, n);
        }

        [Test]
        public void TestStudent_InvalidFirstName_Long()
        {
            Student s = new Student();
            string n = new string('a', 256);
            s.FirstName = n;
            Assert.AreEqual(s.FirstName, n.Substring(0,255));
        }
       

        [Test]
        public void TestStudent_ValidLastName()
        {
            Student s = new Student();
            string n = "a";
            s.LastName = n;
            Assert.AreEqual(s.LastName, n);
        }

        [Test]
        public void TestStudent_InvalidLastName_Long()
        {
            Student s = new Student();
            string n = new string('a', 256);
            s.LastName = n;
            Assert.AreEqual(s.LastName, n.Substring(0, 255));
        }

        #endregion
                

        #region Login

        [Test]
        public void TestLogin_ValidStudent()
        {
            Login lo = new Login();
            Student s = new Student();
            lo.Student = s;
            Assert.AreEqual(lo.Student, s);
        }

        [Test]
        public void TestLogin_InvalidStudent_Null()
        {
            Login lo = new Login();
            Assert.Throws<ArgumentNullException>(() => lo.Student = null);
        }


        [Test]
        public void TestLogin_ValidTimeIn_Now()
        {
            Login lo = new Login();
            DateTime t = DateTime.Now;
            lo.TimeIn = t;
            Assert.AreEqual(lo.TimeIn, t);
        }

        [Test]
        public void TestLogin_InvalidTimeIn_Min()
        {
            Login lo = new Login();
            DateTime t = DateTime.MinValue;
            Assert.Throws<ArgumentOutOfRangeException>(() => lo.TimeIn = t);
        }

        [Test]
        public void TestLogin_ValidTimeOut()
        {
            Login lo = new Login();
            DateTime t = DateTime.Now;
            DateTime to = t.AddMinutes(15.0);

            lo.TimeIn = t;
            lo.TimeOut = to;

            Assert.AreEqual(lo.TimeOut, to);
        }

        [Test]
        public void TestLogin_InvalidTimeOut_Min()
        {
            Login lo = new Login();
            DateTime t = DateTime.MinValue;
            Assert.Throws<ArgumentOutOfRangeException>(() => lo.TimeOut = t);
        }

        [Test]
        public void TestLogin_InvalidTimeOut_EarlierThanTimeIn()
        {
            Login lo = new Login();
            DateTime t = DateTime.Now;
            DateTime to = t.AddMinutes(15.0);

            lo.TimeIn = to;
            Assert.Throws<ArgumentOutOfRangeException>(() => lo.TimeOut = t); 
        }

        #endregion


        #region AreaOfAssistance

        [Test]
        public void TestAreaOfAssistance_ValidLogoffLength()
        {
            AreaOfAssistance a = new AreaOfAssistance();
            a.AutoLogoffLength = 10;
            Assert.AreEqual(10, a.AutoLogoffLength);
        }

        [Test]
        public void TestAreaOfAssistance_InvalidLogoffLength_Zero()
        {
            AreaOfAssistance a = new AreaOfAssistance();
            Assert.Throws<ArgumentOutOfRangeException>(() => a.AutoLogoffLength = 0);
        }

        #endregion


        #region Course

        [Test]
        public void TestCourse_ValidLetterCode()
        {
            Course c = new Course();
            c.LetterCode = "ABCD";
            Assert.AreEqual("ABCD", c.LetterCode);
        }

        [Test]
        public void TestCourse_InvalidLetterCode_Empty()
        {
            Course c = new Course();
            Assert.Throws<ArgumentException>(() => c.LetterCode = "");
        }

        [Test]
        public void TestCourse_InvalidLetterCode_Long()
        {
            Course c = new Course();
            Assert.Throws<ArgumentException>(() => c.LetterCode = "ABCDE");
        }

        [Test]
        public void TestCourse_ValidNumberCode()
        {
            Course c = new Course();
            c.NumberCode = 1234;
            Assert.AreEqual(1234, c.NumberCode);
        }

        [Test]
        public void TestCourse_InvalidNumberCode_Zero()
        {
            Course c = new Course();
            Assert.Throws<ArgumentOutOfRangeException>(() => c.NumberCode = 0);
        }

        [Test]
        public void TestCourse_InvalidNumberCode_High()
        {
            Course c = new Course();
            Assert.Throws<ArgumentOutOfRangeException>(() => c.NumberCode = 10000);
        }

        #endregion
    }
}
