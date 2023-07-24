using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int[] numbers = new int[100000];
            Random random = new Random();

            Console.WriteLine("Number generation Started .....");
            for (int i = 0; i < numbers.Length; i++)
            {
                int number;
                // Getting distinct equally probable number
                do
                {
                    number = random.Next(1, 100001);
                } while (Array.IndexOf(numbers, number) != -1);

                numbers[i] = number;
            }
            Console.WriteLine("Number generation Completed .....");

            var outputData = string.Join(Environment.NewLine, numbers);

            // writing numbers in file
            var filePath = string.Join("\\", AppDomain.CurrentDomain.BaseDirectory.Split("\\").SkipLast(4)) + @"\Output\RandomNumbers.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(outputData);
            }
            Console.WriteLine($"File has been generated successfully at {filePath}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong !", e);
        }
      
    }
}
