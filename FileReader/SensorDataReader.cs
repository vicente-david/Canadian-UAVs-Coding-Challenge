using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
record SensorData(int Id, double Latitude, double Longitude); // record to store data for each sensor
class SensorDataReader
{
    static void Main(string[] args)
    {
        if (args.Length != 2) // error check
        {
            Console.WriteLine("Error: 2 Arguments not found.\nUsage: dotnet run <file.csv> <file.json>");
            return;
        }

        string csvPath = args[0];
        string jsonPath = args[1];

        List<SensorData> sensors = new();

        // =================== Read CSV sensor data ===================
        using var reader = new StreamReader(csvPath);
        reader.ReadLine(); // "skip" header

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var fields = line.Split(',');

            sensors.Add(new SensorData(int.Parse(fields[0]), double.Parse(fields[1]), double.Parse(fields[2])));
        }

        Console.WriteLine($"CSV Rows: {sensors.Count}");
        reader.Close();
        // ============================================================

        // =================== Read JSON sensor data ==================
        string jsonText = File.ReadAllText(jsonPath);
        var jsonData = JsonDocument.Parse(jsonText);

        Console.WriteLine("JSON loaded successfully");
        // ============================================================
    }
}