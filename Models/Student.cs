using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebAPILab.Converters;
namespace WebAPILab.Models
{
    public partial class Student
    {
        [JsonConverter(typeof(IntToStringConverter))]
        public int StudentId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]
        public int Age { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]
        public int Nationality { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]

        public int Major { get; set; }

        public string MajorName { get; set; }
        public string NationalityName { get; set; }
        public virtual Major MajorNavigation { get; set; }
        public virtual Nationality NationalityNavigation { get; set; }
    }
}
