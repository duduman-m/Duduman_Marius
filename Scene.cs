using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Duduman_Marius_Laborator
{
    class Scene : GameWindow
    {
        bool showCube = true;
        int lookat_X = 0;
        int lookat_Y = 0;
        int lookat_Z = 15;
        float cube_X = 0;
        float cube_Y = 0;
        KeyboardState lastKeyPress;

        // Constructor.
        public Scene() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Gray);
            GL.Enable(EnableCap.DepthTest);
            this.CursorGrabbed = true;
            this.CursorVisible = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver3, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[Key.ControlLeft] && keyboard[Key.P] && !keyboard.Equals(lastKeyPress))
            {
                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }
            else if (keyboard[Key.F11])
            {
                if (WindowState == WindowState.Fullscreen)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Fullscreen;
            }
            else if (keyboard[Key.R])
            {
                // Reset button
                lookat_X = lookat_Y = 0;
            }

            // Camera movement
            if (keyboard[Key.Up])
            {
                lookat_Y += 1;
            }
            else if (keyboard[Key.Down])
            {
                lookat_Y -= 1;
            }

            if (keyboard[Key.Left])
            {
                lookat_X += 1;
            }
            else if (keyboard[Key.Right])
            {
                lookat_X -= 1;
            }

            lastKeyPress = keyboard;
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            // Mouse movement
            cube_X = (e.X - Width / 2f) / (Width / 2f);
            cube_Y = -(e.Y - Height / 2f) / (Height / 2f);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(lookat_X, lookat_Y, lookat_Z, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            DrawAxes();
            //DrawTriangle();

            if (showCube == true)
            {
                DrawCube();
            }

            SwapBuffers();
            Thread.Sleep(1);
        }

        private void DrawAxes()
        {
            GL.Begin(PrimitiveType.Lines);

            // X
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0 , 0);
            GL.Vertex3(30, 0, 0);

            // Y
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 30, 0);

            // Z
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 30);


            GL.End();
        }

        private void DrawTriangle()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.Aqua);
            GL.Vertex3(0, 15, 5);
            GL.Vertex3(0, 20, 0);
            GL.Vertex3(5, 15, 0);

            GL.End();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(cube_X - 1.0f, cube_Y - 1.0f, - 1.0f);
            GL.Vertex3(cube_X - 1.0f, cube_Y + 1.0f, - 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y + 1.0f, - 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y - 1.0f, - 1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(cube_X - 1.0f, cube_Y - 1.0f, - 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y - 1.0f, - 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y - 1.0f, 1.0f);
            GL.Vertex3(cube_X - 1.0f, cube_Y - 1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(cube_X - 1.0f, cube_Y - 1.0f, - 1.0f);
            GL.Vertex3(cube_X - 1.0f, cube_Y - 1.0f, 1.0f);
            GL.Vertex3(cube_X - 1.0f, cube_Y + 1.0f, 1.0f);
            GL.Vertex3(cube_X - 1.0f, cube_Y + 1.0f, - 1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(cube_X - 1.0f, cube_Y - 1.0f, 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y - 1.0f, 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y + 1.0f, 1.0f);
            GL.Vertex3(cube_X - 1.0f, cube_Y + 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(cube_X - 1.0f, cube_Y + 1.0f, - 1.0f);
            GL.Vertex3(cube_X - 1.0f, cube_Y + 1.0f, 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y + 1.0f, 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y + 1.0f, - 1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(cube_X + 1.0f, cube_Y - 1.0f, - 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y + 1.0f, - 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y + 1.0f, 1.0f);
            GL.Vertex3(cube_X + 1.0f, cube_Y - 1.0f, 1.0f);

            GL.End();
        }

        static void Main(string[] args)
        {
            using (Scene example = new Scene())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
