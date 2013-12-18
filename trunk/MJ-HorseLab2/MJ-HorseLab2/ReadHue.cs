using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna;
using Microsoft.Xna.Framework.Graphics;

namespace MJ_HorseLab2
{
    class ReadHue
    {
        Texture2D _map;
        Color[,] colors;
        const int HEIGHT = 32;
        const byte STONE = 1;
        const byte DIRT = 2;
        const byte GRASS = 3;

        public ReadHue(Texture2D map)
        {
            _map = map;
            colors = Texture2DArray();
            byte[,,] chunkData = GetChunkData();
        }

        public byte[,,] GetChunkData()
        {
            //byte[,,] chunkData = new byte[_map.Height,_map.Width,_map.Height*_map.Width];
            byte[,,] chunkData = new byte[_map.Width,_map.Height, HEIGHT];
            byte height;
            for (int x = 0; x < _map.Width; x++){
                for (int y = 0; y < _map.Height; y++){
                    height = GetHeight(colors[x, y]);
                    //Console.WriteLine(height);
                    for (int z = 0; z < HEIGHT; z++)
                    {
                        if (z < height)
                        {
                            if (z < 4)
                                chunkData[x, y, z] = STONE;
                            else if (z < 8)
                                chunkData[x, y, z] = DIRT;
                            else if (z < 32)
                                chunkData[x, y, z] = GRASS;
                        }
                    }

                }
            }

            return chunkData;
        }

        public Color[,] Texture2DArray()
        {
            Color[] colors = new Color[_map.Width * _map.Height];
            _map.GetData(colors);
            Color[,] colors2D = new Color[_map.Width, _map.Height];

            for (int x = 0; x < _map.Width; x++)
                for (int y = 0; y < _map.Height; y++)
                    colors2D[x, y] = colors[x + y * _map.Width];
            return colors2D;
        }

        private byte GetHeight(Color c1)
        {
            return (byte)(RGB2HSL(c1).h / 12);
        }

        struct HSL
        {
            public double h, s, l;
        }

        private HSL RGB2HSL(Color c1)
        {
            double themin, themax, delta;
            HSL c2;
            themin = Math.Min(c1.R, Math.Min(c1.G, c1.B));
            themax = Math.Max(c1.R, Math.Max(c1.G, c1.B));
            delta = themax - themin;
            c2.l = (themin + themax) / 2;
            c2.s = 0;
            if (c2.l > 0 && c2.l < 1)
                c2.s = delta / (c2.l < 0.5 ? (2 * c2.l) : (2 - 2 * c2.l));
            c2.h = 0;
            if (delta > 0)
            {
                if (themax == c1.R && themax != c1.G)
                    c2.h += (c1.G - c1.B) / delta;
                if (themax == c1.G && themax != c1.B)
                    c2.h += (2 + (c1.B - c1.R) / delta);
                if (themax == c1.B && themax != c1.R)
                    c2.h += (4 + (c1.R - c1.G) / delta);
                c2.h *= 60;
            }
            return (c2);
        }

    }
}
