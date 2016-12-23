using System.Xml.Serialization;

namespace Mvc5Application1.Data.Model
{
    public class SearchEmployeeCriteria
    {
        public string WorkDate { get; set; }
        public string TeamName { get; set; }

        [XmlElement(IsNullable = true)]
        public string PayrollNumber { get; set; }

        [XmlElement(IsNullable = true)]
        public string Surname { get; set; }

        [XmlElement(IsNullable = true)]
        public string FirstName { get; set; }
    }
}