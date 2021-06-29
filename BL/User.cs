using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class User
    {
        static public bool Authenticate(string username, string password)
        {
            var Authentication = SQLData.Auth(username, password);
            if (Authentication == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public string GetUserRole(string username, string password)
        {
            string userrole = SQLData.UserRole(username, password);

            return userrole;
        }
        static public bool StudentAuth(string studentNumber)
        {
            if (!String.IsNullOrWhiteSpace(studentNumber) || !studentNumber.Contains(" "))
            {
                string studnum = SQLData.GetStudentNumber(studentNumber);
                if (studnum == studentNumber)
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        static public bool TeacherAuth(string teacherID)
        {
            if (!String.IsNullOrWhiteSpace(teacherID) || !teacherID.Contains(" "))
            {
                string studnum = SQLData.GetTeacherID(teacherID);
                if (studnum == teacherID)
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        static public bool DupeCheck(string username, string password)
        {
            var duplicatecheck = SQLData.Auth(username, password);
            if (duplicatecheck == true)
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(password) || password.Contains(" "))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static public bool UserIsExists(string username)
        {
            if (SQLData.GetStudentNumber(username) == username || SQLData.GetTeacherID(username) == username)
            {
                return true;
            }
            else { return false; }
        }
        static public bool Register(string username, string password, int userrole)
        {
            UserRole role = new UserRole();
            role = (UserRole)userrole;
            if (DupeCheck(username, password) && UserIsExists(username) == true)
            {
                SQLData.Register(username, password, role.ToString());
                return true;
            }
            else { return false; }
            
        }

        public enum UserRole
        {
            Student,
            Faculty,
            Admin
        }
    }
}
