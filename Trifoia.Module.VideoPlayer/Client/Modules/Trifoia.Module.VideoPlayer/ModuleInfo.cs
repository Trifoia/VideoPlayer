using Oqtane.Models;
using Oqtane.Modules;

namespace Trifoia.Module.VideoPlayer
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "VideoPlayer",
            Description = "Open Source edition of the Trifoia Video player Oqtane module",
            Version = "1.0.0",
            ServerManagerType = "Trifoia.Module.VideoPlayer.Manager.VideoPlayerManager, Trifoia.Module.VideoPlayer.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Trifoia.Module.VideoPlayer.Shared.Oqtane,MudBlazor",
            PackageName = "Trifoia.VideoPlayer" 
        };
    }
}
