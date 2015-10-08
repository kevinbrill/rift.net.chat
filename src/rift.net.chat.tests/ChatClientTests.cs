using System;
using NUnit.Framework;
using System.Configuration;
using System.Linq;
using rift.net.Models;
using rift.net.chat;

namespace rift.net.chat.tests
{
	[TestFixture()]
	public class ChatClientTests
	{
		private RiftChatClient client;
		private Character character;

		[TestFixtureSetUp()]
		public void SetUp()
		{
			var username = ConfigurationManager.AppSettings ["username"];
			var password = ConfigurationManager.AppSettings ["password"];
			var characterName = ConfigurationManager.AppSettings ["characterName"];

			var sessionFactory = new SessionFactory ();

			var session = sessionFactory.Login (username, password);

			var securedClient = new RiftClientSecured (session);

			character = securedClient.ListCharacters ().FirstOrDefault (x => x.FullName == characterName);

			client = new RiftChatClient (session, character);		
		}

		[Test()]
		public void Verify_Starting_Chat_Client()
		{
			client.Connect();

			client.Listen ();

			client.Stop ();
		}

		[Test()]
		public void Verify_That_Character_Has_Chat_History()
		{
			Assume.That (character, Is.Not.Null);

			var chatHistory = client.ListChatHistory();

			Assert.That (chatHistory, Is.Not.Null.And.Not.Empty);
			Assert.That (chatHistory.All (x => x.RecipientId.ToString() == character.Id), Is.True);
		}

		[Test()]
		public void Verify_That_Character_Guild_Has_Chat_History()
		{
			Assume.That (character, Is.Not.Null);
			Assume.That (character.Guild, Is.Not.Null, "The provided character is not a member of a guild");

			var chatHistory = client.ListGuildChatHistory();

			Assert.That (chatHistory, Is.Not.Null.And.Not.Empty);
			Assert.That (chatHistory.All (x => x.RecipientId == character.Guild.Id), Is.True);
		}
	}
}

