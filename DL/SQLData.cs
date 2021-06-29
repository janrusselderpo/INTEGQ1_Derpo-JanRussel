using System;
using Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace DL
{
    public class SQLData
    {
        static string conString
        = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StudentViolationRecords;Integrated Security=True";
        static SqlConnection con = new SqlConnection(conString);
        static SqlCommand cmd;
        static public List<StudentNames> stud = new List<StudentNames>();
        static public List<TeacherNames> teach = new List<TeacherNames>();
        static public List<int> NoViolations = new List<int>();
        static public void GetStudentNumberUsingID(int id)
        {
            var query = "SELECT StudentNumber FROM StudentsTable WHERE ID = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    stud.Add(new StudentNames
                    {
                        ID = id,
                        StudentNumber = Reader["StudentNumber"].ToString()
                    });
                }
            }
            con.Close();
        }
        static public void GetTeacherIDUsingID(int id)
        {
            var query = "SELECT TeacherID FROM TeachersTable WHERE ID = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    teach.Add(new TeacherNames
                    {
                        ID = id,
                        TeacherID = Reader["TeacherID"].ToString()
                    });
                }
            }
            con.Close();
        }

        static public string AddStudent(string studentnumber, string firstname, string lastname, string courseandsection, int age, string gender)
        {
            string query = "INSERT INTO StudentsTable (StudentNumber, FirstName, LastName, CourseAndSection, Age, Gender) VALUES(@studentnumber, @firstname, @lastname, @courseANDsection, @age, @gender)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@studentNumber", studentnumber);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@courseANDsection", courseandsection);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return studentnumber + " " + firstname + " " + lastname + " " + courseandsection + " " + age + " " + gender;
        }
        static public bool DeleteStudent(int ID)
        {
            string query = "DELETE FROM StudentsTable where ID = @ID ";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        static public bool UpdateStudent(int ID, string studentnumber, string firstname, string lastname, string courseandsection, int age, string gender)
        {
            string query = "UPDATE StudentsTable Set StudentNumber = @StudentNumber, FirstName = @FirstName , LastName = @LastName , CourseAndSection = @CourseAndSection, Age = @Age, Gender = @Gender Where ID = @ID";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@studentNumber", studentnumber);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@courseANDsection", courseandsection);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }

        static public bool IssueOffenseSQL(string studentnumber, string violation, string type, int offenseNo, DateTime dateIssued)
        {
            string query = "INSERT INTO StudentViolations (StudentNumber, Violation, Type, NumberOfOffenses, DateIssued) VALUES(@studentnumber, @violation, @type, @offense, @dateIssued)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@studentnumber", studentnumber);
            cmd.Parameters.AddWithValue("@violation", violation);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@offense", offenseNo);
            cmd.Parameters.AddWithValue("@dateIssued", dateIssued);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        static public bool UpdateViolation(int ID, string violation, string type)
        {
            string query = "UPDATE StudentViolations Set Violation = @violation, Type = @type Where ID = @ID";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@violation", violation);
            cmd.Parameters.AddWithValue("@type", type);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        static public bool DeleteStudentViolation(int ID)
        {
            string query = "DELETE FROM StudentViolations WHERE ID = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        static public bool UpdateViolationAfterDeletion(int oldNum, int newNum)
        {
            string query = "UPDATE StudentViolations Set NumberOfOffenses = @NewNum Where NumberOfOffenses = @OldNum";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@OldNum", oldNum);
            cmd.Parameters.AddWithValue("@NewNum", newNum);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }

        static public bool AddTeacher(string teacherID, string firstname, string lastname, string email, int age, string gender)
        {
            string query = "INSERT INTO TeachersTable (TeacherID, FirstName, LastName, Email, Age, Gender) VALUES(@TeacherID, @FirstName, @LastName, @Email, @Age, @Gender)";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@TeacherID", teacherID);
            cmd.Parameters.AddWithValue("@FirstName", firstname);
            cmd.Parameters.AddWithValue("@Lastname", lastname);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@Gender", gender);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        static public bool UpdateTeacher(int ID, string teacherID, string firstname, string lastname, string email, int age, string gender)
        {
            string query = "UPDATE TeachersTable Set TeacherID = @teacherID, FirstName = @FirstName , LastName = @LastName , Email = @Email, Age = @Age, Gender = @Gender Where ID = @ID";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@teacherID", teacherID);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        static public bool DeleteTeacher(int ID)
        {
            string query = "DELETE FROM TeachersTable where ID = @ID ";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }

        static public bool SendEmailStudent(string teacherID, string email, string studentnumber)
        {
            string query = "INSERT INTO StudentEmail (Sender, Email, Receiver) VALUES(@sender, @email, @receiver)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@sender", teacherID);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@receiver", studentnumber);
            con.Open();
            int check = cmd.ExecuteNonQuery();
            con.Close();

            if (check == 0)
            {
                return false;
            }
            else { return true; }
        }
        static public bool SendEmailTeacher(string studentnumber, string email, string teacherID)
        {
            string query = "INSERT INTO TeachersEmail (Sender, Email, Receiver) VALUES(@studentnumber, @email, @receiver)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@studentNumber", studentnumber);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@receiver", teacherID);
            con.Open();
            int check = cmd.ExecuteNonQuery();
            con.Close();

            if (check == 0)
            {
                return false;
            }
            else { return true; }
        }
        static public string StudentEmails(string studentnumber)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT Sender, Email FROM StudentEmail WHERE Receiver = @StudentNumber", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@StudentNumber", studentnumber);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }
        static public string TeacherEmails(string teacherID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT Sender, Email FROM TeachersEmail WHERE Receiver = @TeacherID", con))
            {   
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }

        static public bool Auth(string username, string password)
        {
            var query = "SELECT Username, Password FROM Users WHERE Username = @username AND Password = @password";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public string UserRole(string username, string password)
        {

            var query = "SELECT UserRole FROM Users WHERE Username = @username and Password = @password";

            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                string value = (string)cmd.ExecuteScalar();
                con.Close();
                return value;
            }
        }
        static public string GetStudentNumber(string studentNumber)
        {
            var query = "SELECT StudentNumber FROM StudentsTable WHERE StudentNumber = @studentNumber";

            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentNumber", studentNumber);
                con.Open();
                string value = (string)cmd.ExecuteScalar();
                con.Close();
                return value;
            }
        }
        static public string GetTeacherID(string teacherID)
        {
            var query = "SELECT TeacherID FROM TeachersTable WHERE TeacherID = @teacherID";

            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@teacherID", teacherID);
                con.Open();
                string value = (string)cmd.ExecuteScalar();
                con.Close();
                return value;
            }
        }
        static public string GetStudNumFromViolation(int ID)
        {
            var query = "SELECT StudentNumber FROM StudentViolations WHERE ID = @ID";

            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                string value = (string)cmd.ExecuteScalar();
                con.Close();
                return value;

            }
        }
        static public void Register(string username, string password, string userrole)
        {
            string query = "INSERT INTO Users (Username, Password, UserRole) VALUES(@username, @password, @userrole)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@userrole", userrole);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        static public bool ViolationsCount(string studentNumber)
        {
            using (cmd = new SqlCommand("SELECT COUNT(StudentNumber) FROM StudentViolations WHERE StudentNumber = @StudentNumber", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@StudentNumber", studentNumber);
                int Count = (int)cmd.ExecuteScalar();
                con.Close();

                if (Count == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static public void GetViolationsNo(string studentNumber)
        {
            var query = "SELECT NumberOfOffenses FROM StudentViolations WHERE StudentNumber = @studentNumber";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@studentNumber", studentNumber);
            con.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    int vio = Convert.ToInt32(Reader["StudentNumber"]);
                    NoViolations.Add(vio);
                }
            }
            con.Close();
        }

        static public bool AuthenticateStudent(int ID)
        {
            var query = "SELECT StudentNumber FROM StudentsTable WHERE ID = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool AuthenticateTeacher(int ID)
        {
            var query = "SELECT TeacherID FROM TeachersTable WHERE ID = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool AuthenticateNoOfViolations(int ID)
        {
            var query = "SELECT StudentNumber FROM StudentViolations WHERE ID = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool AuthenticateTeacherEmail(string TeacherID)
        {
            var query = "SELECT Sender FROM TeachersEmail WHERE Sender= @teacherID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@teacherID", TeacherID);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool AuthenticateStudentEmail(string studentNumber)
        {
            var query = "SELECT Sender FROM StudentEmail WHERE Sender= @studentNumber";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@studentNumber", studentNumber);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool AuthenticateForTeacherIDEmail(string TeacherID)
        {
            var query = "SELECT Receiver FROM StudentEmail WHERE Receiver= @TeacherID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TeacherID", TeacherID);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool AuthenticateForStudentNumberEmail(string TeacherID)
        {
            var query = "SELECT Receiver FROM TeachersEmail WHERE Receiver= @TeacherID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TeacherID", TeacherID);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public int NumberOfOffense(string studentNumber)
        {
            using (cmd = new SqlCommand("SELECT COUNT(StudentNumber) FROM StudentViolations WHERE StudentNumber = @StudentNumber", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("StudentNumber", studentNumber);
                int Count = (int)cmd.ExecuteScalar();
                con.Close();

                return Count + 1;

            }
        }

        static public void UpdateUser(string olduser, string newuser)
        {
            string query = "UPDATE Users Set Username = @newUsername Where Username = @User";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@newUsername", newuser);
            cmd.Parameters.AddWithValue("@User", olduser);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        static public void DeleteUser(string username)
        {
            string query = "DELETE FROM Users WHERE Username = @username";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        static public void UpdateEmailIDTeacher(string olduser, string newuser)
        {
            string query = "UPDATE TeacherEmail Set Sender = @newUsername Where Sender = @User";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@newUsername", newuser);
            cmd.Parameters.AddWithValue("@User", olduser);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        static public void UpdateIDStudentFromTeacherEmail(string olduser, string newuser)
        {
            string query = "UPDATE TeacherEmail Set Receiver = @newUsername Where Receiver = @User";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@newUsername", newuser);
            cmd.Parameters.AddWithValue("@User", olduser);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        static public void UpdateEmailIDStudent(string olduser, string newuser)
        {
            string query = "UPDATE StudentEmail Set Sender = @newUsername Where Sender = @User";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@newUsername", newuser);
            cmd.Parameters.AddWithValue("@User", olduser);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        static public void UpdateIDTeacherFromStudentEmail(string olduser, string newuser)
        {
            string query = "UPDATE StudentEmail Set Receiver = @newUsername Where Receiver = @User";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@newUsername", newuser);
            cmd.Parameters.AddWithValue("@User", olduser);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        static public void UpdateStudNumInViolations(string olduser, string newuser)
        {
            string query = "UPDATE StudentViolations Set StudentNumber = @newUsername Where StudentNumber = @User";
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@newUsername", newuser);
            cmd.Parameters.AddWithValue("@User", olduser);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        static public bool CheckUserName(string username)
        {
            var query = "SELECT Username FROM Users WHERE Username = @username";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool CheckStudent(string username)
        {
            var query = "SELECT StudentNumber FROM StudentsTable WHERE StudentNumber = @username";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            int count = ds.Tables[0].Rows.Count;
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public string TeachersTable()
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM TeachersTable", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }
        static public string StudentsTable()
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTable", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }
        static public string TeachersTableFiltered()
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT ID, FirstName, LastName FROM TeachersTable", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }
        static public string StudentInformation(string studentnumber)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTable WHERE StudentNumber = @StudentNumber", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@StudentNumber", studentnumber);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }
        static public string StudentViolations(string studentnumber)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT Violation FROM StudentViolations WHERE StudentNumber = @StudentNumber", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@StudentNumber", studentnumber);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }
        static public string StudentViolationsTable()
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM StudentViolations", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            return DumpDataTable(dt);
        }
        public static string DumpDataTable(DataTable table)
        {
            string data = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (null != table && null != table.Rows)
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        sb.Append(item);
                        sb.Append('|');
                    }
                    sb.AppendLine();
                }
                data = sb.ToString();
            }
            return data;
        }
    }
}
