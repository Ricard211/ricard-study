using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricard_study
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReadCsvFile();
            Console.Read();
        }

        private static void ReadCsvFile()
        {
            var csvFileDescription = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar = ';',
                UseFieldIndexForReadingData = false
            };

            var csvContext = new CsvContext();
            var bgs = csvContext.Read<Buildings>("apartment_buildings_2019.csv", csvFileDescription);

            foreach(var building in bgs)
            {
                int calcYear1 = building.year;
                int calcYear2;
                int ye = 2022;

                if(calcYear1 != 0)
                {
                    calcYear2 = ye - calcYear1;
                }
                else calcYear2 = 0;
                Console.WriteLine($"{building.id} | {building.Adress} | {building.year} | Building Age: {calcYear2}");
            }
        }
    }
}