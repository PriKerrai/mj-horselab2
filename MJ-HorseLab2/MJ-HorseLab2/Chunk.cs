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

        int chunkWidth = 16;
        int chunkDepth = 16;
        int chunkHeight = 32;

        const byte STONE = 0;
        const byte DIRT = 1;
        const byte GRASS = 2;

        //byte[][][] chunkData = huehuehue;
        List<VertexPositionTexture> stoneVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> grassVertices = new List<VertexPositionTexture>();
        List<VertexPositionTexture> dirtVertices = new List<VertexPositionTexture>();


        /*
        public void Init()
        {
            for (int x = 0; x < chunkWidth; x++)
                for (int y = 0; y < chunkHeight; y++)
                    for (int z = 0; z < chunkDepth; z++)
                        switch (chunkData[x][y][z])
                        {
                            case STONE:
                                break;

                            case DIRT:
                                break;

                            case GRASS:
                                break;

                        }
        }
        */
    }
}
