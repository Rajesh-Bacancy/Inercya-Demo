using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = new int[100000];
        Random random = new Random();

        for (int i = 0; i < numbers.Length; i++)
        {
            int number;
            do
            {
                number = random.Next(1, 100001);
            } while (Array.IndexOf(numbers, number) != -1);

            numbers[i] = number;
        }

        using (StreamWriter writer = new StreamWriter(string.Join("\\", AppDomain.CurrentDomain.BaseDirectory.Split("\\").SkipLast(4)) + @"\Output\RandomNumbers.csv"))
        {
            foreach (int number in numbers)
            {
                writer.WriteLine(number);
            }
        }
    }
}
