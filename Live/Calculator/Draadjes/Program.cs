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
            //OokWelGeinigAsync();

            //BerenOpDeWeg();
            //AndereBlokkers();
            ReadersAndWriters();
            //ZakLampenAsync();

            Console.WriteLine("Einde");
            Console.ReadLine();
        }

        private static async Task ZakLampenAsync()
        {
            int a = 0;
            int b = 0;

            AutoResetEvent zl1 = new AutoResetEvent(false);
            AutoResetEvent zl2 = new AutoResetEvent(false);


            var t1 =Task.Run(() => {
                Task.Delay(1000).Wait();
                a = 10;
                //zl1.Set();
            });

            var t2 = Task.Run(() => {
                Task.Delay(2000).Wait();
                b = 20;
                //zl2.Set();
            });


            await Task.WhenAll(t1, t2);
            //WaitHandle.WaitAny(new AutoResetEvent[] { zl1, zl2});
            Console.WriteLine(a + b);
        }

        private static void AndereBlokkers()
        {
            Barrier b = new Barrier(10);
            Task.Run(() => { 
            
                b.SignalAndWait(); // Wait for 10 threads to arrive
                // then continue
            });
         
            

            CountdownEvent cdev = new CountdownEvent(10); // We need ten approvals to continue;

            Task.Run(() => {
                cdev.Signal();
            });

            cdev.Wait(); // Wait till all 10 tasks sent a signal
            // Then continue

            Semaphore sema = new Semaphore(10, 10);
        }


        private static void ReadersAndWriters()
        {
            var rnd = new Random();
            var rwl = new ReaderWriterLock();

            ThreadPool.SetMinThreads(10, 10);
            for (var i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(Reader, i);
            }

            void Reader(object nr)
            {
                for (var j = 0; j < 10; j++)
                {
                    var useTime = rnd.Next(1000, 5000);
                    Console.WriteLine($"Client {nr} staat voor de reader lock");
                    rwl.AcquireReaderLock(Timeout.Infinite);
                    Console.WriteLine($"Client {nr} is reading...");
                    Thread.Sleep(useTime);
                    if (useTime > 4000)
                    {
                        var cookie = rwl.UpgradeToWriterLock(Timeout.Infinite);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Client {nr} is writing...");
                        Thread.Sleep(rnd.Next(5000, 10000));
                        Console.WriteLine($"Client {nr} finished writing");
                        Console.ResetColor();
                        rwl.DowngradeFromWriterLock(ref cookie);
                    }
                    Console.WriteLine($"Client {nr} finished reading");
                    rwl.ReleaseReaderLock();
                    Console.WriteLine($"Client {nr} releast de reader lock");
                }
            }
        }

        static int teller = 0;
        static object stokje = new object();
        

        private static void BerenOpDeWeg()
        {
            ThreadPool.SetMinThreads(10, 10);

            // Naar kijken
            Parallel.For(0, 10, idx => {
             ReaderWriterLock locker = new ReaderWriterLock();
                locker.AcquireReaderLock(10000);
               
                    Console.WriteLine($"Nu bezig: {Thread.CurrentThread.ManagedThreadId}");
                     locker.UpgradeToWriterLock(1000);
                        int tmp = teller;
                        Task.Delay(100).Wait();            
                        teller = ++tmp;
                        Console.WriteLine(teller);
                locker.ReleaseWriterLock();
               
                locker.ReleaseLock();
            });
            //Parallel.For(0, 10, idx => {
            //    lock (stokje)
            //    {
            //        Console.WriteLine($"Nu bezig: {Thread.CurrentThread.ManagedThreadId}");
            //        int tmp = teller;
            //        Task.Delay(100).Wait();
            //        teller = ++tmp;
            //        Console.WriteLine(teller);
            //    }
            //});
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
