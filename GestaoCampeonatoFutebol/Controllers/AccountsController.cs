﻿using GestaoCampeonatoFutebol.Data;
using GestaoCampeonatoFutebol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoCampeonatoFutebol.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: AccountsController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var users = _userManager.Users;
            var model = new List<UsersRolesViewModel>();
            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                model.Add(new UsersRolesViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = roles.SingleOrDefault()
                });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();

            var model = new UsersRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = (await _userManager.GetRolesAsync(user)).SingleOrDefault(),
                Roles = roles
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, UsersRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var oldRole = model.Role;
            // Remove o user de todos os roles
            var result = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Role", "Um erro ococrreu a dar update ao role do utilizador.");
                model.Role = oldRole;
                return View(model);
            }

            // Adicionar o role selecionado ao utilizador
            result = await _userManager.AddToRoleAsync(user, model.Role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Role", "Um erro ococrreu a dar update ao role do utilizador.");
                model.Role = oldRole;

                return View(model);
            }

            return RedirectToAction("Index");
        }


    }
}
