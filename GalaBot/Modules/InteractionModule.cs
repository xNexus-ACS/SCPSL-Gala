using System.Threading.Tasks;
using Discord.Interactions;

namespace GalaBot.Modules
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("informacion", "Muestra la informacion sobre la Gala")]
        public async Task ShowInfoAsync()
        {
            
        }
    }
}