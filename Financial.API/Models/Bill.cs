using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financial.API.DTOs;
using ModelValidators;
using Utilities.BaseModels;
using Utilities.Enums;

namespace Financial.API.Models;

public class Bill : BaseModel
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

    public Bill()
    {
    }

    public Bill(BillDTO billDTO)
    {
        Id = billDTO.Id;
        NickName = billDTO.NickName;
        Cost = billDTO.Cost;
        DueDate = billDTO.DueDate;
        BillType = billDTO.BillType;
        
        UserId = billDTO.UserId;
    }
}