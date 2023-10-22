using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financial.API.Models;
using ModelValidators;
using Utilities.BaseDTOs;
using Utilities.Enums;

namespace Financial.API.DTOs;

public class BillDTO : BaseDTO
{
    [StringLength(100)]
    public string? NickName { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cost { get; set; }
    [UTCDate]
    public DateTime DueDate { get; set; }
    public BillType BillType { get; set; }
    
    [NonEmptyGuid]
    public Guid UserId { get; set; }
    
    public BillDTO()
    {
    }

    public BillDTO(Bill bill)
    {
        Id = bill.Id;
        NickName = bill.NickName;
        Cost = bill.Cost;
        DueDate = bill.DueDate;
        BillType = bill.BillType;
        
        UserId = bill.UserId;
        
        CreatedAt = bill.CreatedAt;
        UpdatedAt = bill.UpdatedAt;
    }
}