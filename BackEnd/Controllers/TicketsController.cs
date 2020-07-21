using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using DTO;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TrackerDBContext _db;

        public TicketsController(TrackerDBContext db)
        {
            _db = db;
        }

        //GET api/Tickets
        [HttpGet]
        public async Task<ActionResult<List<TicketResponse>>> GetTickets()
        {
            var tickets = await _db.Tickets.AsNoTracking()
                                             .Include(a => a.AssignedDev)
                                             .Include(s => s.SubmittedBy)
                                             .Include(p => p.Project)
                                             .Select(m => m.MapTicketResponse())
                                             .ToListAsync();

            return tickets;
        }

        //GET api/Tickets/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketResponse>> GetTicket(int id)
        {
            var ticket = await _db.Tickets.AsNoTracking()
                                            .Include(a => a.AssignedDev)
                                            .Include(s => s.SubmittedBy)
                                            .Include(p => p.Project)
                                            .SingleOrDefaultAsync(s => s.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket.MapTicketResponse();
        }

        //POST api/Tickets
        [HttpPost]
        public async Task<ActionResult<TicketResponse>> PostTicket(DTO.Ticket input)
        {
            var ticket = new Data.Ticket
            {
                Title = input.Title,
                Description = input.Description,
                Priority = input.Priority,
                Status = input.Status,
                TicketType = input.TicketType,
                CreatedOn = input.CreatedOn,
                AssignedDevId = input.AssignedDevId,
                SubmittedById = input.SubmittedById,
                ProjectId = input.ProjectId
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();

            var result = ticket.MapTicketResponse();

            return CreatedAtAction(nameof(GetTicket), new { id = result.Id }, result);
        }

        //PUT api/Tickets{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, DTO.Ticket input)
        {
            var ticket = await _db.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Title = input.Title;
            ticket.Description = input.Description;
            ticket.Priority = input.Priority;
            ticket.Status = input.Status;
            ticket.TicketType = input.TicketType;
            ticket.CreatedOn = input.CreatedOn;
            ticket.AssignedDevId = input.AssignedDevId;
            ticket.SubmittedById = input.SubmittedById;
            ticket.ProjectId = input.ProjectId;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        //DELETE api/Tickets{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketResponse>> DeleteTicket(int id)
        {
            var session = await _db.Tickets.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            _db.Tickets.Remove(session);
            await _db.SaveChangesAsync();

            return session.MapTicketResponse();
        }
        
    }
}