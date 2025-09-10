namespace Dependencies;

internal class AntiCounter : ICounter
{
    private int _counter = 0;

    public void Increment()
    {
        _counter--;
    }

    public void ShowValue()
    {
        Console.WriteLine(_counter);
    }
}
