using System;
using System.Collections.Generic;
using System.IO;

namespace No7.Solution
{
    internal static class DataProvider
    {
        // Использование итератора
        internal static IEnumerable<TradeRecord> Parther(IEnumerable<string> lines)
        {
            const float LOTSIZE = 100000f;
            

            int lineCount = 1;
            foreach (var line in lines)
            {
                var fields = line.Split(new char[] { ',' });

                if (fields.Length != 3)
                {
                    Logger($"WARN: Line {lineCount} malformed. Only {fields.Length} field(s) found.");
                    continue;
                }

                if (fields[0].Length != 6)
                {
                    Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
                    Logger($"WARN: Trade currencies on line {lineCount} malformed: '{fields[0]}'");
                    continue;
                }

                if (!int.TryParse(fields[1], out var tradeAmount))
                {
                    Console.WriteLine("WARN: Trade amount on line {0} not a valid integer: '{1}'", lineCount, fields[1]);
                    Logger($"WARN: Trade amount on line {lineCount} not a valid integer: '{fields[1]}'");
                }

                if (!decimal.TryParse(fields[2], out var tradePrice))
                {
                    Console.WriteLine("WARN: Trade price on line {0} not a valid decimal: '{1}'", lineCount, fields[2]);
                    Logger($"WARN: Trade price on line {lineCount} not a valid decimal: '{fields[2]}'");
                }

                string sourceCurrencyCode = fields[0].Substring(0, 3);
                string destinationCurrencyCode = fields[0].Substring(3, 3);
                lineCount++;

                var trade = new TradeRecord
                {
                    SourceCurrency = sourceCurrencyCode,
                    DestinationCurrency = destinationCurrencyCode,
                    Lots = tradeAmount / LOTSIZE,
                    Price = tradePrice
                };

                yield return trade;
            }
        }

        // Залогировал
        private static void Logger(String lines)
        {
            StreamWriter file = new StreamWriter("../../Logger.txt", true);
            file.WriteLine(lines + "\t\t\t" + DateTime.Now.ToLocalTime());
            file.Close();
        }

    }
}
