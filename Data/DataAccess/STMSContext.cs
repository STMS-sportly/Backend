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
    public class StmsContext : DbContext
    {
        public StmsContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<UserTeam> UsersTeams { get; set; }

        public DbSet<TeamCode> TeamCodes { get; set; }

        public DbSet<Log> AppLogs { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<GroupChat> GroupChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserTeam>().HasKey(nameof(UserTeam.TeamId), nameof(UserTeam.UserId));

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GroupChat>().HasKey(nameof(GroupChat.MessageId), nameof(GroupChat.TeamId), nameof(GroupChat.UserId));
        }

    }
}
