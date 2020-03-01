using System;
namespace vk.net.Validation
{
    public class Validation
    {
        public static ValidationResult Validate(object obj)
        {
            // здесь пробежать по всем полям модели с использованием рефлексии
            // и если они имеют атрибут потомок ValidationAttribute
            // вызвать соответствующий метод IsValid
            var newObj = obj.GetType().GetProperties();
            foreach(var o in newObj)
            {
                foreach(ValidationAttribute attr in o.GetCustomAttributes(true))
                {
                    if (!attr.IsValid(o.GetValue(obj)))
                        return new ValidationResult(false, attr.ErrorMessage);
                }
            }

            return new ValidationResult(true);
        }
    }
}
