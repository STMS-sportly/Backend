using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }

        public DbSet<User>? Users { get; set; }

        public DbSet<Team>? Teams { get; set; }

        public DbSet<UserTeam>? UsersTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserTeam>()
            .HasKey(nameof(UserTeam.TeamId), nameof(UserTeam.UserId));
        }

    }
}
