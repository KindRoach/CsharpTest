using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace TAP异步编程
{
    class Program
    {
        static void Main(string[] args)
        {
            //DontHandle();
            //CallerGreetingAsync();
            //MultipleAsyncMethods();
            //StartTwoTasks();
            //StartTwoTasksParallel();
            //CancellationTokenSource
            //ShowAggregatedException();

            var cancelTokenSource = new CancellationTokenSource();
            Task t = Task.Factory.StartNew(() =>
            {
                while (!cancelTokenSource.IsCancellationRequested)
                {
                    WriteLine(DateTime.Now);
                    Thread.Sleep(1000);
                }
            }, cancelTokenSource.Token);

            Console.WriteLine(t.Status);
            Thread.Sleep(2000);
            Console.WriteLine(t.Status);
            cancelTokenSource.Cancel();
            Console.WriteLine(t.Status);


            Console.ReadLine();
        }

        private static async void ShowAggregatedException()
        {
            Task taskResult = null;
            try
            {
                Task t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await (taskResult = Task.WhenAll(t1, t2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("handled {0}", ex.Message);
                foreach (var ex1 in taskResult.Exception.InnerExceptions)
                {
                    Console.WriteLine("inner exception {0}", ex1.Message);
                }
            }
        }

        private async static void StartTwoTasksParallel()
        {
            Task t1 = ThrowAfter(2000, "first");
            Task t2 = ThrowAfter(1000, "second");
            try
            {
                await Task.WhenAll(t1, t2);
            }
            catch (Exception /*ex*/)
            {
                // just display the exception information of the first task
                // that is awaited within WhenAll
                //Console.WriteLine("handled {0}", ex.Message);
                if (t1.IsFaulted)
                {
                    Console.WriteLine(t1.Exception.InnerException);
                }
                if (t2.IsFaulted)
                {
                    Console.WriteLine(t2.Exception.Message);
                }
            }
        }

        private static async void StartTwoTasks()
        {
            try
            {
                await ThrowAfter(2000, "first");
                await ThrowAfter(1000, "second"); // the second call is not invoked
                                                  // because the first method throws
                                                  // an exception
            }
            catch (Exception ex)
            {
                Console.WriteLine("handled {0}", ex.Message);
            }
        }

        static async Task ThrowAfter(int ms, string message)
        {
            await Task.Delay(ms);
            Console.WriteLine(ms);
            throw new Exception(message);

        }

        private static async void DontHandle()
        {
            try
            {
                await ThrowAfter(200, "first");
                // exception is not caught because this method is finished
                // before the exception is thrown
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Finished");
        }

        static string Greeting(string name)
        {
            Thread.Sleep(3000);
            Console.WriteLine(name);
            return string.Format("Hello, {0}", name);
        }

        static Task<string> GreetingAsync(string name)
        {
            return Task.Run<string>(() =>
            {
                return Greeting(name);
            });
        }

        static Task SayHelloAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Hello!");
            });
        }

        private static void CallerGreetingAsync()
        {
            Task t = GreetingAsync("Stephanie");
            Console.WriteLine(t.IsCompleted);
            Console.WriteLine("Done");
        }

        private async static void CallerWithAsync2()
        {
            Console.WriteLine(await GreetingAsync("Stephanie"));
        }

        private static void CallerWithContinuationTask()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            t1.ContinueWith(t =>
            {
                string result = t.Result;
                Console.WriteLine("String: \"{0}\" has been made!", result);
            });
        }


        private static void MultipleAsyncMethods()
        {
            //string s1 = await GreetingAsync("Stephanie");
            //string s2 = await GreetingAsync("Matthias");
            //Console.WriteLine("Finished both methods.\n " + "Result 1: {0}\n Result 2: {1}", s1, s2);
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            Task<string[]> t3 = Task.WhenAll(t1, t2);
            Console.WriteLine(t3.Status);
            //Console.WriteLine("Finished both methods.\n " + "Result 1: {0}\n Result 2: {1}", t1.Result, t2.Result);
        }

        private static Func<string, string> greetingInvoker = Greeting;
        static IAsyncResult BeginGreeting(string name, AsyncCallback callback, object state)
        {
            return greetingInvoker.BeginInvoke(name, callback, state);
        }
        static string EndGreeting(IAsyncResult ar)
        {
            return greetingInvoker.EndInvoke(ar);
        }

        private static async void ConvertingAsyncPattern()
        {
            string s = await Task<string>.Factory.FromAsync<string>(BeginGreeting, EndGreeting, "Angela", null);
            Console.WriteLine(s);
        }


    }
}
