using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ctest.Models;
using ctest.Models.dboSchema;
using ctest.ViewModels;

namespace ctest.Controllers
{
    public class ChatuserController : Controller
    {
        private readonly Test2Context _context;

        public ChatuserController(Test2Context context)
        {
            _context = context;
        }


        // GET: Chatuser
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chatuser.ToListAsync());
        }

        // GET: Chatuser/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Chatuser/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ChatuserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Chatuser
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user != null && PasswordHelper.VerifyPassword(model.Password, user.Passwordhash))
                {
                    // Mark the user as online
                    user.Valid = 1; // Assuming 'Valid' represents an active session
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    // Set session
                    HttpContext.Session.SetInt32("UserId", (int)user.Chatuserid);
                    HttpContext.Session.SetString("Username", user.Username);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                // Cast userId to long
                var user = _context.Chatuser.Find((long)userId.Value);
                if (user != null)
                {
                    // Mark the user as offline
                    user.Valid = 0; // Or another suitable value to indicate offline
                    _context.Update(user);
                    _context.SaveChangesAsync();
                }
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Chatuser");
        }



        // GET: Chatuser/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatuser = await _context.Chatuser
                .FirstOrDefaultAsync(m => m.Chatuserid == id);
            if (chatuser == null)
            {
                return NotFound();
            }

            return View(chatuser);
        }

        // GET: Chatuser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chatuser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Chatuserid,Username,Passwordhash,Createdat,Valid,ModUser,ModTimestamp,CrUser,CrTimestamp")] Chatuser chatuser)
        {
            try
            {
                chatuser.Passwordhash = PasswordHelper.HashPassword(chatuser.Passwordhash);

                // Store the user in the database
                var result = await _context.Procedures.ChatuserInsertAsync(chatuser.Username, chatuser.Passwordhash, DateTime.Now, 1);
                return result != null ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Chatuser/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatuser = await _context.Chatuser.FindAsync(id);
            if (chatuser == null)
            {
                return NotFound();
            }
            return View(chatuser);
        }

        // POST: Chatuser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Chatuserid,Username,Passwordhash,Createdat,Valid,ModUser,ModTimestamp,CrUser,CrTimestamp")] Chatuser chatuser)
        {
            if (id != chatuser.Chatuserid)
            {
                return NotFound();
            }

            try
            {
                var result = await _context.Procedures.ChatuserUpdateAsync(chatuser.Chatuserid, chatuser.Username, chatuser.Passwordhash, chatuser.Createdat, chatuser.Valid);
                return result != 0 ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Chatuser/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatuser = await _context.Chatuser
                .FirstOrDefaultAsync(m => m.Chatuserid == id);
            if (chatuser == null)
            {
                return NotFound();
            }

            return View(chatuser);
        }

        // POST: Chatuser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var result = await _context.Procedures.ChatuserDeleteAsync(id);
                return result != 0 ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool ChatuserExists(long id)
        {
            return _context.Chatuser.Any(e => e.Chatuserid == id);
        }
    }
}
