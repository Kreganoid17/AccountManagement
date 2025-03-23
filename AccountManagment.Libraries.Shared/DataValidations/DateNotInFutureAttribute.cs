using System.ComponentModel.DataAnnotations;

namespace AccountManagment.Libraries.Shared.DataValidations
{
    internal class DateNotInFutureAttribute : ValidationAttribute
    {
        public DateNotInFutureAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }

            return true;
        }
    }
}
