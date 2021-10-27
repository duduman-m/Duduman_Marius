using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Duduman_Marius
{
    class Triangle
    {
        private bool draw_random = false;
        private float[] p1 = new float[] { 0, 0, 0 };
        private float[] p2 = new float[] { 0, 0, 0 };
        private float[] p3 = new float[] { 0, 0, 0 };
        private Color[] colors = new Color[] { Color.Blue, Color.Yellow, Color.Red };

        public Triangle(string nume_fisier)
        {
            string[] lines = System.IO.File.ReadAllLines(nume_fisier);
            for (int i = 0; i <= 2; i++)
            {
                string[] coords = lines[i].Split(' ');
                if (coords.Length != 0)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (i == 0)
                        {
                            p1[j] = float.Parse(coords[j]);
                        }
                        else if (i == 1)
                        {
                            p2[j] = float.Parse(coords[j]);
                        }
                        else
                        {
                            p3[j] = float.Parse(coords[j]);
                        }

                    }
                }
            }
        }

        public void SetColors(Color[] new_colors)
        {
            colors = new_colors;
        }

        public Color[] GetColors()
        {
            return colors;
        }

        public float[] GetPoint(int number)
        {
            if (number == 1)
            {
                return p1;
            }
            else if (number == 2)
            {
                return p2;
            }
            else
            {
                return p3;
            }
        }

        public void SetRandom(bool x)
        {
            draw_random = x;
        }

        public bool GetRandom()
        {
            return draw_random;
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);

            float[] point;
            for (int i = 1; i <= 3; i++)
            {
                point = GetPoint(i);
                GL.Color3(colors[i-1]);
                GL.Vertex3(point[0], point[1], point[2]);
            }

            GL.End();
        }

        public void RandomDraw()
        {
            // Only cause it was required to use RGB in Lab 3. The color of the Vertex is random each time. Ex: GL.Color3(0.2, 0.7, 0.1)
            GL.Begin(PrimitiveType.Triangles);

            float[] point;
            Random random = new Random();
            double r1, r2, r3;
            for (int i = 1; i <= 3; i++)
            {
                point = GetPoint(i);
                r1 = random.NextDouble();
                r2 = random.NextDouble();
                r3 = random.NextDouble();
                GL.Color3(r1, r2, r3);
                GL.Vertex3(point[0], point[1], point[2]);

                Console.WriteLine("Vertex #" + i + ": (" + r1 + ", " + r2 + ", " + r3 + ")");
            }

            GL.End();
        }
    }
}
