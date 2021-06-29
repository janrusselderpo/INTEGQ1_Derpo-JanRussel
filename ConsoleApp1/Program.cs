using System;
using BL;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Faculty.myID.Clear();
            Student.myStudentNumber.Clear();
            start();
        }
        static void start()
        {
            Console.Clear();
            Console.WriteLine("Student Violation Records Management System");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1 - Login\n" +
                              "2 - Register");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    login();
                    break;

                case "2":
                    register();
                    break;
                default:
                    start();
                    break;
            }
        }
        static void login()
        {
            ContinueOPT();
            Console.Clear();
            Console.WriteLine("Student Violation Records Management System");
            Console.WriteLine("[---------------------Login--------------------]\n");
            Console.Write("Username: ");
            string user = Console.ReadLine();
            Console.Write("Password: ");
            string pass = Console.ReadLine();

            bool success = User.Authenticate(user, pass);
            if (success == true)
            {
                Console.Clear();
                string userrole = User.GetUserRole(user, pass);
                Console.WriteLine("\nSuccessfully logged in as " + userrole);
                Console.WriteLine("Press any key to continue");
                var key = Console.ReadKey();
                UserRole(user, userrole);
            }
            else
            {
                Console.WriteLine("\nInvalid Username or Password");
                var con = Console.ReadKey();
                login();
            }
        }

        static void register()
        {
            Console.Clear();
            Console.WriteLine("Student Violation Records Management System");
            Console.WriteLine("[---------------------Register--------------------]\n");
            Console.WriteLine("Register as: \n" +
                              "1 - Student\n" +
                              "2 - Teacher\n" +
                              "0 - Return to Login\n");
            Console.Write("Option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    StudentRegister();
                    break;
                case "2":
                    TeacherRegister();
                    break;
                case "0":
                    login();
                    break;
                default:
                    break;
            }
        }
        static void StudentRegister()
        {
            Console.Clear();
            Console.WriteLine("Username must be your Student Number (ex. 2010-00069-BN-0)");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            if (User.StudentAuth(username) == true)
            {
                Console.Write("Password: ");
                string password = Console.ReadLine();

                bool register = User.Register(username, password, 0);
                if (register == false)
                {
                    Console.WriteLine("Password must not contain space");
                    Console.Write("Press any key to continue");
                    var key = Console.ReadKey();
                    start();
                }
                else
                {
                    Console.WriteLine("Register successful");
                    Console.Write("Press any key to login");
                    var key = Console.ReadKey();
                    login();
                }
            }
            else
            {
                Console.WriteLine("\nInvalid Student Number");
                Console.Write("Press any key to continue");
                var key = Console.ReadKey();
                start();
            }
        }
        static void TeacherRegister()
        {
            Console.Clear();
            Console.WriteLine("Username must be your TeacherID (ex. 2010-00069-BN-0)");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            if (User.TeacherAuth(username) == true)
            {
                Console.Write("Password: ");
                string password = Console.ReadLine();

                bool register = User.Register(username, password, 1);
                if (register == false)
                {
                    Console.WriteLine("Password must not contain space");
                    Console.Write("Press any key to continue");
                    login();
                }
                else
                {
                    Console.WriteLine("Register successful");
                    Console.Write("Press any key to continue");
                    var key = Console.ReadKey();
                    start();
                }
            }
            else
            {
                Console.WriteLine("\nInvalid Teacher ID");
                Console.Write("Press any key to continue");
                var key = Console.ReadKey();
                start();
            }
        }
        static void UserRole(string user, string userrole)
        {
            if (userrole == "Admin")
            {
                AdminMenu();
            }
            if (userrole == "Student")
            {
                Student.StoreMyNumber(user);
                StudentMenu();
            }
            if (userrole == "Faculty")
            {
                Faculty.StoreMyID(user);
                FacultyMenu();
            }

        }

        static void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("Student Violation Records Management System");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("  1 - Add Student to Databas\n" +
                              "  2 - Update Student from Database\n" +
                              "  3 - Delete Student from Database\n" +
                              "  4 - Add Teacher to Database\n" +
                              "  5 - Update Teacher from Database\n" +
                              "  6 - Delete Teacher from Database\n" +
                              "  7 - View Student Table\n" +
                              "  8 - View Teachers Table\n" +
                              "  0 - Log out");
            Console.Write("Select an Option : ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    UpdateStudent();
                    break;
                case "3":
                    DeleteStudent();
                    break;
                case "4":
                    AddTeacher();
                    break;
                case "5":
                    UpdateTeacher();
                    break;
                case "6":
                    DeleteTeacher();
                    break;
                case "7":
                    Console.Clear();
                    StudentsTable();
                    Console.WriteLine("Press any key to exit");
                    var key1 = Console.ReadKey();
                    AdminMenu();
                    break;
                case "8":
                    Console.Clear();
                    TeachersTable();
                    Console.WriteLine("Press any key to exit");
                    var key2 = Console.ReadKey();
                    AdminMenu();
                    break;
                case "0":
                    start();
                    break;
                default:
                    Console.WriteLine("Invalid user input\n" +
                                      "Press any key to continue");
                    var key = Console.ReadKey();
                    AdminMenu();
                    break;
            }
        }
        static void FacultyMenu()
        {
            Console.Clear();
            Console.WriteLine("Student Violation Records Management System");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("  1 - Issue a Violation(to a student)\n" +
                              "  2 - Update Student Violation\n" +
                              "  3 - Delete Student Violation Record\n" +
                              "  4 - Student Violation Records\n" +
                              "  5 - Students List\n" +
                              "  6 - My Emails\n" +
                              "  0 - Logout");
            Console.Write("Select an Option : ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    IssueOffense();
                    break;

                case "2":
                    UpdateStudentViolation();
                    break;

                case "3":
                    DeleteViolationRecord();
                    break;

                case "4":
                    Console.Clear();
                    StudentViolationRecordsTable();
                    Console.WriteLine("Press any key to exit");
                    var key1 = Console.ReadKey();
                    FacultyMenu();
                    break;

                case "5":
                    Console.Clear();
                    StudentsTable();
                    Console.WriteLine("Press any key to exit");
                    var key2 = Console.ReadKey();
                    FacultyMenu();
                    break;

                case "6":
                    TeacherEmails();
                    break;

                case "0":
                    start();
                    break;

                default:
                    Console.WriteLine("Invalid user input\n" +
                                      "Press any key to continue");
                    var key = Console.ReadKey();
                    FacultyMenu();
                    break;
            }
        }
        static void StudentMenu()
        {
            Console.Clear();
            Console.WriteLine("Student Violation Records");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1 - My Violations\n" +
                              "2 - My Emails\n" +
                              "0 - Logout");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewStudentViolations();
                    break;
                case "2":
                    StudentEmails();
                    break;
                case "0":
                    Student.myStudentNumber.Clear();
                    start();
                    break;
                default:
                    Console.WriteLine("Invalid user input\n" +
                                      "Press any key to continue");
                    var key = Console.ReadKey();
                    StudentMenu();
                    break;
            }
        }
        static void StudentEmails()
        {
            Console.Clear();
            Console.WriteLine("[---------------------My Emails--------------------]\n");
            Console.WriteLine("1 - Emails\n" +
                              "2 - Send Email\n" +
                              "0 - Back to main menu");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    MyStudentEmails();
                    break;
                case "2":
                    SendEmailToTeacher();
                    break;
                case "0":
                    StudentMenu();
                    break;
            }
        }
        static void TeacherEmails()
        {
            Console.Clear();
            Console.WriteLine("[---------------------My Emails--------------------]\n");
            Console.WriteLine(" 1 - Emails\n" +
                               "2 - Send Email\n" +
                               "0 - Back to main menu");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    MyTeacherEmails();
                    break;
                case "2":
                    SendEmailToStudent();
                    break;
                case "0":
                    FacultyMenu();
                    break;
            }
        }

        static void IssueOffense()
        {
            FacultyContinueOPT();
            Console.Clear();
            StudentsTable();

            Console.WriteLine("Select a table ID from the table above");

            Console.Write("ID: ");
            var ID = Convert.ToInt32(Console.ReadLine());
            if (Faculty.GetStudentAuthenticate(ID) == false)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                IssueOffense();
            }
            if (Faculty.GetStudentAuthenticate(ID) == true)

                Console.WriteLine("\nEnter Violation(Reason):");
            string violation = Console.ReadLine();

            Console.WriteLine("\nSelect Type of Offense: \n" +
                              "0 - Minor Offense\n" +
                              "1 - Major Offense");

            string violationType = Console.ReadLine();
            ViolationSelect(violationType);

            int type = Convert.ToInt32(violationType);

            if (Faculty.IssueViolation(ID, violation, type) == true)
            {
                var myID = Faculty.GetMyID();
                string warning = "Warning you have received a violation check your violations to see Violation offense(Three more warnings and you'll be suspended)";
                Faculty.AutoWarningSend(ID, warning, myID);
                Console.WriteLine("Offense issued successfully");
                var exit = Console.ReadKey();
                FacultyMenu();
            }
            else
            {
                Console.WriteLine("This Student has a maximum of 3 Offense, please take action");
                string warning = "Warning you have reached a maximum of 3 offenses please contact school principal immediately to appeal cancel of suspension";
                string myID = Faculty.GetMyID();
                Faculty.AutoWarningSend(ID, warning, myID);
                var exit = Console.ReadKey();
                FacultyMenu();
            }

        }
        static void UpdateStudentViolation()
        {
            FacultyContinueOPT();
            Console.Clear();
            StudentViolationRecordsTable();
            Console.WriteLine("Select an ID from the table above to update");
            Console.Write("ID : ");
            var ID = Convert.ToInt32(Console.ReadLine());

            if (Faculty.GetStudentAuthenticate(ID) == true)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                UpdateStudentViolation();
            }
            else 
            {
                Console.WriteLine("Enter Violation(Reason): \n");
                string violation = Console.ReadLine();

                Console.WriteLine("Select Type of Offense: \n" +
                                  "0 - Minor Offense\n" +
                                  "1 - Major Offense");

                string violationType = Console.ReadLine();

                ViolationSelect(violationType);
                Faculty.UpdateStudentViolation(ID, violation, violationType);

                Console.WriteLine("Record has been successfully updated\n" +
                                  "Press any key to return to menu");
                var key = Console.ReadKey();
                FacultyMenu();
            }

                
        }
        static void DeleteViolationRecord()
        {
            FacultyContinueOPT();
            Console.Clear();
            StudentViolationRecordsTable();
            Console.WriteLine("Select an ID from the table above to delete a Violation Record");
            Console.WriteLine("ID : ");
            var ID = Convert.ToInt32(Console.ReadLine());

            if (Faculty.GetAuthenticateNoOfViolations(ID) == false)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                IssueOffense();
            }
            else
            {
                Faculty.DeleteViolation(ID);
                Console.WriteLine("Violation record successfully deleted\n" +
                              "Press any key to return to menu");
                var key = Console.ReadKey();
                FacultyMenu();
            }
        }

        static void AddStudent()
        {
            AdminContinueOPT();
            Console.Clear();
            Console.WriteLine("Student Number ex. 2000-00001-BN-0");
            Console.Write("Enter Student Number: ");
            string studentnumber = Console.ReadLine();
            Console.Write("Enter First Name: ");
            string firstname = Console.ReadLine();
            Console.Write("Enter Last  Name: ");
            string lastname = Console.ReadLine();
            Console.Write("Enter Course & Section: ");
            string courseAndsection = Console.ReadLine();
            Console.Write("Enter Age: ");
            var age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Gender: m - Male or f - Female: ");
            string gender = Console.ReadLine();

            switch (gender)
            {
                case "m":
                    Console.WriteLine("Male");
                    break;
                case "f":
                    Console.WriteLine("Female");
                    break;
                default:
                    Console.WriteLine("Invalid input; Press any key to reset");
                    var reset = Console.ReadKey();
                    AdminMenu();
                    break;
            }

            Admin.AddStudent(studentnumber, firstname, lastname, courseAndsection, age, GenderPick(gender));
            AdminMenu();
        }
        static void DeleteStudent()
        {
            AdminContinueOPT();
            Console.Clear();
            StudentsTable();

            Console.WriteLine("\nSelect an ID from the table above to delete student record");
            Console.Write("ID : ");
            var ID = Convert.ToInt32(Console.ReadLine());

            if (Faculty.GetStudentAuthenticate(ID) == false)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                AdminMenu();
            }
            if (Faculty.GetStudentAuthenticate(ID) == true)

                Admin.DeleteStudentFromTable(ID);

            Console.WriteLine("\nRecord has been successfully deleted\n" +
                              "Press any key to return to menu");
            var key = Console.ReadKey();
            AdminMenu();
        }
        static void UpdateStudent()
        {
            AdminContinueOPT();
            Console.Clear();
            StudentsTable();

            Console.WriteLine("Select an ID from the table above to update");
            Console.Write("ID : ");
            var ID = Convert.ToInt32(Console.ReadLine());

            if (Faculty.GetStudentAuthenticate(ID) == false)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                AdminMenu();
            }
            if (Faculty.GetStudentAuthenticate(ID) == true)

                Console.WriteLine("\nStudent number ex. 2012-00420-BN-0");
            Console.Write("Enter Student Number: ");
            string studentnumber = Console.ReadLine();
            Console.Write("Enter First Name: ");
            string firstname = Console.ReadLine();
            Console.Write("Enter Last  Name: ");
            string lastname = Console.ReadLine();
            Console.Write("Enter Course & Section: ");
            string courseAndsection = Console.ReadLine();
            Console.Write("Enter Age: ");
            var age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Gender: m - Male or f - Female: ");
            string gender = Console.ReadLine();

            switch (gender)
            {
                case "m":
                    Console.WriteLine("Male");
                    break;
                case "f":
                    Console.WriteLine("Female");
                    break;
                default:
                    Console.WriteLine("Invalid input; Press any key to reset");
                    var reset = Console.ReadKey();
                    AdminMenu();
                    break;
            }

            Admin.UpdateStudentTable(ID, studentnumber, firstname, lastname, courseAndsection, age, GenderPick(gender));

            Console.WriteLine("\nRecord has been successfully updated\n" +
                              "Press any key to return to menu");
            var key = Console.ReadKey();
            AdminMenu();
        }

        static void AddTeacher()
        {
            AdminContinueOPT();
            Console.Clear();
            Console.WriteLine("Teacher ID ex. 2000-00001-BN-0");
            Console.Write("Enter TeacherID: ");
            string teacherID = Console.ReadLine();
            Console.Write("Enter First Name: ");
            string firstname = Console.ReadLine();
            Console.Write("Enter Last  Name: ");
            string lastname = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Age: ");
            var age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Gender: m - Male or f - Female: ");
            string gender = Console.ReadLine().ToLower();

            switch (gender)
            {
                case "m":
                    Console.WriteLine("Male");
                    break;
                case "f":
                    Console.WriteLine("Female");
                    break;
                default:
                    Console.WriteLine("Invalid input; Press any key to reset");
                    var reset = Console.ReadKey();
                    AddTeacher();
                    break;
            }
            Admin.AddTeacher(teacherID, firstname, lastname, email, age, GenderPick(gender));
            Console.WriteLine("\nRecord has been successfully added\n" +
                              "Press any key to return to menu");
            var key = Console.ReadKey();
            AdminMenu();
        }
        static void DeleteTeacher()
        {
            AdminContinueOPT();
            Console.Clear();
            TeachersTable();

            Console.WriteLine("Select an ID from the table above to delete teacher record");
            Console.Write("ID : ");
            var ID = Convert.ToInt32(Console.ReadLine());

            if (Faculty.GetTeacherAuthenticate(ID) == false)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                AdminMenu();
            }
            if (Faculty.GetTeacherAuthenticate(ID) == true)

                Admin.DeleteTeacherFromTable(ID);
            Console.WriteLine("Record has been successfully deleted\n" +
                              "Press any key to return to menu");
            var key = Console.ReadKey();
            AdminMenu();

        }
        static void UpdateTeacher()
        {
            AdminContinueOPT();
            Console.Clear();
            TeachersTable();
            Console.WriteLine("Select an ID from the table above to update");
            Console.Write("ID : ");
            var ID = Convert.ToInt32(Console.ReadLine());

            if (Faculty.GetTeacherAuthenticate(ID) == false)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                AdminMenu();
            }
            else
            Console.Write("Enter Teachers ID: ");
            string teacherID = Console.ReadLine();
            Console.Write("Enter First Name: ");
            string firstname = Console.ReadLine();
            Console.Write("Enter Last  Name: ");
            string lastname = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Age: ");
            var age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Gender: m - Male or f - Female: ");
            string gender = Console.ReadLine().ToLower();

            switch (gender)
            {
                case "m":
                    Console.WriteLine("Male");
                    break;
                case "f":
                    Console.WriteLine("Female");
                    break;
                default:
                    Console.WriteLine("Invalid input; Press any key to reset");
                    var reset = Console.ReadKey();
                    UpdateTeacher();
                    break;
            }

            Admin.UpdateTeacherTable(ID, teacherID, firstname, lastname, email, age, GenderPick(gender));

            Console.WriteLine("\nRecord has been successfully updated\n" +
                              "Press any key to return to menu");
            var key = Console.ReadKey();
            AdminMenu();
        }

        static void SendEmailToStudent()
        {
            FacultyContinueOPT();
            Console.Clear();
            StudentsTable();

            Console.WriteLine("Select an ID from the table above to send an email");
            Console.Write("ID : ");
            var ID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Compose Email: ");
            string email = Console.ReadLine();

            if (Faculty.GetTeacherAuthenticate(ID) == true)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                TeacherEmails();
            }
            else if(Faculty.SendEmailToStudent(Faculty.GetMyID(), email, Faculty.GetStudentNum(ID)) == true)
            { 
                Console.WriteLine("Email sent successfully");
                Console.WriteLine("Press any key to return to menu");
                var key = Console.ReadKey();
                TeacherEmails();
            }
        }
        static void SendEmailToTeacher()
        {
            StudentContinueOPT();
            Console.Clear();
            TeachersTableNameOnly();

            Console.WriteLine("Select an ID from the table above to send an email");
            Console.Write("ID : ");
            
            var ID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Compose Email: ");
            string email = Console.ReadLine();

            if (Faculty.GetTeacherAuthenticate(ID) == false)
            {
                Console.WriteLine("ID doesn't Exists\n" +
                                  "Press any key to reset");
                var reset = Console.ReadKey();
                StudentMenu();
            }
            else if (Faculty.SendEmailToTeacher(Student.GetMyNumber(), email, Faculty.GetTeacherID(ID)) == true)
            {
                Console.WriteLine("Email sent successfully");
                Console.WriteLine("Press any key to return to menu");
                var key = Console.ReadKey();
                StudentEmails();
            }
        }
        static void MyStudentEmails()
        {
            Console.Clear();
            string StudNum = Student.GetMyNumber();

            Console.WriteLine("Sender | Email");
            Console.WriteLine(Student.GetEmails(StudNum));
            Console.WriteLine("Press any key to go back");
            var key = Console.ReadKey();
            StudentEmails();
        }
        static void MyTeacherEmails()
        {
            Console.Clear();
            Console.WriteLine("Sender | Email");
            Console.WriteLine(Faculty.GetEmails(Faculty.GetMyID()));
            Console.WriteLine("Press any key to go back");
            var key = Console.ReadKey();
            TeacherEmails();
        }
        
        static void TeachersTable()
        {
            Console.WriteLine("ID|TeacherID|FirstName|LastName|Email|Age|Gender\n");
            Console.WriteLine(Faculty.GetTeachersTable());
        }

        static void StudentsTable()
        {
            Console.WriteLine("ID|StudentNumber|FirstName|LastName|Course&Section|Age|Gender\n");
            Console.WriteLine(Faculty.GetStudentsTable());
        }
        static void StudentViolationRecordsTable()
        {
            Console.WriteLine("ID|StudentNumber|Violation|Type|NumberOfOffenses|DateIssued\n");
            Console.WriteLine(Faculty.GetStudentViolationsTable());

        }
        static void TeachersTableNameOnly()
        {
            Console.WriteLine("ID||FirstName|Email|Age|Gender\n");
            Console.WriteLine(Student.GetTeachersTableFiltered());
        }

        static void ViewStudentViolations()
        {
            Console.Clear();
            string StudNum = Student.GetMyNumber();
            Console.WriteLine("Student Violation Records Management System");
            Console.WriteLine("[---------------------My Information--------------------]");
            Console.WriteLine("ID| StudentNumber | FirstName | LastName | Course&Section | Age | Gender|");
            Console.WriteLine(Student.GetStudentsInformation(StudNum));

            Console.WriteLine("\n\n---------------------My Violations--------------------");

            if (Student.GetStudentViolations(StudNum).Length > 0)
            {
                Console.WriteLine(Student.GetStudentViolations(StudNum));
            }
            else
            {

                Console.WriteLine("Good student, you have no violations");
            }

            Console.WriteLine("\nPress any key to exit");
            var key = Console.ReadKey();
            StudentMenu();
        }

        static void FacultyContinueOPT()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Select an option");
                Console.WriteLine("1 - Continue | 0 - Exit");
                string option = Console.ReadLine();
                if (option == "1") { break; }
                if (option == "0") { FacultyMenu(); }
                else
                {
                    Console.WriteLine("Invalid input; press any key to continue");
                    var invalidOPT = Console.ReadKey();
                    continue;
                }
            } while (true);
        }
        static void AdminContinueOPT()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Select an option");
                Console.WriteLine("1 - Continue | 0 - Exit");
                string option = Console.ReadLine();
                if (option == "1") { break; }
                if (option == "0") { AdminMenu(); }
                else
                {
                    Console.WriteLine("Invalid input; press any key to continue");
                    var invalidOPT = Console.ReadKey();
                    continue;
                }
            } while (true);
        }
        static void ContinueOPT()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Select an option");
                Console.WriteLine("1 - Continue | 0 - Exit");
                string option = Console.ReadLine();
                if (option == "1") { break; }
                if (option == "0") { start(); }
                else
                {
                    Console.WriteLine("Invalid input; press any key to continue");
                    var invalidOPT = Console.ReadKey();
                    continue;
                }
            } while (true);
        }
        static void StudentContinueOPT()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Select an option");
                Console.WriteLine("1 - Continue | 0 - Exit");
                string option = Console.ReadLine();
                if (option == "1") { break; }
                if (option == "0") { StudentMenu(); }
                else
                {
                    Console.WriteLine("Invalid input; press any key to continue");
                    var invalidOPT = Console.ReadKey();
                    continue;
                }
            } while (true);
        }

        static void ViolationSelect(string val)
        {
            if (val == "0" || val == "1")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid input press any key to reset");
                var reset = Console.ReadKey();
                IssueOffense();
            }
        }
        static string GenderPick(string gender)
        {
            switch (gender)
            {
                case "m":
                    return "Male";
                case "f":
                    return "Female";
                default:
                    Console.WriteLine("Invalid input; Press any key to reset");
                    var reset = Console.ReadKey();
                    AdminMenu();
                    break;
            }
            throw new InvalidOperationException("No value");
        }
    }
}
