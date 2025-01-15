using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Oqtane.Services;
using Trifoia.Module.VideoPlayer.Services;

namespace Trifoia.Module.VideoPlayer.Client.Services
{
    public class Startup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<VideoPlayerService, VideoPlayerService>();
            services.AddMudServices();
        }
    }
}
