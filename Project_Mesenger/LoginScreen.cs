using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class LoginScreen : IOperation
    {
        string username;

        public void Execute()
        {
            string[] operations = new string[2] { "Login", "Exit" };
            int index = MenuUtilities.SelectOperations(operations, Header);

            switch (index)
            {
                case 0:
                    Login();
                    break;
                case 1:
                    Program.isRunning = false;
                    break;
            }
        }

        void Login()
        {
            Console.Clear();
            Console.WriteLine("Insert Username");
            Console.WriteLine("Username : ");
            username = Console.ReadLine();

            Console.WriteLine("Password : ");
            string password = MenuUtilities.MaskPassword(PasswordHeader);

            User connected = DatabaseAccess.ValidateLogin(username, password);

            if (connected != null)
            {
                Program.currentUser = connected;
                Program.SetOperation("MainMenu");
            }
            else
            {
                Console.WriteLine("Invalid password!");
                Console.ReadKey();
            }
        }

        void PasswordHeader()
        {
            Console.WriteLine("Insert Username");
            Console.WriteLine("Username : ");
            Console.WriteLine(username);
            Console.WriteLine("Password : ");
        }

        void Header()
        {
        
            string h = @"
     __          __  _                          
     \ \        / / | |                         
      \ \  /\  / /__| | ___ ___  _ __ ___   ___ 
       \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \
        \  /\  /  __/ | (_| (_) | | | | | |  __/
         \/  \/ \___|_|\___\___/|_| |_| |_|\___|                                               
";
            Console.WriteLine(h);
            
        }
    }
}
