//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using E_CommerceProject7.Models;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using Microsoft.Extensions.Configuration;
//using E_CommerceProject7.DTO;

//namespace E_CommerceProject7.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ContactUsController : ControllerBase
//    {
//        private readonly MyDbContext _context;
//        private readonly IConfiguration _configuration;

//        public ContactUsController(MyDbContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        // GET: api/ContactUs
//        // Admin can view all submitted contact forms
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ContactUsDTO>>> GetContactForms()
//        {
//            var contactForms = await _context.ContactUs
//                .Select(c => new ContactUsDTO
//                {
//                    Name = c.Name,
//                    PhoneNumber = c.PhoneNumber,
//                    Subject = c.Subject,
//                    Email = c.Email,
//                    Message = c.Message
//                })
//                .ToListAsync();

//            return Ok(contactForms);
//        }

//        // GET: api/ContactUs/5
//        // Admin can view a specific contact form
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ContactUsDTO>> GetContactForm(int id)
//        {
//            var contactForm = await _context.ContactUs.FindAsync(id);

//            if (contactForm == null)
//            {
//                return NotFound();
//            }

//            var contactUsDto = new ContactUsDTO
//            {
//                Name = contactForm.Name,
//                PhoneNumber = contactForm.PhoneNumber,
//                Subject = contactForm.Subject,
//                Email = contactForm.Email,
//                Message = contactForm.Message
//            };

//            return Ok(contactUsDto);
//        }

//        // POST: api/ContactUs
//        // User can submit a contact form
//        [HttpPost]
//        public async Task<ActionResult<ContactUsDTO>> PostContactForm([FromBody] ContactUsDTO contactUsDto)
//        {
//            if (contactUsDto == null)
//            {
//                return BadRequest("Contact form data is required.");
//            }

//            var contactForm = new ContactU
//            {
//                Name = contactUsDto.Name,
//                PhoneNumber = contactUsDto.PhoneNumber,
//                Subject = contactUsDto.Subject,
//                Email = contactUsDto.Email,
//                Message = contactUsDto.Message,
//                CreatedAt = DateTime.Now
//            };

//            _context.ContactUs.Add(contactForm);
//            await _context.SaveChangesAsync();

//            // Send confirmation email to the user
//            await SendEmailAsync(contactForm.Email, "Contact Form Received", "Thank you for reaching out to us!");

//            return CreatedAtAction(nameof(GetContactForm), new { id = contactForm.Id }, contactUsDto);
//        }

//        // POST: api/ContactUs/reply/5
//        // Admin can reply to a contact form and send an email
//        [HttpPost("reply/{id}")]
//        public async Task<IActionResult> ReplyToContactForm(int id, [FromBody] ContactUsReplyDto replyDto)
//        {
//            var contactForm = await _context.ContactUs.FindAsync(id);

//            if (contactForm == null)
//            {
//                return NotFound("Contact form not found.");
//            }

//            if (string.IsNullOrEmpty(contactForm.Email))
//            {
//                return BadRequest("Email address is required to send a reply.");
//            }

//            // Send an email reply to the user
//            await SendEmailAsync(contactForm.Email, $"Re: {contactForm.Subject}", replyDto.ReplyMessage);

//            contactForm.CreatedAt = DateTime.Now;  // Optionally track when the admin replied
//            _context.Entry(contactForm).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return Ok("Reply sent and email delivered.");
//        }

//        // DELETE: api/ContactUs/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteContactForm(int id)
//        {
//            var contactForm = await _context.ContactUs.FindAsync(id);

//            if (contactForm == null)
//            {
//                return NotFound("Contact form not found.");
//            }

//            _context.ContactUs.Remove(contactForm);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // Helper method to send an email using SendGrid
//        private async Task SendEmailAsync(string toEmail, string subject, string message)
//        {
//            var apiKey = _configuration["SendGrid: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"];
//            var client = new SendGridClient(apiKey);
//            var from = new EmailAddress("ruhufatefruhuf@gmail.com", "Orange");
//            var to = new EmailAddress(toEmail);
//            var emailMessage = MailHelper.CreateSingleEmail(from, to, subject, message, message);
//            await client.SendEmailAsync(emailMessage);
//        }

//        private bool ContactFormExists(int id)
//        {
//            return _context.ContactUs.Any(e => e.Id == id);
//        }
//    }
//}
