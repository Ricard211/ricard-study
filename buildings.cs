using LINQtoCSV;
using System;

namespace ricard_study
{
    [Serializable]
    public class Buildings
    {
        [CsvColumn(Name = "id", FieldIndex = 1)]
        public string id { get; set; }

        [CsvColumn(Name = "adresas", FieldIndex = 2)]
        public string Adress { get; set; }

        [CsvColumn(Name = "bendr_plotas", FieldIndex = 9)]
        public float size { get; set; }

        [CsvColumn(Name = "build_year", FieldIndex = 11)]
        public int year { get; set; }
    }
}