using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Helper;
using PL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
	[Authorize]
	public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		public async Task<IActionResult> Index(string SearchValue)
		{
			if (string.IsNullOrEmpty(SearchValue))
				return View(userManager.Users);

			else
			{
				var user = await userManager.FindByEmailAsync(SearchValue);
				if (user == null)
                    return View(userManager.Users);
                return View(new List<ApplicationUser>() { user });
			}
		}

		public async Task<IActionResult> Details([FromRoute] string Id, string viewName = "Details")
		{
			if (Id == null)
				return NotFound();
			var user = await userManager.FindByIdAsync(Id);
			if (user == null)
				return NotFound();
			return View(viewName, user);
		}
		public async Task<IActionResult> Edit(string Id)
		{

			return await Details(Id, "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] string Id, ApplicationUser updatedUser)
		{
			if (Id != updatedUser.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var user = await userManager.FindByIdAsync(Id);
					user.UserName = updatedUser.UserName;
					user.NormalizedEmail = updatedUser.NormalizedEmail;
					user.PhoneNumber = updatedUser.PhoneNumber;
					var result = await userManager.UpdateAsync(user);
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					throw;
				}
			}

			return View(updatedUser);
		}
		public async Task<IActionResult> Delete(string Id)
		{
			return await Details(Id, "Delete");
		}
		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute] string Id, ApplicationUser deletedUser)
		{
			if (Id != deletedUser.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var user= await userManager.FindByIdAsync(deletedUser.Id);
					var result = await userManager.DeleteAsync(user);
					if (result.Succeeded)
						return RedirectToAction("Index");

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
					return View(deletedUser);
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			return View(deletedUser);
		}

	}
}
