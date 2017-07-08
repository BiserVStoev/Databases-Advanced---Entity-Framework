namespace _02.CreateUser.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class TagAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string stringifiedValue = value as string;

            if (stringifiedValue == null)
            {
                return false;
            }

            if (!stringifiedValue.StartsWith("#"))
            {
                return false;
            }

            if (stringifiedValue.Length > 20)
            {
                return false;
            }

            if (stringifiedValue.Contains(" ") || stringifiedValue.Contains("    "))
            {
                return false;
            }

            return true;
        }
    }
}
