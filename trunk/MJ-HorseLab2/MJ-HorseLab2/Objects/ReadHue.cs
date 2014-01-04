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
        Color[,] _colors;
        const int HEIGHT = 32;
        const byte STONE = 1;
        const byte DIRT = 2;
        const byte GRASS = 3;
        const byte EMPTY = 4;
        //public byte[, ,] chunkData;
        private byte[, ,] worldData;
        private List<Chunk> chunkList;

        Texture2D _stoneTexture;
        Texture2D _dirtTexture;
        Texture2D _grassTexture;
        GraphicsDevice _device;

        public List<Tuple<int, int>> spatialData;

        //public ReadHue(Texture2D map)
        //{
        //    _map = map;
        //    _colors = Texture2DArray();

        //}

        public ReadHue(Texture2D map, GraphicsDevice device, Texture2D stoneTexture, Texture2D dirtTexture, Texture2D grassTexture)
        {
            _map = map;
            _colors = Texture2DArray();

            _stoneTexture = stoneTexture;
            _dirtTexture = dirtTexture;
            _grassTexture = grassTexture;
            _device = device;

            InitWorldData();
            CreateChunks();
        }

        struct HSL
        {
            public double h, s, l;
        }

        public List<Chunk> ChunkList
        {
            get { return chunkList; }
        }

        private void InitWorldData()
        {
            worldData = new byte[_map.Width, HEIGHT, _map.Height];
            byte height;
            for (int x = 0; x < _map.Width; x++)
            {
                for (int z = 0; z < _map.Height; z++)
                {
                    height = GetHeight(_colors[x, z]);
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        if (y < height)
                        {
                            if (y < 4)
                                worldData[x, y, z] = STONE;
                            else if (y < 8)
                                worldData[x, y, z] = DIRT;
                            else if (y < 12)
                                worldData[x, y, z] = GRASS;
                            else
                                worldData[x, y, z] = EMPTY;
                        }
                    }

                }
            }
        }

        private void CreateChunks()
        {
            chunkList = new List<Chunk>();
            int xPosition = 0;
            int zPosition = 0;

            for(int x = 0; x < 4; x++ )
            {
                zPosition = 0;
                for(int z = 0; z < 4; z++ )
                {
                    Chunk chunk = new Chunk(_device, _stoneTexture, _dirtTexture, _grassTexture, _map, this, xPosition, zPosition);
                    chunkList.Add(chunk);
                    zPosition += 16;
                }
                xPosition += 16;
            }
        }



        public byte[, ,] GetChunkData(int xPos, int zPos)
        {
            byte[, ,] chunkData = new byte[16, HEIGHT, 16];

            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        chunkData[x, y, z] = worldData[(byte)xPos, y, (byte)zPos];
                    }
                    zPos++;
                }
                xPos++;
            }

            return chunkData;

            //int chunksX = 16;
            //int chunksZ = 16;

            //int ChunkX = _map.Width/chunksX;
            //int ChunkZ = _map.Height/chunksZ;

            //spatialData = new List<Tuple<int, int>>();

            //for (int x = 0; x < ChunkX; x++)
            //{
            //    for (int z = 0; z < ChunkZ; z++)
            //    {
            //        List<Tuple<int, int, int>> heightData = new List<Tuple<int, int, int>>();
                    
            //        for (int tempX = 0; tempX < chunksX; tempX++)
            //        {
            //            for (int tempZ = 0; tempZ < chunksZ; tempZ++)
            //            {
            //                int mapX = (x * chunksX + tempX);
            //                int mapZ = (ushort)(z * chunksZ + tempZ);

            //                byte height = GetHeight(_colors[mapX, mapZ]);
                            
            //                heightData.Add(new Tuple<int, int, int>(tempX, height, tempZ));
            //             }
            //        }
            //        spatialData.Add(new Tuple<int, int>(x, z));
            //    }
            //}

            //byte[,,] chunkData = new byte[_map.Height,_map.Width,_map.Height*_map.Width];
            //byte[, ,] chunkData = new byte[_map.Width, HEIGHT, _map.Height];
            //byte height;
            //for (int x = 0; x < _map.Width; x++)
            //{
            //    for (int z = 0; z < _map.Height; z++)
            //    {
            //        height = GetHeight(_colors[x, z]);
            //        for (int y = 0; y < HEIGHT; y++)
            //        {
            //            if (y < height)
            //            {
            //                if (y < 4)
            //                    chunkData[x, y, z] = STONE;
            //                else if (y < 8)
            //                    chunkData[x, y, z] = DIRT;
            //                else if (y < 12)
            //                    chunkData[x, y, z] = GRASS;
            //                else
            //                    chunkData[x, y, z] = EMPTY;
            //            }
            //        }

            //    }
            //}

            //return chunkData;
            
        }

        private Color[,] Texture2DArray()
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
