using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    /// <summary>
    /// Game settings
    /// </summary>
    class GameSettings
    {
        public static int startMoney = 1000;
        public static int defaultBet = 100;
        public static int prizeFactor = 2;
        public static int xOrigin = 2;
        public static int yOrigin = 1;
        public static int cardDistance = 12;
        public static int cardOffset = 6;
        public static int maxComputerPoints = 17;
        public static string[] gameMenu = { "Взять карту", "Себе" };
        public static string[] mainMenu = { "Продолжить", "Выход" };
    }

    /// <summary>
    /// Card data
    /// </summary>
    class CardData
    {
        public static int[] point = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 2, 3, 4 };
        public static string[] face = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        public static string[] suit = { "\x3", "\x4", "\x5", "\x6" };
        public static string redSuite = "\x3\x4";
        public static string frameSymbols = "╔═╗╚╝║";
        public static string shirt = "░";
        public static string[] cardImages = {
            "╔═════════╗\n║f        ║\n║         ║\n║         ║\n║         ║\n║    x    ║\n║         ║\n║         ║\n║         ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║    x    ║\n║         ║\n║         ║\n║         ║\n║         ║\n║         ║\n║    x    ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║    x    ║\n║         ║\n║         ║\n║    x    ║\n║         ║\n║         ║\n║    x    ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║ x     x ║\n║         ║\n║         ║\n║         ║\n║         ║\n║         ║\n║ x     x ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║ x     x ║\n║         ║\n║         ║\n║    x    ║\n║         ║\n║         ║\n║ x     x ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║ x     x ║\n║         ║\n║         ║\n║ x     x ║\n║         ║\n║         ║\n║ x     x ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║ x     x ║\n║    x    ║\n║         ║\n║ x     x ║\n║         ║\n║         ║\n║ x     x ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║ x     x ║\n║    x    ║\n║         ║\n║ x     x ║\n║         ║\n║    x    ║\n║ x     x ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f        ║\n║ x     x ║\n║         ║\n║ x     x ║\n║    x    ║\n║ x    x  ║\n║         ║\n║ x     x ║\n║        f║\n╚═════════╝\n",
            "╔═════════╗\n║f       ║\n║ x     x ║\n║    x    ║\n║ x     x ║\n║         ║\n║ x    x  ║\n║    x    ║\n║ x     x ║\n║       f║\n╚═════════╝\n",
            "╔═════════╗\n║fx       ║\n║         ║\n║         ║\n║         ║\n║    f    ║\n║         ║\n║         ║\n║         ║\n║       fx║\n╚═════════╝\n",
            "╔═════════╗\n║fx       ║\n║         ║\n║         ║\n║         ║\n║    f    ║\n║         ║\n║         ║\n║         ║\n║       fx║\n╚═════════╝\n",
            "╔═════════╗\n║fx       ║\n║         ║\n║         ║\n║         ║\n║    f    ║\n║         ║\n║         ║\n║         ║\n║       fx║\n╚═════════╝\n",
        };
    }
}
