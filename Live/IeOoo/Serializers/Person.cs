using System.Xml.Serialization;

namespace Serializers;

[XmlRoot("person")]
public class Person
{
    [XmlAttribute("id")]
    public int Id { get; set; }
    [XmlElement("first-name")]
    public string? Firstname { get; set; }
    [XmlElement("last-name")]
    public string? Lastname { get; set; }
    [XmlElement("age")]
    public int Age { get; set; }

    [XmlIgnore]
    public DateTime Birthdate {
        get;
        set; 
    }

    [XmlElement("birthdate")]
    public string BirthDate2 
    { 
        get
        {
            return Birthdate.ToString("yyyy-MM-dd");
        }
        set
        {
            Birthdate = DateTime.Parse(value);
        }
    }
}
