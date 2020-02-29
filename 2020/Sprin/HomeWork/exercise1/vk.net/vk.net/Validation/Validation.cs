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

            return new ValidationResult(true);
        }
    }
}
