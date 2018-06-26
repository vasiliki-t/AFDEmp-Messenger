using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class ReadMessage: IOperation
    {
        public void Execute()
        {
            if (Program.isOutbox)
            {
                string[] operations = new string[2] { "Delete", "Back" };
                int index = MenuUtilities.SelectOperations(operations, ShowMessage);

                if (index == 0)
                {
                    DatabaseAccess.DeleteMessage(Program.currentMessage.MessageId, true);
                }

                Program.SetOperation("Outbox");
            }
            else
            {
                string[] operations = new string[3] { "Reply", "Delete", "Back" };
                int index = MenuUtilities.SelectOperations(operations, ShowMessage);

                switch (index)
                {
                    case 0:
                        Program.SetOperation("Reply");
                        break;
                    case 1:
                        DatabaseAccess.DeleteMessage(Program.currentMessage.MessageId, false);
                        Program.SetOperation("Inbox");
                        break;
                    case 2:
                        Program.SetOperation("Inbox");
                        break;
                }
            }
        }

        void ShowMessage()
        {
            Console.ResetColor();
            Message m = Program.currentMessage;

            string senderName = "unknown";
            User sender = DatabaseAccess.FindUserViaUserId(m.Sender);
            if (sender != null)
                senderName = sender.Username;


            string header = $"From : {senderName} \nTo : {Program.currentUser.Username} \nDate: { m.Date} \nSubject : {m.Subject} \n";
            Console.WriteLine(header);
            Console.WriteLine(m.Body);
            Console.WriteLine(" ");
        }
    }
}
