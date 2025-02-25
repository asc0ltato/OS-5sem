﻿class Program
{
    const int TaskCount = 4;
    const int ThreadLifeTime = 5;
    const int ObservationTime = 8;
    static int[,] Matrix = new int[TaskCount, ObservationTime];
    static DateTime StartTime = DateTime.Now;

    static void Work(object o)
    {
        int id = (int)o;

        for (int i = 0; i < ThreadLifeTime * 20; i++)
        {
            DateTime CurrentTime = DateTime.Now;

            int ElapsedSeconds = (int)Math.Floor(CurrentTime.Subtract(StartTime).TotalSeconds);

            if (ElapsedSeconds >= 0 && ElapsedSeconds < ObservationTime)
            {
                Matrix[id, ElapsedSeconds] += 500;
            }

            MySleep(500);
        }
    }

    static void Main(string[] args)
    {
        Task[] t = new Task[TaskCount];
        int[] numbers = new int[TaskCount];
        for (int i = 0; i < TaskCount; i++)
            numbers[i] = i;
        Console.WriteLine("A student ... is creating tasks...");
        t[0] = new Task(() => { Work(0); });
        t[0].Start();
        t[1] = t[0].ContinueWith(delegate { Work(1); });
        t[2] = new Task(() => { Work(2); });
        t[2].Start();
        t[3] = t[1].ContinueWith(delegate { Work(3); });
        Console.WriteLine("A student ... is waiting for tasks to finish...");
        Task.WaitAll(t);
        for (int s = 0; s < ObservationTime; s++)
        {
            Console.Write("{0,3}: ", s);
            for (int th = 0; th < TaskCount; th++)
                Console.Write(" {0,5}", Matrix[th, s]);
            Console.WriteLine();
        }

    }

    static double MySleep(int ms)
    {
        double sum = 0, Temp;

        for (int t = 0; t < ms; ++t)
        {
            Temp = 0.711 + (double)t / 10000.0;
            double a, b, c, d, e, nt;
            for (int k = 0; k < 5500; ++k)
            {
                nt = Temp - k / 27000.0;
                a = Math.Sin(nt);
                b = Math.Cos(nt);
                c = Math.Cos(nt / 2.0);
                d = Math.Sin(nt / 2);
                e = Math.Abs(1.0 - a * a - b * b) + Math.Abs(1.0 - c * c - d * d);
                sum += e;
            }
        }
        return sum;
    }
}