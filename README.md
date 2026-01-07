# Canadian-UAVs-Coding-Challenge

## Input Data
The two sensors provide different output formats: one sensor outputs data in CSV format, and the other outputs data in JSON format. Please refer to the sample data for the exact format of each sensor's output. Both sensors assign a unique ID to each reading, but note that different sensors may use the same IDs. The sensor readings include location coordinates in decimal degrees, using the WGS 84 format, representing where the anomaly was detected. The sensors have an accuracy of 100 meters, meaning that the reported location is within 100 meters of the actual anomaly location.

## Output Data
The output should consist of pairs of IDs, where one ID is from the first sensor, and the second ID is from the second sensor.

## How To Run The SensorDataReader.cs Solution
The SensorDataReader.cs program should be ran with 2 command line arguments, where the first argument is the csv data, and the second argument is the json data. To run, should use the following command in the terminal:
```dotnet run <data.csv> <data.json>```

## Designed by:
Vicente David

vicentedavid26@gmail.com