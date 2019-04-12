using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSwitcher
{
    public class CommandServer : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "server";

        public string Help => "Moves you to a different Server";

        public string Syntax => "/server [Server Name]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "serverswitcher.server" };


        public void Execute(IRocketPlayer rCaller, string[] Command)
        {
            UnturnedPlayer uPlayer = (UnturnedPlayer)rCaller;

            if (Command.Length > 1 || Command.Length == 0) { UnturnedChat.Say(uPlayer, ServerSwitcher.Instance.Translate("direct_incorrect_syntax_server"), Color.red); return; }

            if (ServerSwitcher.Instance.Configuration.Instance.Servers.Count > 0)
            {
                foreach (Server Server in ServerSwitcher.Instance.Configuration.Instance.Servers)
                {
                    if (Server.Name.ToLower() == Command[0].ToLower())
                    {
                        if (uPlayer.HasPermission("serverswitcher.server.*") || uPlayer.HasPermission($"serverswitcher.server.{Server.Permission}"))
                        {
                            ServerSwitcher.Instance.StartSwitch(Server, uPlayer);
                            return;
                        } else
                        {
                            UnturnedChat.Say(uPlayer, ServerSwitcher.Instance.Translate("direct_no_permission"), Color.red);
                            return;
                        }                        
                    }
                }
                UnturnedChat.Say(uPlayer, ServerSwitcher.Instance.Translate("direct_server_doesnt_exist", Command[0]), Color.red);
                return;
            } else
            {
                UnturnedChat.Say(uPlayer, ServerSwitcher.Instance.Translate("direct_no_view_servers", Command[0]), Color.red);
                return;
            }
        }
    }
}
