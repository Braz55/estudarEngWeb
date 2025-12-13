using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ficha5.Models;

namespace ficha5.Data
{
    public class ficha5Context : DbContext
    {
        public ficha5Context (DbContextOptions<ficha5Context> options)
            : base(options)
        {
        }

        public DbSet<ficha5.Models.Book> Book { get; set; } = default!;
    }
}
