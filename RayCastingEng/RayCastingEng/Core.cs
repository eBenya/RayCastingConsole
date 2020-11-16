using System;
using System.Collections.Generic;
using System.Text;

namespace RayCastingEng
{
    static class Core
    {
        public static void Start()
        {
            RayCasting rc = new RayCasting();
            Map map = new Map(64, 32);

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.A:
                        rc.player.Rotation(1);
                        break;
                    case ConsoleKey.D:
                        rc.player.Rotation(-1);
                        break;
                    case ConsoleKey.W:
                        rc.player.Move(1);
                        break;
                    case ConsoleKey.S:
                        rc.player.Move(-1);
                        break;
                }
                
                rc.Render();
                
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
