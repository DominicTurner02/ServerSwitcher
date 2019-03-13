using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSwitcher
{
    public class CommandServers : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "servers";

        public string Help => "Lists the available servers";

        public string Syntax => "/servers";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "serverswitcher.servers" };


        public void Execute(IRocketPlayer rCaller, string[] Command)
        {
            UnturnedPlayer uPlayer = (UnturnedPlayer)rCaller;

            if (Command.Length > 0) { UnturnedChat.Say(uPlayer, Syntax, Color.red); return; }

            if (ServerSwitcher.Instance.Configuration.Instance.Servers.Count > 0)
            {
                int Count = 0;
                foreach (Server Server in ServerSwitcher.Instance.Configuration.Instance.Servers)
                {
                    Count++;
                    UnturnedChat.Say(uPlayer, $"[{Count}/{ServerSwitcher.Instance.Configuration.Instance.Servers.Count}] {Server.Name} [{Server.IP}]");
                }
            } else
            {
                UnturnedChat.Say(uPlayer, "The are no Servers to view!", Color.red);
                return;
            }
            
        }

    }
}
