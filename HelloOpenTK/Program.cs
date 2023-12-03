using System;
using HelloOpenTK;
using OpenTK;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello ! OpenTK !");

        // This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
        using (MyGameWindow game = new MyGameWindow(800, 600, "LearnOpenTK"))
        {
            game.Run();
        }
    }
}
