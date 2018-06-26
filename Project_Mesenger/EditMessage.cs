using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    class EditMessage : IOperation
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
                    Console.WriteLine("You can't edit admin user's messages! Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    SelectInboxOutbox(allUsers[index]);
                }
            }

        }

        void SelectInboxOutbox(User user)
        {
            string[] menuOptions = new string[2] { "Inbox", "Outbox" };
            int index = MenuUtilities.SelectOperations(menuOptions);
            if (index == 0)
            {
                SelectMessage(user, false);
            }
            else
            {
                SelectMessage(user, true);
            }
        }

        void SelectMessage(User user, bool isOutbox)
        {
            List<Message> allMessages;
            if (isOutbox)
            {
                allMessages = DatabaseAccess.FindUsersOutboxMessages(user.UserId);
            }
            else
            {
                allMessages = DatabaseAccess.FindUsersInboxMessages(user.UserId);
            }

            List<string> elements = new List<string>();

            for (int i = 0; i < allMessages.Count; i++)
            {
                Message m = allMessages[i];

                string senderName = "unknown";
                User sender = DatabaseAccess.FindUserViaUserId(m.Sender);
                if (sender != null)
                    senderName = sender.Username;
                string receiverName = "unknown";
                User receiver = DatabaseAccess.FindUserViaUserId(m.Receiver);
                if (receiver != null)
                    receiverName = receiver.Username;

                elements.Add($"{m.Date} - From: {senderName} - To: {receiverName} - {m.Subject}");
            }

            elements.Add("Back");

            int index = MenuUtilities.SelectOperations(elements.ToArray());
            if (index == elements.Count - 1)
            {
                Program.SetOperation("Edit Message");
            }
            else
            {
                if (Program.currentUser.Role == 2)
                {
                    EditOrDelete(allMessages[index],isOutbox);
                }
                else
                {
                    EditingMessage(allMessages[index]);
                }
            }
        }

        void EditOrDelete(Message message, bool isOutbox)
        {
            string[] menuOptions = new string[2] { "Edit Message", "Delete Message" };
            int index = MenuUtilities.SelectOperations(menuOptions);
            if (index == 0)
            {
                EditingMessage(message);
            }
            else
            {
                DatabaseAccess.DeleteMessage(message.MessageId,isOutbox);
                Console.ResetColor();
                Console.WriteLine("Message deleted! Press any key to continue");
                Console.ReadKey();
                Program.SetOperation("MainMenu");
            }
        }

        void EditingMessage(Message m)
        {
            string senderName = "unknown";
            User sender = DatabaseAccess.FindUserViaUserId(m.Sender);
            if (sender != null)
                senderName = sender.Username;
            string receiverName = "unknown";
            User receiver = DatabaseAccess.FindUserViaUserId(m.Receiver);
            if (receiver != null)
                receiverName = receiver.Username;

            Console.Clear();
            Console.WriteLine("From :");
            Console.WriteLine(senderName);
            Console.WriteLine("To :");
            Console.WriteLine(receiverName);

            Console.WriteLine("Subject :");
            Console.WriteLine(m.Subject);
            Console.WriteLine("Original Message :");
            Console.WriteLine(m.Body);
            Console.WriteLine("New Message :");
            string newBody = Console.ReadLine();
            m.Body = newBody;

            DatabaseAccess.UpdateMessage(m);

            Console.WriteLine("Message Edited! Press any key to continue");
            Console.ReadKey();
            Program.SetOperation("MainMenu");
        }
    }
}
