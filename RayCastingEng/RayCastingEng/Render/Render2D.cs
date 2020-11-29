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
        private List<Point> possitionsLinePOV;
        private List<Point> whatISeeList;
        public Render2D(Player p, Map m)
        {
            player = p;
            map = m;
            buff = new char[map.Width * map.Height];
            possitionsLinePOV = new List<Point>();
            whatISeeList = new List<Point>();
        }
        //TODO: Rename this method as GetLineOfSightCoord. And create iterative method that will remember FOV possitions
        private List<Point> GetAllPointWhatISee()
        {
            whatISeeList.Clear();
            double segment = player.FOV / 2;
            foreach (var item in possitionsLinePOV)
            {
                whatISeeList.Add(item);
            }

            return whatISeeList;
        }

        private List<Point> GetPossitionsLinePOV()
        {
            possitionsLinePOV.Clear();

            Point point = new Point((int)player.X, (int)player.Y);
            double dist = 0;
            while (dist < player.Depht)
            {
                dist += step;
                point.X = (int)(player.X + Math.Sin(player.POV) * dist);
                point.Y = (int)(player.Y + Math.Cos(player.POV) * dist);
                if (map.Segments[GetIndexArray(point.X, point.Y)] != (char)MapLegend.wall)
                {
                    possitionsLinePOV.Add(point);
                }
                else
                {
                    break;
                }
            }
            return possitionsLinePOV;
        }
        private List<Point> GetPossitionsLinePOV(int x, int y, double pov, double playerDepht)
        {
            List<Point> line = new List<Point>();

            //Point point = new Point((int)player.X, (int)player.Y);
            double dist = 0;
            while (dist < playerDepht)
            {
                dist += step;
                x = (int)(player.X + Math.Sin(pov) * dist);
                y = (int)(player.Y + Math.Cos(pov) * dist);
                if (map.Segments[GetIndexArray(x, y)] != (char)MapLegend.wall)
                {
                    line.Add(new Point(x,y));
                }
                else
                {
                    break;
                }
            }
            return line;
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
            possitionsLinePOV = GetPossitionsLinePOV((int)player.X, (int)player.Y, player.POV, player.Depht);
            StringBuilder sb = new StringBuilder(map.Width * map.Height);

            sb.Insert(0, map.Segments);
            //sb.Append(map.Segments);

            sb.Replace((char)MapLegend.space, (char)MapLegend.player,
                (int)player.Y * map.Width + (int)player.X, 1);

            foreach (var item in possitionsLinePOV)
            {
                sb.Replace((char)MapLegend.space, '0', item.Y * map.Width + item.X, 1);
            }

            //Add possition info
            sb.Append($"\nX: {player.X:f4}; Y: {player.Y:f4}; POV: {player.GetPOVInDeegree():f4}");

            Console.SetCursorPosition(0, 0);
            Console.Write(sb);

            //ResetBuff();
        }
    }
}
