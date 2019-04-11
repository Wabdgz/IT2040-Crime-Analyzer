using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CrimeAnalyzer
{
    public static class CrimeStatsReport
    {
        public static string GenerateText(List<CrimeStats> crimeStatsList)
        {
            string report = "Crime Analyzer Report\n\n";

            if (crimeStatsList.Count() < 1)
            {
                report += "No data is available.\n";

                return report;
            }

            var startYear = (from crimeStats in crimeStatsList select crimeStats.Year).Min();
            var endYear = (from crimeStats in crimeStatsList select crimeStats.Year).Max();

            report += $"Period: {startYear}-{endYear} ({crimeStatsList.Count} years)\n";


            report += "Years murders per year < 15000: ";
            var years = from crimeStats in crimeStatsList where crimeStats.Murder < 15000 select crimeStats.Year;
            if (years.Count() > 0)
            {
                foreach (var year in years)
                {
                    report += year + ",";

                }
                report.TrimEnd(',');
                report += "\n";
            }
            else
            {
                report += "not available\n";
            }

            report += "Robberies per year > 500000: ";
            var records = from crimeStats in crimeStatsList where crimeStats.Robbery > 500000 select crimeStats;
            if (records.Count() > 0)
            {
                foreach (var record in records)
                {
                    report += record.Year + " = " + record.Robbery + ",";
                }
                report.TrimEnd(',');
                report += "\n";
            }
            else
            {
                report += "not available\n";
            }

            report += "Violent crime per capita rate (2010): ";
            var violentCrimeRates = from crimeStats in crimeStatsList where crimeStats.Year == 2010 select ((double)(crimeStats.ViolentCrime) / (double)(crimeStats.Population));
            if (violentCrimeRates.Count() > 0)
            {
                report += violentCrimeRates.First();
                report += "\n";
            }
            else
            {
                report += "not available\n";
            }

            var averageMurdersAll = (from crimeStats in crimeStatsList select crimeStats.Murder).Average();
            report += $"Average murder per year (all years): {averageMurdersAll}\n";

            var averageMurders1994_1997 = (from crimeStats in crimeStatsList where crimeStats.Year >= 1994 && crimeStats.Year <= 1997 select crimeStats.Murder).Average();
            report += $"Average murder per year (1994 - 1997): {averageMurders1994_1997}\n";

            var averageMurders2010_2014 = (from crimeStats in crimeStatsList where crimeStats.Year >= 2010 && crimeStats.Year <= 2013 select crimeStats.Murder).Average();
            report += $"Average murder per year (2010 - 2014): {averageMurders2010_2014}\n";

            var minimumThefts = (from crimeStats in crimeStatsList where crimeStats.Year >= 1999 && crimeStats.Year <= 2004 select crimeStats.Theft).Min();
            report += $"Minimum thefts per year (1999 - 2004): {minimumThefts}\n";

            var maximumThefts = (from crimeStats in crimeStatsList where crimeStats.Year >= 1999 && crimeStats.Year <= 2004 select crimeStats.Theft).Max();
            report += $"Maximum thefts per year (1999 - 2004): {maximumThefts}\n";

            report += "Year of highest number of motor vehicle thefts: ";
            var yearMaxMotorVehicleThefts = from crimeStats in crimeStatsList where crimeStats.MotorVehicleTheft == ((from stats in crimeStatsList select stats.MotorVehicleTheft).Max()) select crimeStats.Year;
            if (yearMaxMotorVehicleThefts.Count() > 0)
            {
                report += yearMaxMotorVehicleThefts.First();
                report += "\n";
            }
            else
            {
                report += "not available\n";
            }

            return report;
        }
    }
}
