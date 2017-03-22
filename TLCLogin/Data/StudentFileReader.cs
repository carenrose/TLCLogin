using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLCLogin.Data
{
    public static class StudentFileReader
    {
        private static string filePath = 
            AppDomain.CurrentDomain.GetData("DataDirectory") + @"\registration.csv";
        
        // Column order: STUDENT,	ACTIVE PGM,	LAST NAME,	FIRST NAME
        
        public static List<Business.Student> GetAllStudents()
        {
            StreamReader sr = null;
            List<Business.Student> students = null;

            try
            {   
                sr = new StreamReader(filePath);
                students = new List<Business.Student>();

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineItems = line.Split(',');
                    Business.Student s = new Business.Student();
                    s.StudentID = Convert.ToInt32(lineItems[0]);
                    s.ProgramOfStudy = ParseProgramOfStudy(lineItems[1]);
                    s.LastName = lineItems[2];
                    s.FirstName = lineItems[3];

                    students.Add(s);
                }
            }
            catch (IOException e)
            {
                LoginDB.PrettifyAndLogException(e, "getting data from the registration file");
            }
            finally
            {
                if (sr != null) sr.Close();
            }

            return students;
        }

        public static Business.Student GetStudentByID(int id)
        {
            StreamReader sr = null;
            Business.Student student = null;

            try
            {
                FileStream fs = new FileStream(filePath,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                sr = new StreamReader(fs);

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineItems = line.Split(',');
                    int lineID = Convert.ToInt32(lineItems[0]);

                    if (lineID == id)
                    {
                        student = new Business.Student();
                        student.StudentID = lineID;
                        student.ProgramOfStudy = ParseProgramOfStudy(lineItems[1]);
                        student.LastName = lineItems[2];
                        student.FirstName = lineItems[3];

                        break;
                    }
                }
            }
            catch (IOException e)
            {
                LoginDB.PrettifyAndLogException(e, "getting data from the registration file");
            }
            finally
            {
                if (sr != null) sr.Close();
            }

            return student;
        }


        /*
         * in db as    in file as
            DESL.FARM	DESL.FARM.AAS
            DESL.TRK	DESL.TRK.AAS
            ELEC.ELM	ELEC.ELM.AAS
            ELEC.ELN	ELEC.ELN.AAS
            ELEC.ELT	ELEC.ELT.AAS
            PRE.HEALTH	PRE.HEALTH
         */
        private static string ParseProgramOfStudy(string column)
        {
            string prog = "";
            string[] split = column.Split('.');

            if (split.Length == 3)
            {
                switch (split[0] + "." + split[1])
                {
                    case "DESL.FARM":
                    case "DESL.TRK":
                    case "ELEC.ELM":
                    case "ELEC.ELN":
                    case "ELEC.ELT":
                        prog = split[0] + "." + split[1];
                        break;
                }
            }
            else if (column == "PRE.HEALTH")
            {
                prog = column;
            }

            // if it wasn't assigned
            if(prog == "" ) prog = split[0];
            
            // check if in database
            if (! TLCLoginDA.GetAllProgramsOfStudy().ContainsKey(prog))
                prog = "ZZZ";

            return prog;
        }
    }
}
