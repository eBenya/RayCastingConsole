using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RayCastingEng.Render
{
    class Render2D : IRender
    {
        private const double step = 0.5;
        private readonly Map map;
        private Player player;
        public char[] buff;

        private List<Point> possitionsLinePOV;
        public List<List<Point>> PointsOfAllRay { get; private set; }
        private List<Point> whatISeeList;
        public Render2D(Player p, Map m)
        {
            player = p;
            map = m;
            buff = new char[map.Width * map.Height];
            possitionsLinePOV = new List<Point>();
            whatISeeList = new List<Point>();
            PointsOfAllRay = new List<List<Point>>();
        }

        private List<Point> GetAllPointWhatISee()
        {
            whatISeeList.Clear();
            if (PointsOfAllRay != null && PointsOfAllRay.Count>0)
            {
                foreach (var item in PointsOfAllRay)
                {
                    whatISeeList.AddRange(item);
                }
            }
            
            return whatISeeList;
        }

        //TODO: It`s a separate, independent entity
        private List<List<Point>> GetAllPointsOfRay(int xPos, int yPos, double pov, double fov, double playerDepht)
        {
            PointsOfAllRay.Clear();

            //Number of rays - will be the lenght of the arc of the circle.
            int numberOfIteration = (int)(fov * playerDepht);
            double currentAngle = (pov - fov / 2);
            double step = fov / numberOfIteration;

            //Складируем все точки до которых дотянулись лучи
            for (int i = 0; i < numberOfIteration; i++)
            {
                PointsOfAllRay.Add(GetPossitionsLinePOV(xPos, yPos, currentAngle, playerDepht));
                currentAngle += step;
            }

            return PointsOfAllRay;
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
            return possitionsLinePOV.Distinct().ToList();
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
            return line.Distinct().ToList();
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

            #region Experemental

            GetAllPointsOfRay((int)player.X, (int)player.Y, player.POV, player.FOV, player.Depht);
            GetAllPointWhatISee();

            foreach (var item in whatISeeList)
            {
                sb.Replace((char)MapLegend.space, '0', item.Y * map.Width + item.X, 1);
            }

            #endregion

            //Add possition info
            sb.Append($"\nX: {player.X:f4}; Y: {player.Y:f4}; POV: {player.GetPOVInDeegree():f4}");

            Console.SetCursorPosition(0, 0);
            Console.Write(sb);

            //ResetBuff();
        }
    }
}
