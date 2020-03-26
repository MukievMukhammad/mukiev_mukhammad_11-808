using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Filters;
using SocialMedia.Models;
using SocialMedia.Views.Account;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext _context;
        private UsersContext _usersContext;

        public PostsController(ApplicationDbContext context, UsersContext usersContext)
        {
            _context = context;
            _usersContext = usersContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AuthFilter]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public async Task<IActionResult> Create([Bind("Id,Name,Text,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [HttpGet]
        [AuthFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Text,Date")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [AuthFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        private async Task<IActionResult> AuthorizeAsync()
        {
            var userId = Request.Cookies["id"];
            var token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return RedirectToAction(nameof(Login));

            var user = await _usersContext.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
            if (user == null)
                BadRequest();
            else if (!IsAuthenticated(double.Parse(token), user))
                return RedirectToAction(nameof(Login));

            return Ok();
        }

        private bool IsAuthenticated(double token, User user)
        {
            return token == MyHashCode.GetHash(user.Email + user.Password);
        }
    }
}
