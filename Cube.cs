using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Duduman_Marius
{
    class Cube
    {
        private float size = 0;
        private float x = 0;
        private float y = 0;
        private float z = 0;
        private float jump = 0;
        private float jump_height = (float)4;
        private bool can_jump = false;
        private bool jump_direction = false;

        const float movement_speed = (float)0.2;
        const float jump_duration = (float)10;

        public Cube(string nume_fisier)
        {
            string[] lines = System.IO.File.ReadAllLines(nume_fisier);
            for (int i = 0; i <= 1; i++)
            {
                string[] coords = lines[i].Split(' ');
                if (coords.Length != 0)
                {
                    if (i == 0)
                    {
                        size = float.Parse(coords[0]);
                    }
                    else
                    {
                        x = float.Parse(coords[0]);
                        y = float.Parse(coords[1]);
                        z = float.Parse(coords[2]);
                    }
                }
            }
        }

        public float GetSize()
        {
            return size;
        }

        public void SetX(float X)
        {
            x = X;
        }

        public float GetX()
        {
            return x;
        }

        public void SetY(float Y)
        {
            y = Y;
        }

        public float GetY()
        {
            return y;
        }

        public void SetZ(float Z)
        {
            z = Z;
        }

        public float GetZ()
        {
            return z;
        }

        public void Draw()
        {
            //GL.Rotate(y-x, 0, 1, 0); //Still testing

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x - size, y + size, z - size);
            GL.Vertex3(x + size, y + size, z - size);
            GL.Vertex3(x + size, y - size, z - size);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x + size, y - size, z - size);
            GL.Vertex3(x + size, y - size, z + size);
            GL.Vertex3(x - size, y - size, z + size);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x - size, y - size, z + size);
            GL.Vertex3(x - size, y + size, z + size);
            GL.Vertex3(x - size, y + size, z - size);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(x - size, y - size, z + size);
            GL.Vertex3(x + size, y - size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x - size, y + size, z + size);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(x - size, y + size, z - size);
            GL.Vertex3(x - size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z - size);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(x + size, y - size, z - size);
            GL.Vertex3(x + size, y + size, z - size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y - size, z + size);

            GL.End();
            //GL.Rotate(0, 0, 1, 0); //Still testing
        }

        public void MoveUp()
        {
            z -= movement_speed;
        }

        public void MoveDown()
        {
            z += movement_speed;
        }

        public void MoveLeft()
        {
            x -= movement_speed;
        }

        public void MoveRight()
        {
            x += movement_speed;
        }

        public void Jump()
        {
            if (can_jump)
            {
                jump_direction = true;
            }
        }

        public void CheckJump()
        {
            switch (jump) 
            {
                case jump_duration:
                    jump--;
                    can_jump = jump_direction = false;
                    break;
                case 0:
                    if(jump_direction)
                    {
                        jump++;
                    }
                    can_jump = true;
                    break;
                default:
                    if (jump_direction)
                    {
                        y += jump_height / (jump_duration - 1);
                        jump++;
                    }
                    else
                    {
                        y -= jump_height / (jump_duration - 1);
                        jump--;
                    }
                    can_jump = false;
                    break;
            }
        }
    }
}
