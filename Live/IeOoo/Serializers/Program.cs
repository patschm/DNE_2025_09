using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;

namespace Serializers;

internal class Program
{
    static void Main(string[] args)
    {
        Person p = new Person { Id = 1, Firstname = "Hans", Lastname = "Peters", Age = 32, Birthdate = DateTime.Now.AddYears(-32) };
        Person p2 = new Person { Id = 2, Firstname = "Hans2", Lastname = "Peters2", Age = 322, Birthdate = DateTime.Now.AddYears(-32) };

        Person[] people = [p, p2];
        Directory.CreateDirectory(@"D:\Temp");
        FileInfo file = new(@"D:\Temp\people.xml");

        if (file.Exists)
        {
            file.Delete();
        }

        FileStream fs = file.Create();
        XmlWriter writer = XmlWriter.Create(fs);
        writer.WriteStartElement("root");
        XmlSerializer serializer = new XmlSerializer(typeof(Person[]));
        serializer.Serialize(writer, people);

        writer.WriteEndElement();
        writer.Dispose();
    }
}
