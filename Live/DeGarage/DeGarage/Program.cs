namespace DeGarage;

internal class Program
{
    static void Main(string[] args)
    {
        Parallel.For(1, 51, idx => {
            Car c = new Car(idx.ToString());
            c.KomtBijDeParkeerplaat();
        });

        Console.ReadLine();
    }
}
