using rift.net.chat.rest;
using RestSharp;

namespace rift.net.chat
{
	public class ChatMessageParser
	{
	    public object Parse( string jsonString )
		{
			object parsedObject = null;

			// Parse the contents into JSON
			JsonObject json = SimpleJson.DeserializeObject<JsonObject>( jsonString );

			// Get the type
			string type = json["type"].ToString();

			// Get the value
			string value = json["value"].ToString();

			switch (type) {
			case "Begin":
				parsedObject = ParseBeginMessage (value);
				break;
			case "Login":
				parsedObject = ParseLoginLogOutMessage (value, true);
				break;
			case "Logout":
				parsedObject = ParseLoginLogOutMessage (value, false);
				break;
			case "GuildChat":
                parsedObject = ParseChat(value, ChatChannel.Guild);
                break;
            case "WhisperChat":
				parsedObject = ParseChat (value, ChatChannel.Whisper);
				break;
            case "OfficerChat":
                parsedObject = ParseChat(value, ChatChannel.Officer);
                break;
            }

			return parsedObject;
		}

		private BeginData ParseBeginMessage(string jsonObject)
		{
			return SimpleJson.DeserializeObject<BeginData> (jsonObject);
		}

		private LoginLogoutData ParseLoginLogOutMessage( string jsonObject, bool isLoggingIn )
		{
			var data = SimpleJson.DeserializeObject<LoginLogoutData> (jsonObject);

			data.login = isLoggingIn;

			return data;
		}

		private ChatData ParseChat( string jsonObject, ChatChannel chatChannel)
		{
		    var data = SimpleJson.DeserializeObject<ChatData>(jsonObject);

		    data.ChatChannel = chatChannel;

		    return data;
		}
	}
}

