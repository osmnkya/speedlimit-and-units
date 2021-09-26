using System;

namespace AcademicWork //AnswerA
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                int data = int.Parse(Console.ReadLine());

                if (data == -1)
                    break;

                int[] dataset = new int[data];

                int mil = 0;
                int time = 0;

                for (int i = 0; i < dataset.Length; i++)
                {
                    String[] line = Console.ReadLine().Split(' ');
                    int speed = int.Parse(line[0]);
                    int hour = int.Parse(line[1]);

                    mil += speed * (hour - time);
                    time = hour;
                }
                Console.WriteLine(mil + " miles");
            }
        }
    }
}