﻿using System;

namespace SalesSystem.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public long IdentityNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }  // Administrator, Seller, Customer
    }
}
