using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelValidators;
using Utilities.BaseModels;
using Utilities.Enums;

namespace Financial.API.Models;

public class Bill : BaseModel
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