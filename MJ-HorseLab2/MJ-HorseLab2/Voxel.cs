using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MJ_HorseLab2
{
    class Voxel
    {
        private List<VertexPositionTexture> vertices = new List<VertexPositionTexture>();
        private Vector3 location;
        private Texture2D texture, texture2;
        private GraphicsDevice device;
        private VertexBuffer voxelBuffer;
        BasicEffect effect;

        public Voxel(GraphicsDevice device, Texture2D texture, Texture2D texture2, BasicEffect effect)
        {
            this.device = device;
            this.texture = texture;
            this.texture2 = texture2;
            this.effect = effect;
            SetUpVertices();
        }

        private void SetUpVertices()
        {
            BuildFace(new Vector3(0, 0, 0), new Vector3(0, 1, 1));
            BuildFace(new Vector3(0, 0, 1), new Vector3(1, 1, 1));
            BuildFace(new Vector3(1, 0, 1), new Vector3(1, 1, 0));
            BuildFace(new Vector3(1, 0, 0), new Vector3(0, 1, 0));
            
            voxelBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, vertices.Count, BufferUsage.WriteOnly);
            voxelBuffer.SetData<VertexPositionTexture>(vertices.ToArray());
        }

        private void BuildFace(Vector3 point1, Vector3 point2)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 32; z++)
                    {
                        vertices.Add(BuildVertex(point1.X + x, point1.Y + y, point1.Z + z, 1, 0));
                        vertices.Add(BuildVertex(point1.X + x, point2.Y + y, point1.Z + z, 1, 1));
                        vertices.Add(BuildVertex(point2.X + x, point2.Y + y, point2.Z + z, 0, 1));
                        vertices.Add(BuildVertex(point2.X + x, point2.Y + y, point2.Z + z, 0, 1));
                        vertices.Add(BuildVertex(point2.X + x, point1.Y + y, point2.Z + z, 0, 0));
                        vertices.Add(BuildVertex(point1.X + x, point1.Y + y, point1.Z + z, 1, 0));

                    }
                }
            }
        }

        private VertexPositionTexture BuildVertex(float x, float y, float z, float u, float v)
        {
            return new VertexPositionTexture(new Vector3(x, y, z), new Vector2(u, v));
        }

        public void PositionCube(float minDistance, Vector3 position)
        {
            location = new Vector3(1.5f, 0.5f, 1.5f);

        }

        public void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            effect.Texture = texture2;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;
            effect.CurrentTechnique.Passes[0].Apply();

            device.SetVertexBuffer(voxelBuffer);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, voxelBuffer.VertexCount / 3);
        }
    }
}
