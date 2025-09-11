namespace DeGarage;

internal class Car
{
    private readonly string _kenteken;

    public Car(string kenteken)
    {
        _kenteken = kenteken;
    }

    public void KomtBijDeParkeerplaat()
    {
        Task.Run(() =>
        {
            Console.WriteLine($"De Auto {_kenteken} komt eraan");

            Console.WriteLine($"De Auto {_kenteken} rijdt de parkeergarage in");

            Task.Delay(10000 + Random.Shared.Next(0, 10000));
            Console.WriteLine($"De auto {_kenteken} rijdt eruit");
        });
    }
}
