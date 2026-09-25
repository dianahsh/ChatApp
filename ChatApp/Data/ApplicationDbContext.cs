using ChatApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserConversation> UserConvs { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Contact>()
                .HasOne(c => c.ContactUser)
                .WithMany()
                .HasForeignKey(c => c.ContactUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Contact>()
                .HasIndex(c => new { c.UserId, c.ContactUserId })
                .IsUnique();
        }
    }
}
