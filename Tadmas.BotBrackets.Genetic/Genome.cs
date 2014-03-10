using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tadmas.BotBrackets.Genetic
{
    public class Genome : IXmlSerializable
    {
        private double EstWinPctWeight;
        private double StrengthOfScheduleWeight;
        private double LuckFactorWeight;
        private double TOPctWeight;
        private double FTPctWeight;
        private double ORBPctWeight;
        private double EFGPctWeight;
        private double EstWinPctExponent;
        private double TOPctExponent;
        private double FTPctExponent;
        private double ORBPctExponent;
        private double EFGPctExponent;

        public Genome(Random random)
        {
            EstWinPctExponent = RandomExponent(random);
            EstWinPctWeight = RandomWeight(random);
            StrengthOfScheduleWeight = RandomWeight(random);
            LuckFactorWeight = RandomWeight(random);
            TOPctExponent = RandomExponent(random);
            TOPctWeight = RandomWeight(random);
            FTPctExponent = RandomExponent(random);
            FTPctWeight = RandomWeight(random);
            ORBPctExponent = RandomExponent(random);
            ORBPctWeight = RandomWeight(random);
            EFGPctExponent = RandomExponent(random);
            EFGPctWeight = RandomWeight(random);
        }

        private Genome() { } // for serialization

        private Genome(double[] genes)
        {
            if (genes.Length != 12)
                throw new ArgumentException();

            EstWinPctExponent = ClipExponent(genes[0]);
            EstWinPctWeight = ClipWeight(genes[1]);
            StrengthOfScheduleWeight = ClipWeight(genes[2]);
            LuckFactorWeight = ClipWeight(genes[3]);
            TOPctExponent = ClipExponent(genes[4]);
            TOPctWeight = ClipWeight(genes[5]);
            FTPctExponent = ClipExponent(genes[6]);
            FTPctWeight = ClipWeight(genes[7]);
            ORBPctExponent = ClipExponent(genes[8]);
            ORBPctWeight = ClipWeight(genes[9]);
            EFGPctExponent = ClipExponent(genes[10]);
            EFGPctWeight = ClipWeight(genes[11]);
        }

        private double[] GeneArray()
        {
            return new double[]
            {
                EstWinPctExponent,
                EstWinPctWeight,
                StrengthOfScheduleWeight,
                LuckFactorWeight,
                TOPctExponent,
                TOPctWeight,
                FTPctExponent,
                FTPctWeight,
                ORBPctExponent,
                ORBPctWeight,
                EFGPctExponent,
                EFGPctWeight
            };
        }

        public void Mutate(Random random)
        {
            switch (random.Next(12))
            {
                case 0:
                    AdjustWeight(ref EstWinPctWeight, random);
                    break;
                case 1:
                    AdjustWeight(ref StrengthOfScheduleWeight, random);
                    break;
                case 2:
                    AdjustWeight(ref LuckFactorWeight, random);
                    break;
                case 3:
                    AdjustWeight(ref TOPctWeight, random);
                    break;
                case 4:
                    AdjustWeight(ref FTPctWeight, random);
                    break;
                case 5:
                    AdjustWeight(ref ORBPctWeight, random);
                    break;
                case 6:
                    AdjustWeight(ref EFGPctWeight, random);
                    break;
                case 7:
                    AdjustExponent(ref EstWinPctExponent, random);
                    break;
                case 8:
                    AdjustExponent(ref TOPctExponent, random);
                    break;
                case 9:
                    AdjustExponent(ref FTPctExponent, random);
                    break;
                case 10:
                    AdjustExponent(ref ORBPctExponent, random);
                    break;
                case 11:
                    AdjustExponent(ref EFGPctExponent, random);
                    break;
            }
        }

        public static Genome Crossover(Genome a, Genome b, Random random)
        {
            switch (random.Next(10))
            {
                default:
                case 0:
                case 1:
                case 2:
                case 3:
                    // One-point crossover
                    //
                    // We always want to get the first element from the first parent and the last element
                    // from the last parent, so find a switchover point from somewhere in the middle.
                    //
                    // +---+---+---+---+---+---+---+---+---+---+----+----+
                    // | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 |
                    // +---+---+---+---+---+---+---+---+---+---+----+----+
                    //     ^________________________________________^ crossover in this range

                    int crossoverPoint = random.Next(11);
                    if (random.Next(2) == 0)
                        return new Genome(a.GeneArray()
                            .Take(1 + crossoverPoint)
                            .Concat(b.GeneArray().Skip(1 + crossoverPoint))
                            .ToArray());
                    else
                        return new Genome(b.GeneArray()
                            .Take(1 + crossoverPoint)
                            .Concat(a.GeneArray().Skip(1 + crossoverPoint))
                            .ToArray());
                case 4:
                case 5:
                case 6:
                    // Two-point crossover
                    //
                    // We always want to get the first element from the first parent, the last element from
                    // the first parent, and have two crossover points in between.  Need to find a single
                    // switchover point and then find a second one in the range left over.  This method
                    // also prevents too large of an initial section so that the call to Random.Next will
                    // not pass 0 or 1 (ease of implementation consideration only).
                    //
                    // +---+---+---+---+---+---+---+---+---+---+----+----+
                    // | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 |
                    // +---+---+---+---+---+---+---+---+---+---+----+----+
                    //     ^___________________________^ first crossover point
                    //                        ?_____________________^ second crossover point
                    int crossoverPoint1 = random.Next(8);
                    int crossoverPoint2 = random.Next(9 - crossoverPoint1);
                    if (random.Next(2) == 0)
                        return new Genome(a.GeneArray()
                            .Take(1 + crossoverPoint1)
                            .Concat(b.GeneArray().Skip(1 + crossoverPoint1).Take(1 + crossoverPoint2))
                            .Concat(a.GeneArray().Skip(2 + crossoverPoint1 + crossoverPoint2))
                            .ToArray());
                    else
                        return new Genome(b.GeneArray()
                            .Take(1 + crossoverPoint1)
                            .Concat(a.GeneArray().Skip(1 + crossoverPoint1).Take(1 + crossoverPoint2))
                            .Concat(b.GeneArray().Skip(2 + crossoverPoint1 + crossoverPoint2))
                            .ToArray());
                case 7:
                    // Randomly pick genes.  This will likely result in some funky combinations, which is
                    // why it is least likely to be picked.
                    return new Genome(a.GeneArray().Zip(b.GeneArray(),
                        (aVal, bVal) => random.Next(2) == 0 ? aVal : bVal).ToArray());
                case 8:
                case 9:
                    // Average genes together
                    return new Genome(a.GeneArray().Zip(b.GeneArray(),
                        (aVal, bVal) => (aVal + bVal) / 2).ToArray());
            }
        }

        private static void AdjustWeight(ref double value, Random random)
        {
            double adjustment = random.NextDouble() * 10 - 5;
            value = ClipWeight(value + adjustment);
        }

        private static void AdjustExponent(ref double value, Random random)
        {
            double adjustment = random.NextDouble() * 4 - 2;
            value = ClipExponent(value + adjustment);
        }

        private static double ClipWeight(double value)
        {
            if (value > 100)
                return 100;
            else if (value < -100)
                return -100;
            else
                return value;
        }

        private static double ClipExponent(double value)
        {
            if (value > 40)
                return 40;
            else if (value < 1)
                return 1;
            else
                return value;
        }

        private static double RandomWeight(Random random)
        {
            // Don't start out all the way at the extreme end of the spectrum.
            return ClipWeight(random.NextDouble() * 60 - 30);
        }

        private static double RandomExponent(Random random)
        {
            // Don't start out all the way at the extreme high end of the spectrum, but low is ok.
            return ClipExponent(random.NextDouble() * 14 + 1);
        }

        public string WriteScript()
        {
            // {0} = estWcheck, {1} = sosCheck, {2} = luckCheck, {3} = toCheck, {4} = ftCheck, {5} = orbCheck,
            // {6} = efgCheck, {7} = estWexp, {8} = toExp, {9} = ftExp, {10} = orbExp, {11} = efgExp
            return string.Format(Resources.Script, EstWinPctWeight, StrengthOfScheduleWeight, LuckFactorWeight,
                TOPctWeight, FTPctWeight, ORBPctWeight, EFGPctWeight, EstWinPctExponent, TOPctExponent,
                FTPctExponent, ORBPctExponent, EFGPctExponent);
        }

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            EstWinPctWeight = double.Parse(reader.GetAttribute("estWcheck"));
            StrengthOfScheduleWeight = double.Parse(reader.GetAttribute("sosCheck"));
            LuckFactorWeight = double.Parse(reader.GetAttribute("luckCheck"));
            TOPctWeight = double.Parse(reader.GetAttribute("toCheck"));
            FTPctWeight = double.Parse(reader.GetAttribute("ftCheck"));
            ORBPctWeight = double.Parse(reader.GetAttribute("orbCheck"));
            EFGPctWeight = double.Parse(reader.GetAttribute("efgCheck"));
            EstWinPctExponent = double.Parse(reader.GetAttribute("estWexp"));
            TOPctExponent = double.Parse(reader.GetAttribute("toExp"));
            FTPctExponent = double.Parse(reader.GetAttribute("ftExp"));
            ORBPctExponent = double.Parse(reader.GetAttribute("orbExp"));
            EFGPctExponent = double.Parse(reader.GetAttribute("efgExp"));

            bool isEmptyElement = reader.IsEmptyElement;
            reader.ReadStartElement();
            if (!isEmptyElement)
                reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("estWcheck", EstWinPctWeight.ToString("R"));
            writer.WriteAttributeString("sosCheck", StrengthOfScheduleWeight.ToString("R"));
            writer.WriteAttributeString("luckCheck", LuckFactorWeight.ToString("R"));
            writer.WriteAttributeString("toCheck", TOPctWeight.ToString("R"));
            writer.WriteAttributeString("ftCheck", FTPctWeight.ToString("R"));
            writer.WriteAttributeString("orbCheck", ORBPctWeight.ToString("R"));
            writer.WriteAttributeString("efgCheck", EFGPctWeight.ToString("R"));
            writer.WriteAttributeString("estWexp", EstWinPctExponent.ToString("R"));
            writer.WriteAttributeString("toExp", TOPctExponent.ToString("R"));
            writer.WriteAttributeString("ftExp", FTPctExponent.ToString("R"));
            writer.WriteAttributeString("orbExp", ORBPctExponent.ToString("R"));
            writer.WriteAttributeString("efgExp", EFGPctExponent.ToString("R"));
        }
    }
}
