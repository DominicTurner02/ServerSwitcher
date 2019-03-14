using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace ServerSwitcher
{
    public class ServerSwitcher : RocketPlugin<ConfigurationServerSwitcher>
    {
        public static ServerSwitcher Instance;

        protected override void Load()
        {
            base.Load();
            Instance = this;
            Logger.Log("");
            Logger.Log("Loading ServerSwitcher, made by Mr.Kwabs.", ConsoleColor.Yellow);
            Logger.Log("");
            Logger.Log($"Server List ({Configuration.Instance.Servers.Count} Total): ", ConsoleColor.Cyan);
            Logger.Log("");
            foreach (Server Server in Configuration.Instance.Servers)
            {
                Logger.Log($"Name: {Server.Name}", ConsoleColor.Cyan);
                Logger.Log($"IP & Port: {Server.IP}:{Server.Port}", ConsoleColor.Cyan);
                Logger.Log($"Password: {Server.Password}", ConsoleColor.Cyan);
                Logger.Log($"Permission: serverswitcher.server.{Server.Permission}", ConsoleColor.Cyan);
                Logger.Log($"Delay: {Server.Delay}", ConsoleColor.Cyan);
                Logger.Log("");
            }                  
            Logger.Log("Successfully loaded ServerSwitcher, made by Mr.Kwabs.", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Logger.Log("Unloading ServerSwitcher, made by Mr.Kwabs.", ConsoleColor.Red);
            Instance = null;
            base.Unload();
        }

        public void StartSwitch(Server Server, UnturnedPlayer uPlayer) { StartCoroutine(DelaySwitch(Server, uPlayer)); }

        private IEnumerator DelaySwitch(Server Server, UnturnedPlayer uPlayer)
        {
            UnturnedChat.Say(uPlayer, $"You will be moved in {Server.Delay} second(s).", Color.yellow);
            yield return new WaitForSeconds(Server.Delay);
            uPlayer.Player.sendRelayToServer(Parser.getUInt32FromIP(Server.IP), Server.Port, Server.Password);
        }

    }
}
