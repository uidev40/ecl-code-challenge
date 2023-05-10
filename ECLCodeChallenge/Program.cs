using System;

namespace ECLCodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ECL Code Challenge");

            //I have a folder "InputDataFiles" inside the project, if you have any other folder location for input file,
            //please update below file path
            string filePath = @"\InputDataFiles\example_input_data_1.data";
            int count = 5;

            string result = Generate.GenerateOutPut(filePath, count);
            //Console.WriteLine("---Below is the output----");
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
