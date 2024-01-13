using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenWidth = Console.WindowWidth;
        int screenHeight = Console.WindowHeight;
        Random randomNummer = new Random();
        string movement = "RIGHT";
        int score = 0;

        Pixel hoofd = new Pixel();
        hoofd.xPos = screenWidth / 2;
        hoofd.yPos = screenHeight / 2;
        hoofd.schermKleur = ConsoleColor.Green;

        List<int> teljePositieX = new List<int>();
        List<int> teljePositieY = new List<int>();

        DateTime tijd = DateTime.Now;
        string obstacle = "*";
        int obstacleXpos = randomNummer.Next(1, screenWidth - 1);
        int obstacleYpos = randomNummer.Next(1, screenHeight - 1);

        while (true)
        {
            Console.Clear();
            // Draw Obstacle
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXpos, obstacleYpos);
            Console.Write(obstacle);

            // Draw Snake
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);
            Console.Write("■");

            // Draw Walls
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Score: " + score);

            // Game Logic
            ConsoleKeyInfo info = Console.ReadKey();

            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    movement = "UP";
                    break;
                case ConsoleKey.DownArrow:
                    movement = "DOWN";
                    break;
                case ConsoleKey.LeftArrow:
                    movement = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    movement = "RIGHT";
                    break;
            }

            // Update head position based on movement
            if (movement == "UP" && hoofd.yPos > 0)
                hoofd.yPos--;
            if (movement == "DOWN" && hoofd.yPos < screenHeight - 1)
                hoofd.yPos++;
            if (movement == "LEFT" && hoofd.xPos > 0)
                hoofd.xPos--;
            if (movement == "RIGHT" && hoofd.xPos < screenWidth - 1)
                hoofd.xPos++;

            // Obstacle collision
            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomNummer.Next(1, screenWidth - 1);
                obstacleYpos = randomNummer.Next(1, screenHeight - 1);
            }

            teljePositieX.Insert(0, hoofd.xPos);
            teljePositieY.Insert(0, hoofd.yPos);

            // Wall or self-collision
            if (hoofd.xPos == 0 || hoofd.xPos == screenWidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenHeight - 1)
            {
                GameOver(score, screenWidth, screenHeight);
            }

            for (int i = 1; i < teljePositieX.Count; i++)
            {
                if (hoofd.xPos == teljePositieX[i] && hoofd.yPos == teljePositieY[i])
                {
                    GameOver(score, screenWidth, screenHeight);
                }
            }

            Thread.Sleep(50);
        }
    }

    static void GameOver(int score, int screenWidth, int screenHeight)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
        Console.WriteLine("Game Over");
        Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        Console.WriteLine("Dein Score ist: " + score);
        Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 2);
        Environment.Exit(0);
    }
}

public class Pixel
{
    public int xPos { get; set; }
    public int yPos { get; set; }
    public ConsoleColor schermKleur { get; set; }
    public string karacter { get; set; }
}
