using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserInfo> Users { get; set; }

        public DbSet<UserNotesInfo> UserNotes { get; set; }
        
        public DbSet<LabelInfo> Labels { get; set; }

        public DbSet<NotesLabel> NotesLabels { set; get; }
    }
}
