using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using Intern.MM.Dto;
using ServiceStack;

namespace Intern.MM
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //Test Input
            CsvFilteredPlayers(@"D:\Programming\Software University\Intern.MM\Intern.MM\Dataset\NBA-Players.json", 20, 30, @"D:\Programming\Software University\Intern.MM\Intern.MM\OutputCsv\Test.csv");
        }

        public static void CsvFilteredPlayers(string path, int maximumYearsOfPlay, int minimumPlayerRating, string pathToCsvFileToGenerate)
        {
            var filteredPlayers = JsonConvert.DeserializeObject<PlayerDto[]>(File.ReadAllText(path))
                .Where(p => p.Rating >= minimumPlayerRating && (DateTime.Now.Year - int.Parse(p.PlayingSince)) <= maximumYearsOfPlay).Select(fp => new
                {
                    fp.Name,
                    fp.Rating
                })
                .OrderByDescending(fp => fp.Rating)
                .ToCsv()
                .TrimEnd();

            File.WriteAllText(pathToCsvFileToGenerate, filteredPlayers);
        }
    }
}