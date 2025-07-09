using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class AbsenViewModel
{
    // Properties for the INPUT form:
    [Display(Name = "NIK - Nama")]
    [Required(ErrorMessage = "Please select an employee.")]
    public string SelectedNIK { get; set; }

    public List<SelectListItem> EmployeeList { get; set; } = new List<SelectListItem>(); 

    [Display(Name = "Tgl Absen")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please enter an absence date.")] 
    public DateTime TanggalAbsen { get; set; } = DateTime.Today; 


    public List<AbsenRecordViewModel> AbsenRecords { get; set; } = new List<AbsenRecordViewModel>(); 

}

