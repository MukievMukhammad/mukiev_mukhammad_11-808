using System;
using System.Collections.Generic;

namespace vk.net.Validation
{
    public class TextStartUppercaseAttribute : ValidationAttribute
    {
        private readonly List<char> upperAlphabet = new List<char>()
        {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        public override bool IsValid(object value)
        {
            var name = value as string;
            if (name != null && upperAlphabet.Contains(name[0]))
                return true;

            ErrorMessage = "Name should start with Uppercase letter";
            return false;
        }
    }
}
