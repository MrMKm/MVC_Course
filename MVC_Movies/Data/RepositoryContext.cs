using Microsoft.EntityFrameworkCore;
using MVC_Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVC_Movies.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<Actor> Actor { get; set; }
        public DbSet<Movie> Movie { get; set; }
    }
}
