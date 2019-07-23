using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LinkAggregatorv5.Models
{
    public class LinkAggregatorv5Context : DbContext
    {
        public LinkAggregatorv5Context(DbContextOptions<LinkAggregatorv5Context> options)
            : base(options)
        {
        }

        public class BloggingContextFactory : IDesignTimeDbContextFactory<LinkAggregatorv5Context>
        {
            public LinkAggregatorv5Context CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<LinkAggregatorv5Context>();
                optionsBuilder.UseSqlite("Data Source=blog.db");

                return new LinkAggregatorv5Context(optionsBuilder.Options);
            }
        }

        public DbSet<Link> Link { get; set; }
        public DbSet<User> User { get; set; }
    }
}
