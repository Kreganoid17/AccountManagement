using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Helpers.DataValidations;

public class TransactionAmountAttribute : ValidationAttribute
{
    public TransactionAmountAttribute()
    {
        
    }

    public override bool IsValid(object value)
    {
        if (value is double doubleValue)
        {
            return doubleValue != 0;
        }

        return false;
    }
}
