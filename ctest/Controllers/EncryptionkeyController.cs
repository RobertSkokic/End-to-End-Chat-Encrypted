using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ctest.Models;
using ctest.Models.dboSchema;

namespace ctest.Controllers
{
    public class EncryptionkeyController : Controller
    {
        private readonly Test2Context _context;

        public EncryptionkeyController(Test2Context context)
        {
            _context = context;
        }

        // GET: Encryptionkey
        public async Task<IActionResult> Index()
        {
            var test2Context = _context.Encryptionkey.Include(e => e.Chatuser);
            return View(await test2Context.ToListAsync());
        }

        // GET: Encryptionkey/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encryptionkey = await _context.Encryptionkey
                .Include(e => e.Chatuser)
                .FirstOrDefaultAsync(m => m.Encryptionkeyid == id);
            if (encryptionkey == null)
            {
                return NotFound();
            }

            return View(encryptionkey);
        }

        // GET: Encryptionkey/Create
        public IActionResult Create()
        {
            ViewData["Chatuserid"] = new SelectList(_context.Chatuser, "Chatuserid", "Username");
            return View();
        }

        // POST: Encryptionkey/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Encryptionkeyid,Chatuserid,Keyvalue,Createdat,Valid,ModUser,ModTimestamp,CrUser,CrTimestamp")] Encryptionkey encryptionkey)
        {
            try
            {
                var result = await _context.Procedures.EncryptionKeyInsertAsync(encryptionkey.Chatuserid, encryptionkey.Keyvalue, encryptionkey.Createdat, 1);
                return result != null ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Encryptionkey/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encryptionkey = await _context.Encryptionkey.FindAsync(id);
            if (encryptionkey == null)
            {
                return NotFound();
            }
            ViewData["Chatuserid"] = new SelectList(_context.Chatuser, "Chatuserid", "CrUser", encryptionkey.Chatuserid);
            return View(encryptionkey);
        }

        // POST: Encryptionkey/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Encryptionkeyid,Chatuserid,Keyvalue,Createdat,Valid,ModUser,ModTimestamp,CrUser,CrTimestamp")] Encryptionkey encryptionkey)
        {
            if (id != encryptionkey.Encryptionkeyid)
            {
                return NotFound();
            }

            try
            {
                var result = await _context.Procedures.EncryptionKeyUpdateAsync(encryptionkey.Encryptionkeyid, encryptionkey.Chatuserid, encryptionkey.Keyvalue, encryptionkey.Createdat, encryptionkey.Valid);
                return result != 0 ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Encryptionkey/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encryptionkey = await _context.Encryptionkey
                .Include(e => e.Chatuser)
                .FirstOrDefaultAsync(m => m.Encryptionkeyid == id);
            if (encryptionkey == null)
            {
                return NotFound();
            }

            return View(encryptionkey);
        }

        // POST: Encryptionkey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var result = await _context.Procedures.EncryptionKeyDeleteAsync(id);
                return result != 0 ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool EncryptionkeyExists(long id)
        {
            return _context.Encryptionkey.Any(e => e.Encryptionkeyid == id);
        }
    }
}
