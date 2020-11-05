﻿using JMS_DAL;
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
                    MenuOption_GetJobs();
                    break;

                case 2:
                    MenuOptions_GetClients();
                    break;

                case 3:
                    MenuOptions_GetEquipmentList();
                    break;

                case 4:
                    MenuOptions_GetEmployees();
                    break;

                case 5:
                    MenuOptions_GetJob();
                    break;

                case 6:
                    MenuOptions_GetClient();
                    break;

                case 7:
                    MenuOptions_GetEquipment();
                    break;

                case 8:
                    MenuOptions_GetEmployee();
                    break;

                case 9:
                    MenuOptions_CreateJob();
                    break;

                case 10:
                    MenuOptions_CreateClient();
                    break;

                case 11:
                    MenuOptions_CreateEquipment();
                    break;

                case 12:
                    MenuOptions_CreateEmployee();
                    break;

                case 13:
                    MenuOptions_UpdateJob();
                    break;

                case 14:
                    MenuOptions_UpdateClient();
                    break;

                case 15:
                    MenuOptions_UpdateEquipment();
                    break;

                case 16:
                    MenuOptions_UpdateEmployee();
                    break;

                case 17:
                    MenuOptions_DeleteJob();
                    break;

                case 18:
                    MenuOptions_DeleteClient();
                    break;

                case 19:
                    MenuOptions_DeleteEquipment();
                    break;

                case 20:
                    MenuOptions_DeleteEmployee();
                    break;

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

        #region Menu Options

        static void MenuOption_GetJobs()
        {
            Console.WriteLine("\nJobs:");
            List<JobDTO> lstJ = JMS_Commands.GetJobs();
            if (lstJ != null) { lstJ.ForEach(j => PrintJob(j)); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_GetClients()
        {
            Console.WriteLine("\nClients:");
            List<ClientDTO> lstC = JMS_Commands.GetClients();
            if (lstC != null) { lstC.ForEach(j => PrintClient(j)); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_GetEquipmentList()
        {
            Console.WriteLine("\nEquipment:");
            List<EquipmentDTO> lstEq = JMS_Commands.GetEquipment();
            if (lstEq != null) { lstEq.ForEach(j => PrintEquipment(j)); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_GetEmployees()
        {
            Console.WriteLine("\nEmployees:");
            List<EmployeeDTO> lstEm = JMS_Commands.GetEmployees();
            if (lstEm != null) { lstEm.ForEach(j => PrintEmployee(j)); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_GetJob()
        {
            Console.Write("Please enter ID of Job:");
            string id = Console.ReadLine();
            JobDTO dto = JMS_Commands.GetJob(id);
            if (dto != null) { PrintJob(dto); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_GetClient()
        {
            Console.Write("Please enter ID of Client:");
            string idC = Console.ReadLine();
            ClientDTO dtoC = JMS_Commands.GetClient(idC);
            if (dtoC != null) { PrintClient(dtoC); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_GetEquipment()
        {
            Console.Write("Please enter ID of Equipment:");
            string idEq = Console.ReadLine();
            EquipmentDTO dtoEq = JMS_Commands.GetEquipment(idEq);
            if (dtoEq != null) { PrintEquipment(dtoEq); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_GetEmployee()
        {
            Console.Write("Please enter ID of Employee:");
            string idEm = Console.ReadLine();
            EmployeeDTO dtoEm = JMS_Commands.GetEmployee(idEm);
            if (dtoEm != null) { PrintEmployee(dtoEm); } else { Console.WriteLine("No Results."); }
        }

        static void MenuOptions_CreateJob()
        {
            JobDTO job = new JobDTO();

            Console.Write("Please enter name of Job to add:");
            job.Name = Console.ReadLine();

            Console.WriteLine("Please enter ID of client:");
            job.ClientID = Console.ReadLine();
            job.ID = Guid.NewGuid().ToString();

            Console.WriteLine("Please enter ID's of equipment for job seperated by commas:");
            string idList = Console.ReadLine();

            job.EquipmentList.AddRange(idList.Split(','));

            JMS_Commands.CreateJob(job);
        }

        static void MenuOptions_CreateClient()
        {
            Console.Write("Please enter name of Client to add:");
            ClientDTO client = new ClientDTO();
            client.CompanyName = Console.ReadLine();

            Console.WriteLine("Please enter address of client:");
            client.Address = Console.ReadLine();
            client.ID = Guid.NewGuid().ToString();

            JMS_Commands.CreateClient(client);
        }

        static void MenuOptions_CreateEquipment()
        {
            Console.Write("Please enter name of Equipment to add:");
            EquipmentDTO equipment = new EquipmentDTO();
            equipment.Name = Console.ReadLine();

            Console.WriteLine("Please enter manufacturer of equipment:");
            equipment.Manufacturer = Console.ReadLine();

            Console.WriteLine("Please enter model of equipment:");
            equipment.Model = Console.ReadLine();

            equipment.ID = Guid.NewGuid().ToString();

            JMS_Commands.CreateEquipment(equipment);
        }

        static void MenuOptions_CreateEmployee()
        {
            EmployeeDTO employee = new EmployeeDTO();
            Console.Write("Please enter first name of Employee to add:");
            employee.FirstName = Console.ReadLine();

            Console.WriteLine("Please enter last name of employee:");
            employee.LastName = Console.ReadLine();

            employee.Role = 0;

            employee.ID = Guid.NewGuid().ToString();

            JMS_Commands.CreateEmployee(employee);
        }

        static void MenuOptions_UpdateJob()
        {
            Console.WriteLine("Please enter the id of the object you would like to update:");
            JobDTO dto = JMS_Commands.GetJob(Console.ReadLine());
            Console.WriteLine("Job to update:");
            PrintJob(dto);

            Console.Write("Please enter updated name of Job to add:");
            dto.Name = Console.ReadLine();

            Console.WriteLine("Please enter updated ID of client:");
            dto.ClientID = Console.ReadLine();
            dto.ID = Guid.NewGuid().ToString();

            string eqList = "";
            dto.EquipmentList.ForEach(e => eqList = string.Concat(eqList, e, ","));
            Console.WriteLine(string.Concat("\n","Current IDs:",eqList));
            Console.WriteLine("Please enter ID's of equipment for job seperated by commas:");
            string idList = Console.ReadLine();
            dto.EquipmentList.AddRange(idList.Split(','));

            JMS_Commands.UpdateJob(dto);
        }

        static void MenuOptions_UpdateClient()
        {
            Console.WriteLine("Please enter the id of the object you would like to update:");
            ClientDTO client = JMS_Commands.GetClient(Console.ReadLine());
            Console.WriteLine("Client to update:");
            PrintClient(client);

            Console.Write("Please enter updated name of Client to add:");
            client.CompanyName = Console.ReadLine();

            Console.WriteLine("Please enter updated address of client:");
            client.Address = Console.ReadLine();

            JMS_Commands.UpdateClient(client);
        }

        static void MenuOptions_UpdateEquipment()
        {
            Console.WriteLine("Please enter the id of the equipment item you would like to update:");
            EquipmentDTO equipment = JMS_Commands.GetEquipment(Console.ReadLine());
            Console.WriteLine("Equipment to update:");
            PrintEquipment(equipment);

            Console.Write("Please enter updated name of Equipment to add:");
            equipment.Name = Console.ReadLine();

            Console.WriteLine("Please enter updated manufacturer of equipment:");
            equipment.Manufacturer = Console.ReadLine();

            Console.WriteLine("Please enter updated model of equipment:");
            equipment.Model = Console.ReadLine();

            JMS_Commands.UpdateEquipment(equipment);
        }

        static void MenuOptions_UpdateEmployee()
        {
            Console.WriteLine("Please enter the id of the equipment item you would like to update:");
            EmployeeDTO employee = JMS_Commands.GetEmployee(Console.ReadLine());
            Console.WriteLine("Employee to update:");
            PrintEmployee(employee);

            Console.Write("Please enter updated first name of Employee to add:");
            employee.FirstName = Console.ReadLine();

            Console.WriteLine("Please enter updated last name of employee:");
            employee.LastName = Console.ReadLine();

            employee.Role = 0;

            JMS_Commands.CreateEmployee(employee);
        }

        static void MenuOptions_DeleteJob()
        {
            Console.Write("Please enter ID of Job to delete:");
            if (JMS_Commands.DeleteJob(Console.ReadLine())) { Console.WriteLine("Deleted."); } else { Console.WriteLine("Failed To Delete."); }
        }

        static void MenuOptions_DeleteClient()
        {
            Console.Write("Please enter ID of Client to delete:");
            if (JMS_Commands.DeleteClient(Console.ReadLine())) { Console.WriteLine("Deleted."); } else { Console.WriteLine("Failed To Delete."); }
        }

        static void MenuOptions_DeleteEquipment()
        {
            Console.Write("Please enter ID of Equipment to delete:");
            if (JMS_Commands.DeleteEquipment(Console.ReadLine())) { Console.WriteLine("Deleted."); } else { Console.WriteLine("Failed To Delete."); }
        }

        static void MenuOptions_DeleteEmployee()
        {
            Console.Write("Please enter ID of Employee to delete:");
            if (JMS_Commands.DeleteEmployee(Console.ReadLine())) { Console.WriteLine("Deleted."); } else { Console.WriteLine("Failed To Delete."); }
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


        public static bool UpdateJob(JobDTO dto)
        {
            Task<bool> str = FirebaseHelper.UpdateData(dto);
            str.Wait();
            return str.Result;
        }

        public static bool UpdateEquipment(EquipmentDTO dto)
        {
            Task<bool> str = FirebaseHelper.UpdateData(dto);
            str.Wait();
            return str.Result;
        }

        public static bool UpdateEmployee(EmployeeDTO dto)
        {
            Task<bool> str = FirebaseHelper.UpdateData(dto);
            str.Wait();
            return str.Result;
        }

        public static bool UpdateClient(ClientDTO dto)
        {
            Task<bool> str = FirebaseHelper.UpdateData(dto);
            str.Wait();
            return str.Result;
        }


        public static string CreateJob(JobDTO dto)
        {
            Task<string> str = FirebaseHelper.PostData(dto);
            str.Wait();
            return str.Result;
        }

        public static string CreateEquipment(EquipmentDTO dto)
        {
            Task<string> str = FirebaseHelper.PostData(dto);
            str.Wait();
            return str.Result;
        }

        public static string CreateEmployee(EmployeeDTO dto)
        {
            Task<string> str = FirebaseHelper.PostData(dto);
            str.Wait();
            return str.Result;
        }

        public static string CreateClient(ClientDTO dto)
        {
            Task<string> str = FirebaseHelper.PostData(dto);
            str.Wait();
            return str.Result;
        }


        public static bool DeleteJob(string id)
        {
            Task<bool> dto = FirebaseHelper.DeleteJobData(id);
            dto.Wait();
            return dto.Result;
        }

        public static bool DeleteEquipment(string id)
        {
            Task<bool> dto = FirebaseHelper.DeleteEquipmentData(id);
            dto.Wait();
            return dto.Result;
        }

        public static bool DeleteEmployee(string id)
        {
            Task<bool> dto = FirebaseHelper.DeleteEmployeeData(id);
            dto.Wait();
            return dto.Result;
        }

        public static bool DeleteClient(string id)
        {
            Task<bool> dto = FirebaseHelper.DeleteClientData(id);
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
