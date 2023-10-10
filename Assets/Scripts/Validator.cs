using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sky9th
{
    public static class Validator
    {
        // Regular expression matching validation
        public static bool MatchRegexp(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        // Validate email address
        public static bool IsEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }

        // Validate if string is empty
        public static bool IsEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        // Validate if string is required (non-empty)
        public static bool Required(object input)
        {
            if (input == null)
            {
                return false;
            }

            if (input is string inputString)
            {
                return !string.IsNullOrEmpty(inputString.Trim());
            }
            else if (input is bool inputBool)
            {
                return true; // 针对bool类型的输入，要根据具体需求返回true或false
            }
            else if (input is int inputInt)
            {
                return true; // 针对int类型的输入，要根据具体需求返回true或false
            }
            else if (input is float inputFloat)
            {
                return true; // 针对float类型的输入，要根据具体需求返回true或false
            }
            else
            {
                return false; // 对于其他类型的输入，暂时返回false
            }
        }

        // Trim whitespace characters from both ends of the string
        public static string Trim(string input)
        {
            return input.Trim();
        }

        // Validate if input is a number
        public static bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }

        // Validate if input is a floating-point number
        public static bool IsFloat(string input)
        {
            return float.TryParse(input, out _);
        }

        // Validate if input is a positive number
        public static bool IsPositive(string input)
        {
            if (float.TryParse(input, out float number))
            {
                return number > 0;
            }
            return false;
        }

        // Validate if input is greater than or equal to a minimum number
        public static bool MinNumber(string input, int min)
        {
            if (int.TryParse(input, out int number))
            {
                return number >= min;
            }
            return false;
        }

        // Validate if input is less than or equal to a maximum number
        public static bool MaxNumber(string input, int max)
        {
            if (int.TryParse(input, out int number))
            {
                return number <= max;
            }
            return false;
        }

        // Validate if input is greater than or equal to a minimum floating-point number
        public static bool MinFloat(string input, float min)
        {
            if (float.TryParse(input, out float number))
            {
                return number >= min;
            }
            return false;
        }

        // Validate if input is less than or equal to a maximum floating-point number
        public static bool MaxFloat(string input, float max)
        {
            if (float.TryParse(input, out float number))
            {
                return number <= max;
            }
            return false;
        }

        // Validate if input is a non-empty string
        public static bool IsString(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        // Validate minimum length of a string
        public static bool MinStringLength(string input, int min)
        {
            return input.Length >= min;
        }

        // Validate maximum length of a string
        public static bool MaxStringLength(string input, int max)
        {
            return input.Length <= max;
        }

        // Validate if file size is less than the maximum limit (in bytes)
        public static bool MaxFileSize(byte[] fileData, int maxSize)
        {
            return fileData.Length <= maxSize;
        }

        // Validate if file extension is in the list of allowed extensions
        public static bool AllowedExtensions(string fileName, string[] allowedExtensions)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }
}