using System;
namespace vk.net.Validation
{
    public abstract class ValidationAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
        public abstract bool IsValid(object value);
    }
}
