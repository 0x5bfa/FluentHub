namespace FluentHub.Uwp.Helpers
{
    public static class NumberNormalizerHelper
    {
        private const int Thousand = 1000;
        private const int Million = 1000000;
        private const int Billion = 1000000000;

        /// <summary>
        /// This method converts numbers (greater then 999) to a readable form, such as 25.5k, 1.2m, 2.4b  
        /// </summary>
        /// <param name="number">Positive integer value that is more than 999</param>
        /// <returns>String value of normalized number (e.g. 25.5k)</returns>
        public static string NormalizeNumber(int number)
        {
            string normalizedNumber;

            if (number >= Billion)
            {
                double temp = Math.Round((double)number / Billion, 1);
                normalizedNumber = $"{temp}b";
            }
            else if (number >= Million)
            {
                double temp = Math.Round((double)number / Million, 1);
                normalizedNumber = $"{temp}m";
            }
            else if (number >= Thousand)
            {
                double temp = Math.Round((double)number / Thousand, 1);
                normalizedNumber = $"{temp}k";
            }
            else
            {
                normalizedNumber = number.ToString();
            }

            return normalizedNumber;
        }
    }
}
