using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace GalaBot.Modules
{
    public class UtilsModule
    {
        public static string Footer => $"SCPSL-Gala | {Assembly.GetExecutingAssembly().GetName().Version} | - Team GalaSL";
        
        public static async Task OnUserJoin(SocketGuildUser _user)
        {
            try
            {
                var users = Program.Instance.Guild.Users;

                var embed = new EmbedBuilder()
                    .WithColor(Color.Green)
                    .WithAuthor("Un Usuario a entrado al servidor")
                    .WithDescription("**Usuario:** " + _user.Mention + "\n**ID:** " + _user.Id +
                                     "\n**Fecha de creación:** " +
                                     _user.CreatedAt.ToString("dd/MM/yyyy") + "\n**Fecha:** " +
                                     DateTime.Now.ToString("dd/MM/yyyy") + "\n**Cantidad Total:** " + users.Count)
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static async Task OnUserLeft(SocketGuild _guild, SocketUser _user)
        {
            try
            {
                var users = Program.Instance.Guild.Users;

                var embed = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithAuthor("Un Usuario a salido del servidor")
                    .WithDescription("**Usuario:** " + _user.Mention + "\n**ID:** " + _user.Id + "\n**Fecha:** " +
                                     DateTime.Now.ToString("dd/MM/yyyy") + "\n**Cantidad Total:** " + users.Count)
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static async Task OnMessageDelete(Cacheable<IMessage, ulong> message,
            Cacheable<IMessageChannel, ulong> messageChannel)
        {
            try
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithAuthor(message.Value.Author)
                    .WithDescription($"**Mensaje eliminado en {messageChannel.Value.Name}**" + "\n**Fecha:** " +
                                     DateTime.Now.ToString("dd/MM/yyyy"))
                    .AddField("Contenido", message.Value.Content)
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static async Task OnThreadCreated(SocketThreadChannel thread)
        {
            try
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Green)
                    .WithAuthor("Un Thread a sido creado")
                    .WithDescription("**Nombre:** " + thread.Name + "\n**Autor:** " + thread.Owner.Mention + "\n**Canal:** " + thread.ParentChannel.Name + "\n**Fecha:** " +
                                     DateTime.Now.ToString("dd/MM/yyyy"))
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static async Task OnThreadDeleted(Cacheable<SocketThreadChannel, ulong> thread)
        {
            try
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithAuthor("Un Thread a sido eliminado")
                    .WithDescription("**Nombre:** " + thread.Value.Name + "\n**Autor:** " + thread.Value.Owner.Mention + "\n**Canal:** " + thread.Value.ParentChannel.Name + "\n**Fecha:** " +
                                     DateTime.Now.ToString("dd/MM/yyyy"))
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static async Task OnInviteCreated(SocketInvite invite)
        {
            try
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Blue)
                    .WithAuthor("Una Invite a sido creada")
                    .WithDescription($"**Creador:** {invite.Inviter.Mention}\n**Canal:** {invite.Channel.Name}\n**Codigo:** {invite.Code}\n**Fecha:** {DateTime.Now.ToString("dd/MM/yyyy")}")
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static async Task OnInviteDeleted(SocketGuildChannel channel, string inviteCode)
        {
            try
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Blue)
                    .WithAuthor("Una Invite a sido eliminada")
                    .WithDescription($"**Canal:** {channel.Name}\n**Codigo:** {inviteCode}\n**Fecha:** {DateTime.Now.ToString("dd/MM/yyyy")}")
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static async Task OnUserBanned(SocketUser user, SocketGuild guild)
        {
            try
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithAuthor("Un Usuario a sido baneado")
                    .WithDescription("**Usuario:** " + user.Mention + "\n**ID:** " + user.Id + "\n**Fecha:** " +
                                     DateTime.Now.ToString("dd/MM/yyyy"))
                    .WithFooter(Footer)
                    .WithCurrentTimestamp()
                    .Build();

                var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
                await logChannel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}