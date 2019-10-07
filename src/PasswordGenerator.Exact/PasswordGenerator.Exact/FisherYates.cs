using System;
using System.Collections.Generic;
using System.Text;

namespace clearwaterstream.Security
{
    public static class FisherYates
    {
        static int[] GetShuffleExchanges(int key, int size)
        {
            int[] exchanges = new int[size - 1];

            var rand = new Random(key);

            for (int i = size - 1; i > 0; i--)
            {
                int n = rand.Next(i + 1);

                exchanges[size - 1 - i] = n;
            }

            return exchanges;
        }

        public static string Shuffle(int key, string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var charArr = value.ToCharArray();

            Shuffle(charArr, key);

            return new string(charArr);
        }

        public static void Shuffle<T>(this IList<T> arr, int key)
        {
            if (arr == null || arr.Count <= 1)
                return;

            int size = arr.Count;

            var exchanges = GetShuffleExchanges(key, size);

            for (int i = size - 1; i > 0; i--)
            {
                int n = exchanges[size - 1 - i];
                var tmp = arr[i];
                arr[i] = arr[n];
                arr[n] = tmp;
            }
        }

        public static void DeShuffle<T>(this IList<T> arr, int key)
        {
            if (arr == null || arr.Count <= 1)
                return;

            int size = arr.Count;

            var exchanges = GetShuffleExchanges(key, size);

            for (int i = 1; i < size; i++)
            {
                int n = exchanges[size - i - 1];
                var tmp = arr[i];
                arr[i] = arr[n];
                arr[n] = tmp;
            }
        }

        public static string DeShuffle(int key, string shuffledValue)
        {
            if (string.IsNullOrEmpty(shuffledValue))
                return shuffledValue;

            var charArr = shuffledValue.ToCharArray();

            DeShuffle(charArr, key);

            return new string(charArr);
        }
    }
}
