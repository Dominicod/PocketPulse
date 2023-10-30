using System.ComponentModel.DataAnnotations;

namespace ModelValidators;

/// <summary>
/// This attribute is used to validate that a Guid property is not an empty guid.
/// Reason why this is needed is because the Required attribute does not work on Guids (Defaults to Guid.Empty).
/// We also want to ensure that the user cannot accidentally create entities in the database with an empty guid.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class NonEmptyGuid : ValidationAttribute
{
    public NonEmptyGuid() => ErrorMessage = "The Field {0} must not be an empty guid.";
    public override bool IsValid(object? value)
    {
        if (value is not Guid gValue) 
            throw new ArgumentException("The NonEmptyGuid attribute can only be used on Guid properties.");
        
        return value switch
        {
            //? Let Required attribute handle null values
            //?
            null => true,
            Guid guidValue => guidValue != Guid.Empty,
            _ => false
        };
    }
}