using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;


public class Absen
{
    [Key]
    public int Id { get; set; } // Primary key for Absen table
    public string NIK { get; set; }
    public DateTime TanggalAbsen { get; set; }

    [ForeignKey("NIK")]
    public Employee Employee { get; set; } // Navigation property to Employee
}
