using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using System.Net;

namespace Trifoia.Module.VideoPlayer.Services
{
    public class VideoPlayerService : ResponseServiceBase, IService
    {
        public VideoPlayerService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("VideoPlayer");

        public async Task<(List<Models.VideoPlayer>,HttpStatusCode)> GetVideoPlayersAsync()
        {
            var url = $"{Apiurl}";
            (var data, var response) = await GetJsonWithResponseAsync<List<Models.VideoPlayer>>(url);
            return (data, response.StatusCode);      
        }

        public async Task<(Models.VideoPlayer,HttpStatusCode)> GetVideoPlayerAsync(int id)
        {
            var url = $"{Apiurl}/{id}";
            (var data, var response) = await GetJsonWithResponseAsync<Models.VideoPlayer>(url);
            return (data, response.StatusCode);        
        }

        public async Task<(Models.VideoPlayer,HttpStatusCode)> AddVideoPlayerAsync(Models.VideoPlayer item)
        {
            var url = $"{Apiurl}";
            (var data, var response) = await PostJsonWithResponseAsync<Models.VideoPlayer>(url,item);
            return (data, response.StatusCode);        
        }

        public async Task<(Models.VideoPlayer,HttpStatusCode)> UpdateVideoPlayerAsync(Models.VideoPlayer item)
        {
            var url = $"{Apiurl}/{item.VideoPlayerId}";
            (var data, var response) = await PutJsonWithResponseAsync<Models.VideoPlayer>(url,item);
            return (data, response.StatusCode);        
        }

        public async Task<HttpStatusCode> DeleteVideoPlayerAsync(int id)
        {
            var url = $"{Apiurl}/{id}";
            var response  = await DeleteWithResponseAsync(url);
            return response.StatusCode;
        }
    }
}
