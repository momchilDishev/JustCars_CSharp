using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
class Program
{
    struct Car
    {
        public int x;
        public int y;
        public char c;
        public ConsoleColor color;

    }
    static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.WriteLine(c);
    }

    static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.WriteLine(str);
    }

    static void Main()
    {
        double speed = 30.0;
        int playFieldWidth = 8;
        int hits = 5;
        Console.BufferHeight = Console.WindowHeight = 20;
        Console.BufferWidth = Console.WindowWidth = 30;
        Car userCar = new Car();
        userCar.x = 2;
        userCar.y = Console.WindowHeight - 1;
        userCar.c = '1';
        userCar.color = ConsoleColor.Yellow;
        Random randomGenerator = new Random();
        List<Car> cars = new List<Car>();

        while (true)
        {
            bool hit = false;
            speed++;
            if (speed>400)
            {
                speed = 400;
                hits++;
            }
            Car newCar = new Car();
            newCar.color = ConsoleColor.Green;
            newCar.x = randomGenerator.Next(0, playFieldWidth);
            newCar.y = 0;
            newCar.c = '0';
            cars.Add(newCar);

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (userCar.x - 1 >= 0)
                    {
                        userCar.x--;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (userCar.x + 1 < playFieldWidth)
                    {
                        userCar.x = userCar.x + 1;
                    }

                }
            }
            List<Car> newList = new List<Car>();
            for (int i = 0; i < cars.Count; i++)
            {
                Car oldCar = cars[i];
                Car newCarr = new Car();
                newCarr.x = oldCar.x;
                newCarr.y = oldCar.y + 1;
                newCarr.c = oldCar.c;
                newCarr.color = oldCar.color;
                if (newCarr.y == userCar.y && newCarr.x == userCar.x)
                {
                    hits--;
                    hit = true;
                    speed += 50;
                    if (speed > 400)
                    {
                        speed = 400;
                    }
                    if (hits <= 0)
                    {
                        PrintStringOnPosition(8, 10, "Game OVER!!!", ConsoleColor.Red);
                        PrintStringOnPosition(8, 12, "Press [enter]to exit", ConsoleColor.Red);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                if (newCarr.y < Console.WindowHeight)
                {
                    newList.Add(newCarr);
                }

            }
            cars = newList;

            Console.Clear();
            if (hit)
            {
                cars.Clear();
                PrintOnPosition(userCar.x, userCar.y, 'X', ConsoleColor.Red);
            }
            else
            {
                PrintStringOnPosition(8, 4, "Hits:" + hits, ConsoleColor.White);
                PrintStringOnPosition(8, 5, "Speed:" + speed, ConsoleColor.White);
            }

            PrintOnPosition(userCar.x, userCar.y, userCar.c, userCar.color);

            foreach (Car car in cars)
            {
                PrintOnPosition(car.x, car.y, car.c, car.color);
            }

         
            Thread.Sleep(600 - (int)speed);
        }
    }
}

