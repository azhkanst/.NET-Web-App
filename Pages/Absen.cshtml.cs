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
        private readonly ApplicationDbContext _context; // database context

        // Constructor: Dependency Injection
        public AbsenModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // AbsenVM GET and POST requests
        [BindProperty]
        public AbsenViewModel AbsenVM { get; set; }

        // Handles GET 
        public async Task<IActionResult> OnGetAsync()
        {
      
            AbsenVM = new AbsenViewModel(); // Initialize the ViewModel

            // Populate the Employees dropdown list
            AbsenVM.EmployeeList = await _context.Employee
                                            .OrderBy(e => e.Nama) // Order alphabetically 
                                            .Select(e => new SelectListItem
                                            {
                                                Value = e.NIK,
                                                Text = $"{e.NIK} - {e.Nama}"
                                            })
                                            .ToListAsync();

            // Set default
            AbsenVM.TanggalAbsen = DateTime.Today;

            // Populate the AbsenRecords
            await PopulateAbsenRecordsAsync();

            return Page(); // Return the Razor Page
        }

        // Handles POST
        public async Task<IActionResult> OnPostAsync()
        {
  
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
  
                await PopulateAbsenRecordsAsync();
                return Page();
            }

            var newAbsen = new Absen
            {
                NIK = AbsenVM.SelectedNIK,
                TanggalAbsen = AbsenVM.TanggalAbsen 
            };

            _context.Absen.Add(newAbsen);
            await _context.SaveChangesAsync(); 

  
            ModelState.Clear(); // Clear validation
            AbsenVM = new AbsenViewModel(); // Creat ViewModel
            AbsenVM.EmployeeList = await _context.Employee // Repopulate dropdown
                                            .OrderBy(e => e.Nama)
                                            .Select(e => new SelectListItem
                                            {
                                                Value = e.NIK,
                                                Text = $"{e.NIK} - {e.Nama}"
                                            })
                                            .ToListAsync();
            AbsenVM.TanggalAbsen = DateTime.Today; // Reset date

            await PopulateAbsenRecordsAsync(); // Re-populate

            TempData["SuccessMessage"] = "Absen data saved successfully!"; 
            return Page(); // Stay same page
        }

        // Helper method
        private async Task PopulateAbsenRecordsAsync()
        {
            AbsenVM.AbsenRecords = await (from a in _context.Absen
                                          join e in _context.Employee on a.NIK equals e.NIK
                                          orderby a.TanggalAbsen descending 
                                          select new AbsenRecordViewModel
                                          {
                                              NIK = a.NIK,
                                              Nama = e.Nama,
                                              TanggalAbsen = a.TanggalAbsen
                                          }).ToListAsync();
        }
    }
}
