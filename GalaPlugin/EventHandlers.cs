using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;

namespace GalaPlugin
{
    public class EventHandlers
    {
        private readonly MainClass plugin;
        public EventHandlers(MainClass plugin) => this.plugin = plugin;
        
        public void OnWaitingForPlayers()
        {
#if DEBUG
            Log.Debug("Esperando Jugadores");
#endif
            GameObject.Find("StartRound").transform.localScale = Vector3.zero;
            Round.IsLobbyLocked = true;
            Round.IsLocked = false;
        }

        public void OnTeamSpawn(RespawningTeamEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            if (!Round.IsStarted && (GameCore.RoundStart.singleton.NetworkTimer > 1 ||
                                     GameCore.RoundStart.singleton.NetworkTimer == -2))
            {
                Timing.CallDelayed(0.5f, () =>
                {
                    if (Round.IsStarted || (GameCore.RoundStart.singleton.NetworkTimer <= 1 &&
                                            GameCore.RoundStart.singleton.NetworkTimer != -2)) return;
                    ev.Player.IsOverwatchEnabled = false;
                    ev.Player.Role.Type = RoleType.Tutorial;
                    ev.Player.IsGodModeEnabled = true;
                    Scp096.TurnedPlayers.Add(ev.Player);
                });

                Timing.CallDelayed(1.5f, () =>
                {
                    if (Round.IsStarted || (GameCore.RoundStart.singleton.NetworkTimer <= 1 &&
                                            GameCore.RoundStart.singleton.NetworkTimer != -2)) return;
                    ev.Player.Position = Room.List.First(x => x.Type == RoomType.Hcz106).Position +
                                         Vector3.up * 1.5f;
                });
            }
        }

        public void OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        public void OnRoundStart()
        {
            Round.IsLocked = true;
            Round.IsLobbyLocked = false;

            Timing.CallDelayed(5f, () =>
            {
                foreach(Player player in Player.List)
                    player.SetRole(RoleType.Tutorial);
            });
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            ev.Player.IsInvisible = false;
            ev.Player.NoClipEnabled = false;
            ev.Player.IsBypassModeEnabled = false;
            ev.Player.IsGodModeEnabled = false;
            
            if (ev.Player.Scale != Vector3.one)
                ev.Player.Scale = Vector3.one;
        }
    }
}