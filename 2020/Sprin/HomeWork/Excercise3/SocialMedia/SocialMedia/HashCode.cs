using System;
namespace SocialMedia
{
    public static class MyHashCode
    {
        public static double GetHash(string inputString)
        {
            var fibonach1 = 1;
            var fibonach2 = 1;
            var resultHash = 0d;

            foreach (var ch in inputString)
            {
                var fibonach3 = fibonach2 + fibonach1;

                var hashPart = ch * fibonach3 % 11 + 0.2512846;
                resultHash += hashPart;

                fibonach1 = fibonach2;
                fibonach2 = fibonach3;
            }

            return resultHash;
        }
    }
}
