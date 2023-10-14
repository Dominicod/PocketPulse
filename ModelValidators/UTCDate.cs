using System.ComponentModel.DataAnnotations;

namespace ModelValidators;

/// <summary>
/// This attribute is used to validate that a DateTime property is a valid UTC date.
/// This also checks that the date is not the minimum or maximum date value.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class UTCDate : ValidationAttribute
{
    public UTCDate() => ErrorMessage = "Error occured on Field {0}";
    
    public override bool IsValid(object? value)
    {
        if (value is not DateTime dateTimeValue) 
            throw new ArgumentException("The UTCDate attribute can only be used on DateTime properties.");

        if (!IsNotMaxOrMinDate(dateTimeValue))
        {
            ErrorMessage = "The Field {0} cannot be the minimum or maximum date value.";
            return false;
        }

        if (DateIsUtcFormatted(dateTimeValue)) return true;
        
        ErrorMessage = "The Field {0} must be a valid UTC date.";
        return false;
    }

    private static bool IsNotMaxOrMinDate(DateTime date) => date != DateTime.MinValue && date != DateTime.MaxValue;
    
    private static bool DateIsUtcFormatted(DateTime date) => date.Kind == DateTimeKind.Utc;
}