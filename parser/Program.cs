using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace parser
{
    class Program
    {
        static void Main(string[] args)
        {


            //get the file to parse
            string filePathWithEnv = @"%USERPROFILE%\Documents\AgOpenGPS\Fields\NMEA_log.txt";
            string filePath = Environment.ExpandEnvironmentVariables(filePathWithEnv);

            //Convert the file to list of string
            List<string> data = new List<string>();
            data = File.ReadAllLines(filePath).ToList();

            //create a string list for parsed data to save it as
            List<string> parsed = new List<string>();
            //Add labels to first row so gpsvisualize.com can understand this file
            parsed.Add("Latitude,Longitude");


            //Parsing process for each line
            foreach(string line in data)
            {
                //focu only to the long lines that contain latitude and longitude
                if(line.Length > 30)
                {
                    //Use 'SPACEBAR' as a plitter to split the row data sections to a list
                    string[] splittedLine = line.Split(' ');
                    //Add the latitude and longitude info to parsed list. Seperate with comma
                    parsed.Add($"{splittedLine[1]},{splittedLine[2]}");
                }
            }

            //here is the file to parse
            string saveFilePathWithEnv = @"%USERPROFILE%\Documents\AgOpenGPS\Fields\NMEA_log_parsed.txt";
            string saveFilePath = Environment.ExpandEnvironmentVariables(saveFilePathWithEnv);

            //Save the parsed information as a new textfile
            File.WriteAllLines(saveFilePath, parsed);

            //Ask if the user wants to open gpsvisualizer website.
            Console.WriteLine("File is now parsed. You can find it from Fields folder.");
            Console.WriteLine("Would you like to open gpsvisualizer.com ? y = yes, n = no");
            string answer = Console.ReadLine();
            if (answer.Contains("y"))
                System.Diagnostics.Process.Start("www.gpsvisualizer.com");

        }
    }
}
