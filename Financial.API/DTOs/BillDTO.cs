using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelValidators;
using Utilities.BaseDTOs;
using Utilities.Enums;

namespace Financial.API.DTOs;

public class BillDTO : BaseDTO
{
    [Required]
    [StringLength(100)]
    public string NickName { get; set; } = string.Empty;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cost { get; set; }
    [Required]
    [UTCDate]
    public DateTime DueDate { get; set; }
    [Required]
    public BillType BillType { get; set; }
}