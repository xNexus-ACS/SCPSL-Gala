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
            var users = Program.Instance.Guild.Users;
            
            var embed = new EmbedBuilder()
                .WithColor(Color.Green)
                .WithAuthor("Un Usuario a entrado al servidor")
                .WithDescription("**Usuario:** " + _user.Mention + "\n**ID:** " + _user.Id + "\n**Fecha de creación:** " +
                                 _user.CreatedAt.ToString("dd/MM/yyyy") + "\n**Fecha:** " +
                                 DateTime.Now.ToString("dd/MM/yyyy") + "\n**Cantidad Total:** " + users.Count)
                .WithFooter(Footer)
                .WithCurrentTimestamp()
                .Build();

            var logChannel = Program.Instance.Guild.GetTextChannel(987085381877006366);
            await logChannel.SendMessageAsync(embed: embed);
        }

        public static async Task OnUserLeft(SocketGuild _guild, SocketUser _user)
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
    }
}