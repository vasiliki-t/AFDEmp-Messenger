using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class EditRole :IOperation
    {
        public void Execute()
        {
            SelectUser();
        }

        void SelectUser()
        {
            List<User> allUsers = DatabaseAccess.GetAllUsers();

            List<string> menuOptions = new List<string>();

            for (int i = 0; i < allUsers.Count; i++)
            {
                User u = allUsers[i];
                menuOptions.Add($"{u.Username} - {(RoleEnum)u.Role}");
            }

            menuOptions.Add("Back");

            int index = MenuUtilities.SelectOperations(menuOptions.ToArray());
            if (index == menuOptions.Count - 1)
            {
                Program.SetOperation("MainMenu");
            }
            else
            {
                if (allUsers[index].Role == 2)
                {
                    Console.WriteLine("You can't edit admin user's role! Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    SelectRole(allUsers[index]);
                }
            }

        }

        void SelectRole(User user)
        {
            string[] menuOptions = new string[2] { "Client", "Moderator" };
            int index = MenuUtilities.SelectOperations(menuOptions);
            if (index == 0)
            {
                user.Role = 0;
            }
            else
            {
                user.Role = 1;
            }

            DatabaseAccess.UpdateUser(user);
        }
    }
}
