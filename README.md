# ServerSwitcher
Allows users to select and join a Server from a list whilst in a server.

## Configuration
- `Servers` - A list of Servers with a custom Name and their IP, Port, Password, Permission and Delay.
  - `Name` - The custom name for the Server that users with use when they do `/server [Server Name]`.
  - `IP` - The IP for the Server. Make sure this is a valid IPV4 address, not one such as `play.XXXX.com`.
  - `Port` - The Port for the Server, such as `27015`.
  - `Password` - The Password for the Server, leave this blank or with any password if the desired Server has no password.
  - `Permission` - The Permission a User has to have to be able to access the Server. See the `Permissions` section below.
  - `Delay` - The Delay, in seconds, before a User is moved to that Server.

## Commands:
- `/Servers` - Displays a list of Servers in the configuration file. (Shows Name & IP.)
  - Only Servers that a User has permission to join will be displayed.
  
- `/Server [Name]` - Selects a Server to join from the list.
  - The `Name` is the custom name you enter in the configuration.

## Permissions:
- `serverswitcher.servers` - Users with this permission can execute the `/Servers` command.
- `serverswitcher.server` - Users with this permission can execute the `/Server` command.
- `serverswitcher.server.{SeverPermission}` - Users with this permission can execute the `/Server` command with the selected Servers.
- `serverswitcher.server.*` - Users with this permission can execute the `/Server` command with the all Servers.

## Demonstration:
https://www.youtube.com/watch?v=yQFYHYOUL1U
