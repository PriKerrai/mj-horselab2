using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics:

namespace MJ_HorseLab2
{
    class NewVoxel
    {


        Vector3[] LEFT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 1, 0), new Vector3(0, 1, 1) };
        Vector3[] RIGHT_FACE_POS = { new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 0), new Vector3(1, 1, 1) };
        Vector3[] TOP_FACE_POS = { new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(1, 1, 0), new Vector3(0, 1, 1), new Vector3(1, 1, 1) };
        Vector3[] BOTTOM_FACE_POS = { new Vector3(1, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 1), new Vector3(1, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 1) };
        Vector3[] BACK_FACE_POS = { new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 0, 1), new Vector3(0, 1, 1) };
        Vector3[] FRONT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0) };

        Vector2[] LEFT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
        Vector2[] RIGHT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
        Vector2[] TOP_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
        Vector2[] BOTTOM_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
        Vector2[] BACK_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
        Vector2[] FRONT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };


        GraphicsDevice device;
        VertexBuffer buffer;


        public NewVoxel(GraphicsDevice device)
        {
            this.device = device;

            buffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, BuildDrawable().Count , BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionColor>(BuildDrawable().ToArray());
        }

            




        public List<VertexPositionColor> BuildDrawable()
        {
            Color color = Color.Red;

            List<VertexPositionColor> vList = new List<VertexPositionColor>();

            vList.Add(new VertexPositionColor(LEFT_FACE_POS[0],color));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[1],color));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[2],color));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[3],color));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[4],color));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[5],color));

            return vList;

        }


        public void Draw(BasicEffect effect)
        {
            effect.World = Matrix.Identity;
            effect.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(buffer);
            device.DrawPrimitives(PrimitiveType.TriangleList,0, buffer.VertexCount);
        }
    }
}
