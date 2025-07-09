using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] 
    public string NIK { get; set; }
    public string Nama { get; set; }

    public ICollection<Absen> Absens { get; set; }
}

