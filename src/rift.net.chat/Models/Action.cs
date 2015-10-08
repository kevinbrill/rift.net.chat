using System;
using rift.net.Models;

namespace rift.net.chat.Models
{
    public class Action
    {
        public Contact Character { get; set; }

        public StateAction State { get; set; }

		public Location Location { get; set; }
	}
}