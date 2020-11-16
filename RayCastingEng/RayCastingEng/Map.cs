using System.Text;

namespace RayCastingEng
{
    enum MapLegend
    {
        wall = '#',
        space = '.',
        player = '@',
    };
    class Map
    {
        private int widht;
        private int height;
        public int Width { get=>widht; }
        public int Height { get => height; }
        public char[] Segments { get; set; }
        private string map = string.Empty;

        public Map(int widht, int heigth)
        {
            this.widht = widht;
            this.height = heigth;
            //map = 
            Segments = new char[this.widht * this.height];
            SetBorderForMap();
        }

        private void SetBorderForMap()
        {
            //StringBuilder sb = new StringBuilder(widht * height);
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < widht-1; ++j)
                {
                    if (i == 0 || i == height-1 || j == 0 || j == widht-2)
                    {
                        //sb.Append((char)MapLegend.wall);
                        Segments[i*widht + j] = (char)MapLegend.wall;
                    }
                    else
                    {
                        //sb.Append((char)MapLegend.space);
                        Segments[i*widht + j] = (char)MapLegend.space;
                    }
                }
                //sb.Append('\n');
                Segments[widht * (i+1)-1] = '\n';
            }
            Segments[119] = (char)MapLegend.wall;
            //return  sb.ToString();
        }

        public override string ToString()
        {
            return map;
        }
    }
}
