using ASPNETCore8.Blazor.OpenAI.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore8.Blazor.OpenAI.Components.Player.Data
{
    public class VideoRepository : IVideoRepository
    {
        private readonly DatabaseContext _context;

        public VideoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<Video> GetByIdAsync(Guid id)
        {
            var video = await _context.Videos.FindAsync(id);
            return video ?? new Video();
        }

        public async Task<Video> GetBySlugAsync(string slug)
        {
            var video = await _context.Videos.Where(v => v.Slug == slug).FirstOrDefaultAsync();
            return video ?? new Video();
        }

        public async Task AddAsync(Video video)
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Video video)
        {
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var video = await _context.Videos.FindAsync(Id);
            if (video is not null)
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }
    }
}