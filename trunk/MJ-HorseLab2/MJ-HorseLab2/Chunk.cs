﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MJ_HorseLab2
{
    class Chunk
    {
        const int chunkWidth = 16;
        const int chunkDepth = 16;
        const int chunkHeight = 32;

        const byte STONE = 1;
        const byte DIRT = 2;
        const byte GRASS = 3;

        Texture2D stoneTexture;
        Texture2D dirtTexture;
        Texture2D grassTexture;

        VertexBuffer _stoneBuffer;
        VertexBuffer _grassBuffer;
        VertexBuffer _dirtBuffer;

        GraphicsDevice device;
        
        byte[,,] chunkData;
        List<VertexPositionTexture> stoneVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> grassVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> dirtVertices = new List<VertexPositionTexture>();

        public Chunk( GraphicsDevice device, Texture2D stoneTexture, Texture2D dirtTexture, Texture2D grassTexture, Texture2D map)
        {
            this.stoneTexture = stoneTexture;
            this.dirtTexture = dirtTexture;
            this.grassTexture = grassTexture;
            this.device = device;

            ReadHue hue = new ReadHue(map);

            chunkData = hue.chunkData;

            Init();

            _stoneBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, stoneVertices.Count, BufferUsage.WriteOnly);
            _stoneBuffer.SetData<VertexPositionTexture>(stoneVertices.ToArray());
            
            _dirtBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, dirtVertices.Count, BufferUsage.WriteOnly);
            _dirtBuffer.SetData<VertexPositionTexture>(dirtVertices.ToArray());
            
            _grassBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, grassVertices.Count, BufferUsage.WriteOnly);
            _grassBuffer.SetData<VertexPositionTexture>(grassVertices.ToArray());
        }



        Vector3[] LEFT_FACE_POS = { new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 1), new Vector3(1, 1, 1) };
        Vector3[] RIGHT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 1) };
        Vector3[] TOP_FACE_POS = { new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
        Vector3[] BOTTOM_FACE_POS = { new Vector3(1, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 1), new Vector3(0, 0, 1) };
        Vector3[] BACK_FACE_POS = { new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
        Vector3[] FRONT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0) };

        Vector2[] LEFT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] TOP_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] BOTTOM_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] BACK_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] RIGHT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] FRONT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        /*
        private byte[,,] GetChunkData()
        {
            //byte[,,] chunkData = new byte[_map.Height,_map.Width,_map.Height*_map.Width];
            byte[,,] chunkData = new byte[16,16,32];
            byte height;
            for (int x = 0; x < 16; x++){
                for (int y = 0; y < 16; y++){
                    height = (byte) (3+ x + y);
                    //Console.WriteLine(height);
                    for (int z = 0; z < 32; z++)
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
        */

        public void Init()
        {
            for (int x = 0; x < chunkWidth; x++)
            {
                for (int y = 0; y < chunkHeight; y++)
                {
                    for (int z = 0; z < chunkDepth; z++)
                    {
                        switch (chunkData[x, y, z])
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

                        }
                    }
                }
            }

        }
        


        public void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            DrawStone(camera, effect);
            DrawDirt(camera, effect);
            DrawGrass(camera, effect);
        }

        private void DrawStone(Camera camera, BasicEffect effect)
        {
            effect.Texture = stoneTexture;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.View;
            effect.Projection = camera.projection;
            effect.CurrentTechnique.Passes[0].Apply();

            device.SetVertexBuffer(_stoneBuffer);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, _stoneBuffer.VertexCount / 3);
        }
        
        private void DrawDirt(Camera camera, BasicEffect effect)
        {
            effect.Texture = dirtTexture;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.View;
            effect.Projection = camera.projection;
            effect.CurrentTechnique.Passes[0].Apply();

            device.SetVertexBuffer(_dirtBuffer);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, _dirtBuffer.VertexCount / 3);
        }
        private void DrawGrass(Camera camera, BasicEffect effect)
        {
            effect.Texture = grassTexture;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.View;
            effect.Projection = camera.projection;
            effect.CurrentTechnique.Passes[0].Apply();

            device.SetVertexBuffer(_grassBuffer);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, _grassBuffer.VertexCount / 3);
        }
    }
      
}
