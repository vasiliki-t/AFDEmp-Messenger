using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class ShowAllUsers: IOperation
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

            }
        }
    }
}
    

