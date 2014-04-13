using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmas.BotBrackets.Genetic
{
    public static class HistoricalOutcomes
    {
        public class Game
        {
            public int Winner { get; set; }
            public int Loser { get; set; }
            public int Round { get; set; }
            public int PointDifference { get; set; }
        }

        public static Dictionary<int, string> GetTeams(int season)
        {
            switch (season)
            {
                case 2010: return Teams2010;
                case 2011: return Teams2011;
                case 2012: return Teams2012;
                case 2013: return Teams2013;
                case 2014: return Teams2014;
                default: return new Dictionary<int,string>();
            }
        }

        public static IEnumerable<Game> GetGames(int season)
        {
            switch (season)
            {
                case 2010: return Get2010Games();
                case 2011: return Get2011Games();
                case 2012: return Get2012Games();
                case 2013: return Get2013Games();
                case 2014: return Get2014Games();
                default: return Enumerable.Empty<Game>();
            }
        }

        public static int[] GetLastRoundWon(int season)
        {
            return GetGames(season)
                .Concat(GetTeams(season).Select(t => new Game { Winner = t.Key, Round = 0 }))
                .GroupBy(g => g.Winner)
                .Select(gs => new { Team = gs.Key, LastRound = gs.Max(g => g.Round) })
                .OrderBy(x => x.Team)
                .Select(x => x.LastRound)
                .ToArray();
        }

        // Copy/paste/reformat from http://www.botbrackets.com/help-positions
        private static Dictionary<int, string> Teams2014 = new Dictionary<int,string>()
        {
            {1, "Florida"},           {17, "Virginia"},          {33, "Arizona"},          {49, "Wichita St."},
            {2, "Albany (NY)"},       {18, "Coastal Caro."},     {34, "Weber St."},        {50, "Cal Poly"},
            {3, "Colorado"},          {19, "Memphis"},           {35, "Gonzaga"},          {51, "Kentucky"},
            {4, "Pittsburgh"},        {20, "George Washington"}, {36, "Oklahoma St."},     {52, "Kansas St."},
            {5, "VCU"},               {21, "Cincinnati"},        {37, "Oklahoma"},         {53, "Saint Louis"},
            {6, "Stephen F. Austin"}, {22, "Harvard"},           {38, "North Dakota St."}, {54, "North Carolina St."},
            {7, "UCLA"},              {23, "Michigan St."},      {39, "San Diego St."},    {55, "Louisville"},
            {8, "Tulsa"},             {24, "Delaware"},          {40, "New Mexico St."},   {56, "Manhattan"},
            {9, "Ohio St."},          {25, "North Carolina"},    {41, "Baylor"},           {57, "Massachusetts"},
            {10, "Dayton"},           {26, "Providence"},        {42, "Nebraska"},         {58, "Tennessee"},
            {11, "Syracuse"},         {27, "Iowa St."},          {43, "Creighton"},        {59, "Duke"},
            {12, "Western Mich."},    {28, "N.C. Central"},      {44, "La.-Lafayette"},    {60, "Mercer"},
            {13, "New Mexico"},       {29, "UConn"},             {45, "Oregon"},           {61, "Texas"},
            {14, "Stanford"},         {30, "Saint Joseph's"},    {46, "BYU"},              {62, "Arizona St."},
            {15, "Kansas"},           {31, "Villanova"},         {47, "Wisconsin"},        {63, "Michigan"},
            {16, "Eastern Ky."},      {32, "Milwaukee"},         {48, "American"},         {64, "Wofford"}
        };

        // http://en.wikipedia.org/wiki/2014_NCAA_Men's_Division_I_Basketball_Tournament
        private static IEnumerable<Game> Get2014Games()
        {
            yield return new Game { Winner = 1, Loser = 2, Round = 1, PointDifference = 67 - 55 };
            yield return new Game { Winner = 4, Loser = 3, Round = 1, PointDifference = 77 - 48 };
            yield return new Game { Winner = 6, Loser = 5, Round = 1, PointDifference = 0 }; // OT, 77-75
            yield return new Game { Winner = 7, Loser = 8, Round = 1, PointDifference = 76 - 59 };
            yield return new Game { Winner = 10, Loser = 9, Round = 1, PointDifference = 60 - 59 };
            yield return new Game { Winner = 11, Loser = 12, Round = 1, PointDifference = 77 - 53 };
            yield return new Game { Winner = 14, Loser = 13, Round = 1, PointDifference = 58 - 53 };
            yield return new Game { Winner = 15, Loser = 16, Round = 1, PointDifference = 80 - 69 };
            yield return new Game { Winner = 1, Loser = 4, Round = 2, PointDifference = 61 - 45 };
            yield return new Game { Winner = 7, Loser = 6, Round = 2, PointDifference = 77 - 60 };
            yield return new Game { Winner = 10, Loser = 11, Round = 2, PointDifference = 55 - 53 };
            yield return new Game { Winner = 14, Loser = 15, Round = 2, PointDifference = 60 - 57 };
            yield return new Game { Winner = 1, Loser = 7, Round = 3, PointDifference = 79 - 68 };
            yield return new Game { Winner = 10, Loser = 14, Round = 3, PointDifference = 82 - 72 };
            yield return new Game { Winner = 1, Loser = 10, Round = 4, PointDifference = 62 - 52 };

            yield return new Game { Winner = 17, Loser = 18, Round = 1, PointDifference = 70 - 59 };
            yield return new Game { Winner = 19, Loser = 20, Round = 1, PointDifference = 71 - 68 };
            yield return new Game { Winner = 22, Loser = 21, Round = 1, PointDifference = 61 - 57 };
            yield return new Game { Winner = 23, Loser = 24, Round = 1, PointDifference = 93 - 78 };
            yield return new Game { Winner = 25, Loser = 26, Round = 1, PointDifference = 79 - 77 };
            yield return new Game { Winner = 27, Loser = 28, Round = 1, PointDifference = 93 - 75 };
            yield return new Game { Winner = 29, Loser = 30, Round = 1, PointDifference = 0 }; // OT, 89-81
            yield return new Game { Winner = 31, Loser = 32, Round = 1, PointDifference = 73 - 53 };
            yield return new Game { Winner = 17, Loser = 19, Round = 2, PointDifference = 78 - 60 };
            yield return new Game { Winner = 23, Loser = 22, Round = 2, PointDifference = 80 - 73 };
            yield return new Game { Winner = 27, Loser = 25, Round = 2, PointDifference = 85 - 83 };
            yield return new Game { Winner = 29, Loser = 31, Round = 2, PointDifference = 77 - 65 };
            yield return new Game { Winner = 23, Loser = 17, Round = 3, PointDifference = 61 - 59 };
            yield return new Game { Winner = 29, Loser = 27, Round = 3, PointDifference = 81 - 76 };
            yield return new Game { Winner = 29, Loser = 23, Round = 4, PointDifference = 60 - 54 };

            yield return new Game { Winner = 33, Loser = 34, Round = 1, PointDifference = 68 - 59 };
            yield return new Game { Winner = 35, Loser = 36, Round = 1, PointDifference = 85 - 77 };
            yield return new Game { Winner = 38, Loser = 37, Round = 1, PointDifference = 0 }; // OT, 80-75
            yield return new Game { Winner = 39, Loser = 40, Round = 1, PointDifference = 0 }; // OT, 73-69
            yield return new Game { Winner = 41, Loser = 42, Round = 1, PointDifference = 74 - 60 };
            yield return new Game { Winner = 43, Loser = 44, Round = 1, PointDifference = 76 - 66 };
            yield return new Game { Winner = 45, Loser = 46, Round = 1, PointDifference = 87 - 68 };
            yield return new Game { Winner = 47, Loser = 48, Round = 1, PointDifference = 75 - 35 };
            yield return new Game { Winner = 33, Loser = 35, Round = 2, PointDifference = 84 - 61 };
            yield return new Game { Winner = 39, Loser = 38, Round = 2, PointDifference = 63 - 44 };
            yield return new Game { Winner = 41, Loser = 43, Round = 2, PointDifference = 85 - 55 };
            yield return new Game { Winner = 47, Loser = 45, Round = 2, PointDifference = 85 - 77 };
            yield return new Game { Winner = 33, Loser = 39, Round = 3, PointDifference = 70 - 64 };
            yield return new Game { Winner = 47, Loser = 41, Round = 3, PointDifference = 69 - 52 };
            yield return new Game { Winner = 47, Loser = 33, Round = 4, PointDifference = 0 }; // OT, 64-63

            yield return new Game { Winner = 49, Loser = 50, Round = 1, PointDifference = 64 - 37 };
            yield return new Game { Winner = 51, Loser = 52, Round = 1, PointDifference = 56 - 49 };
            yield return new Game { Winner = 53, Loser = 54, Round = 1, PointDifference = 0 }; // OT, 83-80
            yield return new Game { Winner = 55, Loser = 56, Round = 1, PointDifference = 71 - 64 };
            yield return new Game { Winner = 58, Loser = 57, Round = 1, PointDifference = 86 - 67 };
            yield return new Game { Winner = 60, Loser = 59, Round = 1, PointDifference = 78 - 71 };
            yield return new Game { Winner = 61, Loser = 62, Round = 1, PointDifference = 87 - 85 };
            yield return new Game { Winner = 63, Loser = 64, Round = 1, PointDifference = 57 - 40 };
            yield return new Game { Winner = 51, Loser = 49, Round = 2, PointDifference = 78 - 76 };
            yield return new Game { Winner = 55, Loser = 53, Round = 2, PointDifference = 66 - 51 };
            yield return new Game { Winner = 58, Loser = 60, Round = 2, PointDifference = 83 - 63 };
            yield return new Game { Winner = 63, Loser = 61, Round = 2, PointDifference = 79 - 65 };
            yield return new Game { Winner = 51, Loser = 55, Round = 3, PointDifference = 74 - 69 };
            yield return new Game { Winner = 63, Loser = 58, Round = 3, PointDifference = 73 - 71 };
            yield return new Game { Winner = 51, Loser = 63, Round = 4, PointDifference = 75 - 72 };

            yield return new Game { Winner = 29, Loser = 1, Round = 5, PointDifference = 63 - 53 };
            yield return new Game { Winner = 51, Loser = 47, Round = 5, PointDifference = 74 - 73 };
            yield return new Game { Winner = 29, Loser = 51, Round = 6, PointDifference = 60 - 54 };
        }

        // Copy/paste/reformat from http://www.botbrackets.com/help-positions
        private static Dictionary<int, string> Teams2013 = new Dictionary<int,string>()
        {
            {1, "Louisville"},      {17, "Gonzaga"},        {33, "Kansas"},             {49, "Indiana"},
            {2, "N.C. A&T"},        {18, "Southern U."},    {34, "Western Ky."},        {50, "James Madison"},
            {3, "Colorado St."},    {19, "Pittsburgh"},     {35, "North Carolina"},     {51, "North Carolina St."},
            {4, "Missouri"},        {20, "Wichita St."},    {36, "Villanova"},          {52, "Temple"},
            {5, "Oklahoma St."},    {21, "Wisconsin"},      {37, "VCU"},                {53, "UNLV"},
            {6, "Oregon"},          {22, "Ole Miss"},       {38, "Akron"},              {54, "California"},
            {7, "Saint Louis"},     {23, "Kansas St."},     {39, "Michigan"},           {55, "Syracuse"},
            {8, "New Mexico St."},  {24, "La Salle"},       {40, "South Dakota St."},   {56, "Montana"},
            {9, "Memphis"},         {25, "Arizona"},        {41, "UCLA"},               {57, "Butler"},
            {10, "St. Mary's (CA)"},{26, "Belmont"},        {42, "Minnesota"},          {58, "Bucknell"},
            {11, "Michigan St."},   {27, "New Mexico"},     {43, "Florida"},            {59, "Marquette"},
            {12, "Valparaiso"},     {28, "Harvard"},        {44, "Northwestern St."},   {60, "Davidson"},
            {13, "Creighton"},      {29, "Notre Dame"},     {45, "San Diego St."},      {61, "Illinois"},
            {14, "Cincinnati"},     {30, "Iowa St."},       {46, "Oklahoma"},           {62, "Colorado"},
            {15, "Duke"},           {31, "Ohio St."},       {47, "Georgetown"},         {63, "Miami (FL)"},
            {16, "Albany (NY)"},    {32, "Iona"},           {48, "FGCU"},               {64, "Pacific"}
        };

        // http://en.wikipedia.org/wiki/2013_NCAA_Men's_Division_I_Basketball_Tournament
        private static IEnumerable<Game> Get2013Games()
        {
            yield return new Game { Winner = 1, Loser = 2, Round = 1, PointDifference = 79 - 48 };
            yield return new Game { Winner = 3, Loser = 4, Round = 1, PointDifference = 84 - 72 };
            yield return new Game { Winner = 6, Loser = 5, Round = 1, PointDifference = 68 - 55 };
            yield return new Game { Winner = 7, Loser = 8, Round = 1, PointDifference = 64 - 44 };
            yield return new Game { Winner = 9, Loser = 10, Round = 1, PointDifference = 54 - 52 };
            yield return new Game { Winner = 11, Loser = 12, Round = 1, PointDifference = 65 - 54 };
            yield return new Game { Winner = 13, Loser = 14, Round = 1, PointDifference = 67 - 63 };
            yield return new Game { Winner = 15, Loser = 16, Round = 1, PointDifference = 73 - 61 };
            yield return new Game { Winner = 1, Loser = 3, Round = 2, PointDifference = 82 - 56 };
            yield return new Game { Winner = 6, Loser = 7, Round = 2, PointDifference = 74 - 57 };
            yield return new Game { Winner = 11, Loser = 9, Round = 2, PointDifference = 70 - 48 };
            yield return new Game { Winner = 15, Loser = 13, Round = 2, PointDifference = 66 - 50 };
            yield return new Game { Winner = 1, Loser = 6, Round = 3, PointDifference = 77 - 69 };
            yield return new Game { Winner = 15, Loser = 11, Round = 3, PointDifference = 71 - 61 };
            yield return new Game { Winner = 1, Loser = 15, Round = 4, PointDifference = 85 - 63 };

            yield return new Game { Winner = 33, Loser = 34, Round = 1, PointDifference = 64 - 57 };
            yield return new Game { Winner = 35, Loser = 36, Round = 1, PointDifference = 78 - 71 };
            yield return new Game { Winner = 37, Loser = 38, Round = 1, PointDifference = 88 - 42 };
            yield return new Game { Winner = 39, Loser = 40, Round = 1, PointDifference = 71 - 56 };
            yield return new Game { Winner = 42, Loser = 41, Round = 1, PointDifference = 83 - 63 };
            yield return new Game { Winner = 43, Loser = 44, Round = 1, PointDifference = 79 - 47 };
            yield return new Game { Winner = 45, Loser = 46, Round = 1, PointDifference = 70 - 55 };
            yield return new Game { Winner = 48, Loser = 47, Round = 1, PointDifference = 78 - 68 };
            yield return new Game { Winner = 33, Loser = 35, Round = 2, PointDifference = 70 - 58 };
            yield return new Game { Winner = 39, Loser = 37, Round = 2, PointDifference = 78 - 53 };
            yield return new Game { Winner = 43, Loser = 42, Round = 2, PointDifference = 78 - 64 };
            yield return new Game { Winner = 48, Loser = 45, Round = 2, PointDifference = 81 - 71 };
            yield return new Game { Winner = 39, Loser = 33, Round = 3, PointDifference = 0 }; // OT, 87-85
            yield return new Game { Winner = 43, Loser = 48, Round = 3, PointDifference = 62 - 50 };
            yield return new Game { Winner = 39, Loser = 43, Round = 4, PointDifference = 79 - 59 };

            yield return new Game { Winner = 49, Loser = 50, Round = 1, PointDifference = 83 - 62 };
            yield return new Game { Winner = 52, Loser = 51, Round = 1, PointDifference = 76 - 72 };
            yield return new Game { Winner = 54, Loser = 53, Round = 1, PointDifference = 64 - 61 };
            yield return new Game { Winner = 55, Loser = 56, Round = 1, PointDifference = 81 - 34 };
            yield return new Game { Winner = 57, Loser = 58, Round = 1, PointDifference = 68 - 56 };
            yield return new Game { Winner = 59, Loser = 60, Round = 1, PointDifference = 59 - 58 };
            yield return new Game { Winner = 61, Loser = 62, Round = 1, PointDifference = 57 - 49 };
            yield return new Game { Winner = 63, Loser = 64, Round = 1, PointDifference = 78 - 49 };
            yield return new Game { Winner = 49, Loser = 52, Round = 2, PointDifference = 58 - 52 };
            yield return new Game { Winner = 55, Loser = 54, Round = 2, PointDifference = 66 - 60 };
            yield return new Game { Winner = 59, Loser = 57, Round = 2, PointDifference = 74 - 72 };
            yield return new Game { Winner = 63, Loser = 61, Round = 2, PointDifference = 63 - 59 };
            yield return new Game { Winner = 55, Loser = 49, Round = 3, PointDifference = 61 - 50 };
            yield return new Game { Winner = 59, Loser = 63, Round = 3, PointDifference = 71 - 61 };
            yield return new Game { Winner = 55, Loser = 59, Round = 4, PointDifference = 55 - 39 };

            yield return new Game { Winner = 17, Loser = 18, Round = 1, PointDifference = 64 - 58 };
            yield return new Game { Winner = 20, Loser = 19, Round = 1, PointDifference = 73 - 55 };
            yield return new Game { Winner = 22, Loser = 21, Round = 1, PointDifference = 57 - 46 };
            yield return new Game { Winner = 24, Loser = 23, Round = 1, PointDifference = 63 - 61 };
            yield return new Game { Winner = 25, Loser = 26, Round = 1, PointDifference = 81 - 64 };
            yield return new Game { Winner = 28, Loser = 27, Round = 1, PointDifference = 68 - 62 };
            yield return new Game { Winner = 30, Loser = 29, Round = 1, PointDifference = 76 - 58 };
            yield return new Game { Winner = 31, Loser = 32, Round = 1, PointDifference = 95 - 70 };
            yield return new Game { Winner = 20, Loser = 17, Round = 2, PointDifference = 76 - 70 };
            yield return new Game { Winner = 24, Loser = 22, Round = 2, PointDifference = 76 - 74 };
            yield return new Game { Winner = 25, Loser = 28, Round = 2, PointDifference = 74 - 51 };
            yield return new Game { Winner = 31, Loser = 30, Round = 2, PointDifference = 78 - 75 };
            yield return new Game { Winner = 20, Loser = 24, Round = 3, PointDifference = 72 - 58 };
            yield return new Game { Winner = 31, Loser = 25, Round = 3, PointDifference = 73 - 70 };
            yield return new Game { Winner = 20, Loser = 31, Round = 4, PointDifference = 70 - 66 };

            yield return new Game { Winner = 1, Loser = 20, Round = 5, PointDifference = 72 - 68 };
            yield return new Game { Winner = 39, Loser = 55, Round = 5, PointDifference = 61 - 56 };
            yield return new Game { Winner = 1, Loser = 39, Round = 6, PointDifference = 82 - 76 };
        }

        // Copy/paste/reformat from http://www.botbrackets.com/help-positions
        private static Dictionary<int, string> Teams2012 = new Dictionary<int, string>()
        {
            {1, "Kentucky"},         {17, "Michigan St."},      {33, "Syracuse"},         {49, "North Carolina"},
            {2, "Western Ky."},      {18, "LIU Brooklyn"},      {34, "UNC Asheville"},    {50, "Vermont"},
            {3, "Iowa St."},         {19, "Memphis"},           {35, "Kansas St."},       {51, "Creighton"},
            {4, "Connecticut"},      {20, "Saint Louis"},       {36, "Southern Miss."},   {52, "Alabama"},
            {5, "Wichita St."},      {21, "New Mexico"},        {37, "Vanderbilt"},       {53, "Temple"},
            {6, "VCU"},              {22, "Long Beach St."},    {38, "Harvard"},          {54, "South Fla."},
            {7, "Indiana"},          {23, "Louisville"},        {39, "Wisconsin"},        {55, "Michigan"},
            {8, "New Mexico St."},   {24, "Davidson"},          {40, "Montana"},          {56, "Ohio"},
            {9, "UNLV"},             {25, "Murray St."},        {41, "Cincinnati"},       {57, "San Diego St."},
            {10, "Colorado"},        {26, "Colorado St."},      {42, "Texas"},            {58, "North Carolina St."},
            {11, "Baylor"},          {27, "Marquette"},         {43, "Florida St."},      {59, "Georgetown"},
            {12, "South Dakota St."},{28, "BYU"},               {44, "St. Bonaventure"},  {60, "Belmont"},
            {13, "Notre Dame"},      {29, "Florida"},           {45, "Gonzaga"},          {61, "St. Mary's (CA)"},
            {14, "Xavier"},          {30, "Virginia"},          {46, "West Virginia"},    {62, "Purdue"},
            {15, "Duke"},            {31, "Missouri"},          {47, "Ohio St."},         {63, "Kansas"},
            {16, "Lehigh"},          {32, "Norfolk St."},       {48, "Loyola Maryland"},  {64, "Detroit"}
        };

        // http://en.wikipedia.org/wiki/2012_NCAA_Men's_Division_I_Basketball_Tournament
        private static IEnumerable<Game> Get2012Games()
        {
            yield return new Game { Winner = 1, Loser = 2, Round = 1, PointDifference = 81 - 66 };
            yield return new Game { Winner = 3, Loser = 4, Round = 1, PointDifference = 77 - 64 };
            yield return new Game { Winner = 6, Loser = 5, Round = 1, PointDifference = 62 - 59 };
            yield return new Game { Winner = 7, Loser = 8, Round = 1, PointDifference = 79 - 66 };
            yield return new Game { Winner = 10, Loser = 9, Round = 1, PointDifference = 68 - 64 };
            yield return new Game { Winner = 11, Loser = 12, Round = 1, PointDifference = 68 - 60 };
            yield return new Game { Winner = 14, Loser = 13, Round = 1, PointDifference = 67 - 63 };
            yield return new Game { Winner = 16, Loser = 15, Round = 1, PointDifference = 75 - 70 };
            yield return new Game { Winner = 1, Loser = 3, Round = 2, PointDifference = 87 - 71 };
            yield return new Game { Winner = 7, Loser = 6, Round = 2, PointDifference = 63 - 61 };
            yield return new Game { Winner = 11, Loser = 10, Round = 2, PointDifference = 80 - 63 };
            yield return new Game { Winner = 14, Loser = 16, Round = 2, PointDifference = 70 - 58 };
            yield return new Game { Winner = 1, Loser = 7, Round = 3, PointDifference = 102 - 90 };
            yield return new Game { Winner = 11, Loser = 14, Round = 3, PointDifference = 75 - 70 };
            yield return new Game { Winner = 1, Loser = 11, Round = 4, PointDifference = 82 - 70 };

            yield return new Game { Winner = 17, Loser = 18, Round = 1, PointDifference = 89 - 67 };
            yield return new Game { Winner = 20, Loser = 19, Round = 1, PointDifference = 61 - 54 };
            yield return new Game { Winner = 21, Loser = 22, Round = 1, PointDifference = 75 - 68 };
            yield return new Game { Winner = 23, Loser = 24, Round = 1, PointDifference = 69 - 62 };
            yield return new Game { Winner = 25, Loser = 26, Round = 1, PointDifference = 58 - 41 };
            yield return new Game { Winner = 27, Loser = 28, Round = 1, PointDifference = 88 - 68 };
            yield return new Game { Winner = 29, Loser = 30, Round = 1, PointDifference = 71 - 45 };
            yield return new Game { Winner = 32, Loser = 31, Round = 1, PointDifference = 86 - 84 };
            yield return new Game { Winner = 17, Loser = 20, Round = 2, PointDifference = 65 - 61 };
            yield return new Game { Winner = 23, Loser = 21, Round = 2, PointDifference = 59 - 56 };
            yield return new Game { Winner = 27, Loser = 25, Round = 2, PointDifference = 62 - 53 };
            yield return new Game { Winner = 29, Loser = 32, Round = 2, PointDifference = 84 - 50 };
            yield return new Game { Winner = 23, Loser = 17, Round = 3, PointDifference = 57 - 44 };
            yield return new Game { Winner = 29, Loser = 27, Round = 3, PointDifference = 68 - 58 };
            yield return new Game { Winner = 23, Loser = 29, Round = 4, PointDifference = 72 - 68 };

            yield return new Game { Winner = 33, Loser = 34, Round = 1, PointDifference = 72 - 65 };
            yield return new Game { Winner = 35, Loser = 36, Round = 1, PointDifference = 70 - 64 };
            yield return new Game { Winner = 37, Loser = 38, Round = 1, PointDifference = 79 - 70 };
            yield return new Game { Winner = 39, Loser = 40, Round = 1, PointDifference = 73 - 49 };
            yield return new Game { Winner = 41, Loser = 42, Round = 1, PointDifference = 65 - 59 };
            yield return new Game { Winner = 43, Loser = 44, Round = 1, PointDifference = 66 - 63 };
            yield return new Game { Winner = 45, Loser = 46, Round = 1, PointDifference = 77 - 54 };
            yield return new Game { Winner = 47, Loser = 48, Round = 1, PointDifference = 78 - 59 };
            yield return new Game { Winner = 33, Loser = 35, Round = 2, PointDifference = 75 - 59 };
            yield return new Game { Winner = 39, Loser = 37, Round = 2, PointDifference = 60 - 57 };
            yield return new Game { Winner = 41, Loser = 43, Round = 2, PointDifference = 62 - 56 };
            yield return new Game { Winner = 47, Loser = 45, Round = 2, PointDifference = 73 - 66 };
            yield return new Game { Winner = 33, Loser = 39, Round = 3, PointDifference = 64 - 63 };
            yield return new Game { Winner = 47, Loser = 41, Round = 3, PointDifference = 81 - 66 };
            yield return new Game { Winner = 47, Loser = 33, Round = 4, PointDifference = 77 - 70 };

            yield return new Game { Winner = 49, Loser = 50, Round = 1, PointDifference = 77 - 58 };
            yield return new Game { Winner = 51, Loser = 52, Round = 1, PointDifference = 58 - 57 };
            yield return new Game { Winner = 54, Loser = 53, Round = 1, PointDifference = 58 - 44 };
            yield return new Game { Winner = 56, Loser = 55, Round = 1, PointDifference = 65 - 60 };
            yield return new Game { Winner = 58, Loser = 57, Round = 1, PointDifference = 79 - 65 };
            yield return new Game { Winner = 59, Loser = 60, Round = 1, PointDifference = 74 - 59 };
            yield return new Game { Winner = 62, Loser = 61, Round = 1, PointDifference = 72 - 69 };
            yield return new Game { Winner = 63, Loser = 64, Round = 1, PointDifference = 65 - 50 };
            yield return new Game { Winner = 49, Loser = 51, Round = 2, PointDifference = 87 - 73 };
            yield return new Game { Winner = 56, Loser = 54, Round = 2, PointDifference = 62 - 56 };
            yield return new Game { Winner = 58, Loser = 59, Round = 2, PointDifference = 66 - 63 };
            yield return new Game { Winner = 63, Loser = 62, Round = 2, PointDifference = 63 - 60 };
            yield return new Game { Winner = 49, Loser = 56, Round = 3, PointDifference = 0 }; // OT: 73-65
            yield return new Game { Winner = 63, Loser = 58, Round = 3, PointDifference = 60 - 57 };
            yield return new Game { Winner = 63, Loser = 49, Round = 4, PointDifference = 80 - 67 };

            yield return new Game { Winner = 1, Loser = 23, Round = 5, PointDifference = 69 - 61 };
            yield return new Game { Winner = 63, Loser = 47, Round = 5, PointDifference = 64 - 62 };
            yield return new Game { Winner = 1, Loser = 63, Round = 6, PointDifference = 67 - 59 };
        }

        // Copy/paste/reformat from http://www.botbrackets.com/help-positions
        private static Dictionary<int, string> Teams2011 = new Dictionary<int, string>()
        {
            {1, "Ohio St."},        {17, "Duke"},           {33, "Kansas"},         {49, "Pittsburgh"},
            {2, "UTSA"},            {18, "Hampton"},        {34, "Boston U."},      {50, "UNC Asheville"},
            {3, "George Mason"},    {19, "Michigan"},       {35, "UNLV"},           {51, "Butler"},
            {4, "Villanova"},       {20, "Tennessee"},      {36, "Illinois"},       {52, "Old Dominion"},
            {5, "West Virginia"},   {21, "Arizona"},        {37, "Vanderbilt"},     {53, "Kansas St."},
            {6, "Clemson"},         {22, "Memphis"},        {38, "Richmond"},       {54, "Utah St."},
            {7, "Kentucky"},        {23, "Texas"},          {39, "Louisville"},     {55, "Wisconsin"},
            {8, "Princeton"},       {24, "Oakland"},        {40, "Morehead St."},   {56, "Belmont"},
            {9, "Xavier"},          {25, "Cincinnati"},     {41, "Georgetown"},     {57, "St. John's (NY)"},
            {10, "Marquette"},      {26, "Missouri"},       {42, "VCU"},            {58, "Gonzaga"},
            {11, "Syracuse"},       {27, "Connecticut"},    {43, "Purdue"},         {59, "BYU"},
            {12, "Indiana St."},    {28, "Bucknell"},       {44, "St. Peter's"},    {60, "Wofford"},
            {13, "Washington"},     {29, "Temple"},         {45, "Texas A&M"},      {61, "UCLA"},
            {14, "Georgia"},        {30, "Penn St."},       {46, "Florida St."},    {62, "Michigan St."},
            {15, "North Carolina"}, {31, "San Diego St."},  {47, "Notre Dame"},     {63, "Florida"},
            {16, "Long Island"},    {32, "Northern Colo."}, {48, "Akron"},          {64, "UC Santa Barbara"}
        };

        // http://en.wikipedia.org/wiki/2011_NCAA_Men's_Division_I_Basketball_Tournament
        private static IEnumerable<Game> Get2011Games()
        {
            yield return new Game { Winner = 1, Loser = 2, Round = 1, PointDifference = 75 - 46 };
            yield return new Game { Winner = 3, Loser = 4, Round = 1, PointDifference = 61 - 57 };
            yield return new Game { Winner = 5, Loser = 6, Round = 1, PointDifference = 84 - 76 };
            yield return new Game { Winner = 7, Loser = 8, Round = 1, PointDifference = 59 - 57 };
            yield return new Game { Winner = 10, Loser = 9, Round = 1, PointDifference = 66 - 55 };
            yield return new Game { Winner = 11, Loser = 12, Round = 1, PointDifference = 77 - 60 };
            yield return new Game { Winner = 13, Loser = 14, Round = 1, PointDifference = 68 - 65 };
            yield return new Game { Winner = 15, Loser = 16, Round = 1, PointDifference = 102 - 87 };
            yield return new Game { Winner = 1, Loser = 3, Round = 2, PointDifference = 98 - 66 };
            yield return new Game { Winner = 7, Loser = 5, Round = 2, PointDifference = 71 - 63 };
            yield return new Game { Winner = 10, Loser = 11, Round = 2, PointDifference = 66 - 62 };
            yield return new Game { Winner = 15, Loser = 13, Round = 2, PointDifference = 86 - 83 };
            yield return new Game { Winner = 7, Loser = 1, Round = 3, PointDifference = 62 - 60 };
            yield return new Game { Winner = 15, Loser = 10, Round = 3, PointDifference = 81 - 63 };
            yield return new Game { Winner = 7, Loser = 15, Round = 4, PointDifference = 76 - 69 };

            yield return new Game { Winner = 17, Loser = 18, Round = 1, PointDifference = 87 - 45 };
            yield return new Game { Winner = 19, Loser = 20, Round = 1, PointDifference = 75 - 45 };
            yield return new Game { Winner = 21, Loser = 22, Round = 1, PointDifference = 77 - 75 };
            yield return new Game { Winner = 23, Loser = 24, Round = 1, PointDifference = 85 - 81 };
            yield return new Game { Winner = 25, Loser = 26, Round = 1, PointDifference = 78 - 63 };
            yield return new Game { Winner = 27, Loser = 28, Round = 1, PointDifference = 81 - 52 };
            yield return new Game { Winner = 29, Loser = 30, Round = 1, PointDifference = 66 - 64 };
            yield return new Game { Winner = 31, Loser = 32, Round = 1, PointDifference = 68 - 50 };
            yield return new Game { Winner = 17, Loser = 19, Round = 2, PointDifference = 73 - 71 };
            yield return new Game { Winner = 21, Loser = 23, Round = 2, PointDifference = 70 - 69 };
            yield return new Game { Winner = 27, Loser = 25, Round = 2, PointDifference = 69 - 58 };
            yield return new Game { Winner = 31, Loser = 29, Round = 2, PointDifference = 0 }; // 2OT, 71-64
            yield return new Game { Winner = 21, Loser = 17, Round = 3, PointDifference = 93 - 77 };
            yield return new Game { Winner = 27, Loser = 31, Round = 3, PointDifference = 74 - 67 };
            yield return new Game { Winner = 27, Loser = 21, Round = 4, PointDifference = 65 - 63 };

            yield return new Game { Winner = 33, Loser = 34, Round = 1, PointDifference = 72 - 53 };
            yield return new Game { Winner = 36, Loser = 35, Round = 1, PointDifference = 73 - 62 };
            yield return new Game { Winner = 38, Loser = 37, Round = 1, PointDifference = 69 - 66 };
            yield return new Game { Winner = 40, Loser = 39, Round = 1, PointDifference = 62 - 61 };
            yield return new Game { Winner = 42, Loser = 41, Round = 1, PointDifference = 74 - 56 };
            yield return new Game { Winner = 43, Loser = 44, Round = 1, PointDifference = 65 - 43 };
            yield return new Game { Winner = 46, Loser = 45, Round = 1, PointDifference = 57 - 50 };
            yield return new Game { Winner = 47, Loser = 48, Round = 1, PointDifference = 69 - 56 };
            yield return new Game { Winner = 33, Loser = 36, Round = 2, PointDifference = 73 - 59 };
            yield return new Game { Winner = 38, Loser = 40, Round = 2, PointDifference = 65 - 48 };
            yield return new Game { Winner = 42, Loser = 43, Round = 2, PointDifference = 94 - 76 };
            yield return new Game { Winner = 46, Loser = 47, Round = 2, PointDifference = 71 - 57 };
            yield return new Game { Winner = 33, Loser = 38, Round = 3, PointDifference = 77 - 57 };
            yield return new Game { Winner = 42, Loser = 46, Round = 3, PointDifference = 0 }; // OT, 72-71
            yield return new Game { Winner = 42, Loser = 33, Round = 4, PointDifference = 71 - 61 };

            yield return new Game { Winner = 49, Loser = 50, Round = 1, PointDifference = 74 - 51 };
            yield return new Game { Winner = 51, Loser = 52, Round = 1, PointDifference = 60 - 58 };
            yield return new Game { Winner = 53, Loser = 54, Round = 1, PointDifference = 73 - 68 };
            yield return new Game { Winner = 55, Loser = 56, Round = 1, PointDifference = 72 - 58 };
            yield return new Game { Winner = 58, Loser = 57, Round = 1, PointDifference = 86 - 71 };
            yield return new Game { Winner = 59, Loser = 60, Round = 1, PointDifference = 74 - 66 };
            yield return new Game { Winner = 61, Loser = 62, Round = 1, PointDifference = 78 - 76 };
            yield return new Game { Winner = 63, Loser = 64, Round = 1, PointDifference = 79 - 51 };
            yield return new Game { Winner = 51, Loser = 49, Round = 2, PointDifference = 71 - 70 };
            yield return new Game { Winner = 55, Loser = 53, Round = 2, PointDifference = 70 - 65 };
            yield return new Game { Winner = 59, Loser = 58, Round = 2, PointDifference = 89 - 67 };
            yield return new Game { Winner = 63, Loser = 61, Round = 2, PointDifference = 73 - 65 };
            yield return new Game { Winner = 51, Loser = 55, Round = 3, PointDifference = 61 - 54 };
            yield return new Game { Winner = 63, Loser = 59, Round = 3, PointDifference = 0 }; // OT, 83-74
            yield return new Game { Winner = 51, Loser = 63, Round = 4, PointDifference = 0 }; // OT, 74-71

            yield return new Game { Winner = 27, Loser = 7, Round = 5, PointDifference = 56 - 55 };
            yield return new Game { Winner = 51, Loser = 42, Round = 5, PointDifference = 70 - 62 };
            yield return new Game { Winner = 27, Loser = 51, Round = 6, PointDifference = 53 - 41 };
        }

        // Copy/paste/reformat from http://www.botbrackets.com/help-positions
        private static Dictionary<int, string> Teams2010 = new Dictionary<int, string>()
        {
            {1, "Kansas"},              {17, "Syracuse"},       {33, "Kentucky"},       {49, "Duke"},
            {2, "Lehigh"},              {18, "Vermont"},        {34, "East Tenn. St."}, {50, "Ark.-Pine Bluff"},
            {3, "UNLV"},                {19, "Gonzaga"},        {35, "Texas"},          {51, "California"},
            {4, "UNI"},                 {20, "Florida St."},    {36, "Wake Forest"},    {52, "Louisville"},
            {5, "Michigan St."},        {21, "Butler"},         {37, "Temple"},         {53, "Texas A&M"},
            {6, "New Mexico St."},      {22, "UTEP"},           {38, "Cornell"},        {54, "Utah St."},
            {7, "Maryland"},            {23, "Vanderbilt"},     {39, "Wisconsin"},      {55, "Purdue"},
            {8, "Houston"},             {24, "Murray St."},     {40, "Wofford"},        {56, "Siena"},
            {9, "Tennessee"},           {25, "Xavier"},         {41, "Marquette"},      {57, "Notre Dame"},
            {10, "San Diego St."},      {26, "Minnesota"},      {42, "Washington"},     {58, "Old Dominion"},
            {11, "Georgetown"},         {27, "Pittsburgh"},     {43, "New Mexico"},     {59, "Baylor"},
            {12, "Ohio"},               {28, "Oakland"},        {44, "Montana"},        {60, "Sam Houston St."},
            {13, "Oklahoma St."},       {29, "BYU"},            {45, "Clemson"},        {61, "Richmond"},
            {14, "Georgia Tech"},       {30, "Florida"},        {46, "Missouri"},       {62, "St. Mary's (CA)"},
            {15, "Ohio St."},           {31, "Kansas St."},     {47, "West Virginia"},  {63, "Villanova"},
            {16, "UC Santa Barbara"},   {32, "North Texas"},    {48, "Morgan St."},     {64, "Robert Morris"}
        };

        // http://en.wikipedia.org/wiki/2010_NCAA_Men's_Division_I_Basketball_Tournament
        private static IEnumerable<Game> Get2010Games()
        {
            yield return new Game { Winner = 1, Loser = 2, Round = 1, PointDifference = 90 - 74 };
            yield return new Game { Winner = 4, Loser = 3, Round = 1, PointDifference = 69 - 66 };
            yield return new Game { Winner = 5, Loser = 6, Round = 1, PointDifference = 70 - 67 };
            yield return new Game { Winner = 7, Loser = 8, Round = 1, PointDifference = 89 - 77 };
            yield return new Game { Winner = 9, Loser = 10, Round = 1, PointDifference = 62 - 59 };
            yield return new Game { Winner = 12, Loser = 11, Round = 1, PointDifference = 97 - 83 };
            yield return new Game { Winner = 14, Loser = 13, Round = 1, PointDifference = 64 - 59 };
            yield return new Game { Winner = 15, Loser = 16, Round = 1, PointDifference = 68 - 51 };
            yield return new Game { Winner = 4, Loser = 1, Round = 2, PointDifference = 69 - 67 };
            yield return new Game { Winner = 5, Loser = 7, Round = 2, PointDifference = 85 - 83 };
            yield return new Game { Winner = 9, Loser = 12, Round = 2, PointDifference = 83 - 68 };
            yield return new Game { Winner = 15, Loser = 14, Round = 2, PointDifference = 75 - 66 };
            yield return new Game { Winner = 5, Loser = 4, Round = 3, PointDifference = 59 - 52 };
            yield return new Game { Winner = 9, Loser = 15, Round = 3, PointDifference = 76 - 73 };
            yield return new Game { Winner = 5, Loser = 9, Round = 4, PointDifference = 70 - 69 };

            yield return new Game { Winner = 17, Loser = 18, Round = 1, PointDifference = 79 - 56 };
            yield return new Game { Winner = 19, Loser = 20, Round = 1, PointDifference = 67 - 60 };
            yield return new Game { Winner = 21, Loser = 22, Round = 1, PointDifference = 77 - 59 };
            yield return new Game { Winner = 24, Loser = 23, Round = 1, PointDifference = 66 - 65 };
            yield return new Game { Winner = 25, Loser = 26, Round = 1, PointDifference = 65 - 54 };
            yield return new Game { Winner = 27, Loser = 28, Round = 1, PointDifference = 89 - 66 };
            yield return new Game { Winner = 29, Loser = 30, Round = 1, PointDifference = 0 }; // 2OT, 99-92
            yield return new Game { Winner = 31, Loser = 32, Round = 1, PointDifference = 82 - 62 };
            yield return new Game { Winner = 17, Loser = 19, Round = 2, PointDifference = 87 - 65 };
            yield return new Game { Winner = 21, Loser = 24, Round = 2, PointDifference = 54 - 52 };
            yield return new Game { Winner = 25, Loser = 27, Round = 2, PointDifference = 71 - 68 };
            yield return new Game { Winner = 31, Loser = 29, Round = 2, PointDifference = 84 - 72 };
            yield return new Game { Winner = 21, Loser = 17, Round = 3, PointDifference = 63 - 59 };
            yield return new Game { Winner = 31, Loser = 25, Round = 3, PointDifference = 0 }; // 2OT, 101-96
            yield return new Game { Winner = 21, Loser = 31, Round = 4, PointDifference = 63 - 56 };

            yield return new Game { Winner = 33, Loser = 34, Round = 1, PointDifference = 100 - 71 };
            yield return new Game { Winner = 36, Loser = 35, Round = 1, PointDifference = 0 }; // OT, 81-80
            yield return new Game { Winner = 38, Loser = 37, Round = 1, PointDifference = 78 - 65 };
            yield return new Game { Winner = 39, Loser = 40, Round = 1, PointDifference = 53 - 49 };
            yield return new Game { Winner = 42, Loser = 41, Round = 1, PointDifference = 80 - 78 };
            yield return new Game { Winner = 43, Loser = 44, Round = 1, PointDifference = 62 - 57 };
            yield return new Game { Winner = 46, Loser = 45, Round = 1, PointDifference = 86 - 78 };
            yield return new Game { Winner = 47, Loser = 48, Round = 1, PointDifference = 77 - 50 };
            yield return new Game { Winner = 33, Loser = 36, Round = 2, PointDifference = 90 - 60 };
            yield return new Game { Winner = 38, Loser = 39, Round = 2, PointDifference = 87 - 69 };
            yield return new Game { Winner = 42, Loser = 43, Round = 2, PointDifference = 82 - 64 };
            yield return new Game { Winner = 47, Loser = 46, Round = 2, PointDifference = 68 - 59 };
            yield return new Game { Winner = 33, Loser = 38, Round = 3, PointDifference = 62 - 45 };
            yield return new Game { Winner = 47, Loser = 42, Round = 3, PointDifference = 69 - 56 };
            yield return new Game { Winner = 47, Loser = 33, Round = 4, PointDifference = 73 - 66 };

            yield return new Game { Winner = 49, Loser = 50, Round = 1, PointDifference = 73 - 44 };
            yield return new Game { Winner = 51, Loser = 52, Round = 1, PointDifference = 77 - 62 };
            yield return new Game { Winner = 53, Loser = 54, Round = 1, PointDifference = 69 - 53 };
            yield return new Game { Winner = 55, Loser = 56, Round = 1, PointDifference = 72 - 64 };
            yield return new Game { Winner = 58, Loser = 57, Round = 1, PointDifference = 51 - 50 };
            yield return new Game { Winner = 59, Loser = 60, Round = 1, PointDifference = 68 - 59 };
            yield return new Game { Winner = 62, Loser = 61, Round = 1, PointDifference = 80 - 71 };
            yield return new Game { Winner = 63, Loser = 64, Round = 1, PointDifference = 0 }; // OT, 73-70
            yield return new Game { Winner = 49, Loser = 51, Round = 2, PointDifference = 68 - 53 };
            yield return new Game { Winner = 55, Loser = 53, Round = 2, PointDifference = 0 }; // OT, 63-61
            yield return new Game { Winner = 59, Loser = 58, Round = 2, PointDifference = 76 - 68 };
            yield return new Game { Winner = 62, Loser = 63, Round = 2, PointDifference = 75 - 68 };
            yield return new Game { Winner = 49, Loser = 55, Round = 3, PointDifference = 70 - 57 };
            yield return new Game { Winner = 59, Loser = 62, Round = 3, PointDifference = 72 - 49 };
            yield return new Game { Winner = 49, Loser = 59, Round = 4, PointDifference = 78 - 71 };

            yield return new Game { Winner = 21, Loser = 5, Round = 5, PointDifference = 52 - 50 };
            yield return new Game { Winner = 49, Loser = 47, Round = 5, PointDifference = 78 - 57 };
            yield return new Game { Winner = 49, Loser = 21, Round = 6, PointDifference = 61 - 59 };
        }
    }
}
