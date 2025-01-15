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

        public Models.VideoPlayer AddVideoPlayer(Models.VideoPlayer VideoPlayer)
        {
            using var db = _factory.CreateDbContext();
            db.VideoPlayer.Add(VideoPlayer);
            db.SaveChanges();
            return VideoPlayer;
        }

        public Models.VideoPlayer UpdateVideoPlayer(Models.VideoPlayer VideoPlayer)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(VideoPlayer).State = EntityState.Modified;
            db.SaveChanges();
            return VideoPlayer;
        }

        public void DeleteVideoPlayer(int VideoPlayerId)
        {
            using var db = _factory.CreateDbContext();
            Models.VideoPlayer VideoPlayer = db.VideoPlayer.Find(VideoPlayerId);
            db.VideoPlayer.Remove(VideoPlayer);
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

        public async Task<Models.VideoPlayer> AddVideoPlayerAsync(Models.VideoPlayer VideoPlayer)
        {
            using var db = _factory.CreateDbContext();
            db.VideoPlayer.Add(VideoPlayer);
            await db.SaveChangesAsync();
            return VideoPlayer;
        }

        public async Task<Models.VideoPlayer> UpdateVideoPlayerAsync(Models.VideoPlayer VideoPlayer)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(VideoPlayer).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return VideoPlayer;
        }

        public async Task DeleteVideoPlayerAsync(int VideoPlayerId)
        {
            using var db = _factory.CreateDbContext();
            Models.VideoPlayer VideoPlayer = db.VideoPlayer.Find(VideoPlayerId);
            db.VideoPlayer.Remove(VideoPlayer);
            await db.SaveChangesAsync();
        }
    }
}
