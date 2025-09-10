namespace Dependencies;

internal class Counter : ICounter
{
    private int _counter = 0;

    public void Increment()
    {
        _counter++;
    }

    public void ShowValue()
    {
        Console.WriteLine(_counter);
    }
}
