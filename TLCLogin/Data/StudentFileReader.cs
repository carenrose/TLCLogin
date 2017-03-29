/* Copyright (C) 2017 Brianna Williams
 *
 * This file is part of TLC Login.
 * 
 * TLC Login is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * TLC Login is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with TLC Login.  If not, see <http://www.gnu.org/licenses/>.
 */

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
        

        public static bool RegistrationFileExists()
        {
            return File.Exists(filePath);
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

                    int lineID;
                    if (Int32.TryParse(lineItems[0], out lineID))
                    {
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
