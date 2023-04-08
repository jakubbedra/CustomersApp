using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersApp.Model;

[Table("customers")]
public class Customer
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("surname")]
    public string? Surname { get; set; }

    [Column("certificate_number")]
    public string? CertificateNumber { get; set; }
    
    [Column("sex")]
    public char Sex { get; set; }

    [Column("date_of_birth")]
    public DateOnly? DateOfBirth{ get; set; }

    [Column("place_of_birth")]
    public string? PlaceOfBirth{ get; set; }

    [Column("date_of_death")]
    public DateOnly? DateOfDeath{ get; set; }

    [Column("place_of_death")]
    public string? PlaceOfDeath{ get; set; }
    
    [Column("death_certificate_number")]
    public string? DeathCertificateNumber { get; set; }
    
    [Column("issued_by")]
    public string? IssuedBy { get; set; }
    
    [Column("issue_date")]
    public DateOnly? IssueDate { get; set; }
    
    [Column("address")]
    public string? Address { get; set; }
    
}
