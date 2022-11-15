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
            float avgsize = 0;
            float i = 1;
            float maxsize = 0;

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

                avgsize = building.size + avgsize;

                i = i + 1;

                if(building.size > maxsize)
                {
                    maxsize = building.size;
                }

                Console.WriteLine($"{building.id} | {building.Adress} | Building Age: {calcYear2}");
            }

            Console.WriteLine($"Average size: {avgsize/i} |  Max size: {maxsize}");
        }
    }
}