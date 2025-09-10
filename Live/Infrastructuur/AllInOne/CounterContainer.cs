
namespace Dependencies;

internal class CounterContainer
{
    private readonly ICounter nt;// = new AntiCounter();

    public CounterContainer(ICounter counter)
    {
        nt = counter;
    }
    public void DoeJeDing()
    {
       
        nt.Increment();
        nt.Increment();
        nt.Increment();
        nt.Increment();
        nt.Increment();
        nt.Increment();
        nt.Increment();
        nt.Increment();

        nt.ShowValue();
    }
}
