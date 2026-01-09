using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
record SensorData(int Id, double Latitude, double Longitude); // record to store data for each sensor
class SensorDataReader
{
    class SensorMatch
    {
        public int id1
        {
            get;
            set;
        }
        public int id2
        {
            get;
            set;
        }
    }
    // =================== Helper Functions =======================
    static double ToRad(double deg)
    {
        return deg * Math.PI / 180.0;
    }
    static double HaversineMeters(double lat1, double lon1, double lat2, double lon2)
    {
        // calculate the great-circle distance between two points on a sphere using Haversine formula. 
        const double r = 6371000; // radius of the earth in meters
        
        // need radians for trig functions
        var deltaLat = ToRad(lat2 - lat1);
        var deltaLon = ToRad(lon2 - lon1);

        var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) * Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return r * c; // returns the distance in meters
    }
    // ============================================================

    static void Main(string[] args)
    {
        if (args.Length != 2) // error check
        {
            Console.WriteLine("Error: 2 Arguments not found.\nUsage: dotnet run <file.csv> <file.json>");
            return;
        }

        string csvPath = args[0];
        string jsonPath = args[1];

        List<SensorData> csvSensors = new();

        // =================== Read CSV sensor data ===================
        using var reader = new StreamReader(csvPath);
        reader.ReadLine(); // "skip" header

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var fields = line.Split(',');

            csvSensors.Add(new SensorData(int.Parse(fields[0]), double.Parse(fields[1]), double.Parse(fields[2])));
        }

        reader.Close();
        // ============================================================

        // =================== Read JSON sensor data ==================
        string jsonText = File.ReadAllText(jsonPath);
        var jsonSensors = JsonSerializer.Deserialize<List<SensorData>>(jsonText, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

        if (jsonSensors == null)
        {
            Console.WriteLine("Failed to read JSON sensor data.");
            return;
        }
        // ============================================================


        // =================== Evaluate Sensor Data ===================
        var matches = new Dictionary<int, int>();
        const double MAX_DISTANCE = 100.0; // max distance of 100 meters as this is the accuracy of the sensors

        foreach (var csvSensor in csvSensors)
        {
            foreach(var jsonSensor in jsonSensors)
            {
                double distance = HaversineMeters(csvSensor.Latitude, csvSensor.Longitude, jsonSensor.Latitude, jsonSensor.Longitude);

                if (distance <= MAX_DISTANCE) // match found
                {
                    Console.WriteLine($"Match found.. CSV ID: {csvSensor.Id} JSON ID: {jsonSensor.Id}");
                    matches[csvSensor.Id] = jsonSensor.Id; // add match to the dictionary
                }
            }
        }
        // ============================================================


        // =================== Output Sensor Data =====================
        // serialize matches to a formatted JSON string
        string jsonString = JsonSerializer.Serialize(matches, new JsonSerializerOptions {WriteIndented = true });

        // get file path to write the output to
        string fileName = "output.json";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        // write the JSON string to the output directory
        try
        {
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine($"Successfully Wrote JSON Output To: {filePath}");    
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An Error Occured: {ex.Message}\n"); 
        }
        // ============================================================
    }
}