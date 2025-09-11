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
            TaskVariant();

            Console.WriteLine("Einde");
            Console.ReadLine();
        }

        private static void TaskVariant()
        {
            //ThreadPool.QueueUserWorkItem(DoeIets);


            Task<int> t1 = Task.Run(() =>LongAdd(5, 6));
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
            t1.ContinueWith(previousTask => {
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
            while(!ar.IsCompleted)
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
            ThreadStart add = new ThreadStart(() => {
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
}
}
