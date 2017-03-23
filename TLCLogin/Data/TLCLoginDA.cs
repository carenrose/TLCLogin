using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLCLogin.Business;

namespace TLCLogin.Data
{
    static class TLCLoginDA
    {
        // Note: The OLE DB .NET Provider does not support named parameters for passing parameters to an SQL statement ...
        //       (naming parameters still works but HAVE to add them in ORDER!!)

        private const string ErrorMessagePart = "with the database, ";

        #region Campuses
        
        public static Dictionary<int,string> GetCampuses()
        {
            OleDbConnection conn = null;
            Dictionary<int,string> campuses = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT CampusID, CampusName " +
                             "FROM " + DBSchemaTables.Campus;
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                campuses = new Dictionary<int, string>();

                while (rdr.Read())
                {
                    campuses.Add(Convert.ToInt16(rdr["CampusID"]), rdr["CampusName"].ToString());
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving campuses", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return campuses;
        }

        #endregion

        #region Students
        public static Student GetStudentByID(int id)
        {
            OleDbConnection conn = null;
            Student student = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT FirstName, LastName, NativeLanguage, CountryOfOrigin, ProgramCode " +
                             "FROM " + DBSchemaTables.Student + " inner JOIN " + DBSchemaTables.ProgramOfStudy + 
                             " ON " + DBSchemaTables.Student + ".ProgramOfStudy = " + DBSchemaTables.ProgramOfStudy + ".ProgramID " +
                             "WHERE StudentID = @id";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    student = new Student();
                    student.StudentID = id;
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.NativeLanguage = Convert.ToInt16(rdr["NativeLanguage"]);
                    student.CountryOfOrigin = Convert.ToInt16(rdr["CountryOfOrigin"]);
                    student.ProgramOfStudy = rdr["ProgramCode"].ToString();
                }

                rdr.Close();

                // get spec progs
                if (student != null)
                {
                    cmd.CommandText = "SELECT ProgramID " +
                                      "FROM " + DBSchemaTables.StudentStuService + " " +
                                      "WHERE StudentID = @id";
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        student.SpecialPrograms.Add(
                            GetStudentServiceProgramByID(Convert.ToInt16(rdr["ProgramID"])));
                    }
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving student info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return student;
        }

        public static bool StudentExistsInDB(int id)
        {
            bool exists = false;
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT COUNT(*) FROM " + DBSchemaTables.Student + " " +
                             "WHERE StudentID = @stuid";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@stuid", id);

                conn.Open();
                exists = ((int)cmd.ExecuteScalar() > 0);
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "checking for student info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return exists;
        }

        public static void AddStudent(Student student)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();
                string sql = "INSERT INTO " + DBSchemaTables.Student + " " + 
                             "(StudentID, FirstName, LastName, NativeLanguage, CountryOfOrigin, ProgramOfStudy) " +
                             "VALUES (@stuid, @first, @last, @lang, @count, NULL)";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@stuid", student.StudentID);
                cmd.Parameters.AddWithValue("@first", student.FirstName);
                cmd.Parameters.AddWithValue("@last", student.LastName);
                cmd.Parameters.AddWithValue("@lang", student.NativeLanguage);
                cmd.Parameters.AddWithValue("@count", student.CountryOfOrigin);

                conn.Open();
                cmd.ExecuteNonQuery();

                // program of study
                cmd.CommandText = "SELECT ProgramID FROM " + DBSchemaTables.ProgramOfStudy + " " +
                                  "WHERE ProgramCode = @code";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@code", student.ProgramOfStudy);

                int programID = (int)cmd.ExecuteScalar();

                cmd.CommandText = "UPDATE " + DBSchemaTables.Student + " " +
                                  "SET ProgramOfStudy = @prog " +
                                  "WHERE StudentID = @stuid";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@prog", programID);
                cmd.Parameters.AddWithValue("@stuid", student.StudentID);
                cmd.ExecuteNonQuery();

