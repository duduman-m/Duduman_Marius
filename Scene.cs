using Duduman_Marius;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Threading;

namespace Duduman_Marius_Laborator
{
    class Scene : GameWindow
    {
        bool showCube = true;
        float camera_pos_X = 0;
        float camera_pos_Y = 2;
        float camera_pos_Z = 35;
        float camera_angle_X = 0;
        float camera_angle_Y = 0;
        float camera_angle_Z = 0;
        float camera_movement_speed = (float)0.5;
        KeyboardState lastKeyPress;

        Triangle tr;
        Cube cb;

        // Constructor.
        public Scene() : base(800, 600)
        {
            VSync = VSyncMode.On;
            tr = new Triangle("Triangle.txt");
            cb = new Cube("Cube.txt");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Gray);
            GL.Enable(EnableCap.DepthTest);
            CursorGrabbed = true;
            CursorVisible = false;
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


            if (keyboard.Equals(lastKeyPress))
                ; // Go over if it's the same key pressed
            else if (keyboard[Key.Escape])
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
                camera_pos_X = 0;
                camera_pos_Y = 2;
                camera_pos_Z = 35;
            }
            else if (keyboard[Key.Number1])
            {
                tr.SetColors(new Color[] { Color.Blue, Color.Yellow, Color.Red });
            }
            else if (keyboard[Key.Number2])
            {
                tr.SetColors(new Color[] { Color.Black, Color.Red, Color.Yellow });
            }
            else if (keyboard[Key.Number3])
            {
                tr.SetColors(new Color[] { Color.Red, Color.White, Color.Blue });
            }

            // Cube Movement
            cb.CheckJump();
            if (keyboard[Key.Space])
            {
                cb.Jump();
            }

            if (keyboard[Key.W])
            {
                cb.MoveUp();
            }
            else if (keyboard[Key.S])
            {
                cb.MoveDown();
            }

            if (keyboard[Key.A])
            {
                cb.MoveLeft();
            }
            else if (keyboard[Key.D])
            {
                cb.MoveRight();
            }

            // Camera Movement
            MouseState mouse = Mouse.GetState();
            camera_pos_X = cb.GetX();
            camera_pos_Y = cb.GetY() * camera_movement_speed + 3;
            camera_pos_Z = cb.GetZ() + 10;

            camera_angle_X = camera_pos_X + (mouse.X - Width / 2f) / (Width / 16f);
            camera_angle_Y = camera_pos_Y - (mouse.Y - Height / 2f) / (Height / 16f);
            camera_angle_Z = camera_pos_Z - 35; // For now


            lastKeyPress = keyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(camera_pos_X, camera_pos_Y, camera_pos_Z, camera_angle_X, camera_angle_Y, camera_angle_Z, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            DrawAxes();
            tr.Draw();

            if (showCube == true)
            {
                cb.Draw();
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

        static void Main(string[] args)
        {
            using (Scene example = new Scene())
            {
                example.Run(60.0, 0.0);
            }
        }
    }
}
