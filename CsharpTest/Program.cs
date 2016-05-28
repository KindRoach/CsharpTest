using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.Threading;
using System.IO;

namespace CsharpTest
{

    class Program
    {
        static void Main(string[] args)
        {

            BigNumber bn1 = new BigNumber("999999999");  //5 2000 0000 0000 0000
            BigNumber bn2 = new BigNumber("10000000000000000");
            BigNumber bn3 = BigNumber.Plus(bn1, bn2);
            Console.WriteLine(bn1.Decimal);
            Console.WriteLine(bn2.Decimal);
            Console.WriteLine(BigNumber.Plus(bn1, bn2).Decimal);
            Console.WriteLine(BigNumber.multiplyByNumber(bn1, 9999).Decimal);
            Console.WriteLine(BigNumber.multiplyByBigNumber(bn1,bn2).Decimal);
        }


    }

    public class BigNumber
    {
        public string Binary { get; set; }
        public string Decimal { get; set; }
        public List<int> Nums { get; set; }
        public static readonly int MAX_INT_LENGTH = 64;

        /// <summary>
        /// 通过表示十进制的string构造BigNumber实例
        /// </summary>
        /// <param name="newDecimal"></param>
        public BigNumber(string newDecimal)
        {
            Decimal = newDecimal;
            int lengthBy4 = Decimal.Length / 4;
            if (Decimal.Length % 4 != 0) lengthBy4++;
            Nums = new List<int>(lengthBy4);
            for (int i = 0; i < lengthBy4; i++)
            {
                if (i * 4 + 4 > Decimal.Length)
                {
                    Nums.Add(Convert.ToInt32(newDecimal.Substring(0, newDecimal.Length % 4)));
                }
                else
                {
                    Nums.Add(Convert.ToInt32(newDecimal.Substring(newDecimal.Length - i * 4 - 4, 4)));
                }
            }
        }

        /// <summary>
        /// 将存放4位整数的List转为BigNumber
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BigNumber ListToBigNumber(List<int> source)
        {
            StringBuilder resultString = new StringBuilder(MAX_INT_LENGTH + 1);
            for (int i = 0; i < source.Count - 1; i++)
            {
                resultString.Insert(0, source[i]);
                if (source[i] <= 999 && source[i] >= 100)
                {
                    resultString.Insert(0, '0');
                }
                else if (source[i] >= 10 && source[i] <= 99)
                {
                    resultString.Insert(0, "00");
                }
                else if (source[i] >= 0 && source[i] <= 9)
                {
                    resultString.Insert(0, "000");
                }
            }
            resultString.Insert(0, source[source.Count - 1]);
            return new BigNumber(resultString.ToString());
        }

        /// <summary>
        /// 对存放4位整数的List进行进位操作
        /// </summary>
        /// <param name="source"></param>
        public static void CompleteCarry(List<int> source)
        {
            int carry = 0;
            for (int i = 0; i < source.Count; i++)
            {
                source[i] = source[i] + carry;
                carry = source[i] / 10000;
                source[i] = source[i] % 10000;
            }
            while (carry != 0)
            {
                source.Add(carry % 10000);
                carry = carry / 10000;
            }
        }

        /// <summary>
        /// 返回两个BigNumber相加的结果
        /// </summary>
        /// <param name="L"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        public static BigNumber Plus(BigNumber L, BigNumber R)
        {
            List<int> resultList = new List<int>(MAX_INT_LENGTH / 4 + 1);
            int minLength = Math.Min(L.Nums.Count, R.Nums.Count);
            for (int i = 0; i < minLength; i++)
            {
                resultList.Add(L.Nums[i] + R.Nums[i]);
            }
            if (L.Nums.Count > minLength)
            {
                for (int i = minLength; i < L.Nums.Count; i++)
                {
                    resultList.Add(L.Nums[i] + 0);
                }

            }
            else if (R.Nums.Count > minLength)
            {
                for (int i = minLength; i < R.Nums.Count; i++)
                {
                    resultList.Add(0 + R.Nums[i]);
                }
            }
            CompleteCarry(resultList);
            return ListToBigNumber(resultList);



        }

        //public static BigNumber Power(BigNumber X,BigNumber Y)
        //{
        //    BigNumber resultBigNumber = new BigNumber("1");
        //    for (int i = 0; i < Y; i++)
        //    {
        //        resultBigNumber = multiplyByBigNumber(resultBigNumber, X);
        //    }
        //    return resultBigNumber;
        //}

        /// <summary>
        /// 返回BigNumber与一个4位整数相乘的结果
        /// </summary>
        /// <param name="L"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        public static BigNumber multiplyByNumber(BigNumber L, int R)
        {
            List<int> resultList = new List<int>(L.Nums);
            for (int i = 0; i < resultList.Count; i++)
            {
                resultList[i] = resultList[i] * R;
            }
            CompleteCarry(resultList);
            return ListToBigNumber(resultList);
        }

        public static BigNumber multiplyByBigNumber(BigNumber L, BigNumber R)
        {
            BigNumber reusltBigNumber = new BigNumber("0");
            for (int i = 0; i < R.Nums.Count; i++)
            {
                reusltBigNumber = Plus(reusltBigNumber, multiplyByNumber(multiplyByNumber(L, R.Nums[i]),10000));
            }
            return reusltBigNumber;
        }



        //static string ToDecimal(string binary)
        //{
        //    StringBuilder result = new StringBuilder(128);

        //}



    }



}