                // special programs
                cmd.CommandText = "INSERT INTO " + DBSchemaTables.StudentStuService +
                                  "(StudentID, ProgramID) " +
                                  "VALUES (@stuid, @serv)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@stuid", student.StudentID);
                cmd.Parameters.AddWithValue("@serv", "");

                foreach (SpecialProgram prog in student.SpecialPrograms)
                {
                    cmd.Parameters["@serv"].Value = prog.ID;
                    cmd.ExecuteNonQuery();
                }

                // insert into "past" table
                cmd.CommandText = "INSERT INTO " + DBSchemaTables.PastStuServices +
                                  "(StudentID, ProgramID) " +
                                  "VALUES (@stuid, @serv)";

                foreach (SpecialProgram prog in student.SpecialPrograms)
                {
                    cmd.Parameters["@serv"].Value = prog.ID;
                    try { cmd.ExecuteNonQuery(); } catch (OleDbException) { }
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "adding student info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void UpdateStudent(Student student)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();
                string sql = "UPDATE " + DBSchemaTables.Student + " " +
                             "SET FirstName = @first, LastName = @last, NativeLanguage = @lang, CountryOfOrigin = @count " +
                             "WHERE StudentID = @stuid";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first", student.FirstName);
                cmd.Parameters.AddWithValue("@last", student.LastName);
                cmd.Parameters.AddWithValue("@lang", student.NativeLanguage);
                cmd.Parameters.AddWithValue("@count", student.CountryOfOrigin);
                cmd.Parameters.AddWithValue("@stuid", student.StudentID);

                conn.Open();
                cmd.ExecuteNonQuery();

                // program of study
                cmd.CommandText = "SELECT ProgramID FROM " + DBSchemaTables.ProgramOfStudy + " " +
                                  "WHERE ProgramCode = @code";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@code", student.ProgramOfStudy);

                int programID = (int)cmd.ExecuteScalar();

                cmd.CommandText = "UPDATE " + DBSchemaTables.Student + " " +
                                  "SET ProgramOfStudy = @prog " +
                                  "WHERE StudentID = @stuid";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@prog", programID);
                cmd.Parameters.AddWithValue("@stuid", student.StudentID);
                cmd.ExecuteNonQuery();

                // special programs
                cmd.CommandText = "DELETE FROM " + DBSchemaTables.StudentStuService + " " +
                                  "WHERE StudentID = @stuid " + 
                                  "AND ProgramID IN " + 
                                  "(SELECT ServiceID FROM " + DBSchemaTables.StuServiceProgram + " WHERE Enabled = Yes)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@stuid", student.StudentID);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO " + DBSchemaTables.StudentStuService +
                                  "(StudentID, ProgramID) " +
                                  "VALUES (@stuid, @serv)";
                cmd.Parameters.AddWithValue("@serv", "");

                foreach (SpecialProgram prog in student.SpecialPrograms)
                {
                    cmd.Parameters["@serv"].Value = prog.ID;
                    cmd.ExecuteNonQuery();
                }

                // insert into "past" table
                cmd.CommandText = "INSERT INTO " + DBSchemaTables.PastStuServices +
                                  "(StudentID, ProgramID) " +
                                  "VALUES (@stuid, @serv)";

