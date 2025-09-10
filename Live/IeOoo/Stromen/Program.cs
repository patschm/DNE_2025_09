using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;

namespace Stromen;

internal class Program
{
    static void Main(string[] args)
    {
        //SchrijvenNaarEenStream();
        //LezenVanEenStream();
        //ModernSchrijven();
        //ModernLezen();
        //ZippenNaarStream();
        // LezenVanZip();
        WriteXmlData();
        Console.WriteLine("Done!");
        Console.ReadLine();
    }

    private static void WriteXmlData()
    {
        Person p = new Person { Id = 1, Firstname = "Hans", Lastname = "Peters", Age = 32, Birthdate = DateTime.Now.AddYears(-32) };
        Directory.CreateDirectory(@"D:\Temp");
        FileInfo file = new(@"D:\Temp\data.xml");

        if (file.Exists)
        {
            file.Delete();
        }
        using (FileStream fs = file.Create())
        using (XmlWriter writer = XmlWriter.Create(fs))
        {

            writer.WriteStartElement("root");
            writer.WriteStartElement("person");
            writer.WriteAttributeString("id", p.Id.ToString());
            writer.WriteStartElement("first-name");
            writer.WriteString(p.Firstname);
            writer.WriteEndElement();
            writer.WriteStartElement("last-name");
            writer.WriteString(p.Lastname);
            writer.WriteEndElement();
            writer.WriteStartElement("birthdate");
            writer.WriteString(p.Birthdate.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("age");
            writer.WriteString(p.Age.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }

    private static void LezenVanZip()
    {
        FileInfo file = new(@"D:\Temp\data.zip");
        using FileStream fs = file.OpenRead();
        using GZipStream zipper = new(fs, CompressionMode.Decompress);
        using StreamReader sr = new(zipper);
        string? line = null;
        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }

    private static void ZippenNaarStream()
    {
        Directory.CreateDirectory(@"D:\Temp");
        FileInfo file = new(@"D:\Temp\data.zip");

        if (file.Exists)
        {
            file.Delete();
        }
        using (FileStream fs = file.Create())
        using (GZipStream zipper = new GZipStream(fs, CompressionMode.Compress))
        using (StreamWriter sw = new StreamWriter(zipper))
        {
            for (int i = 0; i < 1000; i++)
            {
                sw.WriteLine($"Hello World {i}");
            }
        }
    }

    private static void ModernLezen()
    {
        FileInfo file = new(@"D:\Temp\data.txt");
        using (FileStream fs = file.OpenRead())
        using (StreamReader sr = new StreamReader(fs))
        {
            string? line = null;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

    }

    private static void ModernSchrijven()
    {
        Directory.CreateDirectory(@"D:\Temp");
        FileInfo file = new(@"D:\Temp\data.txt");

        if (file.Exists)
        {
            file.Delete();
        }
        using (FileStream fs = file.Create())
        using (StreamWriter sw = new StreamWriter(fs))
        {
            for (int i = 0; i < 1000; i++)
            {
                sw.WriteLine($"Hello World {i}");
            }
        }
        //sw.Dispose();
        // sw.Flush();
        // sw.Close();
        // fs.Close();
    }

    private static void LezenVanEenStream()
    {
        Stopwatch watch = new Stopwatch();
        FileInfo file = new(@"D:\Temp\data.txt");
        FileStream fs = file.OpenRead();

        Console.WriteLine($"File Size {file.Length}");
        byte[] buffer = new byte[6];
        int nrRead = 0;
        watch.Start();
        do
        {
            Array.Clear(buffer, 0, buffer.Length);
            nrRead = fs.Read(buffer);
            string text = Encoding.UTF8.GetString(buffer);
            Console.Write(text);
        }
        while (nrRead == buffer.Length);
        watch.Stop();
        Console.WriteLine(watch.Elapsed);

    }

    private static void SchrijvenNaarEenStream()
    {
        Directory.CreateDirectory(@"D:\Temp");
        FileInfo file = new(@"D:\Temp\data.txt");

        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();
        for (int i = 0; i < 1000; i++)
        {
            byte[] buffer = Encoding.UTF8.GetBytes($"Hello World {i}\r\n");
            fs.Write(buffer, 0, buffer.Length);
        }
        fs.Close();
    }
}
