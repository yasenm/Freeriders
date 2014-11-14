namespace FreeRiders.Data.Common
{
    using System;
    using System.Text;

    public static class RandomGenerator
    {
        private static Random random = new Random();
        private static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUWXYZabcdefghijklmnopqrstuwxyz";

        public static string RandomString(int minLength, int maxLength)
        {
            StringBuilder result = new StringBuilder();
            var length = RandomNumber(minLength, maxLength);

            for (int i = 0; i < length; i++)
            {
                result.Append(Alphabet[RandomNumber(0, Alphabet.Length - 1)]);
            }

            return result.ToString();
        }

        public static int RandomNumber(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue + 1);
        }
    }
}
