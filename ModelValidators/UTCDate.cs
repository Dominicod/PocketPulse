using System.ComponentModel.DataAnnotations;

namespace ModelValidators;

//? This attribute is used to validate that a DateTime property is a valid UTC date.
//? This also checks that the date is not the minimum or maximum date value.
//?
[AttributeUsage(AttributeTargets.Property)]
public class UTCDate : ValidationAttribute
{
    private string _errorMessage = "Error occured on Field {0}";
    
    public UTCDate() => ErrorMessage = _errorMessage;
    
    public override bool IsValid(object? value)
    {
        if (value is not DateTime dateTimeValue) 
            throw new ArgumentException("The UTCDate attribute can only be used on DateTime properties.");

        if (!IsNotMaxOrMinDate(dateTimeValue))
        {
            _errorMessage = "The Field {0} cannot be the minimum or maximum date value.";
            return false;
        }

        if (DateIsUtcFormatted(dateTimeValue)) return true;
        
        _errorMessage = "The Field {0} must be a valid UTC date.";
        return false;
    }

    private static bool IsNotMaxOrMinDate(DateTime date) => date != DateTime.MinValue && date != DateTime.MaxValue;
    
    private static bool DateIsUtcFormatted(DateTime date) => date.Kind == DateTimeKind.Utc;
}