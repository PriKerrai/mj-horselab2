using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MJ_HorseLab2
{
    class NewVoxel
    {

        public NewVoxel()
        {
        }

        public List<Vector3[]> BuildCube()
        {
            List<Vector3[]> cubeFaces = new List<Vector3[]>();
            Vector3[] LEFT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 1, 0), new Vector3(0, 1, 1) };
            Vector3[] RIGHT_FACE_POS = { new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 0), new Vector3(1, 1, 1) };
            Vector3[] TOP_FACE_POS = { new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(1, 1, 0), new Vector3(0, 1, 1), new Vector3(1, 1, 1) };
            Vector3[] BOTTOM_FACE_POS = { new Vector3(1, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 1), new Vector3(1, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 1) };
            Vector3[] BACK_FACE_POS = { new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 0, 1), new Vector3(0, 1, 1) };
            Vector3[] FRONT_FACE_POS = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0) };

            cubeFaces.Add(LEFT_FACE_POS);
            cubeFaces.Add(RIGHT_FACE_POS);
            cubeFaces.Add(TOP_FACE_POS);
            cubeFaces.Add(BOTTOM_FACE_POS);
            cubeFaces.Add(BACK_FACE_POS);
            cubeFaces.Add(FRONT_FACE_POS);

            return cubeFaces;

        }

        public List<Vector2[]> SetTextureCoordinates()
        {
            List<Vector2[]> textureCoordinates = new List<Vector2[]>();

            Vector2[] LEFT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
            Vector2[] RIGHT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
            Vector2[] TOP_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
            Vector2[] BOTTOM_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
            Vector2[] BACK_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };
            Vector2[] FRONT_FACE_TEXCOORD = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 0) };

            return textureCoordinates;

        }



    }
}
