using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class MainMenu: IOperation
    {
       public void Execute()
        {
            switch (Program.currentUser.Role)
            {
                case 0:
                    Client();
                    break;
                case 1:
                    Moderator();
                    break;
                case 2:
                    Admin();
                    break;
            }
        }

        private void Moderator()
        {
            string[] operations = new string[5] { "Send Message", "Inbox", "Outbox", "Edit Message", "Logout" };
            int index = MenuUtilities.SelectOperations(operations, Header);

            if (index == operations.Length - 1)
            {
                BackToLogin();
            }
            else
            {
                Program.SetOperation(operations[index]);
            }
        }

        private void Admin()
        {
            string[] operations = new string[9] { "Send Message", "Inbox", "Outbox","Add User","Show All Users", "Edit User", "Delete User", "Edit Message", "Logout" };
            int index = MenuUtilities.SelectOperations(operations, Header);

            if (index == operations.Length - 1)
            {
                BackToLogin();
            }
            else
            {
                Program.SetOperation(operations[index]);
            }
        }

        void Client()
        {
            string[] operations = new string[4] { "Send Message", "Inbox", "Outbox", "Logout" };
            int index = MenuUtilities.SelectOperations(operations, Header);

            if (index == operations.Length - 1)
            {
                BackToLogin();
            }
            else
            {
                Program.SetOperation(operations[index]);
            }
        }

        void BackToLogin()
        {
            Program.SetOperation("Login");
        }

        void Header()
        {
            string h = @"
      __  __       _         __  __                  
     |  \/  |     (_)       |  \/  |                 
     | \  / | __ _ _ _ __   | \  / | ___ _ __  _   _ 
     | |\/| |/ _` | | '_ \  | |\/| |/ _ \ '_ \| | | |
     | |  | | (_| | | | | | | |  | |  __/ | | | |_| |
     |_|  |_|\__,_|_|_| |_| |_|  |_|\___|_| |_|\__,_|
";
            Console.WriteLine(h);
            Console.WriteLine(" ");

            List<Message> inbox = DatabaseAccess.FindUsersInboxMessages(Program.currentUser.UserId);
            int unread = inbox.Count;

            for (int i = 0; i < inbox.Count; i++)
            {
                if (inbox[i].isRead)
                    unread--;
            }
            
            Console.WriteLine($"Unread Messages : {unread} Inbox :{inbox.Count} \n");

        }
    }
}
