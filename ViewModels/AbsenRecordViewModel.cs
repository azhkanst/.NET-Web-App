using System;
using System.ComponentModel.DataAnnotations; // Optional, for display names

public class AbsenRecordViewModel
{
    [Display(Name = "NIK")]
    public string NIK { get; set; }

    [Display(Name = "Nama")]
    public string Nama { get; set; }

    [Display(Name = "Tanggal Absen")]
    [DataType(DataType.Date)] // To format date nicely in display
    public DateTime TanggalAbsen { get; set; }
}