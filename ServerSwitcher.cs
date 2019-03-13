using Rocket.Core.Plugins;
using System;
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


    }
}
