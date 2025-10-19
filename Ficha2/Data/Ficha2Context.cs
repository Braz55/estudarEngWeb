using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ficha2.Models;

namespace Ficha2.Data
{
    public class Ficha2Context : DbContext
    {
        public Ficha2Context (DbContextOptions<Ficha2Context> options)
            : base(options)
        {
        }

        public DbSet<Ficha2.Models.Category> Category { get; set; } = default!;
    }
}
