namespace Calculator;

public partial class CalculatorApp : Form
{
    private SynchronizationContext SyncContext;

    public CalculatorApp()
    {
        InitializeComponent();
        SyncContext = SynchronizationContext.Current!;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b))
        {
            //int result = LongAdd(a, b);
            //UpdateAnswer(result);

            Task.Run(() => LongAdd(a, b))
                .ContinueWith(pt => {
                     SyncContext.Send(UpdateAnswer, pt.Result);
                    });
        }
    }

  
    private void UpdateAnswer(object? result)
    {
        lblAnswer.Text = result?.ToString();
    }

    private int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
   }