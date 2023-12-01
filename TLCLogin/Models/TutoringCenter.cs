//  TutoringLogin
//  Author: Brianna Williams <williams.brianna.k.0607@gmail.com>
//  Copyright (c) 2020 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace TLCLogin.Models
{
    public class TutoringCenter
    {
        public int    CampusID   { get; set; }
        public string CampusName { get; set; }

        public UserLoginModel CurrentLogin { get; set; }
        public List<UserLoginModel> LoggedInSessions { get; set; }

        public TutoringCenter(int campus)
        {
            try
            {
                // Dictionary<int, string> campuses = Data.TLCLoginDA.GetCampuses();
                // 

                // get all current logins from database
                // assign to Center
            }
            catch (Exception ex)
            {

            }
        }

        // Step 1
        // LoginValid

        // ValidateStudentID

        // CreateStudent    StudentExistsInDB

        // Step 2
        // UpdateStudentInfo

        // Step 3
        // CreateLogin

        // Step 4
        // UpdateLoginCourse

        // Step "Z"
        // LogOff

        // Survey

        // CloseCenter

    }
}
