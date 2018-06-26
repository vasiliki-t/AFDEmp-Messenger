using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    static class DatabaseAccess
    {
        static public User ValidateLogin(string username, string password)
        {
            User result = null;
            User targetUser = FindUserViaUsername(username);

            if (targetUser != null)
            {
                string passwordHash = getHashSha256(password + targetUser.Salt);

                if (string.Equals(targetUser.Password, passwordHash))
                {
                   result = targetUser;                    
                }
            }

            return result;
        }

        static public User FindUserViaUsername(string username)
        {
            User result = null;

            using (var db = new MyContext())
            {
                result = db.Users.Where(x => x.Username == username).SingleOrDefault();
            }

            return result;
        }

        static public User FindUserViaUserId(int id)
        {
            User result = null;

            using (var db = new MyContext())
            {
                result = db.Users.Find(id);
            }

            return result;
        }
        
        static public void AddUser(string username, string password, int role)
        {
            User user = new User()
            {
                Username = username,
                Role = role
            };

            string salt = RandomString(10);
            string hashedSalt = getHashSha256(salt);
            string saltedPassword = password + hashedSalt;
            user.Password = getHashSha256(saltedPassword);
            user.Salt = hashedSalt;
            
            using (var db = new MyContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        static public void DeleteUser(int userid)
        {
            using (var db = new MyContext())
            {
                User u = db.Users.Find(userid);
                db.Users.Remove(u);
                db.SaveChanges();
            }     
        }

        static public List<User> GetAllUsers()
        {
            using (var db = new MyContext())
            {
                return db.Users.ToList();
            }
        }

        static public void UpdateUser(User user)
        {
            using (var db = new MyContext())
            {
                User originalUser = db.Users.Find(user.UserId);
                originalUser.Role = user.Role;
                db.SaveChanges();
            }
        }
        
        static public List<Message> FindUsersInboxMessages(int userId)
        {
            using (var db = new MyContext())
            {
                return db.Messages.Where(x => x.Receiver == userId && x.isDeletedFromInbox == false).ToList();
            }
        }

        static public List<Message> FindUsersOutboxMessages(int userId)
        { 
            using (var db = new MyContext())
            {
                return db.Messages.Where(x => x.Sender == userId && x.isDeletedFromOutbox == false).ToList();
            }   
        }

        static public void DeleteMessage(int messageId, bool isOutbox)
        {
            using (var db = new MyContext())
            {
                Message message = db.Messages.Find(messageId);
                if (isOutbox)
                    message.isDeletedFromOutbox = true;
                else
                    message.isDeletedFromInbox = true;

                if(message.isDeletedFromInbox && message.isDeletedFromOutbox)
                    db.Messages.Remove(message);
                db.SaveChanges();
            }
        }

        static public void SendMessage(int senderId, string receiver, string subject, string body)
        {
            Message message = new Message();
            message.Subject = subject;
            message.Body = body;
            message.Sender = senderId;
            message.Date = DateTime.Now.ToShortDateString();

            User targetReceiver = FindUserViaUsername(receiver);
            if(targetReceiver != null)
            {
                message.Receiver = targetReceiver.UserId;
            }
            
            using (var db = new MyContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();

                string senderName = "unknown";
                User sender = FindUserViaUserId(senderId);
                if (sender != null)
                    senderName = sender.Username;

                string log = $"{message.Date} - From : {senderName} - To : {receiver} - Subject : {subject} - Body : {body}";
                log += Environment.NewLine;
                Program.Log(log);
            }
            
        }

        static public void UpdateMessage(Message updatedMessage)
        {
            using (var db = new MyContext())
            {
                Message originalMessage = db.Messages.Find(updatedMessage.MessageId);
                originalMessage.Body = updatedMessage.Body;
                db.SaveChanges();
            }
        }

        static public void MarkMessageAsRead(int messageId)
        {
            using (var db = new MyContext())
            {
                Message originalMessage = db.Messages.Find(messageId);
                originalMessage.isRead = true;
                db.SaveChanges();
            }
            
        }

        //Encryption
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
        //Salt before going through hash encryption
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefg";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
