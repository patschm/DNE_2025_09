namespace RssReader;

internal class Program
{
    static void Main(string[] args)
    {

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(@"https:\nu.nl\rss");

        var response = client.GetAsync("").Result;
        if (response.StatusCode == System.Net.HttpStatusCode.OK )
        {
            Stream str = response.Content.ReadAsStream();
        }
        Console.WriteLine("Hello, World!");
    }
}
