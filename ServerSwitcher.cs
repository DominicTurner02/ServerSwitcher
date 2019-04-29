using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections;
using System.Net;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;
using RocketRegions;
using RocketRegions.Model;
using System.Collections.Generic;
using RocketRegions.Model.Flag;
using ServerSwitcher.Flags;

namespace ServerSwitcher
{
    public class ServerSwitcher : RocketPlugin<ConfigurationServerSwitcher>
    {
        public static ServerSwitcher Instance;
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public List<Region> Regions => RegionsPlugin.Instance?.Configuration?.Instance?.Regions ?? new List<Region>();

        protected override void Load()
        {         
            base.Load();
            Instance = this;
            Logger.Log("");
            Logger.Log("Loading ServerSwitcher, made by Mr.Kwabs.", ConsoleColor.Yellow);
            if (Configuration.Instance.RocketRegionsSupport)
            {
                RegionFlag.RegisterFlag("EnterServerSwitch", typeof(EnterServerSwitchFlag));
            }
            U.Events.OnPlayerConnected += OnPlayerConnected;
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
                if (Configuration.Instance.RocketRegionsSupport)
                {
                    Logger.Log($"Can be used in Rocket Region: {Server.CanBeUsedInRocketRegion}", ConsoleColor.Cyan);
                }
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

        private void OnPlayerConnected(UnturnedPlayer uPlayer)
        {
            if (Configuration.Instance.RocketRegionsSupport)
            {
                uPlayer.GetComponent<SwitchPlayer>().JoinUnix = CurrentUnix();
            }
        }

        public long CurrentUnix() { return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds; }

        public void StartSwitch(Server Server, UnturnedPlayer uPlayer, bool Delay = false) { StartCoroutine(DelaySwitch(Server, uPlayer, Delay)); }

        private IEnumerator DelaySwitch(Server Server, UnturnedPlayer uPlayer, bool Delay)
        {
            if (Delay)
            {
                UnturnedChat.Say(uPlayer, Translate("direct_transfer_countdown", Server.Delay), Color.yellow);
            }
            string serverAddress = "";
            bool isIPV4 = false;

            if (IPAddress.TryParse(Server.IP, out IPAddress tAddress))
            {
                switch (tAddress.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        serverAddress = tAddress.ToString();
                        isIPV4 = true;
                        break;
                }
            }
            if (!isIPV4)
            {
                serverAddress = Dns.GetHostAddresses(Server.IP)[0].ToString();
            }
            if (Delay)
            {
                yield return new WaitForSeconds(Server.Delay);
            }
            uPlayer.Player.sendRelayToServer(Parser.getUInt32FromIP(serverAddress), Server.Port, Server.Password);
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                    {"direct_transfer_countdown", "You will be moved in {0} second(s)"},
                    {"direct_no_view_servers", "There are no Servers to view!"},
                    {"direct_no_display_servers", "There are no Servers to display! Check you have the correct Permissions!"},
                    {"direct_no_permission", "You do not have permission to go to this Server!"},
                    {"direct_server_doesnt_exist", "The Server {0} does not exist!"},
                    {"direct_incorrect_syntax_server", "/server [Server Name]"},
                    {"direct_incorrect_syntax_servers", "/servers"}
                };
            }

        }

    }

    
}
