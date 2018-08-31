using Microsoft.EntityFrameworkCore;
using Pos.DataAccess.Model;
using System.Collections.Generic;

namespace Pos.DataAccess.DbContext
{
    class MyContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Product> Products { get; set; }

        

       
    }
}