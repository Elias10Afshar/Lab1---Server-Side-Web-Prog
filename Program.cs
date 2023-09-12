using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Lab1;

public class Program
{
    static void Main(string[] args)
    {
        string csvFilePath = @"C:\Users\elias\Server Side Web Prog\Lab1\videogames.csv";

        List<VideoGame> videoGames = ReadVideoGamesFromCsv(csvFilePath);

        if (videoGames.Count > 0)
        {
            PublisherData(videoGames);
            GenreData(videoGames);
        }
        else
        {
            Console.WriteLine("No video game data found.");
        }
    }
    // Read video game data from a CSV file and return a List of VideoGame objects
    static List<VideoGame> ReadVideoGamesFromCsv(string filePath)
    {
        List<VideoGame> videoGames = new List<VideoGame>();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                // Skip the header row
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var videoGame = new VideoGame
                    {
                        Name = values[0],
                        Platform = values[1],
                        Year = int.Parse(values[2]),
                        Genre = values[3],
                        Publisher = values[4],
                        NA_Sales = double.Parse(values[5], CultureInfo.InvariantCulture),
                        EU_Sales = double.Parse(values[6], CultureInfo.InvariantCulture),
                        JP_Sales = double.Parse(values[7], CultureInfo.InvariantCulture),
                        Other_Sales = double.Parse(values[8], CultureInfo.InvariantCulture),
                        Global_Sales = double.Parse(values[9], CultureInfo.InvariantCulture)
                    };

                    videoGames.Add(videoGame);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return videoGames;
    }

    static void PublisherData(List<VideoGame> videoGames)
    {
        Console.Write("Enter the name of the publisher: ");
        string selectedPublisher = Console.ReadLine();

        var publisherGames = videoGames.Where(game => game.Publisher == selectedPublisher).ToList();

        if (publisherGames.Any())
        {
            publisherGames = publisherGames.OrderBy(game => game.Name).ToList(); // Sort using LINQ
            double percentage = (double)publisherGames.Count / videoGames.Count * 100;

            Console.WriteLine($"Games published by {selectedPublisher}:");
            foreach (var game in publisherGames)
            {
                Console.WriteLine(game);
            }

            Console.WriteLine($"\nOut of {videoGames.Count} games, {publisherGames.Count} are published by {selectedPublisher}, which is {percentage:F2}%.");
        }
        else
        {
            Console.WriteLine($"No games found for the publisher: {selectedPublisher}");
        }
    }
                    

    static void GenreData(List<VideoGame> videoGames)
    {
        Console.Write("Enter the name of the genre: ");
        string selectedGenre = Console.ReadLine();

        var genreGames = videoGames.Where(game => game.Genre == selectedGenre).ToList();

        if (genreGames.Any())
        {
            genreGames = genreGames.OrderBy(game => game.Name).ToList(); // Sort using LINQ
            double percentage = (double)genreGames.Count / videoGames.Count * 100;

            Console.WriteLine($"\nGames of the genre '{selectedGenre}':");
            foreach (var game in genreGames)
            {
                Console.WriteLine(game);
            }

            Console.WriteLine($"\nOut of {videoGames.Count} games, {genreGames.Count} are of the genre '{selectedGenre}', which is {percentage:F2}%.");
        }
        else
        {
            Console.WriteLine($"No games found for the genre: {selectedGenre}");
        }

    }
}
            
        
            
        
        



