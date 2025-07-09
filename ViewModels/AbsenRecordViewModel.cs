using System;
using System.ComponentModel.DataAnnotations; 

public class AbsenRecordViewModel
{
    [Display(Name = "NIK")]
    public string NIK { get; set; }

    [Display(Name = "Nama")]
    public string Nama { get; set; }

    [Display(Name = "Tanggal Absen")]
    [DataType(DataType.Date)] 
    public DateTime TanggalAbsen { get; set; }
}