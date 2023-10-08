using Financial.API.DTOs;
using Financial.API.Models;

namespace Financial.Test.Factories;

public static class BillFactory
{
    # region Bill
    public static Faker<Bill> BuildBill()
    {
        var faker = new Faker<Bill>();
        return faker
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.Cost, f => f.Random.Decimal())
            .RuleFor(a => a.DueDate, f => f.Date.Future())
            .RuleFor(a => a.NickName, f => f.Lorem.Word())
            .RuleFor(a => a.UserId, f => f.Random.Guid())
            .RuleFor(a => a.CreatedAt, f => f.Date.Past())
            .RuleFor(a => a.UpdatedAt, f => f.Date.Past());
    }
    
    public static Faker<BillDTO> BuildBillDTO()
    {
        var faker = new Faker<BillDTO>();
        return faker
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.Cost, f => f.Random.Decimal())
            .RuleFor(a => a.DueDate, f => f.Date.Future())
            .RuleFor(a => a.NickName, f => f.Lorem.Word())
            .RuleFor(a => a.UserId, f => f.Random.Guid())
            .RuleFor(a => a.CreatedAt, f => f.Date.Past())
            .RuleFor(a => a.UpdatedAt, f => f.Date.Past());
    }
    # endregion
}