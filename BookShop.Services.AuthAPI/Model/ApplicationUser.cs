﻿using Microsoft.AspNetCore.Identity;

namespace BookShop.Services.AuthAPI.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
