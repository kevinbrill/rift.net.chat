using System;

namespace rift.net.chat.Models
{
    public class Message
    {
        public string Id { get; set; }

        public long RecipientId { get; set; }

        public DateTime ReceiveDateTime { get; set; }

        public Sender Sender { get; set; }

        public string Text { get; set; }
    }
}