                foreach (SpecialProgram prog in student.SpecialPrograms)
                {
                    cmd.Parameters["@serv"].Value = prog.ID;
                    try { cmd.ExecuteNonQuery(); } catch (OleDbException) { }
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "updating student info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static SpecialProgram GetStudentServiceProgramByID(int id)
        {
            OleDbConnection conn = null;
            SpecialProgram prog = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT ServiceName, Enabled " +
                             "FROM " + DBSchemaTables.StuServiceProgram + " " +
                             "WHERE ServiceID = @id";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prog = new SpecialProgram();
                    prog.ID = id;
                    prog.Name = rdr["ServiceName"].ToString();
                    prog.IsEnabled = Convert.ToBoolean(rdr["Enabled"]);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "could not load Student Services", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return prog;
        }

        public static bool IsFirstTimeStudent(int id)
        {
            bool firstTime = true;
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT COUNT(*) FROM " + DBSchemaTables.LoginHistory + " " +
                             "WHERE StudentID = @stuid";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@stuid", id);

                conn.Open();
                firstTime = ((int)cmd.ExecuteScalar() <= 1);     // because this is used after one login
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "checking for student info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return firstTime;
        }


        #endregion

        #region StudentLogins
        /*
        public static List<Login> GetAllLogins(int campusID)
        {
            OleDbConnection conn = null;
            List<Login> logins = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT HistoryID, " + DBSchemaTables.Student + ".StudentID, FirstName, LastName, AreaID, AreaName, DefaultLogoffMinutes, LogInTime " +
                             //"FROM " + DBSchemaTables.LoginHistory + " " + 
                             //"JOIN " + DBSchemaTables.Student + " ON " + DBSchemaTables.LoginHistory + ".StudentID = " + DBSchemaTables.Student + ".StudentID " + 
                             //"JOIN " + DBSchemaTables.AreaOfAssistance + " ON " + DBSchemaTables.LoginHistory + ".AreaOfAssistance = " + DBSchemaTables.AreaOfAssistance + ".AreaID " +
                             "FROM AreaOfAssistance INNER JOIN (Student INNER JOIN LoginHistory ON Student.StudentID = LoginHistory.StudentID) ON AreaOfAssistance.AreaID = LoginHistory.AreaOfAssistance " +
                             "WHERE Campus = @camp";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@camp", campusID);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();

                logins = new List<Login>();
                while (rdr.Read())
                {
                    Login l = new Login();
                    l.ID = Convert.ToInt16(rdr["HistoryID"]);
                    l.TimeIn = Convert.ToDateTime(rdr["LogInTime"]);
                    
                    l.Student = new Student();
                    l.Student.StudentID = Convert.ToInt32(rdr["StudentID"]);
                    l.Student.FirstName = rdr["FirstName"].ToString();
                    l.Student.LastName = rdr["LastName"].ToString();

                    l.AreaOfAssistance = new AreaOfAssistance();
                    l.AreaOfAssistance.ID = Convert.ToInt16(rdr["AreaID"]);
                    l.AreaOfAssistance.Name = rdr["AreaName"].ToString();
                    l.AreaOfAssistance.AutoLogoffLength = Convert.ToInt16(rdr["DefaultLogoffMinutes"]);

                    logins.Add(l);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.HandleException(ex, "Message");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return logins;
        }
        */
        public static DataTable GetAllLoginsTable(int campusID)
        {
            OleDbConnection conn = null;
            DataTable dt = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT HistoryID, " + DBSchemaTables.Student + ".StudentID, FirstName & ' ' & LastName AS StudentName, AreaName, DefaultLogoffMinutes, LogInTime, DATEDIFF('n', NOW(), DATEADD('n', DefaultLogoffMinutes, LogInTime)) AS MinutesRemain " +
                             //"FROM " + DBSchemaTables.LoginHistory + " " + 
                             //"JOIN " + DBSchemaTables.Student + " ON " + DBSchemaTables.LoginHistory + ".StudentID = " + DBSchemaTables.Student + ".StudentID " + 
                             //"JOIN " + DBSchemaTables.AreaOfAssistance + " ON " + DBSchemaTables.LoginHistory + ".AreaOfAssistance = " + DBSchemaTables.AreaOfAssistance + ".AreaID " +
                             "FROM AreaOfAssistance INNER JOIN (Student INNER JOIN LoginHistory ON Student.StudentID = LoginHistory.StudentID) ON AreaOfAssistance.AreaID = LoginHistory.AreaOfAssistance " +
                             "WHERE Campus = @camp AND LogOutTime IS NULL";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@camp", campusID);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    dt = new DataTable();
                    dt.Load(rdr);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving current logins", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return dt;
        }

        public static List<Login> GetAllLogins(int campusID)
        {
            OleDbConnection conn = null;
            List<Login> logins = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT HistoryID, " + DBSchemaTables.Student + ".StudentID, LogInTime, DefaultLogoffMinutes " +
                             //"FROM " + DBSchemaTables.LoginHistory + " INNER JOIN " + DBSchemaTables.Student + " ON " + DBSchemaTables.LoginHistory + ".StudentID = " + DBSchemaTables.Student + ".StudentID " + 
                             "FROM AreaOfAssistance INNER JOIN (Student INNER JOIN LoginHistory ON Student.StudentID = LoginHistory.StudentID) ON AreaOfAssistance.AreaID = LoginHistory.AreaOfAssistance " +
                             "WHERE Campus = @camp AND LogOutTime IS NULL";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@camp", campusID);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                logins = new List<Login>();

                while (rdr.Read())
                {
                    Login lo = new Login();

                    Student s = new Student();
                    s.StudentID = Convert.ToInt32(rdr["StudentID"]);
                    lo.Student = s;

                    AreaOfAssistance a = new AreaOfAssistance();
                    a.AutoLogoffLength = Convert.ToInt16(rdr["DefaultLogoffMinutes"]);
                    lo.AreaOfAssistance = a;

                    lo.ID = Convert.ToInt32(rdr["HistoryID"]);
                    lo.TimeIn = Convert.ToDateTime(rdr["LogInTime"]);

                    logins.Add(lo);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving current logins", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return logins;
        }

        public static bool StudentIsLoggedIn(int studentID, int campusID)
        {
            OleDbConnection conn = null;
            bool loggedIn = false;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT COUNT(*) FROM " + DBSchemaTables.LoginHistory + " " +
                             "WHERE StudentID = @id AND Campus = @cam AND LogOutTime IS NULL";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", studentID);
                cmd.Parameters.AddWithValue("@cam", campusID);

                conn.Open();
                loggedIn = ((int)cmd.ExecuteScalar() > 0);
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving login info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return loggedIn;
        }

        public static int AddLogin(Login login, int campusID)
        {
            OleDbConnection conn = null;
            int newID = -1;

            try
            {
                // remove milliseconds for access' sake 
                DateTime newTimeIn = new DateTime(login.TimeIn.Year, login.TimeIn.Month, login.TimeIn.Day, 
                    login.TimeIn.Hour, login.TimeIn.Minute, login.TimeIn.Second, login.TimeIn.Kind);

                conn = LoginDB.GetConnection();
                string sql = "INSERT INTO " + DBSchemaTables.LoginHistory + " " +
                             "(StudentID, Campus, AreaOfAssistance, CourseCategory, CourseNumber, LogInTime, SurveyHearAbout) " +
                             "VALUES (@stu, @cam, @aoa, NULL, NULL, @tim, NULL)";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@stu", login.Student.StudentID);
                cmd.Parameters.AddWithValue("@cam", campusID);
                cmd.Parameters.AddWithValue("@aoa", login.AreaOfAssistance.ID);
                cmd.Parameters.AddWithValue("@tim", newTimeIn);

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Select @@Identity";
                newID = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "creating login");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return newID;
        }

        public static void UpdateLoginWithCourse(Login login)
        {
            OleDbConnection conn = null;
            
            try
            {
                conn = LoginDB.GetConnection();
                string sql = "UPDATE " + DBSchemaTables.LoginHistory + " " +
                             "SET CourseCategory = @cat, CourseNumber = @crs " +
                             "WHERE HistoryID = @id";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cat", login.Course.LetterCode);
                cmd.Parameters.AddWithValue("@crs", login.Course.NumberCode);
                cmd.Parameters.AddWithValue("@id", login.ID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "saving course info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void LogStudentOff(Login login)
        {
            OleDbConnection conn = null;

            try
            {
                // remove milliseconds for access' sake 
                DateTime newTime = new DateTime(login.TimeOut.Year, login.TimeOut.Month, login.TimeOut.Day,
                    login.TimeOut.Hour, login.TimeOut.Minute, login.TimeOut.Second, login.TimeOut.Kind);

                conn = LoginDB.GetConnection();

                string sql = "UPDATE " + DBSchemaTables.LoginHistory + " " +
                             "SET LogOutTime = @time " +
                             "WHERE HistoryID = @id AND LogOutTime IS NULL";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", newTime);
                cmd.Parameters.AddWithValue("@id", login.ID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "saving logoff info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void LogStudentOff(int loginID, DateTime time)
        {
            OleDbConnection conn = null;

            try
            {
                // remove milliseconds for access' sake 
                DateTime newtime = new DateTime(time.Year, time.Month, time.Day,
                    time.Hour, time.Minute, time.Second, time.Kind);

                conn = LoginDB.GetConnection();

                string sql = "UPDATE " + DBSchemaTables.LoginHistory + " " +
                             "SET LogOutTime = @time " +
                             "WHERE HistoryID = @id AND LogOutTime IS NULL";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", newtime);
                cmd.Parameters.AddWithValue("@id", loginID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "saving logoff info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void UpdateLoginWithSurvey(Login login, int numStars = -1, int adPlaceID = -1)
        {
            OleDbConnection conn = null;

            try
            {
                conn = LoginDB.GetConnection();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                if (numStars > -1 && adPlaceID > -1)
                {
                    // both
                    cmd.CommandText =
                        "UPDATE " + DBSchemaTables.LoginHistory + " " + 
                        "SET SurveyRating = @star, SurveyHearAbout = @ad ";
                    cmd.Parameters.AddWithValue("@star", numStars);
                    cmd.Parameters.AddWithValue("@ad", adPlaceID);
                }
                else if (numStars > -1)
                {
                    // only stars
                    cmd.CommandText =
                        "UPDATE " + DBSchemaTables.LoginHistory + " " +
                        "SET SurveyRating = @star ";
                    cmd.Parameters.AddWithValue("@star", numStars);
                }
                else if (adPlaceID > -1)
                {
                    // only heard about
                    cmd.CommandText =
                        "UPDATE " + DBSchemaTables.LoginHistory + " " +
                        "SET SurveyHearAbout = @ad ";
                    cmd.Parameters.AddWithValue("@ad", adPlaceID);
                }
                else
                {
                    // what are we saving then?
                    return;
                }

                // works because this is last in statement
                cmd.CommandText += "WHERE HistoryID = @id";
                cmd.Parameters.AddWithValue("@id", login.ID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "saving survey info");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void LogAllStudentsOff(int campusID)
        {
            OleDbConnection conn = null;

            try
            {
                DateTime now = DateTime.Now;
                // remove milliseconds for access' sake 
                DateTime newtime = new DateTime(now.Year, now.Month, now.Day,
                    now.Hour, now.Minute, now.Second, now.Kind);

                conn = LoginDB.GetConnection();

                string sql = "UPDATE " + DBSchemaTables.LoginHistory + " " +
                             "SET LogOutTime = @time " +
                             "WHERE Campus = @id AND LogOutTime IS NULL";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", newtime);
                cmd.Parameters.AddWithValue("@id", campusID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "logging students off", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        #endregion

        #region AdminStuff

        public static List<string> GetAllAdminUsernames()
        {
            OleDbConnection conn = null;
            List<string> users = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT Username FROM Admin";
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                users = new List<string>();

                while (rdr.Read())
                {
                    users.Add(rdr["Username"].ToString());
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving authorized admin usernames", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return users;
        }

        public static bool IsValidAdminUsername(string user)
        {
            OleDbConnection conn = null;
            bool valid = false;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT COUNT(*) FROM Admin WHERE Username = @usr";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@usr", user);

                conn.Open();
                valid = ((int)cmd.ExecuteScalar() > 0);
            } 
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving authorized admin usernames", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return valid;
        }

        public static bool SimpleAdminLoginIsEnabled()
        {
            bool enabled = false;
            OleDbConnection conn = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT COUNT(*) " + 
                             "FROM " + DBSchemaMisc.OtherCredentials + " " +
                             "WHERE " + DBSchemaMisc.OtherCred_IsAdmin + " = Yes " +
                             "AND " + DBSchemaMisc.OtherCred_Enabled + " = Yes";
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                enabled = ((int)cmd.ExecuteScalar() > 0);
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving authorized admin usernames", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }


            return enabled;
        }

        public static string[] GetSimpleAdminLogin()
        {
            string[] unpw = null;
            OleDbConnection conn = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT " + DBSchemaMisc.OtherCred_Username + ", " + DBSchemaMisc.OtherCred_PW + " " +
                             "FROM " + DBSchemaMisc.OtherCredentials + " " +
                             "WHERE " + DBSchemaMisc.OtherCred_IsAdmin + " = Yes " +
                             "AND " + DBSchemaMisc.OtherCred_Enabled + " = Yes";
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                unpw = new string[2];

                while (rdr.Read())
                {
                    unpw[0] = rdr[DBSchemaMisc.OtherCred_Username].ToString();
                    unpw[1] = rdr["Password"].ToString();
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving authorized admin usernames", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            
            return unpw;
        }

        public static void UpdateSimpleAdminLogin(bool enable, string un = null, string pw = null)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();
                OleDbCommand cmd = new OleDbCommand("", conn);

                if (un == null || pw == null)
                {
                    cmd.CommandText = 
                        "UPDATE " + DBSchemaMisc.OtherCredentials + " " +
                        "SET " + DBSchemaMisc.OtherCred_Enabled + " = @enabl " +
                        "WHERE " + DBSchemaMisc.OtherCred_IsAdmin + " = Yes";
                    cmd.Parameters.AddWithValue("@enabl", enable);
                }
                else
                {
                    cmd.CommandText = 
                        "UPDATE " + DBSchemaMisc.OtherCredentials + " " +
                        "SET " + DBSchemaMisc.OtherCred_Username + " = @user, " +
                        DBSchemaMisc.OtherCred_PW + " = @pass " +
                        "WHERE " + DBSchemaMisc.OtherCred_IsAdmin + " = Yes";
                    cmd.Parameters.AddWithValue("@user", un);
                    cmd.Parameters.AddWithValue("@pass", pw);
                }
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving authorized admin usernames", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void AddAdmin(string username)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();

                string sql = "INSERT INTO Admin (Username) VALUES (@uname)";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uname", username);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "adding admin username", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void RemoveAdmin(string username)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();

                string sql = "DELETE FROM Admin WHERE Username = @uname";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uname", username);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "removing admin", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }


        public static void AddAreaOfAssistance(AreaOfAssistance area)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();

                string sql = "INSERT INTO " + DBSchemaTables.AreaOfAssistance + " " +
                             "(AreaName, DefaultLogoffMinutes) " +          
                             "VALUES (@name, @min)";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", area.Name);
                cmd.Parameters.AddWithValue("@min", area.AutoLogoffLength);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "adding Area/Category info", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void UpdateAreaOfAssistance(AreaOfAssistance area)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();

                string sql = "UPDATE " + DBSchemaTables.AreaOfAssistance + " " +
                             "SET AreaName = @name, DefaultLogoffMinutes = @min " +
                             "WHERE AreaID = @id";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", area.Name);
                cmd.Parameters.AddWithValue("@min", area.AutoLogoffLength);
                cmd.Parameters.AddWithValue("@id", area.ID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "saving Area/Category info", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }



        public static List<SpecialProgram> GetAllSpecialPrograms()
        {
            OleDbConnection conn = null;
            List<SpecialProgram> progs = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT ServiceID, ServiceName, Enabled " +
                             "FROM " + DBSchemaTables.StuServiceProgram; ;
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                progs = new List<SpecialProgram>();

                while (rdr.Read())
                {
                    SpecialProgram p = new SpecialProgram();
                    p.ID = Convert.ToInt16(rdr["ServiceID"]);
                    p.Name = rdr["ServiceName"].ToString();
                    p.IsEnabled = Convert.ToBoolean(rdr["Enabled"]);
                    progs.Add(p);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "could not load Student Services", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return progs;
        }

        public static void AddStudentService(string name)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();

                string sql = "INSERT INTO " + DBSchemaTables.StuServiceProgram + "(ServiceName) " +
                             "VALUES (@name)";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "adding Student Service info", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void UpdateStudentService(int id, string newName)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();

                string sql = "UPDATE " + DBSchemaTables.StuServiceProgram + " " +
                             "SET ServiceName = @name " +
                             "WHERE ServiceID = @id";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "saving Student Service info", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        
        public static void EnableDisableStudentService(int id, bool enable)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();

                string sql = "UPDATE " + DBSchemaTables.StuServiceProgram + " " +
                             "SET Enabled = @enabl " +
                             "WHERE ServiceID = @id";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@enabl", enable);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "disabling the Student Service", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }


        public static double GetTutoredHoursByStudent(int id)
        {
            OleDbConnection conn = null;
            double hours = 0;

            try
            {
                conn = LoginDB.GetConnection();

                string sql = "SELECT HoursThisWeek " + 
                             "FROM " + DBSchemaQueries.HoursTutoredThisWeek + " " +
                             "WHERE StudentID = @id";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                hours = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "disabling the Student Service", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return hours;
        }


        public static void AddSurveyWhereFound(string found)
        {
            OleDbConnection conn = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "INSERT INTO " + DBSchemaTables.SurveyAdReasons + " (HowHeard) " +
                             "VALUES (@val)";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@val", found);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "disabling the Student Service", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static void UpdateSurveyWhereFound(int id, string newname)
        {
            OleDbConnection conn = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "UPDATE " + DBSchemaTables.SurveyAdReasons + " " +
                             "SET HowHeard = @n WHERE ID = @id";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@n", newname);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "disabling the Student Service", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        #endregion

        #region Miscellaneous Data

        public static int GetSurveyFrequencyPercent()
        {
            OleDbConnection conn = null;
            int perc = -1;

            try
            {
                conn = LoginDB.GetConnection();

                string sql = "SELECT " + DBSchemaMisc.SurveyFrequencyCol + " FROM " + DBSchemaMisc.SurveyFrequency;
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                perc = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                //LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving survey frequency percnt", false);
                Logger.LogException(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return perc;
        }

        public static void SetSurveyFrequencyPercent(int perc)
        {
            OleDbConnection conn = null;

            try
            {
                conn = LoginDB.GetConnection();

                string sql = "UPDATE " + DBSchemaMisc.SurveyFrequency + " " + 
                             "SET " + DBSchemaMisc.SurveyFrequencyCol + " = @perc";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@perc", perc);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "saving survey frequency percent", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        
        public static string GetByePassword()
        {
            string bye = null;
            OleDbConnection conn = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT " + DBSchemaMisc.OtherCred_PW + " " +
                             "FROM " + DBSchemaMisc.OtherCredentials + " " +
                             "WHERE " + DBSchemaMisc.OtherCred_IsAdmin + " = No " +
                             "AND " + DBSchemaMisc.OtherCred_Enabled + " = Yes";
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                bye = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "retrieving password", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return bye;
        }

        public static void SetByePassword(string pw)
        {
            OleDbConnection conn = null;
            try
            {
                conn = LoginDB.GetConnection();
                string sql = "UPDATE " + DBSchemaMisc.OtherCredentials + " " +
                             "SET " + DBSchemaMisc.OtherCred_PW + " = @bye " +
                             "WHERE " + DBSchemaMisc.OtherCred_IsAdmin + " = No";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bye", pw);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "setting new password", false);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }


        #endregion

        #region Dropdown Fills

        // all areas of assistance
        public static List<AreaOfAssistance> GetAllAreasOfAssistance()
        {
            OleDbConnection conn = null;
            List<AreaOfAssistance> areas  = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT AreaID, AreaName, DefaultLogoffMinutes, SkipsCourseSelection " +
                             "FROM " + DBSchemaTables.AreaOfAssistance;
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                areas = new List<AreaOfAssistance>();

                while (rdr.Read())
                {
                    AreaOfAssistance a = new AreaOfAssistance();
                    a.ID = Convert.ToInt16(rdr["AreaID"]);
                    a.Name = rdr["AreaName"].ToString();
                    a.AutoLogoffLength = Convert.ToInt16(rdr["DefaultLogoffMinutes"]);
                    a.SkipsCourseSelection = Convert.ToBoolean(rdr["SkipsCourseSelection"]);
                    areas.Add(a);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "could not load Categories");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return areas;
        }
        
        // courses by category
        public static List<Course> GetCoursesByCategory(string category)
        {
            OleDbConnection conn = null;
            List<Course> courses = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT CourseNumber, CourseTitle " +
                             "FROM " + DBSchemaTables.Course + " " +
                             "WHERE CourseCategory = @cat"; 
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cat", category);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                courses = new List<Course>();

                while (rdr.Read())
                {
                    Course c = new Course();
                    c.LetterCode = category;
                    c.NumberCode = Convert.ToInt16(rdr["CourseNumber"]);
                    c.CourseTitle = rdr["CourseTitle"].ToString();
                    courses.Add(c);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "could not load Courses");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return courses;
        }


        // all programs of study
        public static Dictionary<string, string> GetAllProgramsOfStudy()
        {
            return GetAllOfTypeString(DBSchemaTables.ProgramOfStudy, "ProgramCode", "ProgramName", "programs of study");
        }

        // all countries
        public static Dictionary<int, string> GetAllCountries()
        {
            return GetAllOfType(DBSchemaTables.CountryOfOrigin, "CountryID", "CountryName", "countries");
        }

        // all languages
        public static Dictionary<int, string> GetAllLanguages()
        {
            return GetAllOfType(DBSchemaTables.NativeLanguage, "LanguageID", "LanguageName", "languages");
        }

        // all special programs
        public static List<SpecialProgram> GetAllEnabledSpecialPrograms()
        {
            OleDbConnection conn = null;
            List<SpecialProgram> progs = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT ServiceID, ServiceName " +
                             "FROM " + DBSchemaTables.StuServiceProgram + " " +
                             "WHERE Enabled = true";
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                progs = new List<SpecialProgram>();

                while (rdr.Read())
                {
                    SpecialProgram p = new SpecialProgram();
                    p.ID = Convert.ToInt16(rdr["ServiceID"]);
                    p.Name = rdr["ServiceName"].ToString();
                    p.IsEnabled = true;
                    progs.Add(p);
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "could not load Student Services");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return progs;
        }

        // all course categories
        public static Dictionary<string, string> GetAllCourseCategories()
        {
            return GetAllOfTypeString(DBSchemaTables.CourseCategory, "CategoryCode", "CategoryName", "course categories");
        }

        // all survey how did you hear about us reasons
        public static Dictionary<int, string> GetAllSurveyAdPlaces()
        {
            return GetAllOfType(DBSchemaTables.SurveyAdReasons, "ID", "HowHeard", "survey advertising places");
        }



        // all of type (integer id)
        private static Dictionary<int, string> GetAllOfType(string tableName, string idColumn, string nameColumn, string readableDescription)
        {
            OleDbConnection conn = null;
            Dictionary < int, string> items = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT " + idColumn + ", " + nameColumn + " " +
                             "FROM " + tableName;
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                items = new Dictionary<int, string>();

                while (rdr.Read())
                {
                    items.Add(Convert.ToInt16(rdr[idColumn]), rdr[nameColumn].ToString());
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "could not load " + readableDescription);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return items;
        }

        // all of type (string id)
        private static Dictionary<string, string> GetAllOfTypeString(string tableName, string idColumn, string nameColumn, string readableDescription)
        {
            OleDbConnection conn = null;
            Dictionary<string, string> items = null;

            try
            {
                conn = LoginDB.GetConnection();
                string sql = "SELECT " + idColumn + ", " + nameColumn + " " +
                             "FROM " + tableName;
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                items = new Dictionary<string, string>();

                while (rdr.Read())
                {
                    items.Add(rdr[idColumn].ToString(), rdr[nameColumn].ToString());
                }
            }
            catch (Exception ex) when (ex is OleDbException || ex is InvalidOperationException)
            {
                LoginDB.PrettifyAndLogException(ex, ErrorMessagePart + "could not load " + readableDescription);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return items;
        }

        #endregion
    }
}
