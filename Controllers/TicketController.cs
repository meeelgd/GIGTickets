﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GIGTickets.Data;
using GIGTickets.Models;
using System.Net.Http;

namespace GIGTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly APIDBContext _context;

        public TicketController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Ticket.Where(f => f.ConcertId == 1).ToListAsync();
        } 

        //[HttpGet]
        [HttpGet("Concert/{value1}/")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTicketsByConcert(int value1)
        {
            return await _context.Ticket.Where(f => f.ConcertId == value1).ToListAsync();
        }

        //[HttpGet]
        [HttpGet("User/{value1}/")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTicketsByUser(String value1)
        {
            return await _context.Ticket.Where(f => f.UserId == value1).ToListAsync();
        }



        [Route("api/Ticket/{param1}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> Get([FromQuery] int param1)
        {
           
            var ticketsList = await _context.Ticket.Where(f => f.ConcertId == param1).ToListAsync(); 
            List<Ticket> dd = new List<Ticket>();

            foreach (Ticket t in ticketsList.ToList()) {
                dd.Add(t);
            }
             
            return Ok(dd);
        }

          
        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        } 

        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Ticket 
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticket>> DeleteTicket(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
