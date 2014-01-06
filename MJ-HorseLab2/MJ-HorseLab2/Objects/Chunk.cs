using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

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
        const byte EMPTY = 0;

        private int _xPos;
        private int _zPos;

        Texture2D _stoneTexture;
        Texture2D _dirtTexture;
        Texture2D _grassTexture;

        VertexBuffer _stoneBuffer;
        VertexBuffer _grassBuffer;
        VertexBuffer _dirtBuffer;

        GraphicsDevice _device;

        byte[,,] chunkData;
        List<VertexPositionTexture> stoneVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> grassVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> dirtVertices = new List<VertexPositionTexture>();

        public Chunk(GraphicsDevice device, Texture2D stoneTexture, Texture2D dirtTexture, Texture2D grassTexture, Texture2D map, ReadHue hue, int xPos, int zPos)
        {
            _stoneTexture = stoneTexture;
            _dirtTexture = dirtTexture;
            _grassTexture = grassTexture;
            _device = device;
            _xPos = xPos;
            _zPos = zPos;

            //ReadHue hue = new ReadHue(map);

            //chunkData = hue.chunkData;

            chunkData = hue.GetChunkData(xPos, zPos);

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


        public void Init()
        {

            for (int tempX = 0; tempX < 16; tempX++)
            {
                for (int y = 0; y < chunkHeight; y++)
                {
                    for (int tempZ = 0; tempZ < 16; tempZ++)
                    //for (int z = 0; z < chunkDepth; z++)
                    {
                        int x = tempX + _xPos;
                        int z = tempZ + _zPos;
                        //Debug.WriteLine("X är =" + _xPos + "  Z är då: " + _zPos);
                        switch (chunkData[tempX, y, tempZ])
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
            effect.Texture = _grassTexture;
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
