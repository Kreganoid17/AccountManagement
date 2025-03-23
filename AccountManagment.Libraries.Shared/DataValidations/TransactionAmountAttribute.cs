using System.ComponentModel.DataAnnotations;

namespace AccountManagment.Libraries.Shared.DataValidations
{
    public class TransactionAmountAttribute: ValidationAttribute
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
}
