using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmas.BotBrackets.Genetic
{
    public static class FitnessFunction
    {
        private static int[] Seasons = new int[] { 2010, 2011, 2012, 2013, 2014 };

        public static double Evaluate(Genome genome, string authCookie)
        {
            double squareError = 0.0;

            foreach (int season in Seasons)
            {
                var teams = HistoricalOutcomes.GetTeams(season);
                var games = HistoricalOutcomes.GetGames(season).ToLookup(g => g.Round);
                var script = WriteEvaluationScript(season, genome);
                var scores = WebsiteRequest.GetScoringData(season, script, authCookie);
                foreach (var score in scores)
                {
                    var historicalOutcome = games[score.Round].Where(g => teams[g.Winner] == score.Winner).Single();
                    var originalRetVal = score.Score * 4 - 10;

                    var correctRetVal = historicalOutcome.PointDifference;
                    if (correctRetVal > 10) correctRetVal = 10;
                    if (correctRetVal < -10) correctRetVal = -10;

                    var error = Math.Abs(originalRetVal - correctRetVal);
                    squareError += error * error;
                }
            }

            return squareError;
        }

        private static string WriteEvaluationScript(int season, Genome genome)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(genome.WriteScript());
            sb.AppendLine("if (retVal > 10) retVal = 10;");
            sb.AppendLine("if (retVal < -10) retVal = -10;");
            sb.AppendLine("var targetScore = (retVal + 10) / 4;");
            sb.AppendLine(string.Format("var lastRoundWon = [{0}]",
                string.Join(",", HistoricalOutcomes.GetLastRoundWon(season).Select(i => i.ToString()))));
            sb.AppendLine("if (lastRoundWon[$1.Position - 1] >= $Round) {");
            sb.AppendLine("  return (2 * targetScore)/$Score1 - 1;");
            sb.AppendLine("} else {");
            sb.AppendLine("  return (2 * ($Score2 - (5 - targetScore)))/$Score2 - 1;");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
