using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly PhoneBookContext _context;
        public ContactsController(PhoneBookContext context) 
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            var contacts = _context.Contacts.Include(c => c.PhoneNumbers).ToList();
            return View(contacts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact, List<string> PhoneLabels, List<string> PhoneNumbers)
        {
            if (ModelState.IsValid)
            {
                // Add each phone number to the contact
                for (int i = 0; i < PhoneNumbers.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(PhoneNumbers[i]))
                    {
                        contact.PhoneNumbers.Add(new PhoneNumber
                        {
                            Number = PhoneNumbers[i],
                            Label = PhoneLabels[i]
                        });
                    }
                }
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);

        }
        public IActionResult Edit(int id)
        {
            var contact = _context.Contacts.Include(c => c.PhoneNumbers).FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact updatedContact, List<string> PhoneNumbers, List<string> PhoneLabels)
        {
            if (id != updatedContact.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Load existing contact WITH phone numbers
                    var existingContact = await _context.Contacts
                        .Include(c => c.PhoneNumbers)
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (existingContact == null)
                        return NotFound();

                    // 2. Update scalar properties (name, email, etc.)
                    _context.Entry(existingContact).CurrentValues.SetValues(updatedContact);

                    // 3. Process phone numbers
                    var processedNumbers = new List<PhoneNumber>();

                    for (int i = 0; i < PhoneNumbers.Count; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(PhoneNumbers[i]))
                        {
                            // Find or create phone number
                            var phoneNumber = existingContact.PhoneNumbers
                                .FirstOrDefault(p =>
                                    p.Number == PhoneNumbers[i] &&
                                    p.Label == PhoneLabels[i])
                                ?? new PhoneNumber();

                            phoneNumber.Number = PhoneNumbers[i];
                            phoneNumber.Label = PhoneLabels[i];
                            phoneNumber.ContactId = id;

                            processedNumbers.Add(phoneNumber);
                        }
                    }

                    // 4. Remove deleted numbers and add new ones
                    foreach (var existingNumber in existingContact.PhoneNumbers.ToList())
                    {
                        if (!processedNumbers.Any(p =>
                            p.Number == existingNumber.Number &&
                            p.Label == existingNumber.Label))
                        {
                            _context.PhoneNumbers.Remove(existingNumber);
                        }
                    }

                    foreach (var newNumber in processedNumbers.Where(p => p.Id == 0))
                    {
                        existingContact.PhoneNumbers.Add(newNumber);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(id))
                        return NotFound();
                    throw;
                }
            }
            return View(updatedContact);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
