using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmas.BotBrackets.Genetic
{
    public static class Selection
    {
        public static Population SelectNextGeneration(Population population, Random random, string authCookie)
        {
            var seasons = HistoricalOutcomes.GetRandomSeasons(3, random);

            // First, get the fitness value for all the members of the population.
            var scores = population.Members
                .AsParallel()
                .WithDegreeOfParallelism(3)
                .Select(x => new { Genome = x, Score = FitnessFunction.Evaluate(x, seasons, authCookie) })
                .ToList();

            scores.Sort((a, b) => Comparer<double>.Default.Compare(a.Score, b.Score));

            // Methods of selection:
            // Top 10 make it to the next generation unchanged. (Elitism)
            Population nextGeneration = new Population { Generation = population.Generation + 1 };
            nextGeneration.Members.AddRange(scores.Take(10).Select(x => x.Genome));

            // Other 40 are the result of crossover between pairs chosen by roulette wheel selection.
            // Note that since our fitness is deemed by values approaching 0, roulette wheel selection is a bit tricky.
            // We will take the log of the fitness value, then the weight is max(x)-x for each item.
            double maxWeight = Math.Log(scores.Max(x => x.Score));
            var weights = scores.Select(x => new { Genome = x.Genome, Weight = maxWeight - Math.Log(x.Score) });
            double sumWeights = weights.Sum(x => x.Weight);

            double runningTotal = 0.0d;
            List<Tuple<Genome, double>> cumulativeWeights = new List<Tuple<Genome, double>>();
            foreach (var item in weights)
            {
                runningTotal += (item.Weight / sumWeights);
                cumulativeWeights.Add(Tuple.Create(item.Genome, runningTotal));
            }

            for (int i = 0; i < 40; i++)
            {
                Genome parent1 = RouletteWheelSelection(cumulativeWeights, random);
                Genome parent2 = RouletteWheelSelection(cumulativeWeights, random);
                nextGeneration.Members.Add(Genome.Crossover(parent1, parent2, random));
            }

            // Last thing to do is apply any mutations.
            foreach (Genome genome in nextGeneration.Members)
            {
                if (random.NextDouble() < 0.15)
                    genome.Mutate(random);
            }

            return nextGeneration;
        }

        private static Genome RouletteWheelSelection(List<Tuple<Genome, double>> cumulativeWeights, Random random)
        {
            double selection = random.NextDouble();
            for (int i = 0; i < cumulativeWeights.Count; i++)
            {
                if (selection <= cumulativeWeights[i].Item2)
                {
                    return cumulativeWeights[i].Item1;
                }
            }
            // Shouldn't happen.  If it does, it's probably due to some funkiness around 1.0, so return the last item.
            return cumulativeWeights[cumulativeWeights.Count - 1].Item1;
        }
    }
}
