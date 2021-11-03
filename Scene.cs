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
        KeyboardState lastKeyPress;

        Triangle tr;
        Cube cb;
        Axes ax;
        Grid grid;
        Camera3DIsometric cam;

        // Constructor.
        public Scene() : base(800, 600)
        {
            VSync = VSyncMode.On;
            tr = new Triangle("Triangle.txt");
            cb = new Cube("Cube.txt");
            ax = new Axes();
            grid = new Grid();
            cam = new Camera3DIsometric();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Gray);
            GL.Enable(EnableCap.DepthTest);
            CursorGrabbed = true;
            CursorVisible = false;

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver3, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            cam.SetCamera();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            ax.Show();
            grid.Show();


            if (keyboard.Equals(lastKeyPress))
                ; // Go over if it's the same key pressed
            else if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[Key.ControlLeft] && keyboard[Key.P] && !keyboard.Equals(lastKeyPress))
            {
                cb.SetShow();
            }
            else if (keyboard[Key.F11])
            {
                if (WindowState == WindowState.Fullscreen)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Fullscreen;
            }
            else if (keyboard[Key.Number1])
            {
                tr.SetColors(new Color[] {Color.Blue, Color.Yellow, Color.Red });
                tr.SetRandom(false);
            }
            else if (keyboard[Key.Number2])
            {
                tr.SetColors(new Color[] { Color.Black, Color.Red, Color.Yellow });
                tr.SetRandom(false);
            }
            else if (keyboard[Key.Number3])
            {
                tr.SetColors(new Color[] { Color.Red, Color.White, Color.Blue });
                tr.SetRandom(false);
            }
            else if (keyboard[Key.Number4])
            {
                tr.SetRandom(true);
            }
            else if (keyboard[Key.R])
            {
                if(!cb.GetRandom())
                {
                    cb.RandomColors();
                } 
                else
                {
                    cb.ResetColors();
                }
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
            cam.FollowCube(cb.GetCoords(), new Vector3((mouse.X - Width / 2f) / (Width / 16f), (mouse.Y - Height / 2f) / (Height / 16f), 35));

            lastKeyPress = keyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);


            if (tr.GetRandom())
                tr.RandomDraw();
            else
                tr.Draw();

            cb.Draw();
            ax.Draw();
            grid.Draw();

            SwapBuffers();
            Thread.Sleep(1);
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
