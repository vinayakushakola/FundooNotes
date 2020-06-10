using Experimental.System.Messaging;

namespace CommonLayer.Models
{
    public class MSMQSender
    {
        public static void SendToMSMQ(string email, string token)
        {
            string path = @".\Private$\FundooNotesQueue";
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(path))
            {
                messageQueue = new MessageQueue(path);
            }
            else
            {
                messageQueue = MessageQueue.Create(path);
            }
            messageQueue.Label = "FundooNotes Mail Sending...";
            string resetPassword = "Reset Password Token: " + token;
            Message message = new Message(resetPassword);
            message.Formatter = new BinaryMessageFormatter();
            messageQueue.Send(message, email);
        }
    }
}
