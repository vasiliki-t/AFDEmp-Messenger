using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    public class Message
    {
        public int MessageId { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public bool isRead { get; set; }
        public bool isDeletedFromInbox { get; set; }
        public bool isDeletedFromOutbox { get; set; }
    }
}
