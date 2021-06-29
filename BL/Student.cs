using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DL;

namespace BL
{
    public class Student
    {
        static public List<StudentNames> myStudentNumber = new List<StudentNames>();

        static public bool SendEmailToTeacher(string studentnumber, string email, string teacherID)
        {
            return SQLData.SendEmailTeacher(studentnumber, email, teacherID);
        }
        static public string GetEmails(string studentnumber)
        {
            return SQLData.StudentEmails(studentnumber);
        }

        static public string GetStudentsInformation(string studentnumber)
        {
            return SQLData.StudentInformation(studentnumber);
        }
        static public string GetStudentViolations(string studentnumber)
        {
            return SQLData.StudentViolations(studentnumber);
        }

        static public string GetTeachersTableFiltered()
        {
            return SQLData.TeachersTableFiltered();
        }

        static public string GetMyNumber()
        {
            var value = myStudentNumber.FirstOrDefault(e => e.ID == 1).StudentNumber;
            return value;
        }
        static public void StoreMyNumber(string studentnumber)
        {
            myStudentNumber.Add(new StudentNames
            {
                ID = 1,
                StudentNumber = studentnumber
            });
        }
    }
}
