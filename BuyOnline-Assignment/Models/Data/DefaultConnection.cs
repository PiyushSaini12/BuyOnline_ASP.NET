﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BuyOnline_Assignment.Models.Data
{
    public class DefaultConnection : DbContext
    {
        public DbSet<PageDetail> Pages { get; set; }
        public DbSet<SidebarDTO> Sidebar { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<ProductDTO> Products { get; set; }

        public System.Data.Entity.DbSet<BuyOnline_Assignment.Models.ViewModels.Shop.ProductVM> ProductVMs { get; set; }
        /*public DbSet<UserDTO> Users { get; set; }
public DbSet<RoleDTO> Roles { get; set; }
public DbSet<UserRoleDTO> UserRoles { get; set; }
public DbSet<OrderDTO> Orders { get; set; }
public DbSet<OrderDetailsDTO> OrderDetails { get; set; } */
    }
}