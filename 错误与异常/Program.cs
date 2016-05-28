using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 错误与异常
{
    class Program
    {
        static void Main(string[] args)
        {
            ThrowException(0);
        }

        static void ThrowException(int x)
        {

            try
            {
                if (x >= 10)
                {
                    Exception ex = new Exception("Test") { HelpLink = "HelpLink" };
                    ex.Data.Add("Time", DateTime.Now);
                    throw ex;
                }
                else
                    ThrowException(x + 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data["Time"]);
                Console.WriteLine(ex.HelpLink);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.TargetSite);
                Console.WriteLine(ex.HResult);
                //Console.WriteLine(ex);
                Console.WriteLine($"Exception has been handled in {x}");
            }

            Console.WriteLine($"{x} work very well");
        }
    }


}
