using Microsoft.AspNetCore.Mvc.Rendering; // Needed for SelectListItem
using System.ComponentModel.DataAnnotations; // Needed for [Display] and [DataType]

public class AbsenViewModel
{
    // Properties for the INPUT form:
    [Display(Name = "NIK - Nama")]
    [Required(ErrorMessage = "Please select an employee.")] // Added validation
    public string SelectedNIK { get; set; }

    public List<SelectListItem> EmployeeList { get; set; } = new List<SelectListItem>(); // Initialize to avoid null reference

    [Display(Name = "Tgl Absen")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please enter an absence date.")] // Added validation
    public DateTime TanggalAbsen { get; set; } = DateTime.Today; // Default to today

    // Properties for the DISPLAY table:
    public List<AbsenRecordViewModel> AbsenRecords { get; set; } = new List<AbsenRecordViewModel>(); // Initialize

}

