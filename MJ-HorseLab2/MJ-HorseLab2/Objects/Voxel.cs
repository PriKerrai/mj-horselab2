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

        Vector3[] LEFT_FACE_POS = { new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 1), new Vector3(1, 1, 1) };
        Vector3[] RIGHT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 1) };
        Vector3[] TOP_FACE_POS = { new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
        Vector3[] BOTTOM_FACE_POS = { new Vector3(1, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 1),new Vector3(0, 0, 1) };
        Vector3[] BACK_FACE_POS = { new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
        Vector3[] FRONT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0) };

        Vector2[] LEFT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] TOP_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] BOTTOM_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] BACK_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] RIGHT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        Vector2[] FRONT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        GraphicsDevice device;
        VertexBuffer buffer;
        Texture2D texture2;


        public Voxel(GraphicsDevice device, Texture2D texture2)
        {
            this.device = device;
            this.texture2 = texture2;
            /*
            buffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, BuildDrawable().Count , BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionColor>(BuildDrawable().ToArray());
             * */
            buffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, BuildTexture().Count, BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionTexture>(BuildTexture().ToArray());
             
        }

        public List<VertexPositionColor> BuildDrawable()
        {
            Color color = Color.Red;
            Color color1 = Color.Beige;
            Color color2 = Color.Black;
            Color color3 = Color.Green;

            List<VertexPositionColor> vList = new List<VertexPositionColor>();
            
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[2],color2));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[1],color2));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[0],color2));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[2],color2));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[3],color2));
            vList.Add(new VertexPositionColor(LEFT_FACE_POS[1],color2));

            vList.Add(new VertexPositionColor(RIGHT_FACE_POS[0], color));
            vList.Add(new VertexPositionColor(RIGHT_FACE_POS[1], color));
            vList.Add(new VertexPositionColor(RIGHT_FACE_POS[2], color));
            vList.Add(new VertexPositionColor(RIGHT_FACE_POS[1], color));
            vList.Add(new VertexPositionColor(RIGHT_FACE_POS[3], color));
            vList.Add(new VertexPositionColor(RIGHT_FACE_POS[2], color));

            vList.Add(new VertexPositionColor(BACK_FACE_POS[2], color3));
            vList.Add(new VertexPositionColor(BACK_FACE_POS[1], color3));
            vList.Add(new VertexPositionColor(BACK_FACE_POS[0], color3));
            vList.Add(new VertexPositionColor(BACK_FACE_POS[0], color3));
            vList.Add(new VertexPositionColor(BACK_FACE_POS[3], color3));
            vList.Add(new VertexPositionColor(BACK_FACE_POS[2], color3));

            vList.Add(new VertexPositionColor(FRONT_FACE_POS[0], color1));
            vList.Add(new VertexPositionColor(FRONT_FACE_POS[1], color1));
            vList.Add(new VertexPositionColor(FRONT_FACE_POS[2], color1));
            vList.Add(new VertexPositionColor(FRONT_FACE_POS[0], color1));
            vList.Add(new VertexPositionColor(FRONT_FACE_POS[2], color1));
            vList.Add(new VertexPositionColor(FRONT_FACE_POS[3], color1));

            vList.Add(new VertexPositionColor(TOP_FACE_POS[0], color1));
            vList.Add(new VertexPositionColor(TOP_FACE_POS[1], color1));
            vList.Add(new VertexPositionColor(TOP_FACE_POS[2], color1));
            vList.Add(new VertexPositionColor(TOP_FACE_POS[0], color1));
            vList.Add(new VertexPositionColor(TOP_FACE_POS[2], color1));
            vList.Add(new VertexPositionColor(TOP_FACE_POS[3], color1));

            vList.Add(new VertexPositionColor(BOTTOM_FACE_POS[0], color2));
            vList.Add(new VertexPositionColor(BOTTOM_FACE_POS[1], color2));
            vList.Add(new VertexPositionColor(BOTTOM_FACE_POS[2], color2));
            vList.Add(new VertexPositionColor(BOTTOM_FACE_POS[3], color2));
            vList.Add(new VertexPositionColor(BOTTOM_FACE_POS[2], color2));
            vList.Add(new VertexPositionColor(BOTTOM_FACE_POS[1], color2));

            return vList;

        }
             
        public List<VertexPositionTexture> BuildTexture()
        {
            Color color = Color.Red;
            Color color1 = Color.Beige;
            Color color2 = Color.Black;
            Color color3 = Color.Green;

            List<VertexPositionTexture> vList = new List<VertexPositionTexture>();

            vList.Add(new VertexPositionTexture(LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(LEFT_FACE_POS[0], LEFT_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(LEFT_FACE_POS[2], LEFT_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(LEFT_FACE_POS[3], LEFT_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(LEFT_FACE_POS[1], LEFT_FACE_TEXCOORD[0]));

            vList.Add(new VertexPositionTexture(RIGHT_FACE_POS[0], RIGHT_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(RIGHT_FACE_POS[1], RIGHT_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(RIGHT_FACE_POS[3], RIGHT_FACE_TEXCOORD[3]));
            vList.Add(new VertexPositionTexture(RIGHT_FACE_POS[2], RIGHT_FACE_TEXCOORD[2]));

            vList.Add(new VertexPositionTexture(BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(BACK_FACE_POS[1], BACK_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(BACK_FACE_POS[0], BACK_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(BACK_FACE_POS[3], BACK_FACE_TEXCOORD[3]));
            vList.Add(new VertexPositionTexture(BACK_FACE_POS[2], BACK_FACE_TEXCOORD[2]));
            
            vList.Add(new VertexPositionTexture(FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(FRONT_FACE_POS[1], FRONT_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(FRONT_FACE_POS[0], FRONT_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(FRONT_FACE_POS[2], FRONT_FACE_TEXCOORD[3]));
            vList.Add(new VertexPositionTexture(FRONT_FACE_POS[3], FRONT_FACE_TEXCOORD[0]));

            vList.Add(new VertexPositionTexture(TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(TOP_FACE_POS[1], TOP_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(TOP_FACE_POS[0], TOP_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(TOP_FACE_POS[2], TOP_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(TOP_FACE_POS[3], TOP_FACE_TEXCOORD[3]));

            vList.Add(new VertexPositionTexture(BOTTOM_FACE_POS[0], BOTTOM_FACE_TEXCOORD[0]));
            vList.Add(new VertexPositionTexture(BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));
            vList.Add(new VertexPositionTexture(BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(BOTTOM_FACE_POS[3], BOTTOM_FACE_TEXCOORD[3]));
            vList.Add(new VertexPositionTexture(BOTTOM_FACE_POS[2], BOTTOM_FACE_TEXCOORD[2]));
            vList.Add(new VertexPositionTexture(BOTTOM_FACE_POS[1], BOTTOM_FACE_TEXCOORD[1]));
            
            return vList;

        }

        /*
        public void Draw(BasicEffect effect)
        {
            effect.World = Matrix.Identity;
            effect.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(buffer);
            device.DrawPrimitives(PrimitiveType.TriangleList,0, buffer.VertexCount);
        }
         * */

        public void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            effect.Texture = texture2;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            //Matrix translate = Matrix.CreateTranslation(location);
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;
            effect.CurrentTechnique.Passes[0].Apply();

            device.SetVertexBuffer(buffer);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, buffer.VertexCount / 3);
        }
    }
}
