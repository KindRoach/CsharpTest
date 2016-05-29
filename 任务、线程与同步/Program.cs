using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 任务_线程与同步
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.ReadLine();
            //ParallelExample.ForWithSleep(10);
            //ParallelExample.ForWithBreak(1000);
            //ParallelExample.NormalFor(10000000);
            Console.WriteLine(ParallelExample.ForTLocal(10));

            Console.ReadLine();
        }
    }

    public class ParallelExample
    {
        public static void NormalFor(int x)
        {
            for (int i = 0; i < x; i++)
            {
                string result = $"{i}. task:{Task.CurrentId}  thread:{Thread.CurrentThread.ManagedThreadId}";
                //Console.WriteLine(result);
            }
        }

        public static void ForWithSleep(int x)
        {
            ParallelLoopResult loopResult = Parallel.For(0, x, i =>
            {
                string result = $"{i}. task:{Task.CurrentId}  thread:{Thread.CurrentThread.ManagedThreadId}";
                Console.WriteLine(result);

                Thread.Sleep(100);

                result = $"{i}. task:{Task.CurrentId}  thread:{Thread.CurrentThread.ManagedThreadId}";
                Console.WriteLine(result);
            });
            Console.WriteLine($"Loop is completed? {loopResult.IsCompleted}");
        }

        public static void ForWithDelay(int x)
        {
            ParallelLoopResult loopResult = Parallel.For(0, x, async i =>
            {
                string result = $"{i}. task:{Task.CurrentId}  thread:{Thread.CurrentThread.ManagedThreadId}";
                Console.WriteLine(result);

                await Task.Delay(1000);

                result = $"{i}. task:{Task.CurrentId}  thread:{Thread.CurrentThread.ManagedThreadId}";
                Console.WriteLine(result);
            });
            Console.WriteLine($"Loop is completed? {loopResult.IsCompleted}");
        }

        public static void ForWithBreak(int x)
        {
            ParallelLoopResult loopResult = Parallel.For(0, x, (int i, ParallelLoopState pls) =>
            {
                string result = $"{i}. task:{Task.CurrentId}  thread:{Thread.CurrentThread.ManagedThreadId}";
                Console.WriteLine(result);

                if (i > 15)
                    pls.Break();
            });
            Console.WriteLine($"Loop is completed? {loopResult.IsCompleted}");
            Console.WriteLine(loopResult.LowestBreakIteration);
        }

        public static long ForTLocal(int x)
        {
            //Add 0 to x
            int[] nums = Enumerable.Range(0, x).ToArray();
            long total = 0;

            // Use type parameter to make subtotal a long, not an int
            Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += nums[j];
                return subtotal;
            },
                (subtotal) => Interlocked.Add(ref total, subtotal)
            );

            return total;

        }
    }
}
