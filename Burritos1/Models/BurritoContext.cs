﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Burritos1.Models
{
    public class BurritoContext :DbContext
    {
        public BurritoContext() : base("DefaultConnection")
        {

        }
        public DbSet<Hielera> Hieleras { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}