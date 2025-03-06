using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace Trifoia.Module.VideoPlayer.Repository
{
    public class VideoPlayerRepository : ITransientService
    {
        private readonly IDbContextFactory<VideoPlayerContext> _factory;

        public VideoPlayerRepository(IDbContextFactory<VideoPlayerContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.VideoPlayer> GetVideoPlayers()
        {
            using var db = _factory.CreateDbContext();
            return db.VideoPlayer.ToList();
        }

        public Models.VideoPlayer GetVideoPlayer(int VideoPlayerId)
        {
            return GetVideoPlayer(VideoPlayerId, true);
        }

        public Models.VideoPlayer GetVideoPlayer(int VideoPlayerId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.VideoPlayer.Find(VideoPlayerId);
            }
            else
            {
                return db.VideoPlayer.AsNoTracking().FirstOrDefault(item => item.VideoPlayerId == VideoPlayerId);
            }
        }

        public Models.VideoPlayer AddVideoPlayer(Models.VideoPlayer item)
        {
            using var db = _factory.CreateDbContext();
            db.VideoPlayer.Add(item);
            db.SaveChanges();
            return item;
        }

        public Models.VideoPlayer UpdateVideoPlayer(Models.VideoPlayer item)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public void DeleteVideoPlayer(int VideoPlayerId)
        {
            using var db = _factory.CreateDbContext();
            var item = db.VideoPlayer.Find(VideoPlayerId);
            db.VideoPlayer.Remove(item);
            db.SaveChanges();
        }


        public async Task<IEnumerable<Models.VideoPlayer>> GetVideoPlayersAsync(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.VideoPlayer.Where(item => item.ModuleId == ModuleId).ToListAsync();
        }

        public async Task<Models.VideoPlayer> GetVideoPlayerAsync(int VideoPlayerId)
        {
            return await GetVideoPlayerAsync(VideoPlayerId, true);
        }

        public async Task<Models.VideoPlayer> GetVideoPlayerAsync(int VideoPlayerId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.VideoPlayer.FindAsync(VideoPlayerId);
            }
            else
            {
                return await db.VideoPlayer.AsNoTracking().FirstOrDefaultAsync(item => item.VideoPlayerId == VideoPlayerId);
            }
        }

        public async Task<Models.VideoPlayer> AddVideoPlayerAsync(Models.VideoPlayer item)
        {
            using var db = _factory.CreateDbContext();
            db.VideoPlayer.Add(item);
            await db.SaveChangesAsync();
            return item;
        }

        public async Task<Models.VideoPlayer> UpdateVideoPlayerAsync(Models.VideoPlayer item)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return item;
        }

        public async Task DeleteVideoPlayerAsync(int VideoPlayerId)
        {
            using var db = _factory.CreateDbContext();
           var item = db.VideoPlayer.Find(VideoPlayerId);
            db.VideoPlayer.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}
