using System;
using Exiled.API.Features;

namespace GalaPlugin
{
    public class MainClass : Plugin<Config>
    {
        public override string Author { get; } = "Team Gala ESP";
        public override string Name { get; } = typeof(MainClass).Namespace;
        public override string Prefix { get; } = typeof(MainClass).Namespace.ToLower();
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 3, 0);
        
        public EventHandlers EventHandlers { get; private set; }
        
        public override void OnEnabled()
        {
            EventHandlers = new EventHandlers(this);
            
            Exiled.Events.Handlers.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RespawningTeam += EventHandlers.OnTeamSpawn;
            Exiled.Events.Handlers.Player.ChangingRole += EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.Verified += EventHandlers.OnVerified;
            Exiled.Events.Handlers.Map.AnnouncingNtfEntrance += EventHandlers.OnAnnouncingNtfEntrance;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RespawningTeam -= EventHandlers.OnTeamSpawn;
            Exiled.Events.Handlers.Player.ChangingRole -= EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.Verified -= EventHandlers.OnVerified;
            Exiled.Events.Handlers.Map.AnnouncingNtfEntrance -= EventHandlers.OnAnnouncingNtfEntrance;

            EventHandlers = null;
            base.OnDisabled();
        }
    }
}