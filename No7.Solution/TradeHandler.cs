using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading;

namespace No7.Solution
{
    public static class TradeHandler
    {
        public static void HandleTrades(Stream stream)
        {
            // Исправление price
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            // SRP
            var trades = DataProvider.Parther(GetLines.GetFillLines(stream));

            // save into database
            SaveToDB.Saver(trades);
        }
    }
}
