using System;
using System.ComponentModel.DataAnnotations;

namespace corea;

public class User
{

    [Key]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
