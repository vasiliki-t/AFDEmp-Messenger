using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class AddUser : IOperation
    {
        string targetUser;

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("Write username :");
            targetUser = Console.ReadLine();

            //Check for duplicate user
            User duplicateUser = DatabaseAccess.FindUserViaUsername(targetUser);
            if (duplicateUser != null)
            {
                Console.WriteLine("Username already exists! Press any key to try again");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Write password :");
            string targetPassword = MenuUtilities.MaskPassword(Header);

            Console.WriteLine("Rewrite password :");
            string password2 = MenuUtilities.MaskPassword(RewriteHeader);

            if (string.Equals(targetPassword, password2))
            {
                string[] roles = new string[2] { "client", "moderator" };
                int targetIndex = MenuUtilities.SelectOperations(roles);

                DatabaseAccess.AddUser(targetUser, targetPassword, targetIndex);
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Successfuly added User!  \nPress any key to continue.");
                Console.ReadKey();
                Program.SetOperation("MainMenu");
            }
            else
            {
                Console.WriteLine("Passwords don't match!");
            }

        }

        void Header()
        {
            Console.WriteLine("Write username :");
            Console.WriteLine(targetUser);
            Console.WriteLine("Write password :");
        }

        void RewriteHeader()
        {
            Console.WriteLine("Write username :");
            Console.WriteLine(targetUser);
            Console.WriteLine("Rewrite password :");
        }
    }
}
