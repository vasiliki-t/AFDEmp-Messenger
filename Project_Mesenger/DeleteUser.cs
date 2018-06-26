using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class DeleteUser: IOperation
    {
        public void Execute()
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

                    Console.WriteLine("You can't delete the admin user! Press any key to continue");
                    Console.ReadKey();
                }
                else
                {

                    Console.WriteLine("Are you sure you want to delete the selected user? y/n");
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Y || key == ConsoleKey.Enter)
                    {
                        DatabaseAccess.DeleteUser(allUsers[index].UserId);
                        Console.WriteLine("User deleted! Press any key to continue");
                    }
                    else
                    {
                        Console.WriteLine("Aborting... press any key to continue");

                    }

                    Console.ReadKey();
                    Program.SetOperation("MainMenu");
                }
            }
        }
    }
}
