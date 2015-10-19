using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Game21
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(866);
            Game game = new Game();
            game.play();
        }
    }
}