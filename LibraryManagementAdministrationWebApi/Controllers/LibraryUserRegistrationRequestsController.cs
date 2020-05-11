using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAdministrationWebApi.Models;

namespace LibraryManagementAdministrationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryUserRegistrationRequestsController : ControllerBase
    {
        private readonly LibraryManagementContext _context;

        public LibraryUserRegistrationRequestsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: api/LibraryUserRegistrationRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryUserRegistrationRequest>>> GetLibraryUserRegistrationRequest()
        {
            return await _context.LibraryUserRegistrationRequest.ToListAsync();
        }

        // GET: api/LibraryUserRegistrationRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryUserRegistrationRequest>> GetLibraryUserRegistrationRequest(int id)
        {
            var libraryUserRegistrationRequest = await _context.LibraryUserRegistrationRequest.FindAsync(id);

            if (libraryUserRegistrationRequest == null)
            {
                return NotFound();
            }

            return libraryUserRegistrationRequest;
        }

        // PUT: api/LibraryUserRegistrationRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibraryUserRegistrationRequest(int id, LibraryUserRegistrationRequest libraryUserRegistrationRequest)
        {
            if (id != libraryUserRegistrationRequest.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(libraryUserRegistrationRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryUserRegistrationRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LibraryUserRegistrationRequests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LibraryUserRegistrationRequest>> PostLibraryUserRegistrationRequest(LibraryUserRegistrationRequest libraryUserRegistrationRequest)
        {
            _context.LibraryUserRegistrationRequest.Add(libraryUserRegistrationRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibraryUserRegistrationRequest", new { id = libraryUserRegistrationRequest.RequestId }, libraryUserRegistrationRequest);
        }

        // DELETE: api/LibraryUserRegistrationRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LibraryUserRegistrationRequest>> DeleteLibraryUserRegistrationRequest(int id)
        {
            var libraryUserRegistrationRequest = await _context.LibraryUserRegistrationRequest.FindAsync(id);
            if (libraryUserRegistrationRequest == null)
            {
                return NotFound();
            }

            _context.LibraryUserRegistrationRequest.Remove(libraryUserRegistrationRequest);
            await _context.SaveChangesAsync();

            return libraryUserRegistrationRequest;
        }

        private bool LibraryUserRegistrationRequestExists(int id)
        {
            return _context.LibraryUserRegistrationRequest.Any(e => e.RequestId == id);
        }
    }
}
