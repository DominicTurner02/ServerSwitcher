using Rocket.Unturned.Player;

namespace ServerSwitcher
{
    public class SwitchPlayer : UnturnedPlayerComponent
    {
        public long JoinUnix { get; set; }

        public SwitchPlayer()
        {
            JoinUnix = 0;
        }

    }
}
