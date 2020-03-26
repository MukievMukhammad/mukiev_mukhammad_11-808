using System;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}
