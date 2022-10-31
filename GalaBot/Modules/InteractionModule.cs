using System;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace GalaBot.Modules
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("ban", "Banea a un usuario")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanAsync([Summary("Usuario", "El usuario a banear")] SocketGuildUser user,
            [Summary("Razon", "La Razon del baneo")] string reason)
        {
            try
            {
                await user.SendMessageAsync(
                    $"Ha sido baneado de {Context.Guild.Name} | STAFF: {Context.User.Username} | RAZON: {reason}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Fallo al enviar el mensaje a {user.Username}");
            }
            await user.BanAsync(7, reason);
            await RespondAsync(embed: await EmbedModule.CreateBasicEmbed("Usuario baneado", $"{user.Username} Ha sido baneado por: {reason}", Color.Orange));
        }

        [SlashCommand("unban", "Desbanea a un usuario")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task UnbanAsync([Summary("UserID", "El userID a desbanear")] string id)
        {
            if (!ulong.TryParse(id, out ulong userId))
            {
                await RespondAsync("La ID no es valida");
                return;
            }
            await Context.Guild.RemoveBanAsync(userId);
            await Program.Instance.Guild.GetTextChannel(987085381877006366).SendMessageAsync(
                embed: await EmbedModule.CreateBasicEmbed("Usuario Desbaneado", $"{id} Ha sido desbaneado",
                    Color.Orange));
            await RespondAsync(embed: await EmbedModule.CreateBasicEmbed("Usuario Desbaneado", $"{id} Ha sido desbaneado", Color.Green));
        }
        [SlashCommand("kick", "Expulsa a un usuario")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickAsync([Summary("Usuario", "Usuario a Kickear")] SocketUser user,
            [Summary("Razon", "Razon del kick")] string reason)
        {
            try
            {
                await user.SendMessageAsync(
                    $"Ha sido kickeado de {Context.Guild.Name} | STAFF: {Context.User.Username} | RAZON: {reason}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Fallo al enviar el mensaje a {user.Username}");
            }
            await ((IGuildUser)user).KickAsync(reason);
            await RespondAsync(embed: await EmbedModule.CreateBasicEmbed("Usuario Kickeado", $"{user.Username} Ha sido kickeado por: {reason}", Color.Orange));
            await Program.Instance.Guild.GetTextChannel(987085381877006366).SendMessageAsync(
                embed: await EmbedModule.CreateBasicEmbed("Usuario Kickeado", $"{user.Username} Ha sido kickeado por: {reason}", Color.Orange));
        }
    }
}