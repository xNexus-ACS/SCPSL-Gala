using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace GalaBot
{
    public class InteractionHandler
    {
        private readonly InteractionService _service;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _provider;

        public InteractionHandler(InteractionService service, DiscordSocketClient client, IServiceProvider provider)
        {
            _service = service;
            _client = client;
            _provider = provider;
        }
        public async Task InstallCommandsAsync()
        {
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
            _client.InteractionCreated += HandleInteraction;
            _service.SlashCommandExecuted += HandleSlashCommand;
        }
        private async Task HandleInteraction(SocketInteraction interaction)
        {
            try
            {
                SocketInteractionContext context = new(_client, interaction);
                await _service.ExecuteCommandAsync(context, _provider);
            }
            catch (Exception e)
            {
                if (interaction.Type == InteractionType.ApplicationCommand)
                    await interaction.RespondAsync("Ha ocurrido un error: " + e, ephemeral: true);
            }
        }
        private async Task HandleServiceError(IInteractionContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                if (result.Error == InteractionCommandError.UnknownCommand)
                    return;

                await context.Interaction.RespondAsync("Ha ocurrido un error: " + result.ErrorReason, ephemeral: true);
            }
        }
        private async Task HandleSlashCommand(SlashCommandInfo info, IInteractionContext context, IResult result) =>
            await HandleServiceError(context, result);
    }
}