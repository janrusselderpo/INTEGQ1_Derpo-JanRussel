using System;
using Common;
using DL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace BL
{
    public class Faculty
    {
        static public List<TeacherNames> myTeacherID = new List<TeacherNames>();
        static public List<String> myID = new List<string>();
        static public bool IssueViolation(int ID, string violation, int type)
        {
            Violation val = new Violation();
            val = (Violation)type;
            string violationtype = Convert.ToString(val);
            string studentNum = GetStudentNum(ID);
            int offensenum = NoOfOffense(studentNum);
            DateTime date = DateTime.Now;

            if (offensenum < 3)
            { 
                SQLData.IssueOffenseSQL(studentNum, violation, violationtype, offensenum, date);
                SQLData.NoViolations.Clear();
                return true;
            }
            if (offensenum == 3)
            {
                SQLData.IssueOffenseSQL(studentNum, violation, violationtype, offensenum, date);
                SQLData.NoViolations.Clear();
                return false;
            }
            else
            {
                return false;
            }

        }
        static public bool DeleteViolation(int ID)
        {
            SQLData.DeleteStudentViolation(ID);
            string studentNum = SQLData.GetStudNumFromViolation(ID);
            if (studentNum == null)
            {
                return true;
            }
            else 
            {
                return ViolationDeleteThenMinus(studentNum);
            }
        }
        static public bool UpdateStudentViolation(int ID, string violation, string type)
        {
            if (SQLData.UpdateViolation(ID, violation, type))
            {
                return true;
            }
            else { return false; }
        }

        static public string GetStudentNum(int ID)
        {
            SQLData.GetStudentNumberUsingID(ID);
            string StudNum = SQLData.stud.FirstOrDefault(e => e.ID == ID).StudentNumber;

            return StudNum;

            throw new InvalidOperationException("No Value");
        }
        static public string GetTeacherID(int ID)
        {
            SQLData.GetTeacherIDUsingID(ID);
            var teacherID = SQLData.teach.FirstOrDefault(e => e.ID == ID).TeacherID;

            return teacherID.ToString();

            throw new InvalidOperationException("No Value");
        }

        static public int NoOfOffense(string studentnumber)
        {
            int noofoffense = SQLData.NumberOfOffense(studentnumber);
            return noofoffense;
        }


        static public bool SendEmailToTeacher(string teacherID, string email, string studentnumber)
        {
            return SQLData.SendEmailTeacher(teacherID, email, studentnumber);
        }
        static public bool SendEmailToStudent(string teacherID, string email, string studentnumber)
        {
            return SQLData.SendEmailStudent(teacherID, email, studentnumber);
        }

        static public bool AutoWarningSend(int ID, string email, string teacherID)
        {

            SQLData.GetStudentNumberUsingID(ID);
            string studentNum = GetStudentNum(ID);

            return SQLData.SendEmailStudent(teacherID, email, studentNum);
        }
        static public string GetEmails(string teacherID)
        {
            return SQLData.TeacherEmails(teacherID);
        }

        static public string GetTeachersTable()
        {
            return SQLData.TeachersTable();
        }
        static public string GetStudentsTable()
        {
            return SQLData.StudentsTable();
        }
        static public string GetStudentViolationsTable()
        {
            return SQLData.StudentViolationsTable();
        }

        static public bool GetStudentAuthenticate(int ID)
        {
            return SQLData.AuthenticateStudent(ID);
        }
        static public bool GetTeacherAuthenticate(int ID)
        {
            return SQLData.AuthenticateTeacher(ID);
        }
        static public bool GetAuthenticateNoOfViolations(int ID)
        {
            return SQLData.AuthenticateNoOfViolations(ID);
        }

        static public bool GetViolationsCount(string studNumber)
        {
            if (SQLData.ViolationsCount(studNumber) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public string GetMyID()
        {
            var value = myID.FirstOrDefault();
            return value;
        }
        static public void StoreMyID(string teacherID)
        {
            myID.Add(teacherID);
        }
        static public bool ViolationDeleteThenMinus(string studentNumber)
        {

            int num = 1;
            int num2 = 2;
            int num3 = 3;

            if (SQLData.NoViolations.Contains(1) && SQLData.NoViolations.Contains(3) == true)
            {
                SQLData.NoViolations.Clear();
                return SQLData.UpdateViolationAfterDeletion(num3, num2);
            }
            if (SQLData.NoViolations.Contains(2) && SQLData.NoViolations.Contains(3) == true)
            {
                SQLData.NoViolations.Clear();
                SQLData.UpdateViolationAfterDeletion(num3, num2);
                return SQLData.UpdateViolationAfterDeletion(num2, num);
            }else if(SQLData.NoViolations.Count() == 0)
            {
                return false;
            }
            else
            {
                return false;
            }

            throw new InvalidOperationException("No Value");
        }
        public enum Violation
        {
            MajorOffense,
            MinorOffense
        }
    }
}
