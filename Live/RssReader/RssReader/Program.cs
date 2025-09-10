
using System.Xml;
using System.Xml.Serialization;

namespace RssReader;

internal class Program
{
    static void Main(string[] args)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(@"https://nu.nl/rss/");

        var response = client.GetAsync("binnenland").Result;
        if (response.StatusCode == System.Net.HttpStatusCode.OK )
        {
            Stream str = response.Content.ReadAsStream();
            foreach (Item item in ProcessFeed(str))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"##### {item.Category}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(item.Title);
                
                Console.WriteLine(item.Description);

                Console.ResetColor();
            }
        }

        //foreach(int number in GetNumbers())
        //{
        //    Console.WriteLine(number);
        //}

    }
    //static IEnumerable<int> GetNumbers()
    //{
    //    Console.WriteLine("1");
    //    yield return 1;
    //    Console.WriteLine("2");
    //    yield return 2;
    //    Console.WriteLine("3");
    //    yield return 3;
    //}
     static IEnumerable<Item> ProcessFeed(Stream str)
    {
        XmlSerializer ser = new XmlSerializer(typeof(Item));
       var rdr = XmlReader.Create(str);
        //List<Item> items = new List<Item>();
        while (rdr.ReadToFollowing("item"))
        {
            var it = ser.Deserialize(rdr.ReadSubtree()) as Item;
            if (it != null)
                yield return it;
                //items.Add(it);
        }
        //return items;
    }
}
