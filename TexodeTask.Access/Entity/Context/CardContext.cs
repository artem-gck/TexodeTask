using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TexodeTask.Access.Entity.Context
{
    public class CardContext : DbContext
    {
        public CardContext()
        { }

        public CardContext(DbContextOptions<CardContext> options)
            : base(options)
        { }

        public DbSet<CardEntity> Cards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Texode;Integrated Security=True"); 
    }
}
