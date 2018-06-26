using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class Inbox: IOperation
    {
        public void Execute()
        {
            Program.isOutbox = false;
            
            List<Message> allMessages = DatabaseAccess.FindUsersInboxMessages(Program.currentUser.UserId);

            List<string> elements = new List<string>();

            
            for (int i = 0; i < allMessages.Count; i++)
            {
                Message m = allMessages[i];

                string senderName = "unknown";
                User sender = DatabaseAccess.FindUserViaUserId(m.Sender);
                if (sender != null)
                    senderName = sender.Username;

                elements.Add($"{m.Date} - {senderName} - {m.Subject}");
            }

            elements.Add("Back")    ;

         
            int index = MenuUtilities.SelectOperations(elements.ToArray());
            if (index == elements.Count - 1)
            {
                Program.SetOperation("MainMenu");
            }
            else
            {
                Program.currentMessage = allMessages[index];
                DatabaseAccess.MarkMessageAsRead(allMessages[index].MessageId);
                Program.SetOperation("ReadMessage");
            }
        }
    }
}
