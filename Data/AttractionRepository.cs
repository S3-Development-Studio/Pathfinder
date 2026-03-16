using System.Collections.Generic;
using Pathfinder.Models;

namespace Pathfinder.Data;

public interface IAttractionRepository
{
    List<Attraction> GetAllAttractions();
}

public class AttractionRepository : IAttractionRepository
{
    public List<Attraction> GetAllAttractions()
    {
        return new List<Attraction>
        {
            // --- WARSZAWA ---
            new Attraction { Id = 1, Name = "Zamek Królewski w Warszawie", City = "Warszawa", Latitude = 52.2482, Longitude = 21.0144, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 2, RecommendedDurationMinutes = 120 },
            new Attraction { Id = 2, Name = "Łazienki Królewskie", City = "Warszawa", Latitude = 52.2150, Longitude = 21.0354, IsOutdoor = true, ExplorationScore = 6, RelaxationScore = 10, RecommendedDurationMinutes = 150 },
            new Attraction { Id = 3, Name = "Muzeum Powstania Warszawskiego", City = "Warszawa", Latitude = 52.2323, Longitude = 20.9808, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 1, RecommendedDurationMinutes = 180 },
            new Attraction { Id = 4, Name = "Centrum Nauki Kopernik", City = "Warszawa", Latitude = 52.2418, Longitude = 21.0286, IsOutdoor = false, ExplorationScore = 8, RelaxationScore = 6, RecommendedDurationMinutes = 200 },
            new Attraction { Id = 5, Name = "Pałac Kultury i Nauki", City = "Warszawa", Latitude = 52.2318, Longitude = 21.0060, IsOutdoor = false, ExplorationScore = 9, RelaxationScore = 4, RecommendedDurationMinutes = 90 },
            new Attraction { Id = 6, Name = "Ogród Botaniczny UW", City = "Warszawa", Latitude = 52.2173, Longitude = 21.0253, IsOutdoor = true, ExplorationScore = 4, RelaxationScore = 10, RecommendedDurationMinutes = 60 },
            new Attraction { Id = 7, Name = "Muzeum Narodowe w Warszawie", City = "Warszawa", Latitude = 52.2320, Longitude = 21.0249, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 3, RecommendedDurationMinutes = 150 },
            new Attraction { Id = 8, Name = "Bulwary Wiślane", City = "Warszawa", Latitude = 52.2435, Longitude = 21.0298, IsOutdoor = true, ExplorationScore = 3, RelaxationScore = 10, RecommendedDurationMinutes = 120 },
            new Attraction { Id = 9, Name = "Hala Koszyki", City = "Warszawa", Latitude = 52.2227, Longitude = 21.0101, IsOutdoor = false, ExplorationScore = 2, RelaxationScore = 9, RecommendedDurationMinutes = 60 },
            new Attraction { Id = 10, Name = "Stare Miasto - Rynek", City = "Warszawa", Latitude = 52.2497, Longitude = 21.0122, IsOutdoor = true, ExplorationScore = 9, RelaxationScore = 5, RecommendedDurationMinutes = 60 },
            new Attraction { Id = 11, Name = "Muzeum Historii Żydów Polskich POLIN", City = "Warszawa", Latitude = 52.2495, Longitude = 20.9930, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 2, RecommendedDurationMinutes = 180 },
            new Attraction { Id = 12, Name = "Park Skaryszewski", City = "Warszawa", Latitude = 52.2423, Longitude = 21.0543, IsOutdoor = true, ExplorationScore = 3, RelaxationScore = 10, RecommendedDurationMinutes = 90 },

            // --- KRAKÓW ---
            new Attraction { Id = 101, Name = "Zamek Królewski na Wawelu", City = "Kraków", Latitude = 50.0540, Longitude = 19.9354, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 3, RecommendedDurationMinutes = 180 },
            new Attraction { Id = 102, Name = "Rynek Główny w Krakowie", City = "Kraków", Latitude = 50.0614, Longitude = 19.9366, IsOutdoor = true, ExplorationScore = 9, RelaxationScore = 6, RecommendedDurationMinutes = 90 },
            new Attraction { Id = 103, Name = "Sukiennice", City = "Kraków", Latitude = 50.0616, Longitude = 19.9373, IsOutdoor = false, ExplorationScore = 8, RelaxationScore = 4, RecommendedDurationMinutes = 60 },
            new Attraction { Id = 104, Name = "Kazimierz - Dzielnica Żydowska", City = "Kraków", Latitude = 50.0515, Longitude = 19.9472, IsOutdoor = true, ExplorationScore = 10, RelaxationScore = 7, RecommendedDurationMinutes = 150 },
            new Attraction { Id = 105, Name = "Fabryka Emalia Oskara Schindlera", City = "Kraków", Latitude = 50.0475, Longitude = 19.9618, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 1, RecommendedDurationMinutes = 120 },
            new Attraction { Id = 106, Name = "Planty", City = "Kraków", Latitude = 50.0620, Longitude = 19.9348, IsOutdoor = true, ExplorationScore = 4, RelaxationScore = 10, RecommendedDurationMinutes = 90 },
            new Attraction { Id = 107, Name = "Muzeum Narodowe (Gmach Główny)", City = "Kraków", Latitude = 50.0605, Longitude = 19.9239, IsOutdoor = false, ExplorationScore = 9, RelaxationScore = 3, RecommendedDurationMinutes = 150 },
            new Attraction { Id = 108, Name = "Kopiec Kościuszki", City = "Kraków", Latitude = 50.0551, Longitude = 19.8935, IsOutdoor = true, ExplorationScore = 7, RelaxationScore = 6, RecommendedDurationMinutes = 120 },
            new Attraction { Id = 109, Name = "Bulwary Wiślane w Krakowie", City = "Kraków", Latitude = 50.0493, Longitude = 19.9348, IsOutdoor = true, ExplorationScore = 3, RelaxationScore = 10, RecommendedDurationMinutes = 90 },
            new Attraction { Id = 110, Name = "Muzeum Sztuki Współczesnej MOCAK", City = "Kraków", Latitude = 50.0470, Longitude = 19.9602, IsOutdoor = false, ExplorationScore = 8, RelaxationScore = 4, RecommendedDurationMinutes = 120 },
            new Attraction { Id = 111, Name = "Smocza Jama", City = "Kraków", Latitude = 50.0536, Longitude = 19.9328, IsOutdoor = false, ExplorationScore = 7, RelaxationScore = 4, RecommendedDurationMinutes = 45 },

            // --- GDAŃSK ---
            new Attraction { Id = 201, Name = "Długi Targ i Fontanna Neptuna", City = "Gdańsk", Latitude = 54.3486, Longitude = 18.6534, IsOutdoor = true, ExplorationScore = 9, RelaxationScore = 5, RecommendedDurationMinutes = 90 },
            new Attraction { Id = 202, Name = "Europejskie Centrum Solidarności", City = "Gdańsk", Latitude = 54.3608, Longitude = 18.6493, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 1, RecommendedDurationMinutes = 200 },
            new Attraction { Id = 203, Name = "Muzeum II Wojny Światowej", City = "Gdańsk", Latitude = 54.3562, Longitude = 18.6599, IsOutdoor = false, ExplorationScore = 10, RelaxationScore = 1, RecommendedDurationMinutes = 240 },
            new Attraction { Id = 204, Name = "Żuraw nad Motławą", City = "Gdańsk", Latitude = 54.3510, Longitude = 18.6582, IsOutdoor = true, ExplorationScore = 8, RelaxationScore = 5, RecommendedDurationMinutes = 45 },
            new Attraction { Id = 205, Name = "Ulica Mariacka", City = "Gdańsk", Latitude = 54.3496, Longitude = 18.6558, IsOutdoor = true, ExplorationScore = 9, RelaxationScore = 6, RecommendedDurationMinutes = 60 },
            new Attraction { Id = 206, Name = "Park Oliwski", City = "Gdańsk", Latitude = 54.4103, Longitude = 18.5606, IsOutdoor = true, ExplorationScore = 5, RelaxationScore = 10, RecommendedDurationMinutes = 120 },
            new Attraction { Id = 207, Name = "Archikatedra Oliwska", City = "Gdańsk", Latitude = 54.4110, Longitude = 18.5583, IsOutdoor = false, ExplorationScore = 8, RelaxationScore = 4, RecommendedDurationMinutes = 60 },
            new Attraction { Id = 208, Name = "Plaża w Brzeźnie", City = "Gdańsk", Latitude = 54.4095, Longitude = 18.6186, IsOutdoor = true, ExplorationScore = 2, RelaxationScore = 10, RecommendedDurationMinutes = 150 },
            new Attraction { Id = 209, Name = "Góra Gradowa (Hevelianum)", City = "Gdańsk", Latitude = 54.3556, Longitude = 18.6385, IsOutdoor = true, ExplorationScore = 7, RelaxationScore = 8, RecommendedDurationMinutes = 120 },
            new Attraction { Id = 210, Name = "Molo w Brzeźnie", City = "Gdańsk", Latitude = 54.4144, Longitude = 18.6148, IsOutdoor = true, ExplorationScore = 3, RelaxationScore = 10, RecommendedDurationMinutes = 60 }
        };
    }
}
