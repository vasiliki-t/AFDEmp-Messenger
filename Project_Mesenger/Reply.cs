using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class Reply: IOperation
    {
        public void Execute()
        {
            User receiver = DatabaseAccess.FindUserViaUserId(Program.currentMessage.Sender);

            Console.Clear();
            string targetUser = "unknown";
            if(receiver != null)
                targetUser = receiver.Username;

            Console.WriteLine("To :" + targetUser);
            string subject = "Re:" + Program.currentMessage.Subject;
            Console.WriteLine("Subject :" + subject);

            Console.WriteLine("Message :");
            string body = Console.ReadLine();

            DatabaseAccess.SendMessage(Program.currentUser.UserId, targetUser, subject, body);

            Console.WriteLine("Message sent! Press any key to continue");
            Console.ReadKey();
            Program.SetOperation("MainMenu");
        }
    }
}
