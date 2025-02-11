﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TayDuKy.Models;

namespace TayDuKy
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = tcp:loinvsql.database.windows.net, 1433; Initial Catalog = Tayduky; Persist Security Info = False; User ID = sql; Password =123@Admin; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Role
            modelBuilder.Entity<Role>().HasData(
                new Role(roleId: 1, roleName: "Admin"),
                new Role(roleId: 2, roleName: "Actor")
                );

            //ClamityCharacter
            modelBuilder.Entity<CalamityCharacter>().HasKey(p => new { p.CalamityId, p.CharacterId });

            //Chacters
            modelBuilder.Entity<Character>().HasOne(c => c.User).WithMany(u => u.Characters).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);

            //CalamityQuipment
            modelBuilder.Entity<CalamityEquipment>().HasKey(c => new { c.CalamityId, c.EquipmentId });
        }

        public DbSet<Models.Role> Role { get; set; }

        public DbSet<Models.CalamityCharacter> CalamityCharacter { get; set; }

        public DbSet<TayDuKy.Models.User> User { get; set; }

        public DbSet<TayDuKy.Models.Equipment> Equipment { get; set; }

        public DbSet<TayDuKy.Models.Calamity> Calamity { get; set; }

        public DbSet<TayDuKy.Models.Character> Character { get; set; }



        public DbSet<TayDuKy.Models.CalamityEquipment> CalamityEquipment { get; set; }



    }
}
