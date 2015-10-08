using System;

namespace rift.net.chat.rest
{
	public class ChatData
	{
		public string messageId { get; set; }

		public long recipientId { get; set; }

		public long messageTime { get; set; }

		public long senderId { get; set; }

		public string senderName { get; set; }

		public string message { get; set; }

		public ChatChannel ChatChannel { get; set; }
	}
}