##Rift.Net.Chat

###About

rift.net.chat is a managed wrapper around the chat functionality of [Trion World's](http://www.trionworlds.com) MMO [Rift](http://www.riftgame.com).

For details on the parent project, please refer to [Rift.Net](http://github.com/kevinbrill/rift.net)

###Features

Currently, the following features of the API are supported:

#####Authenticated
* Connect and talk in guild chat
* Connect and talk in officer chat
* Send whispers to other players

###Limitations
Due to limitations in the underlying REST API, you currently cannot receive whispers while not logged into the game.  This means that you most likely will not be able to use this to communicate with people not in your guild.

###Install
From the package manager console, type in

    Install-Package rift.net.chat

###Using

The core of connecting to the chat infrastructure is encapsulated in the *RiftChatClient*.  The client manages connecting to the server, and communicating all messages received via custom events.

To instantiate a new instance of the chat client, you'll need to provide a session, and an instance of the character to whom this client will apply.

	// Create a new session factory
	var sessionFactory = new SessionFactory ();

	// Login using provided username and password
	var session = sessionFactory.Login (username, password);

	// Create a new secured client 
	var client = new RiftClientSecured (session);
	
	// Hello handsome
	var bruun = client.ListCharacters().FirstOrDefault( x=>x.FullName == "Bruun@Wolfsbane" );
	
	// Create a chat client
	var chatClient = new RiftChatClient( session, bruun );
	
Once, you get a chat client, the next step is to connect, which will mark your character online, and pull a list of friends and guildies.

    // Connect
    chatClient.Connect();
    
    // Hook into the Connecting and Connected events
	chatClient.Connecting += (object sender, EventArgs e) => {
		Debug.WriteLine( "Connecting to chat server..." );
	};

	chatClient.Connected += (object sender, EventArgs e) => {
		Debug.WriteLine( "Connected!" );
	};

Next, before we start listening for events, let's wire up to receive the events.

	chatClient.GuildChatReceived += (object sender, Message e) => {
		Debug.WriteLine(string.Format("{0}: {1}", e.Sender.Name, e.Text));
	};

	chatClient.WhisperReceived += (object sender, Message e) => {
        Debug.WriteLine(string.Format("{0}: {1}", e.Sender.Name, e.Text));
	};

	chatClient.OfficerChatReceived += (object sender, Message e) => {
        Debug.WriteLine(string.Format("{0}: {1}", e.Sender.Name, e.Text));
	};

	chatClient.Login += (object sender, rift.net.Models.Action e) => {
		Debug.WriteLine(string.Format("{0} has come online.", e.Character.Name));
	};

	chatClient.Logout += (object sender, rift.net.Models.Action e) => {
		Debug.WriteLine(string.Format("{0} has gone offline.", e.Character.Name));
	};
	
Next, start listening.

    // Listen
    chatClient.Listen();
    
This call spins up a background thread that will connect to the Rift chat server, and listen for incoming messages.  These messages are then relayed back via events.

To talk in guild chat, use the chat client and call the **SendGuildMessage** method:

    // Say hello
    chatClient.SendGuildMessage( "Hello everyone.  Hope you're doing fabulous!" );
    
Once you're finished with the chat client, the proper way to dispose is to call the **Stop** method.  This will disconnect from the chat server, and take that character offline.

    // All done!
    chatClient.Stop();

###Contributing
If there's a feature that you'd like to see, you can create an issue.  If there's something that you've fixed or improved, then create a pull request.  Pull requests are great!

You can reach me in game at **Bruun@Wolfsbane**.  If you're looking for a home in Telara, check out **Grievance** at [http://grievancegaming.org/](http://grievancegaming.org/)

##Legal Stuff
I am in no way affiliated or employed by Trion Worlds, nor do I receive any compensation from them for my work on this project or any other project.

I am also not responsible for any damages that are incurred by using this library.  Any usage should be done at your own risk.