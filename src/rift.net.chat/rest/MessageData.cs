using System;
using RestSharp;

namespace rift.net.chat
{
	public class MessageData
	{
		public string type {
			get;
			set;
		}

		public JsonObject value {
			get;
			set;
		}
	}
}

