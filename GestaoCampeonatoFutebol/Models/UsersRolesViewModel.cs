﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoCampeonatoFutebol.Models
{
    public class UsersRolesViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
