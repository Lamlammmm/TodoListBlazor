﻿using Microsoft.AspNetCore.Identity;

namespace TodoList.api.Entities
{
    public class Role:IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
