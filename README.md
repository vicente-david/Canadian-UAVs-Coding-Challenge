# Canadian-UAVs-Coding-Challenge

## Input Data
The two sensors provide different output formats: one sensor outputs data in CSV format, and the other outputs data in JSON format. Please refer to the sample data for the exact format of each sensor's output. Both sensors assign a unique ID to each reading, but note that different sensors may use the same IDs. The sensor readings include location coordinates in decimal degrees, using the WGS 84 format, representing where the anomaly was detected. The sensors have an accuracy of 100 meters, meaning that the reported location is within 100 meters of the actual anomaly location.

## Output Data
The output should consist of pairs of IDs, where one ID is from the first sensor, and the second ID is from the second sensor.

## Solution
All necessary files, for both input and output, are located in the **FileReader** directory. The program which contains my solution is labeled **SensorDataReader.cs** inside the aforementioned directory.

### Considerations
Note that this solution isn't necessarily optimized in the way I would like it to be. This is because comparing the distances of each sensor location currently has a time complexity of O(n^2). This works perfectly fine for the given sample inputs, but for much larger datasets this could take far longer to compute than we would like it to. Ideally, I could store all the data in a database which I could then query in order to get the necessary information from each sensor, but I felt that that would have made this solution somewhat redundant and more complex than it was required for the given dataset.

Another consideration was that I wanted to provide some functionality to find not just the *first* anomaly from 2 given sensors, but find the 2 *closest* sensors that pinged the same anomaly. I believe I was able to implement this successfuly, but I wasn't able to test it effectively, as the sample input data did not have these "test" cases available for me to test against, and I didn't want to modify the given data.

## How To Run The SensorDataReader.cs Solution
The SensorDataReader.cs program should be ran with 2 command line arguments, where the first argument is the csv data, and the second argument is the json data. To run, should use the following command in the terminal:
```dotnet run <data.csv> <data.json>```

## Designed by:
Vicente David

vicentedavid26@gmail.com
