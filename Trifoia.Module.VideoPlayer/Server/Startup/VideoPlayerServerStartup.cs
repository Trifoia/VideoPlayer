using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Trifoia.Module.VideoPlayer.Repository;

namespace Trifoia.Module.VideoPlayer.Startup
{
    public class VideoPlayerServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextFactory<VideoPlayerContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
