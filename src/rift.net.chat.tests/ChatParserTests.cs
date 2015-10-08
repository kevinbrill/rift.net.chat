using System;
using NUnit.Framework;
using rift.net.chat;
using rift.net.chat.rest;

namespace rift.net.chat.tests
{
	[TestFixture()]
	public class ChatParserTests
	{
		const long charactedId = 218846794414042822;

		const string begin = "{\"type\":\"Begin\",\"value\":{\"timeout\":500000}}";

		const string login_game = "{\"type\":\"Login\",\"value\":{\"characterId\":218846794414042822,\"game\":true}}";
		const string login_web = "{\"type\":\"Login\",\"value\":{\"characterId\":218846794414042822,\"game\":false}}";

		const string logout_game = "{\"type\":\"Logout\",\"value\":{\"characterId\":218846794414042822,\"game\":true}}";
		const string logout_web = "{\"type\":\"Logout\",\"value\":{\"characterId\":218846794414042822,\"game\":false}}";

		const string guild_chat = "{\"type\":\"GuildChat\",\"value\":{\"messageId\":\"417747648\",\"recipientId\":219691219334217446,\"messageTime\":1423070407,\"senderId\":218846794414042822,\"senderName\":\"Bruun\",\"message\":\"This is a test\"}}";

        const string whisper_chat = "{\"type\":\"WhisperChat\",\"value\":{\"messageId\":\"417747648\",\"recipientId\":218846794414042821,\"messageTime\":1423070407,\"senderId\":218846794414042822,\"senderName\":\"Bruun\",\"message\":\"Hello handsome\"}}";

		ChatMessageParser parser = new ChatMessageParser();

		[Test()]
		public void Verify_That_Begin_Message_Returns_The_Correct_TimeOut()
		{
			var data = parser.Parse (begin);

			Assert.That (data, Is.Not.Null);
			Assert.That (data, Is.InstanceOf<BeginData> ());

			var beginData = (BeginData)data;

			Assert.That (beginData.timeout, Is.EqualTo (500000));
		}

		[TestCase(login_game, true)]
		[TestCase(login_web, false)]
		public void Verify_That_Login_Parses_Properly(string login, bool isInGame)
		{
			var data = parser.Parse (login);

			Assert.That (data, Is.Not.Null);
			Assert.That (data, Is.InstanceOf<LoginLogoutData> ());

			var loginLogoutData = (LoginLogoutData)data;

			Assert.That (loginLogoutData.characterId, Is.EqualTo (charactedId));
			Assert.That (loginLogoutData.login, Is.True);
			Assert.That (loginLogoutData.game, Is.EqualTo (isInGame));
		}

		[TestCase(logout_game, true)]
		[TestCase(logout_web, false)]
		public void Verify_That_LogOut_Parses_Properly(string logout, bool isInGame)
		{
			var data = parser.Parse (logout);

			Assert.That (data, Is.Not.Null);
			Assert.That (data, Is.InstanceOf<LoginLogoutData> ());

			var loginLogoutData = (LoginLogoutData)data;

			Assert.That (loginLogoutData.characterId, Is.EqualTo (charactedId));
			Assert.That (loginLogoutData.login, Is.False);
			Assert.That (loginLogoutData.game, Is.EqualTo (isInGame));
		}

		[Test()]
		public void Verify_That_Guild_Chat_Parses_Properly()
		{
			var data = parser.Parse (guild_chat);

			Assert.That (data, Is.Not.Null);
			Assert.That (data, Is.InstanceOf<ChatData> ());

			var chatData = (ChatData)data;

			Assert.That (chatData.recipientId, Is.EqualTo (219691219334217446));
			Assert.That (chatData.message, Is.Not.Null.And.EqualTo ("This is a test"));
			Assert.That (chatData.messageId, Is.Not.Null.And.Not.Empty);
			Assert.That (chatData.messageTime, Is.GreaterThan (0));
			Assert.That (chatData.senderId, Is.EqualTo (charactedId));
			Assert.That (chatData.senderName, Is.EqualTo ("Bruun"));
            Assert.That(chatData.ChatChannel, Is.EqualTo(ChatChannel.Guild));
		}

        [Test()]
        public void Verify_That_Whisper_Chat_Parses_Properly()
        {
            var data = parser.Parse(whisper_chat);

            Assert.That(data, Is.Not.Null);
            Assert.That(data, Is.InstanceOf<ChatData>());

            var chatData = (ChatData)data;

            Assert.That(chatData.recipientId, Is.EqualTo(218846794414042821));
            Assert.That(chatData.message, Is.Not.Null.And.EqualTo("Hello handsome"));
            Assert.That(chatData.messageId, Is.Not.Null.And.Not.Empty);
            Assert.That(chatData.messageTime, Is.GreaterThan(0));
            Assert.That(chatData.senderId, Is.EqualTo(charactedId));
            Assert.That(chatData.senderName, Is.EqualTo("Bruun"));
            Assert.That(chatData.ChatChannel, Is.EqualTo(ChatChannel.Whisper));
        }
	}
}

