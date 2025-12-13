using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ficha6.Models;

[Table("student")]
public partial class Student
{
    [Key]
    [Column("number")]
    public int Number { get; set; }

    [Column("name")]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("class_id")]
    public int? ClassId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Students")]
    public virtual Class? Class { get; set; }
}
