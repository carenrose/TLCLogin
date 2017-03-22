using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLCLogin.Business;

namespace TLCLogin
{
    public class TLCLoginController
    {
        private int campusID;

        public int CampusID
        {
            get { return campusID; }
            set {
                campusID = value;
                try
                {
                    List<Login> fromDB = Data.TLCLoginDA.GetAllLogins(value);
                    if (fromDB != null) this.LoggedInSessions = fromDB;
                }
                catch
                {
                    throw;
                }
            }
        }

        public string CampusName { get; set; }
        public Login CurrentLogin { get; set; }
        public Student CurrentStudent { get; set; }
        public List<Login> LoggedInSessions { get; set; }


        public TLCLoginController()
        {
            this.LoggedInSessions = new List<Login>();
        }

        
        /// <summary>
        /// Step 1 : Checks if a student is logged in
        /// If the student is logged on, they will be logged off
        /// </summary>
        /// <param name="id">A string representing a possible student Id</param>
        /// <returns>True if student is logged in already, false if student is not logged in or exception</returns>
        public bool CheckLoginStatus(string id)
        {
            bool worked = false;

            if (worked = CreateStudent(id))
            {
                try
                {
                    if (Data.TLCLoginDA.StudentIsLoggedIn(CurrentStudent.StudentID, CampusID))
                    {
                        LogOff();
                        //ClearCurrent();
                    }
                }
                catch
                {
                    throw;
                }
            }

            return worked;
        }


        /// <summary>
        /// Step 1b: Validates the entered ID, searches the database and registration.csv file for the student
        /// Assigns this Student object to CurrentStudent
        /// </summary>
        /// <param name="id">A string representing a possible student id</param>
        /// <returns>True if a valid student object was created</returns>
        public bool CreateStudent(string id)
        {
            bool valid = false;

            CurrentStudent = new Student();
            try
            {
                CurrentStudent.StudentID = Convert.ToInt32(id);

                valid = true;

                // get data
                try
                {
                    Student fill = Data.TLCLoginDA.GetStudentByID(CurrentStudent.StudentID);
                    if (fill != null) CurrentStudent = fill;
                }
                catch
                {
                    throw;
                }
            }
            catch (FormatException)
            {
                throw new FormatException("Enter only numbers in the student ID.");
            }
            return valid;
        }

        /// <summary>
        /// Step 2: Saves student demographic info from Form 3 to the database
        /// </summary>
        /// <param name="student"></param>
        public void UpdateStudentInfo(Student student)
        {
            try
            {
                if (Data.TLCLoginDA.StudentExistsInDB(student.StudentID))
                    Data.TLCLoginDA.UpdateStudent(student);
                else
                    Data.TLCLoginDA.AddStudent(student);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Step 3: Creats a Login object and saves it to the database
        /// Assigns Login_AutoLogoff() handler to the Login's AutoLogoff event
        /// </summary>
        /// <param name="area"></param>
        public void CreateLogin(AreaOfAssistance area)
        {
            CurrentLogin = new Login();
            CurrentLogin.Student = CurrentStudent;
            CurrentLogin.AreaOfAssistance = area;
            CurrentLogin.TimeIn = DateTime.Now;
            CurrentLogin.AutoLogoff += Login_AutoLogoff;

            try
            {
                CurrentLogin.ID = Data.TLCLoginDA.AddLogin(CurrentLogin, CampusID);
                LoggedInSessions.Add(CurrentLogin);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Step 4: Updates the Login with the selected Course and saves it to the database
        /// Optional step - some Areas of Assistance may be marked to skip course selection
        /// </summary>
        /// <param name="course"></param>
        public void UpdateLoginCourse(Course course)
        {
            try
            {
                CurrentLogin.Course = course;
                Data.TLCLoginDA.UpdateLoginWithCourse(CurrentLogin);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Step Z: Logs a chosen Login off, updates database with logout time and removes from LoggedInSessions List
        /// May show survey on logoff to random students
        /// </summary>
        public void LogOff()
        {
            // find in current logins
            CurrentLogin = LoggedInSessions.Find(l => l.Student.StudentID == CurrentStudent.StudentID);

            if (CurrentLogin != null)
            {
                try
                {
                    Data.TLCLoginDA.LogStudentOff(CurrentLogin.ID, DateTime.Now);
                    LoggedInSessions.Remove(CurrentLogin);
                    CurrentStudent = null;
                }
                catch
                {
                    throw;
                }
            }
        }

        public void Survey(int starsClicked)
        {
            if (CurrentLogin != null)
            {
                Data.TLCLoginDA.UpdateLoginWithSurvey(CurrentLogin, starsClicked);
            }
        }


        /// <summary>
        /// A Login's AutoLogoff event
        /// Updates the database with logoff time and removes from the LoggedInSessions List
        /// </summary>
        /// <param name="login"></param>
        private void Login_AutoLogoff(Login login)
        {
            try
            {
                Data.TLCLoginDA.LogStudentOff(login);
                //MessageBox.Show("AutoLogoff Run: " + login.Student.StudentID);
                LoggedInSessions.Remove(login);
            }
            catch
            {
                throw;
            }
        }

        
        /// <summary>
        /// Logs off any logins that are still logged in
        /// </summary>
        public void CloseCenter()
        {
            try
            {
                Data.TLCLoginDA.LogAllStudentsOff(CampusID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Sets CurrentStudent and CurrentLogin to null
        /// </summary>
        public void ClearCurrent()
        {
            this.CurrentStudent = null;
            this.CurrentLogin = null;
        }

    }
}
