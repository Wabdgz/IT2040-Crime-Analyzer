using System;
using System.Collections.Generic;
using System.IO;

namespace CrimeAnalyzer
{
    public static class CrimeStatsLoader
    {
        private static int NumItemsInRow = 11;

        public static List<CrimeStats> Load(string csvDataFilePath) {
            List<CrimeStats> crimeStatsList = new List<CrimeStats>();

            try
            {
                using (var reader = new StreamReader(csvDataFilePath))
                {
                    int lineNumber = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineNumber++;
                        if (lineNumber == 1) continue;

                        var values = line.Split(',');

                        if (values.Length != NumItemsInRow)
                        {
                            throw new Exception($"Row {lineNumber} contains {values.Length} values. It should contain {NumItemsInRow}.");
                        }
                        try
                        {
                            int year = Int32.Parse(values[0]);
                            int population = Int32.Parse(values[1]);
                            int violentCrime = Int32.Parse(values[2]);
                            int murder = Int32.Parse(values[3]);
                            int rape = Int32.Parse(values[4]);
                            int robbery = Int32.Parse(values[5]);
                            int aggravatedAssault = Int32.Parse(values[6]);
                            int propertyCrime = Int32.Parse(values[7]);
                            int burglary = Int32.Parse(values[8]);
                            int theft = Int32.Parse(values[9]);
                            int motorVehicleTheft = Int32.Parse(values[10]);
                            CrimeStats crimeStats = new CrimeStats(year, population, violentCrime, murder, rape, robbery, aggravatedAssault, propertyCrime, burglary, theft, motorVehicleTheft);
                            crimeStatsList.Add(crimeStats);
                        }
                        catch (FormatException e)
                        {
                            throw new Exception($"Row {lineNumber} contains invalid data. ({e.Message})");
                        }
                    }
                }
            } catch (Exception e){
                throw new Exception($"Unable to open {csvDataFilePath} ({e.Message}).");
            }

            return crimeStatsList;
        }
    }
}
