namespace DeGarage;

internal class Program
{
    static void Main(string[] args)
    {
        ThreadPool.SetMinThreads(50, 50);
        Semaphore stoplicht = new Semaphore(10, 10);

        Parallel.For(1, 51, idx => {
            Car c = new Car(idx.ToString());
            c.KomtBijDeParkeerplaat(stoplicht);
        });

        Console.WriteLine("Klaar");
        Console.ReadLine();
    }
}
