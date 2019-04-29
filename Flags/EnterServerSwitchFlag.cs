using System;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using RocketRegions.Model.Flag;

namespace ServerSwitcher.Flags
{
    public class EnterServerSwitchFlag : StringFlag
    {
        public override string Description => "Switches server when you enter a region";
        public override string Usage => "<Server Name>";
        public override void UpdateState(List<UnturnedPlayer> players)
        {

        }

        public override void OnRegionEnter(UnturnedPlayer uPlayer)
        {
            if ((ServerSwitcher.Instance.CurrentUnix() - uPlayer.GetComponent<SwitchPlayer>().JoinUnix) < 3)
            {
                return;
            }

            var pValue = GetValue<string>(Region.GetGroup(uPlayer));
            if (pValue == null)
                return;

            bool serverExists = false;
            Server desiredServer = null;

            foreach (Server uServer in ServerSwitcher.Instance.Configuration.Instance.Servers)
            {
                if (uServer.Name.ToLower() == pValue.ToLower())
                {
                    desiredServer = uServer;
                    serverExists = true;
                    break;
                }
            }

            if (serverExists)
            {
                if (desiredServer.CanBeUsedInRocketRegion)
                {
                    ServerSwitcher.Instance.StartSwitch(desiredServer, uPlayer, desiredServer.IgnoreDelayInRocketRegion);
                } else
                {
                    Rocket.Core.Logging.Logger.Log($"Server {desiredServer.Name} can not be used in a Rocket Region!", ConsoleColor.Red);
                }
            } else
            {
                Rocket.Core.Logging.Logger.Log($"Server {pValue} does not exist for player {uPlayer.DisplayName}!", ConsoleColor.Red);
            }
        }

        public override void OnRegionLeave(UnturnedPlayer player)
        {

        }
    }
}
