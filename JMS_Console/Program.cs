using JMS_DAL;
using JMS_DAL.Data_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace JMS_Console
{
    public class Program
    {
        static bool loggedIn = false;
        static bool loop = true;
        

        public static void Main()
        {
            Login();
            Loop();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static public void Login()
        {
            while (!loggedIn)
            {
                Console.Write("Please enter email: ");
                string email = Console.ReadLine();
                SecureString password = getPasswordFromConsole("Please enter password: ");

                Task<bool> login = FirebaseHelper.SignIn(email, password.SecureStringToString());
                login.Wait();
                loggedIn = login.Result;
            }
        }

        static public void Loop()
        {
            while (loop)
            {
                PrintOptions();
                string num = Console.ReadLine();
                CheckOption(int.Parse(num));
            }
        }

        static public void PrintOptions()
        {
            Console.WriteLine("\nEnter a number from the list of options:");
            Console.WriteLine("1. Get All Jobs");
            Console.WriteLine("2. Get All Clients");
            Console.WriteLine("3. Get All Equipment");
            Console.WriteLine("4. Get All Employees");
            Console.WriteLine("-----------------------");
            Console.WriteLine("5. Get Job");
            Console.WriteLine("6. Get Client");
            Console.WriteLine("7. Get Equipment");
            Console.WriteLine("8. Get Employee");
            Console.WriteLine("-----------------------");
            Console.WriteLine("9. Create Job");
            Console.WriteLine("10. Create Client");
            Console.WriteLine("11. Create Equipment");
            Console.WriteLine("12. Create Employee");
            Console.WriteLine("-----------------------");
            Console.WriteLine("13. Update Job");
            Console.WriteLine("14. Update Client");
            Console.WriteLine("15. Update Equipment");
            Console.WriteLine("16. Update Employee");
            Console.WriteLine("-----------------------");
            Console.WriteLine("17. Delete Job");
            Console.WriteLine("18. Delete Client");
            Console.WriteLine("19. Delete Equipment");
            Console.WriteLine("20. Delete Employee");
            Console.WriteLine("-----------------------");
            Console.WriteLine("0. Exit");
            Console.Write("\nEnter number: ");
        }

        static public void CheckOption(int value)
        {
            switch (value)
            {
                case 1:
                    Console.WriteLine("\nJobs:");
                    List<JobDTO> lstJ = JMS_Commands.GetJobs();
                    if (lstJ != null) { lstJ.ForEach(j => PrintJob(j)); } else { Console.WriteLine("No Results."); }
                    break;

                case 2:
                    Console.WriteLine("\nClients:");
                    List<ClientDTO> lstC = JMS_Commands.GetClients();
                    if (lstC != null) { lstC.ForEach(j => PrintClient(j)); } else { Console.WriteLine("No Results."); }
                    break;

                case 3:
                    Console.WriteLine("\nEquipment:");
                    List<EquipmentDTO> lstEq = JMS_Commands.GetEquipment();
                    if (lstEq != null) { lstEq.ForEach(j => PrintEquipment(j)); } else { Console.WriteLine("No Results."); }
                    break;

                case 4:
                    Console.WriteLine("\nEmployees:");
                    List<EmployeeDTO> lstEm = JMS_Commands.GetEmployees();
                    if (lstEm != null) { lstEm.ForEach(j => PrintEmployee(j)); } else { Console.WriteLine("No Results."); }
                    break;

                case 5:
                    Console.Write("Please enter ID of Job:");
                    string id = Console.ReadLine();
                    JobDTO dto = JMS_Commands.GetJob(id);
                    if(dto != null) { PrintJob(dto); } else { Console.WriteLine("No Results."); }
                    break;

                case 6:
                    Console.Write("Please enter ID of Client:");
                    string idC = Console.ReadLine();
                    ClientDTO dtoC = JMS_Commands.GetClient(idC);
                    if (dtoC != null) { PrintClient(dtoC); } else { Console.WriteLine("No Results."); }
                    break;

                case 7:
                    Console.Write("Please enter ID of Equipment:");
                    string idEq = Console.ReadLine();
                    EquipmentDTO dtoEq = JMS_Commands.GetEquipment(idEq);
                    if (dtoEq != null) { PrintEquipment(dtoEq); } else { Console.WriteLine("No Results."); }
                    break;

                case 8:
                    Console.Write("Please enter ID of Employee:");
                    string idEm = Console.ReadLine();
                    EmployeeDTO dtoEm = JMS_Commands.GetEmployee(idEm);
                    if (dtoEm != null) { PrintEmployee(dtoEm); } else { Console.WriteLine("No Results."); }
                    break;

                //case 17:
                //    Console.Write("Please enter ID of Job to delete:");
                    
                //    JobDTO dto = JMS_Commands.DeleteJob(Console.ReadLine());
                //    if (dto != null) { PrintJob(dto); } else { Console.WriteLine("No Results."); }
                //    break;

                //case 18:
                //    Console.Write("Please enter ID of Client to delete:");
                //    string idC = Console.ReadLine();
                //    ClientDTO dtoC = JMS_Commands.GetClient(idC);
                //    if (dtoC != null) { PrintClient(dtoC); } else { Console.WriteLine("No Results."); }
                //    break;

                //case 19:
                //    Console.Write("Please enter ID of Equipment to delete:");
                //    string idEq = Console.ReadLine();
                //    EquipmentDTO dtoEq = JMS_Commands.GetEquipment(idEq);
                //    if (dtoEq != null) { PrintEquipment(dtoEq); } else { Console.WriteLine("No Results."); }
                //    break;

                //case 20:
                //    Console.Write("Please enter ID of Employee to delete:");
                //    string idEm = Console.ReadLine();
                //    EmployeeDTO dtoEm = JMS_Commands.GetEmployee(idEm);
                //    if (dtoEm != null) { PrintEmployee(dtoEm); } else { Console.WriteLine("No Results."); }
                //    break;

                case 0:
                default:
                    loop = false;
                    break;
            }
        }

        public static SecureString getPasswordFromConsole(string displayMessage)
        {
            SecureString pass = new SecureString();
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (!char.IsControl(key.KeyChar))
                {
                    pass.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass.RemoveAt(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            return pass;
        }

        #region Print Objects

        public static void PrintJob(JobDTO dto)
        {
            Console.WriteLine($"ID:{dto.ID} - Name:{dto.Name} - Client ID:{dto.ClientID}");
            Console.WriteLine("Equipment List:");
            dto.EquipmentList.ForEach(e => Console.WriteLine(e));
            Console.WriteLine();
        }

        public static void PrintClient(ClientDTO dto)
        {
            Console.WriteLine($"ID:{dto.ID} - Company Name:{dto.CompanyName} - Address:{dto.Address}");
        }

        public static void PrintEmployee(EmployeeDTO dto)
        {
            Console.WriteLine($"ID:{dto.ID} - First Name:{dto.FirstName} - Last Name:{dto.LastName} - Role: {(EmployeeRole)dto.Role}");
        }

        public static void PrintEquipment(EquipmentDTO dto)
        {
            Console.WriteLine($"ID:{dto.ID} - Name:{dto.Name} - Model:{dto.Model} - Manufacturer:{dto.Manufacturer}");
        }

        #endregion
    }

    public static class JMS_Commands
    {
        public static JobDTO GetJob(string id)
        {
            Task<JobDTO> dto = FirebaseHelper.GetJobData(id);
            dto.Wait();
            return dto.Result;
        }

        public static ClientDTO GetClient(string id)
        {
            Task<ClientDTO> dto = FirebaseHelper.GetClientData(id);
            dto.Wait();
            return dto.Result;
        }

        public static EquipmentDTO GetEquipment(string id)
        {
            Task<EquipmentDTO> dto = FirebaseHelper.GetEquipmentData(id);
            dto.Wait();
            return dto.Result;
        }

        public static EmployeeDTO GetEmployee(string id)
        {
            Task<EmployeeDTO> dto = FirebaseHelper.GetEmployeeData(id);
            dto.Wait();
            return dto.Result;
        }

        public static List<JobDTO> GetJobs()
        {
            Task<List<JobDTO>> dto = FirebaseHelper.GetAllJobData();
            dto.Wait();
            return dto.Result;
        }


        public static List<EquipmentDTO> GetEquipment()
        {
            Task<List<EquipmentDTO>> dto = FirebaseHelper.GetAllEquipmentData();
            dto.Wait();
            return dto.Result;
        }

        public static List<EmployeeDTO> GetEmployees()
        {
            Task<List<EmployeeDTO>> dto = FirebaseHelper.GetAllEmployeeData();
            dto.Wait();
            return dto.Result;
        }

        public static List<ClientDTO> GetClients()
        {
            Task<List<ClientDTO>> dto = FirebaseHelper.GetAllClientData();
            dto.Wait();
            return dto.Result;
        }
    }

    public static class Extensions
    {
        static public string SecureStringToString(this SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
