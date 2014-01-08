using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MJ_HorseLab2
{
    public class ReadHue
    {
        Texture2D _map;
        Color[,] _colors;
        const int HEIGHT = 32;
        const byte STONE = 1;
        const byte DIRT = 2;
        const byte GRASS = 3;
        const byte EMPTY = 0;
        //public byte[, ,] chunkData;
        private byte[, ,] worldData;
        private byte[, ,] culledWorldData;
        private List<Chunk> chunkList;
        private bool culling;

        Texture2D _stoneTexture;
        Texture2D _dirtTexture;
        Texture2D _grassTexture;
        GraphicsDevice _device;


        //public ReadHue(Texture2D map)
        //{
        //    _map = map;
        //    _colors = Texture2DArray();

        //}

        public ReadHue(Texture2D map, GraphicsDevice device, Texture2D stoneTexture, Texture2D dirtTexture, Texture2D grassTexture)
        {
            culling = true;
            _map = map;
            _colors = Texture2DArray();

            _stoneTexture = stoneTexture;
            _dirtTexture = dirtTexture;
            _grassTexture = grassTexture;
            _device = device;

            InitWorldData();
            //CullWorldData();
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
                    //Debug.WriteLine(height);
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        if (y < height)
                        {
                            if (y < 4)
                                worldData[x, y, z] = STONE;
                            else if (y < 8)
                                worldData[x, y, z] = DIRT;
                            else if (y < 33)
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

            for (int x = 0; x < 16; x++)
            {
                zPosition = 0;
                for (int z = 0; z < 16; z++)
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
                        //if culling is chosen used culledWorldData
                        //else used worldData
                        chunkData[x, y, z] = worldData[(byte)xPos + x, y, (byte)zPos + z];
                        //chunkData[x, y, z] = culledWorldData[(byte)xPos, y, (byte)zPos];
                    }

                }

            }

            return culling ? GetCulledChunkData(chunkData) : chunkData;



        }


        public byte[, ,] GetCulledChunkData(byte[, ,] chunkData)
        {
            byte[, ,] culledChunkData = new byte[16, HEIGHT, 16];

            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        if (x == 0 || z == 0 || y == 0 || x == 15 || z == 15 || y == 31)
                        {
                            culledChunkData[x, y, z] = chunkData[x, y, z];
                        }
                        else if ((chunkData[x + 1, y, z] != EMPTY) && (chunkData[x - 1, y, z] != EMPTY) &&
                                 (chunkData[x, y, z + 1] != EMPTY) && (chunkData[x, y, z - 1] != EMPTY) &&
                                 (chunkData[x, y + 1, z] != EMPTY) && (chunkData[x, y - 1, z] != EMPTY) &&
                                 (chunkData[x + 1, y, z] != 0) && (chunkData[x - 1, y, z] != 0) &&
                                 (chunkData[x, y, z + 1] != 0) && (chunkData[x, y, z - 1] != 0) &&
                                 (chunkData[x, y + 1, z] != 0) && (chunkData[x, y - 1, z] != 0))
                        {
                            culledChunkData[x, y, z] = EMPTY;
                        }
                        else
                        {
                            culledChunkData[x, y, z] = chunkData[x, y, z];
                        }

                    }
                }
            }

            return culledChunkData;
        }




        private void CreateBoundingBoxes()
        {
            byte[, ,] chunkData = new byte[16, HEIGHT, 16];

            for (int x = 0; x < _map.Width; x++)
            {
                for (int z = 0; z < _map.Height; z++)
                {
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        if (y != HEIGHT - 1 && worldData[x, y + 1, z] == EMPTY) //och även om den är 0???
                        {
                            //create new bounding box for that voxel
                            break;
                        }
                    }
                }
            }

        }


        private Color[,] Texture2DArray()
        {
            Color[] colors = new Color[_map.Width * _map.Height];
            _map.GetData(colors);
            Color[,] colors2D = new Color[_map.Width, _map.Height];

            for (int x = 0; x < _map.Width; x++)
                for (int y = 0; y < _map.Height; y++)
                    colors2D[x, y] = colors[x + (y * _map.Width)];
            return colors2D;
        }

        private byte GetHeight(Color c1)
        {
            return (byte)(RGB2HSL(c1).h / 12);
        }

        public byte GetYPosition(float x, float z)
        {
            byte xPos = (byte)x;
            byte zPos = (byte)z;

            for (byte y = 0; y <= 31; y++)
            {
                if (worldData[xPos, y, zPos] == 0)
                {
                    return y;
                }
            }
            return 0;
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
