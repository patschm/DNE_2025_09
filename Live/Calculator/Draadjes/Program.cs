using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Draadjes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TestSync();
            //MetThreads();
            // APMVariant();
            //TaskVariant();
            //EnFoutenDan();
            //TeLang();
            OokWelGeinigAsync();

            Console.WriteLine("Einde");
            Console.ReadLine();
        }

        private static async Task OokWelGeinigAsync()
        {
            Task<int> t1 = Task.Run(() => LongAdd(5, 6));

            int result = await t1;
            Console.WriteLine(result);

            result = await LongAddAsync(9, 10);
            Console.WriteLine(result);

            try
            {
                await Task.Run(() =>
                {
                    Task.Delay(1000).Wait();
                    Console.WriteLine("We leven nog net");
                    throw new Exception("Ooops");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void TeLang()
        {
            CancellationTokenSource nikko = new CancellationTokenSource();

            CancellationToken bommetje = nikko.Token;
            Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Task.Delay(100).Wait();
                    Console.WriteLine($"Nummertje {i}");
                    if (bommetje.IsCancellationRequested)
                    {
                        Console.WriteLine("Doei!");
                        return;
                    }
                }
            });

            nikko.CancelAfter(3000);


            //Task.Delay(10000).Wait();
            //nikko.Cancel();

        }

        private static void EnFoutenDan()
        {
            //try
            //{
            Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                Console.WriteLine("We leven nog net");
                throw new Exception("Ooops");
            }).ContinueWith(pt =>
            {
                Console.WriteLine(pt.IsFaulted);
                if (pt.Exception != null)
                {
                    Console.WriteLine(pt.Exception.InnerException.Message);
                }
            });
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        private static void TaskVariant()
        {
            //ThreadPool.QueueUserWorkItem(DoeIets);


            Task<int> t1 = Task.Run(() => LongAdd(5, 6));
            //var t1 = new Task<(int, int, int Res)>(() =>
            //{
            //    int result = LongAdd(1, 2);
            //    return (1, 2, result);
            //});
            //t1.Wait();
            t1.ContinueWith(previousTask =>
            {
                Console.WriteLine(previousTask.Result);
            });
            t1.ContinueWith(previousTask =>
            {
                Console.WriteLine("Punt");
            });

            // t1.Start();

            //while (!t1.IsCompleted)
            //{
            //    Thread.Sleep(100);
            //    Console.Write(".");
            //}
            //Console.WriteLine(t1.Result); // Result is blocking
        }

        private static void DoeIets(object state)
        {
            int result = LongAdd(1, 2);
            Console.WriteLine(result);
        }

        private static void APMVariant()
        {
            Func<int, int, int> del = LongAdd;

            // Werkt niet in moderne .NET
            IAsyncResult ar = del.BeginInvoke(2, 3, null, null);
            while (!ar.IsCompleted)
            {
                Thread.Sleep(100);
                Console.Write(".");
            }
            int result = del.EndInvoke(ar);
            Console.WriteLine(result);
        }

        private static void MetThreads()
        {
            // Bij voorkeur niet gebruiken
            ThreadStart add = new ThreadStart(() =>
            {
                int result = LongAdd(1, 2);
                Console.WriteLine(result);
            });
            Thread thr = new Thread(add);
            thr.IsBackground = false;

            thr.Start();
        }

        private static void TestSync()
        {
            int result = LongAdd(1, 2);
            Console.WriteLine(result);
        }

        static int LongAdd(int a, int b)
        {
            Task.Delay(5000).Wait();
            return a + b;
        }
        static Task<int> LongAddAsync(int a, int b)
        {
            return Task.Run(() => LongAdd(a, b));
        }
    }
}
