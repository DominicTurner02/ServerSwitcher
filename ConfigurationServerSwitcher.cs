using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ServerSwitcher
{
    public class ConfigurationServerSwitcher : IRocketPluginConfiguration
    {
        public bool RocketRegionsSupport;
        [XmlArrayItem(ElementName = "Server")]
        public List<Server> Servers;

        public void LoadDefaults()
        {
            RocketRegionsSupport = false;
            Servers = new List<Server>()
            {
                new Server() { Name = "Server1", IP = "127.0.0.1", Port = 27015, Password = "password", Permission = "server1", Delay = 5f, CanBeUsedInRocketRegion = true, IgnoreDelayInRocketRegion = false}
            };
        }
    }

    public class Server
    {
        public Server() { }

        public string Name;
        public string IP;
        public ushort Port;
        public string Password;
        public string Permission;
        public float Delay;
        public bool CanBeUsedInRocketRegion;
        public bool IgnoreDelayInRocketRegion;

    }
}
