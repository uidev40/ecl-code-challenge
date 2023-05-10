using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECLCodeChallenge
{
    public static class Generate
    {
        public static string GenerateOutPut(string filePath, int count)
        {

            string json = string.Empty;

            Dictionary<long, string> validDataDict = new Dictionary<long, string>();
            List<string> inValidRecords = new List<string>();

            //Check if filepath exist
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    //Check for empty line
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    //Check for "id"
                    if (!line.Contains("\"id\""))
                    {
                        inValidRecords.Add(line);

                        return "exit with code 2";
                    }
                    long score;
                    string id;

                    //this time spliting code first by ',' then by ':'
                    //[0] index is score
                    //[2] index is id
                    string[] parts = line.Split(',')[0].Split(':');

                    if (parts.Length > 2)
                    {  
                        score = long.Parse(parts[0]);
                        id = parts[2].Replace("\"", "");

                        // Add the key-value pair to the dictionary
                        validDataDict.Add(score, id);
                    }
                    else
                    {
                        inValidRecords.Add(line);
                        return "exit with code 2";
                    }
                }
                // Sort the dictionary by key
                var sortedDict = from entry in validDataDict
                                 orderby entry.Key descending
                                 select entry;

                int tempCount = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append("[");

                foreach (KeyValuePair<long, string> entry in sortedDict)
                {
                    tempCount++;

                    json = JsonConvert.SerializeObject(entry, Formatting.Indented);
                    sb.Append(Environment.NewLine);
                    sb.Append(json.Replace("Key", "score").Replace("Value", "id"));

                    if (tempCount >= count)
                    {
                        sb.Append(Environment.NewLine);
                        sb.Append("]");
                        break;
                    }

                    sb.Append(",");

                }

                Console.WriteLine("---Below is the output----");
                Console.WriteLine(sb.ToString());
                return "exit with code 0";
            }
            else
            {
                return "exit with code 1";
            }
        }
    }
}
