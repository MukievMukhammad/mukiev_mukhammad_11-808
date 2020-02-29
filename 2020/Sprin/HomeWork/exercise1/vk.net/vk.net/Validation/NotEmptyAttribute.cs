using System;
namespace vk.net.Validation
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && value.ToString() != "")
                return true;

            ErrorMessage = "Fileld should not be empty";
            return false;
        }
    }
}
