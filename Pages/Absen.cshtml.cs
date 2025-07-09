using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // For SelectListItem
using Microsoft.EntityFrameworkCore; // For ToListAsync, Select etc.
using System.Linq; // For LINQ queries
using System.Collections.Generic; // For List
using System; // For DateTime

// Add this line to import the namespace where ApplicationDbContext is located:
using Take_Home_Test___Web_App.Models; // Also ensure you have this if Employee/Absen models are here


namespace Take_Home_Test___Web_App.Pages
{
    public class AbsenModel : PageModel
    {
        private readonly ApplicationDbContext _context; // Our database context

        // Constructor: Dependency Injection will provide the ApplicationDbContext instance
        public AbsenModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // [BindProperty] makes AbsenVM available for both GET and POST requests
        // and automatically binds form data to its properties on POST.
        [BindProperty]
        public AbsenViewModel AbsenVM { get; set; }

        // Handles GET requests (when the page is first loaded)
        public async Task<IActionResult> OnGetAsync()
        {
      
            AbsenVM = new AbsenViewModel(); // Initialize the ViewModel

            // Populate the Employees dropdown list
            AbsenVM.EmployeeList = await _context.Employee
                                            .OrderBy(e => e.Nama) // Order alphabetically by name
                                            .Select(e => new SelectListItem
                                            {
                                                Value = e.NIK,
                                                Text = $"{e.NIK} - {e.Nama}"
                                            })
                                            .ToListAsync();

            // Set default date for the input
            AbsenVM.TanggalAbsen = DateTime.Today;

            // Populate the AbsenRecords for display table
            await PopulateAbsenRecordsAsync();

            return Page(); // Return the Razor Page
        }

        // Handles POST requests (when the "Save" button is clicked)
        public async Task<IActionResult> OnPostAsync()
        {
            // Repopulate Employees dropdown even if ModelState is invalid,
            // so the dropdown doesn't become empty if the form submission fails.
            AbsenVM.EmployeeList = await _context.Employee
                                            .OrderBy(e => e.Nama)
                                            .Select(e => new SelectListItem
                                            {
                                                Value = e.NIK,
                                                Text = $"{e.NIK} - {e.Nama}"
                                            })
                                            .ToListAsync();

            if (!ModelState.IsValid)
            {
                // If validation fails, repopulate records and return the page with errors
                await PopulateAbsenRecordsAsync();
                return Page();
            }

            // Create a new Absen entity from the ViewModel data
            var newAbsen = new Absen
            {
                NIK = AbsenVM.SelectedNIK,
                TanggalAbsen = AbsenVM.TanggalAbsen 
            };

            _context.Absen.Add(newAbsen); // Add to DbContext
            await _context.SaveChangesAsync(); // Save changes to the database

            // After successful save, reset form fields and rebind the grid
            ModelState.Clear(); // Clear validation messages from previous post
            AbsenVM = new AbsenViewModel(); // Create a new, clean ViewModel instance for the form
            AbsenVM.EmployeeList = await _context.Employee // Repopulate dropdown
                                            .OrderBy(e => e.Nama)
                                            .Select(e => new SelectListItem
                                            {
                                                Value = e.NIK,
                                                Text = $"{e.NIK} - {e.Nama}"
                                            })
                                            .ToListAsync();
            AbsenVM.TanggalAbsen = DateTime.Today; // Reset date to today

            await PopulateAbsenRecordsAsync(); // Re-populate the display table with the new entry

            TempData["SuccessMessage"] = "Absen data saved successfully!"; // Optional: show success message
            return Page(); // Stay on the same page
        }

        // Helper method to populate the AbsenRecords for the display table
        private async Task PopulateAbsenRecordsAsync()
        {
            AbsenVM.AbsenRecords = await (from a in _context.Absen
                                          join e in _context.Employee on a.NIK equals e.NIK
                                          orderby a.TanggalAbsen descending // Order as per your example
                                          select new AbsenRecordViewModel
                                          {
                                              NIK = a.NIK,
                                              Nama = e.Nama,
                                              TanggalAbsen = a.TanggalAbsen
                                          }).ToListAsync();
        }
    }
}
