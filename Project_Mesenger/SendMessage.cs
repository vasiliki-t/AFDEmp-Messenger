using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class SendMessage: IOperation
    {
        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("To :");
            string targetUser = Console.ReadLine();
            Console.WriteLine("Subject :");
            string subject = Console.ReadLine();
            Console.WriteLine("Message :");
            string body = Console.ReadLine();

            while (body.Count() > 250)
            {
                Console.WriteLine("Message text is limited to 250 characters! Try again");
                body = Console.ReadLine();
            }

            DatabaseAccess.SendMessage(Program.currentUser.UserId, targetUser, subject, body);
            Console.WriteLine("Message sent! Press any key to continue");
            Console.ReadKey();
            Program.SetOperation("MainMenu");
        }

    }
}
