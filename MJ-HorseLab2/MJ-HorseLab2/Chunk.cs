using System;
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

        Texture2D _stoneTexture;
        Texture2D _dirtTexture;
        Texture2D _grassTexture;
        
        //byte[,,] chunkData = huehuehue;
        List<VertexPositionTexture> stoneVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> grassVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> dirtVertices = new List<VertexPositionTexture>();

        public Chunk(Texture2D stoneTexture, Texture2D dirtTexture, Texture2D grassTexture)
        {
            _stoneTexture = stoneTexture;
            _dirtTexture = dirtTexture;
            _grassTexture = grassTexture;
            Init();


        }

        public void Init()
        {
            for (int x = 0; x < chunkWidth; x++)
                for (int y = 0; y < chunkHeight; y++)
                    for (int z = 0; z < chunkDepth; z++)
                        switch (chunkData[x,y,z])
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
        


        public void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;

            effect.Texture = texture2;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.View;
            effect.Projection = camera.projection;
            effect.CurrentTechnique.Passes[0].Apply();

            device.SetVertexBuffer(buffer);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, buffer.VertexCount / 3);
        }

    }
}
