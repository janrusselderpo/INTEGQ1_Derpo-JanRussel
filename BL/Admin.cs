using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class Admin
    {
        static public string AddStudent(string studentnumber, string firstname, string lastname, string courseandsection, int age, string gender)
        {
            var val = SQLData.AddStudent(studentnumber, firstname, lastname, courseandsection, age, gender);
            return val;
        }
        static public bool UpdateStudentTable(int ID, string studentnumber, string firstname, string lastname, string courseandsection, int age, string gender)
        {
            UpdateUserStudent(ID, studentnumber);
            UpdateEmailStudent(ID, studentnumber);
            UpdateStudNumViolation(ID, studentnumber);
            if(SQLData.UpdateStudent(ID, studentnumber, firstname, lastname, courseandsection, age, gender))
            {
                return true;
            }else { return false; }    
        }
        static public bool DeleteStudentFromTable(int ID)
        {
            DeleteUserStudent(ID);
            if (SQLData.DeleteStudent(ID))
            {
                return true;
            }    
            else { return false; }
        }

        static public bool AddTeacher(string teacherID, string firstname, string lastname, string email, int age, string gender)
        {
            if (SQLData.AddTeacher(teacherID, firstname, lastname, email, age, gender))
            {
                return true;
            }
            else { return false; }
            ;
            
        }
        static public bool UpdateTeacherTable(int ID, string teacherID, string firstname, string lastname, string email, int age, string gender)
        {
            UpdateUserTeacher(ID, teacherID);
            UpdateEmailTeacher(ID, teacherID);
            
            if (SQLData.UpdateTeacher(ID, teacherID, firstname, lastname, email, age, gender))
            {
                return true;
            }
            else { return false; }
        }
        static public bool DeleteTeacherFromTable(int ID)
        {
            DeleteUserTeacher(ID);
            if(SQLData.DeleteTeacher(ID))
            {
                return true;
            }
            else { return false; }
        }

        static public void UpdateUserStudent(int ID, string newStudNum)
        {
            SQLData.GetStudentNumberUsingID(ID);
            var StudNum = SQLData.stud.FirstOrDefault(e => e.ID == ID).StudentNumber;

            switch (SQLData.CheckUserName(StudNum))
            {
                case true:
                    SQLData.UpdateUser(StudNum, newStudNum);
                    break;
                case false:
                    break;
            }
        }
        static public void DeleteUserStudent(int ID)
        {
            SQLData.GetStudentNumberUsingID(ID);
            var StudNum = SQLData.stud.FirstOrDefault(e => e.ID == ID).StudentNumber;

            SQLData.DeleteUser(StudNum);
        }

        static public void UpdateUserTeacher(int ID, string newTeacherID)
        {
            SQLData.GetTeacherIDUsingID(ID);
            var TeacherID = SQLData.teach.FirstOrDefault(e => e.ID == ID).TeacherID;

            switch (SQLData.CheckUserName(TeacherID))
            {
                case true:
                    SQLData.UpdateUser(TeacherID, newTeacherID);
                    break;
                case false:
                    break;
            }
        }
        static public void DeleteUserTeacher(int ID)
        {
            SQLData.GetTeacherIDUsingID(ID);
            var TeacherID = SQLData.teach.FirstOrDefault(e => e.ID == ID).TeacherID;

            SQLData.DeleteUser(TeacherID);

        }

        static public void UpdateEmailStudent(int ID, string newStudNum)
        {
            var StudNum = SQLData.stud.FirstOrDefault(e => e.ID == ID).StudentNumber;
            if(SQLData.AuthenticateForStudentNumberEmail(StudNum) == true)
            {
                SQLData.UpdateEmailIDStudent(StudNum, newStudNum);
            }
            if(SQLData.AuthenticateStudentEmail(StudNum) == true)
            {
                SQLData.UpdateIDStudentFromTeacherEmail(StudNum, newStudNum);
            }
            else
            {
                
            }

        }
        static public void UpdateEmailTeacher(int ID, string newTeacherID)
        {
            SQLData.GetTeacherIDUsingID(ID);
            var TeacherID = SQLData.teach.FirstOrDefault(e => e.ID == ID).TeacherID;

            if(SQLData.AuthenticateForTeacherIDEmail(TeacherID) == true)
            {
                SQLData.UpdateIDTeacherFromStudentEmail(TeacherID, newTeacherID);
            }
            if(SQLData.AuthenticateTeacherEmail(TeacherID) == true)
            {
                SQLData.UpdateEmailIDTeacher(TeacherID, newTeacherID);
            }
            else
            {

            }
            
        }
        static public void UpdateStudNumViolation(int ID, string newStudNum)
        {
            var StudNum = SQLData.stud.FirstOrDefault(e => e.ID == ID).StudentNumber;

            switch (SQLData.CheckStudent(StudNum))
            {
                case true:
                    SQLData.UpdateStudNumInViolations(StudNum, newStudNum);
                    break;
                case false:
                    break;
            }
        }
        static public bool VerifyTeacherIDfromTeachersEmail(string TeachersID)
        {
            return SQLData.AuthenticateTeacherEmail(TeachersID);
        }
    }
}
