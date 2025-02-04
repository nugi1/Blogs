using BlogsDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogsDAL;

public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<UserJwt> UserJwts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(e =>
        {
            e.HasKey(c => c.Id);
            
            e.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            e.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);
        });

        modelBuilder.Entity<UserJwt>(e =>
        {
            e.HasKey(uj => uj.Id);
        });

        modelBuilder.Entity<Comment>(e =>
        {
            e.HasKey(c => c.Id);
            
            e.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);
            
            e.Property(c => c.CreatedAt)
                .IsRequired();
            
            e.Property(c => c.PostId)
                .IsRequired();
        });

        modelBuilder.Entity<Post>(e =>
        {
            e.HasKey(c => c.Id);
            
            e.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
        
            e.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(5000);
        
            e.Property(p => p.PublishedAt)
                .IsRequired();
        
            e.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);
        
            e.HasMany(p => p.Tags)
                .WithMany(t => t.Posts);
        });
        
        modelBuilder.Entity<Tag>(e =>
        {
            e.HasKey(c => c.Id);
            
            e.HasKey(t => t.Id);

            e.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            e.HasMany(t => t.Posts)
                .WithMany(p => p.Tags);
        });
    }
}