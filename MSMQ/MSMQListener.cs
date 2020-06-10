using Experimental.System.Messaging;
using System;

namespace MSMQ
{

    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);
    public class MSMQListener
    {
        private bool _listen;
        MessageQueue _queue;

        public event MessageReceivedEventHandler MessageReceived;

        public MSMQListener(string queuePath)
        {
            _queue = new MessageQueue(queuePath);
        }

        public void Start()
        {
            _listen = true;
            _queue.Formatter = new BinaryMessageFormatter();
            _queue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);
            StartListening();
        }
        public void Stop()
        {
            _listen = false;
            _queue.PeekCompleted -= new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveCompleted);
        }
        private void StartListening()
        {
            if (!_listen)
            {
                return;
            }

            // The MSMQ class does not have a BeginRecieve method that can take in a 
            // MSMQ transaction object. This is a workaround – we do a BeginPeek and then 
            // recieve the message synchronously in a transaction.
            // Check documentation for more details
            if (_queue.Transactional)
            {
                _queue.BeginPeek();
            }
            else
            {
                _queue.BeginReceive();
            }
        }
        private void OnPeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            _queue.EndPeek(e.AsyncResult);
            MessageQueueTransaction trans = new MessageQueueTransaction();
            Message msg = null;
            try
            {
                trans.Begin();
                msg = _queue.Receive(trans);
                trans.Commit();
                StartListening();
                FireRecieveEvent(msg.Body);
            }
            catch
            {
                trans.Abort();
            }
        }
        private void FireRecieveEvent(object body)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, new MessageEventArgs(body));

                string data = body.ToString();

                SMTPSender.SendMail("vinayakushakola2@gmail.com", data);
            }
        }
        private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            Message msg = _queue.EndReceive(e.AsyncResult);
            StartListening();
            FireRecieveEvent(msg.Body);
        }
    }
    public class MessageEventArgs : EventArgs
    {
        private object _messageBody;

        private string name;

        private string mail;

        public object MessageBody
        {
            get { return _messageBody; }
        }

        public string Email
        {
            get { return name; }
        }

        public string UserName
        {
            get { return name; }
        }

        public MessageEventArgs(object body)
        {
            _messageBody = body;
        }
    }
}