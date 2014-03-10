using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tadmas.BotBrackets.Genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            string authCookie = File.ReadAllText("authcookie.txt");
            string serializeFile = "population.xml";

            Random random = new Random();
            Population population;

            if (File.Exists(serializeFile))
            {
                population = Population.Deserialize(File.ReadAllText(serializeFile));
            }
            else
            {
                population = new Population();
                population.Generation = 1;
                for (int i = 0; i < 50; i++)
                {
                    population.Members.Add(new Genome(random));
                }

                File.WriteAllText(serializeFile, population.Serialize());
            }

            if (args.Length > 0 && args[0] == "dumpscripts")
            {
                if (!Directory.Exists("scripts"))
                    Directory.CreateDirectory("scripts");

                foreach (Genome genome in population.Members)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(genome.WriteScript());
                    sb.AppendLine("return retVal / 3;");
                    File.WriteAllText(Path.Combine("scripts", Guid.NewGuid().ToString("N") + ".js"), sb.ToString());
                }
            }
            else
            {
                while (true)
                {
                    Console.Out.WriteLine("Generation {0}.", population.Generation);

                    population = Selection.SelectNextGeneration(population, random, authCookie);
                    File.WriteAllText(serializeFile, population.Serialize());
                }
            }
        }
    }
}
