using RayCastingEng.Render;
using System;

namespace RayCastingEng
{
    class RayCasting
    {
        public const int cScreenWidth = 100;
        public const int cScreenHeight = 50;
        public static readonly char[] screen = new char[cScreenWidth * cScreenHeight];

        internal Map map;
        internal Player player;
        internal bool tooglRender;

        private IRender render2D;

        public RayCasting(int mapWidth = 32, int mapHeiht = 32, 
            double playerX = 2.0, double playerY = 2.0)
        {
            this.map = new Map(mapWidth, mapHeiht);
            this.player = new Player(playerX, playerY);
            this.tooglRender = true;
            this.render2D = new Render2D(this.player, this.map);
            
            Console.SetWindowSize(cScreenWidth, cScreenHeight);
            Console.SetBufferSize(cScreenWidth, cScreenHeight);
            Console.CursorVisible = false;
        }
        public void Render()
        {
            render2D.Show();
        }
    }
}