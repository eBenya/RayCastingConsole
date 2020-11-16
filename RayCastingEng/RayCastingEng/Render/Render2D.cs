using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RayCastingEng.Render
{
    class Render2D
    {
        private const double step = 0.5;
        private readonly Map map;
        private Player player;
        public char[] buff;
        private List<Point> PossitionsLinePOV;
        public Render2D(Player p, Map m)
        {
            player = p;
            map = m;
            buff = new char[map.Width * map.Height];
            PossitionsLinePOV = new List<Point>();
        }
        //TODO: Rename this method as GetLineOfSightCoord. And create iterative method that will remember FOV possitions
        private List<Point> GetPossitionsLinePOV()
        {
            PossitionsLinePOV.Clear();

            Point point = new Point((int)player.X, (int)player.Y);
            double dist = 0;
            while (point.X != 0 && point.X != map.Width - 1 &&
                  point.Y != 0 && point.Y != map.Height -1 &&
                  dist != player.Depht)
            {
                dist += step;
                point.X = (int)(player.X + Math.Sin(player.POV) * dist);
                point.Y = (int)(player.Y + Math.Cos(player.POV) * dist);
                if (map.Segments[GetIndexArray(point.X, point.Y)] != (char)MapLegend.wall)
                {
                    PossitionsLinePOV.Add(point);
                }
                else
                {
                    break;
                }
            }
            return PossitionsLinePOV;
        }
        private int GetIndexArray(int x, int y)
        {
            return y * map.Width + x;
        }
        private void ResetBuff()
        {
            for (int i = 0; i < map.Height; ++i)
            {
                for (int j = 0; j < map.Width ; ++j)
                {
                    buff[i*j + j] = map.Segments[i*j + j];
                }
            }
        }
        public void Show()
        {
            GetPossitionsLinePOV();
            StringBuilder sb = new StringBuilder(map.Width * map.Height);

            sb.Insert(0, map.Segments);
            //sb.Append(map.Segments);

            sb.Replace((char)MapLegend.space, (char)MapLegend.player,
                (int)player.Y * map.Width + (int)player.X, 1);

            foreach (var item in PossitionsLinePOV)
            {
                sb.Replace((char)MapLegend.space, '0', item.Y * map.Width + item.X, 1);
            }

            //Add possition info
            sb.Append($"\nX: {player.X:f4}; Y: {player.Y:f4}; POV: {player.GetDeegreePOV():f4}");

            Console.SetCursorPosition(0, 0);
            Console.Write(sb);

            //ResetBuff();
        }
    }
}
