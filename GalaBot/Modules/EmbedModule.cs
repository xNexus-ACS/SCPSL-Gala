using System.Threading.Tasks;
using Discord;

namespace GalaBot.Modules
{
    public class EmbedModule
    {
        public static async Task<Embed> CreateBasicEmbed(string title, string description, Color color)
        {
            var embed = await Task.Run(() => new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithFooter(UtilsModule.Footer)
                .WithCurrentTimestamp().Build());
            return embed;
        }
    }
}