using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class Program
    {
        public static bool isRunning;
        public static User currentUser;
        public static Message currentMessage;
        static IOperation currentOperation;
        static Dictionary<string,IOperation> operationData = new Dictionary<string, IOperation>();
        static public bool isOutbox;

        static void Main(string[] args)
        {          
            isRunning = true;
            Init();

            while (isRunning)
            {
                if (currentOperation != null)
                {
                    currentOperation.Execute();
                }
                else
                {
                    Console.WriteLine("Invalid Operation call! Program will exit");
                    Console.ReadKey();
                    isRunning = false;

                }
            }

        }

        public static void Init()
        {
            operationData.Add("Login", new LoginScreen());
            operationData.Add("MainMenu", new MainMenu());
            operationData.Add("Send Message", new SendMessage());
            operationData.Add("Inbox", new Inbox());
            operationData.Add("Show All Users", new ShowAllUsers());
            operationData.Add("Add User", new AddUser());
            operationData.Add("Delete User", new DeleteUser());
            operationData.Add("Outbox", new Outbox());
            operationData.Add("ReadMessage", new ReadMessage());
            operationData.Add("Edit Message", new EditMessage());
            operationData.Add("Edit User", new EditRole());
            operationData.Add("Reply", new Reply());

            SetOperation("Login");
        }

        public static void SetOperation(string targetOperation)
        {
            operationData.TryGetValue(targetOperation, out currentOperation);
        }

        //text file
        public static void Log(string logInfo)
        {
            string path = @"C:\Users\Vassiliki\Desktop\Logger.txt";
            File.AppendAllText(path, logInfo);
        }
    }
}
