using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Duduman_Marius
{
    class Cube
    {
        private bool show = true;
        private float size = 0;
        private Color4[] colors = new Color4[] { Color.Silver, Color.Honeydew, Color.Moccasin, Color.IndianRed, Color.PaleVioletRed, Color.ForestGreen };
        private bool random = false;
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

        public void SetShow()
        {
            show = !show;
        }

        public float GetSize()
        {
            return size;
        }

        public void ResetColors()
        {
            colors = new Color4[] { Color.Silver, Color.Honeydew, Color.Moccasin, Color.IndianRed, Color.PaleVioletRed, Color.ForestGreen };
            random = false;
        }

        public void RandomColors()
        {
            colors = new Color4[6];
            Random random = new Random();
            this.random = true;

            for (int i=0; i<6; i++)
            {
                double r = random.NextDouble();
                double g = random.NextDouble();
                double b = random.NextDouble();
                double t = random.NextDouble();
                colors[i] = new Color4((float)r, (float)g, (float)b, (float)t);
            }
        }

        public void SetRandom()
        {
            random = !random;
        }

        public bool GetRandom()
        {
            return random;
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

        public Vector3 GetCoords()
        {
            return new Vector3(GetX(), GetY(), GetZ());
        }

        public void Draw()
        {
            //GL.Rotate(y-x, 0, 1, 0); //Still testing

            GL.Begin(PrimitiveType.Quads);

            GL.Color4(colors[0]);
            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x - size, y + size, z - size);
            GL.Vertex3(x + size, y + size, z - size);
            GL.Vertex3(x + size, y - size, z - size);

            GL.Color4(colors[1]);
            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x + size, y - size, z - size);
            GL.Vertex3(x + size, y - size, z + size);
            GL.Vertex3(x - size, y - size, z + size);

            GL.Color4(colors[2]);
            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x - size, y - size, z + size);
            GL.Vertex3(x - size, y + size, z + size);
            GL.Vertex3(x - size, y + size, z - size);

            GL.Color4(colors[3]);
            GL.Vertex3(x - size, y - size, z + size);
            GL.Vertex3(x + size, y - size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x - size, y + size, z + size);

            GL.Color4(colors[4]);
            GL.Vertex3(x - size, y + size, z - size);
            GL.Vertex3(x - size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z - size);

            GL.Color4(colors[5]);
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

        public void StartFalling()
        {
            y = 10;
        }

        public void FallExample()
        {
            Draw();
            if (y - size != 0)
            {
                y -= (float)0.25;
            }
        }
    }
}
