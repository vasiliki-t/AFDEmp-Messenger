using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class Outbox : IOperation
    {
        public void Execute()
        {
            Program.isOutbox = true;
            List<Message> allMessages = DatabaseAccess.FindUsersOutboxMessages(Program.currentUser.UserId);
            List<string> elements = new List<string>();

            for (int i = 0; i < allMessages.Count; i++)
            {
                Message m = allMessages[i];

                string receiverName = "unknown";
                User receiver = DatabaseAccess.FindUserViaUserId(m.Receiver);
                if (receiver != null)
                    receiverName = receiver.Username;

                elements.Add($"{m.Date} - {receiverName} - {m.Subject}");
            }

            elements.Add("Back");

            int index = MenuUtilities.SelectOperations(elements.ToArray());
            if (index == elements.Count - 1)
            {
                Program.SetOperation("MainMenu");
            }
            else
            {
                Program.currentMessage = allMessages[index];
                Program.SetOperation("ReadMessage");
            }
        }
    }
}
