using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorWebApp01.Models;

namespace BlazorWebApp01.Data
{
    public class BlazorWebApp01Context : DbContext
    {
        public BlazorWebApp01Context (DbContextOptions<BlazorWebApp01Context> options)
            : base(options)
        {
        }

        public DbSet<BlazorWebApp01.Models.Contact> Contact { get; set; } = default!;
    }
}
