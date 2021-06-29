using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BL;

namespace WebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public bool Login(string Username, string Password)
        {
            return BL.User.Authenticate(Username, Password);
        }
        [WebMethod]
        public string AddStudent(string StudentNumber, string FirstName, string LastName, string CourseAndSection, int Age, string Gender)
        {
            return BL.Admin.AddStudent(StudentNumber, FirstName, LastName, CourseAndSection, Age, Gender);
        }
        [WebMethod]
        public bool DeleteStudent(int ID)
        {
            return BL.Admin.DeleteStudentFromTable(ID);
        }
        [WebMethod]
        public bool UpdateStudent(int ID, string StudentNumber, string FirstName, string LastName, string CourseAndSection, int Age, string Gender)
        {
            return BL.Admin.UpdateStudentTable(ID, StudentNumber, FirstName, LastName, CourseAndSection, Age, Gender);
        }
        [WebMethod]
        public bool AddTeacher(string teacherID, string firstname, string lastname, string email, int age, string gender)
        {
            return BL.Admin.AddTeacher(teacherID, firstname, lastname, email, age, gender);
        }
        [WebMethod]
        public bool DeleteTeacher(int ID)
        {
            return BL.Admin.DeleteTeacherFromTable(ID);
        }
        [WebMethod]
        public bool UpdateTeacher(int ID, string teacherID, string firstname, string lastname, string email, int age, string gender)
        {
            return BL.Admin.UpdateTeacherTable(ID, teacherID, firstname, lastname, email, age, gender);
        }
        [WebMethod]
        public bool IssueOffense(int ID, string violation, int type)
        {
            return BL.Faculty.IssueViolation(ID, violation, type);
        }
        [WebMethod]
        public bool UpdateStudentViolation(int ID, string violation, string type)
        {
            return BL.Faculty.UpdateStudentViolation(ID, violation, type);
        }
        [WebMethod]
        public bool DeleteStudentViolation(int ID)
        {
            return BL.Faculty.DeleteViolation(ID);
        }
        [WebMethod]
        public string ViewStudentTable()
        {
            return BL.Faculty.GetStudentsTable();
        }
        [WebMethod]
        public string ViewTeacherTable()
        {
            return BL.Faculty.GetTeachersTable();
        }
        [WebMethod]
        public string ViewOffenses()
        {
            return BL.Faculty.GetStudentViolationsTable();
        }
    }

}
