using System;

namespace MSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var listener = new MSMQListener(@".\Private$\FundooNotesQueue");
            listener.MessageReceived += new MessageReceivedEventHandler(listener_MessageReceived);
            listener.Start();
            Console.WriteLine("Listening...");
            Console.ReadLine();
            listener.Stop();
        }

        public static void listener_MessageReceived(object sender, MessageEventArgs args)
        {
            Console.WriteLine(args.MessageBody);
        }

    }
}