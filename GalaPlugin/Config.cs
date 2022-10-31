using Exiled.API.Interfaces;

namespace GalaPlugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}