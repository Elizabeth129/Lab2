using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Models
{
    public class Performance_ActorContext:DbContext
    {

        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<PerformanceActorLinker> PerformanceActorLinker { get; set; }
        public virtual DbSet<RoleCollection> RoleCollection { get; set; }
        public virtual DbSet<Theater> Theater { get; set; }
        public virtual DbSet<TypeOfPerformanceCollection> TypeOfPerformanceCollection { get; set; }

        public Performance_ActorContext(DbContextOptions<Performance_ActorContext> options):base(options)
        {
            Database.EnsureCreated();   
        }
    }
}
