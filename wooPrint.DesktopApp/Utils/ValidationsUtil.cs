using System;
using System.Linq;

namespace wooPrint.DesktopApp.Utils
{
    /// <summary>
    ///     Utility class for perform validations.
    /// </summary>
    public static class ValidationsUtil
    {
        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxTimeOut"></param>
        /// <returns></returns>
        public static bool IsValidTimeout(string text, int maxTimeOut = 600)
        {
            var parseResult = int.TryParse(text, out var timeoutNumber);
            if (!parseResult)
                return false;

            return timeoutNumber >= 0 && timeoutNumber <= maxTimeOut;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string text)
        {
            return Uri.TryCreate(text, UriKind.Absolute, out _);
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmptyValue(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasOnlyNumbers(string value)
        {
            return value.All(char.IsDigit);
        }
    }
}