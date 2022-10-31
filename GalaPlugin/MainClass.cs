using System;
using Exiled.API.Features;
using HarmonyLib;

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
        private static Harmony harmony;
        public static MainClass Singleton;
        
        public override void OnEnabled()
        {
            Singleton = this;
            EventHandlers = new EventHandlers(this);
            harmony = new Harmony("gala.plugin");
            harmony.PatchAll();
            
            Exiled.Events.Handlers.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RespawningTeam += EventHandlers.OnTeamSpawn;
            Exiled.Events.Handlers.Server.RoundStarted += EventHandlers.OnRoundStart;
            Exiled.Events.Handlers.Player.ChangingRole += EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.Verified += EventHandlers.OnVerified;
            Exiled.Events.Handlers.Player.InteractingDoor += EventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.InteractingElevator += EventHandlers.OnInteractingElevator;
            Exiled.Events.Handlers.Player.UsingItem += EventHandlers.OnUsingItem;
            Exiled.Events.Handlers.Player.Transmitting += EventHandlers.OnTransmitting;
            Exiled.Events.Handlers.Player.VoiceChatting += EventHandlers.OnVoiceChatting;
            Exiled.Events.Handlers.Player.Dying += EventHandlers.OnDying;
            Exiled.Events.Handlers.Map.AnnouncingNtfEntrance += EventHandlers.OnAnnouncingNtfEntrance;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RespawningTeam -= EventHandlers.OnTeamSpawn;
            Exiled.Events.Handlers.Server.RoundStarted -= EventHandlers.OnRoundStart;
            Exiled.Events.Handlers.Player.ChangingRole -= EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.Verified -= EventHandlers.OnVerified;
            Exiled.Events.Handlers.Player.InteractingDoor -= EventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.InteractingElevator -= EventHandlers.OnInteractingElevator;
            Exiled.Events.Handlers.Player.UsingItem -= EventHandlers.OnUsingItem;
            Exiled.Events.Handlers.Player.Transmitting -= EventHandlers.OnTransmitting;
            Exiled.Events.Handlers.Player.VoiceChatting -= EventHandlers.OnVoiceChatting;
            Exiled.Events.Handlers.Player.Dying -= EventHandlers.OnDying;
            Exiled.Events.Handlers.Map.AnnouncingNtfEntrance -= EventHandlers.OnAnnouncingNtfEntrance;

            harmony.UnpatchAll(harmony.Id);
            EventHandlers = null;
            Singleton = null;
            base.OnDisabled();
        }
    }
}