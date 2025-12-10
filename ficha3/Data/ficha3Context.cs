using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ficha3.Models;

namespace ficha3.Data
{
    public class ficha3Context : DbContext
    {
        public ficha3Context (DbContextOptions<ficha3Context> options)
            : base(options)
        {
        }

        public DbSet<ficha3.Models.Category> Category { get; set; } = default!;
    }
}
