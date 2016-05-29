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
            //Console.WriteLine(ParallelExample.ForTLocal(1000000));
            ParallelExample.ForEachTLocal(1000000);
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
            int[] nums = Enumerable.Range(0, x).ToArray();

            long total = 0;
            Parallel.For<long>(0, nums.Length,
            () =>
             {
                 Console.WriteLine($"Thread{Thread.CurrentThread.ManagedThreadId} start.");
                 return 0;
             },
            (j, loop, subtotal) =>
             {
                 subtotal += nums[j];
                 return subtotal;
             },
            (subtotal) =>
             {
                 Interlocked.Add(ref total, subtotal);
                 Console.WriteLine($"Thread{Thread.CurrentThread.ManagedThreadId} end.");
             }
            );

            return total;

        }

        public static void ForEachTLocal(int x)
        {
            int[] nums = Enumerable.Range(0, x).ToArray();
            long total = 0;
            ParallelLoopResult result =
            Parallel.ForEach<int, long>(nums, () => 0,
            (i, pls, subtotal) =>
            {
                subtotal += nums[i];
                return subtotal;
            },
            (subtotal) =>
            {
                Interlocked.Add(ref total, subtotal);
            });
            Console.WriteLine(total);
        }
    }
}
