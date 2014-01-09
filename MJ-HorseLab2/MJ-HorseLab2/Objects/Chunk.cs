using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using MJ_HorseLab2.Models;

namespace MJ_HorseLab2
{
    public class Chunk
    {
        const int chunkWidth = 16;
        const int chunkDepth = 16;
        const int chunkHeight = 32;

        static float x = 1;

        const byte STONE = 1;
        const byte DIRT = 2;
        const byte GRASS = 3;
        const byte EMPTY = 0;

        private int _xPos;
        private int _zPos;
        private int nrChunksX;
        private int nrChunksY;
        private int nrChunksZ;
        private bool isSetVoxelSmal;

        Texture2D _stoneTexture;
        Texture2D _dirtTexture;
        Texture2D _grassTexture;
        Texture2D _dynamicTexture;

        public VertexBuffer _stoneBuffer;
        public VertexBuffer _grassBuffer;
        public VertexBuffer _dirtBuffer;


        GraphicsDevice _device;

        byte[, ,] chunkData;
        ushort[, ,] doubleChunkData;
        List<VertexPositionTexture> stoneVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> grassVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> dirtVertices = new List<VertexPositionTexture>();

        Tank _tank;

        public Chunk(GraphicsDevice device, Texture2D[] texture, Texture2D map, ReadHue hue, int xPos, int zPos, Tank tank)
        {
            _stoneTexture = texture[0];
            _dirtTexture = texture[1];
            _grassTexture = texture[2];
            _dynamicTexture = texture[3];
            _device = device;
            _xPos = xPos;
            _zPos = zPos;
            _tank = tank;
            isSetVoxelSmal = false;
            nrChunksX = 16;
            nrChunksY = 32;
            nrChunksZ = 16;
            //x = 1f;
            chunkData = hue.GetChunkData(xPos, zPos);
            ConvertToDoubleVoxelsInChunk();
            isSetVoxelSmal = tank.isVoxelSmall;


                Init();
            

            if (stoneVertices.Count > 0)
            {
                _stoneBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, stoneVertices.Count, BufferUsage.WriteOnly);
                _stoneBuffer.SetData<VertexPositionTexture>(stoneVertices.ToArray());
            }
            if (dirtVertices.Count > 0)
            {
                _dirtBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, dirtVertices.Count, BufferUsage.WriteOnly);
                _dirtBuffer.SetData<VertexPositionTexture>(dirtVertices.ToArray());
            }
            if (grassVertices.Count > 0)
            {
                _grassBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, grassVertices.Count, BufferUsage.WriteOnly);
                _grassBuffer.SetData<VertexPositionTexture>(grassVertices.ToArray());
            }
        }

        private void setVoxelSmall()
        {
            if (_tank.isVoxelSmall)
            {
                nrChunksX = 32;
                nrChunksY = 64;
                nrChunksZ = 32;
                x = 0.5f;
                Init();
                
            }
            else
            {
                nrChunksX = 16;
                nrChunksY = 32;
                nrChunksZ = 16;
                x = 1f;
            }
            
        }


        private void ConvertToDoubleVoxelsInChunk()
        {
            doubleChunkData = new ushort[32, 64, 32];

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        //for (int i = 0; i < 2; i++)
                        //{
                            doubleChunkData[x,y,z] = chunkData[x, y, z];
                        //}
                    }
                }
            }
        }

        Vector3[] LEFT_FACE_POS = { new Vector3(x, 0, 0), new Vector3(x, x, 0), new Vector3(x, 0, x), new Vector3(x, x, x) };
        Vector3[] RIGHT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(0, x, 0), new Vector3(0, 0, x), new Vector3(0, x, x) };
        Vector3[] TOP_FACE_POS = { new Vector3(0, x, 0), new Vector3(x, x, 0), new Vector3(x, x, x), new Vector3(0, x, x) };
        Vector3[] BOTTOM_FACE_POS = { new Vector3(x, 0, 0), new Vector3(0, 0, 0), new Vector3(x, 0, x), new Vector3(0, 0, x) };
        Vector3[] BACK_FACE_POS = { new Vector3(0, 0, x), new Vector3(x, 0, x), new Vector3(x, x, x), new Vector3(0, x, x) };
        Vector3[] FRONT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(x, 0, 0), new Vector3(x, x, 0), new Vector3(0, x, 0) };

        Vector2[] LEFT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] TOP_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] BOTTOM_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] BACK_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] RIGHT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] FRONT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };


        public void Init()
        {
            float offsetX = 0;
            float offsetZ = 0;
            float offsetY = 0;
            for (float tempX = 0; tempX < nrChunksX; tempX++)
            {
                for (float tempY = 0; tempY < nrChunksY; tempY++)
                {
                    for (float tempZ = 0; tempZ < nrChunksZ; tempZ++)
                    {
                        if (isSetVoxelSmal)
                        {
                            offsetX = (tempX % 2) == 0 ? (float)0.5 : 0;
                            offsetZ = (tempZ % 2) == 0 ? (float)0.5 : 0;
                            offsetY = (tempY % 2) == 0 ? (float)0.5 : 0;
                        }
                        float x = tempX + _xPos + offsetX;
                        float z = tempZ + _zPos + offsetZ;
                        float y = tempY + offsetY;
                        switch (chunkData[(int)tempX, (int)tempY, (int)tempZ])
                        {
                            case STONE:
                                #region StoneVertices
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[0], LEFT_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[3], LEFT_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[0]));

                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[0], RIGHT_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[3], RIGHT_FACE_TEXCOORD[3]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));

                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[1], BACK_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[3], BACK_FACE_TEXCOORD[3]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));

                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[1], FRONT_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[3]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[3], FRONT_FACE_TEXCOORD[0]));

                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[1], TOP_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[3], TOP_FACE_TEXCOORD[3]));

                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[0], BOTTOM_FACE_TEXCOORD[0]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[3], BOTTOM_FACE_TEXCOORD[3]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
                                stoneVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));

                                #endregion
                                break;

                            case DIRT:
                                #region DirtVertices
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[0], LEFT_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[3], LEFT_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[0]));

                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[0], RIGHT_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[3], RIGHT_FACE_TEXCOORD[3]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));

                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[1], BACK_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[3], BACK_FACE_TEXCOORD[3]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));

                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[1], FRONT_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[3]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[3], FRONT_FACE_TEXCOORD[0]));

                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[1], TOP_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[3], TOP_FACE_TEXCOORD[3]));

                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[0], BOTTOM_FACE_TEXCOORD[0]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[3], BOTTOM_FACE_TEXCOORD[3]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
                                dirtVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));
                                #endregion
                                break;

                            case GRASS:
                                #region GrassVertices
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[0], LEFT_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[3], LEFT_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[0]));

                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[0], RIGHT_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[3], RIGHT_FACE_TEXCOORD[3]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));

                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[1], BACK_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[3], BACK_FACE_TEXCOORD[3]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));

                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[1], FRONT_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[3]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + FRONT_FACE_POS[3], FRONT_FACE_TEXCOORD[0]));

                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[1], TOP_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + TOP_FACE_POS[3], TOP_FACE_TEXCOORD[3]));

                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[0], BOTTOM_FACE_TEXCOORD[0]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[3], BOTTOM_FACE_TEXCOORD[3]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
                                grassVertices.Add(new VertexPositionTexture(new Vector3(x, y, z) + BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));
                                #endregion
                                break;
                            case EMPTY:
                                break;

                        }
                    }
                }
            }

        }


        public void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            if (stoneVertices.Count > 0)
            {
                DrawStone(camera, effect);
            }
            if (dirtVertices.Count > 0)
            {
                DrawDirt(camera, effect);
            }
            if (grassVertices.Count > 0)
            {
                DrawGrass(camera, effect);
            }
        }

        private void DrawStone(Camera camera, BasicEffect effect)
        {
            effect.Texture = _stoneTexture;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;
            effect.CurrentTechnique.Passes[0].Apply();

            _device.SetVertexBuffer(_stoneBuffer);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, _stoneBuffer.VertexCount / 3);
        }

        private void DrawDirt(Camera camera, BasicEffect effect)
        {
            effect.Texture = _dirtTexture;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;
            effect.CurrentTechnique.Passes[0].Apply();

            _device.SetVertexBuffer(_dirtBuffer);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, _dirtBuffer.VertexCount / 3);
        }
        private void DrawGrass(Camera camera, BasicEffect effect)
        {
            if(_tank.Position.Y > 10) 
            {
                 effect.Texture = _dynamicTexture;
            }
            else
            {
                effect.Texture = _grassTexture;
            }
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;
            effect.CurrentTechnique.Passes[0].Apply();

            _device.SetVertexBuffer(_grassBuffer);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, _grassBuffer.VertexCount / 3);
        }
    }

}
