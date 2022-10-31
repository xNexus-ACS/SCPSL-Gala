using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using GalaBot.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GalaBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private SocketGuild? guild;

        public SocketGuild Guild => guild ??= _client.Guilds.FirstOrDefault(g => g.Id == 985788875089793104)!;
        
        public static Program Instance;
        
        public static Task Main() => new Program().MainAsync();

        public Program()
        {
            Instance = this;
        }

        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("config.yml")
                .Build();
            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                    services
                        .AddSingleton(config)
                        .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                        {
                            GatewayIntents = GatewayIntents.All,
                            AlwaysDownloadUsers = true,
                            LogLevel = LogSeverity.Debug
                        }))
                        .AddMemoryCache()
                        .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                        .AddSingleton<InteractionHandler>())
                        .Build();
            await RunAsync(host);
        }

        public async Task RunAsync(IHost host)
        {
            Console.WriteLine($"Iniciando Bot. Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            
            var commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();
            
            await provider.GetRequiredService<InteractionHandler>().InstallCommandsAsync();

            _client.UserJoined += UtilsModule.OnUserJoin;
            _client.UserLeft += UtilsModule.OnUserLeft;
            _client.MessageDeleted += UtilsModule.OnMessageDelete;
            _client.ThreadCreated += UtilsModule.OnThreadCreated;
            _client.ThreadDeleted += UtilsModule.OnThreadDeleted;
            _client.InviteCreated += UtilsModule.OnInviteCreated;
            _client.InviteDeleted += UtilsModule.OnInviteDeleted;
            _client.UserBanned += UtilsModule.OnUserBanned;

            _client.Ready += async () =>
            {
                await _client.SetStatusAsync(UserStatus.DoNotDisturb);
                
                Console.WriteLine($"Conectado como => {_client.CurrentUser}");
                
                if (IsDebug())
                    await commands.RegisterCommandsToGuildAsync(UInt64.Parse(config["guild"]), true);
                else
                    await commands.RegisterCommandsGloballyAsync(true);
            };
            await _client.LoginAsync(TokenType.Bot, config["token"]);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}