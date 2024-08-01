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
    public class MessageController : Controller
    {
        private readonly Test2Context _context;

        public MessageController(Test2Context context)
        {
            _context = context;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            var test2Context = _context.Message.Include(m => m.Receiverchatuser).Include(m => m.Senderchatuser);
            return View(await test2Context.ToListAsync());
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Receiverchatuser)
                .Include(m => m.Senderchatuser)
                .FirstOrDefaultAsync(m => m.Messageid == id);
            if (message == null)
            {
                return NotFound();
            }

            // Entschlüsselung der Nachricht
            message.Encryptedcontent = EncryptionHelper.Decrypt(message.Encryptedcontent);

            return View(message);
        }

        // GET: Message/Create
        public IActionResult Create()
        {
            ViewData["Receiverchatuserid"] = new SelectList(_context.Chatuser, "Chatuserid", "Username");
            ViewData["Senderchatuserid"] = new SelectList(_context.Chatuser, "Chatuserid", "Username");
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Messageid,Senderchatuserid,Receiverchatuserid,Encryptedcontent,Timestamp,Valid,ModUser,ModTimestamp,CrUser,CrTimestamp")] Message message)
        {
            try
            {
                // Verschlüsselung der Nachricht
                message.Encryptedcontent = EncryptionHelper.Encrypt(message.Encryptedcontent);

                var result = await _context.Procedures.MessageInsertAsync(message.Senderchatuserid, message.Receiverchatuserid, message.Encryptedcontent, message.Timestamp, 1);
                return result != null ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          
        }

        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["Receiverchatuserid"] = new SelectList(_context.Chatuser, "Chatuserid", "CrUser", message.Receiverchatuserid);
            ViewData["Senderchatuserid"] = new SelectList(_context.Chatuser, "Chatuserid", "CrUser", message.Senderchatuserid);
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Messageid,Senderchatuserid,Receiverchatuserid,Encryptedcontent,Timestamp,Valid,ModUser,ModTimestamp,CrUser,CrTimestamp")] Message message)
        {
            if (id != message.Messageid)
            {
                return NotFound();
            }

            try
            {
                var result = await _context.Procedures.MessageUpdateAsync(message.Messageid, message.Senderchatuserid, message.Receiverchatuserid, message.Encryptedcontent, message.Timestamp, message.Valid);
                return result != 0 ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Receiverchatuser)
                .Include(m => m.Senderchatuser)
                .FirstOrDefaultAsync(m => m.Messageid == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var result = await _context.Procedures.MessageDeleteAsync(id);
                return result != 0 ? RedirectToAction(nameof(Index)) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool MessageExists(long id)
        {
            return _context.Message.Any(e => e.Messageid == id);
        }
    }
}
