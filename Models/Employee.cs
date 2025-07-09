using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // If NIK is manually assigned and not auto-incremented
    public string NIK { get; set; }
    public string Nama { get; set; }

    // Optional: Navigation property for Absen if you want to link them
    public ICollection<Absen> Absens { get; set; }
}

