using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloOpenTK
{
    internal class MyGameWindow : GameWindow
    {
        float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
             0.5f, -0.5f, 0.0f, //Bottom-right vertex
             -0.5f,  0.5f, 0.0f  //Top-left vertex
        };

        float[] vertices2 = {
            -0.5f, 0.5f, 0.0f, //top-left vertex
             0.5f, 0.5f, 0.0f, //top-right vertex
             0.5f,  -0.5f, 0.0f  //bottom-right vertex
        };

        Shader shader;

        int VertexArrayObject;
        int VertexArrayObject2;

        public MyGameWindow(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) 
        { 
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.0f, 0.0f, 1.0f, 1.0f);

            //////////////////////////////////////////////////////////
            /// VBO (Vertex Buffer Object)
            int VertexBufferObject;
            // 그래픽카드에 Buffer (저장공간)를 생성
            VertexBufferObject = GL.GenBuffer();
            // 생성한 Buffer를 어떤 특성(ArrayBuffer)으로 선택
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            // 선택한 Buffer에 데이터를 보내기
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            //////////////////////////////////////////////////////////
            /// Shader
            string ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string vertexPath = Path.Join(ProjectDirectory, "vertex.glsl").ToString();
            string fragmentPath = Path.Join(ProjectDirectory, "fragment.glsl").ToString();
            shader = new Shader(vertexPath, fragmentPath);

            /////////////////////////////////////////////////////////
            /// VAO (Vertex Array Object)
            // 그래픽카드에 VertexArrayObject를 저장할 공간 할당
            VertexArrayObject = GL.GenVertexArray();
            // VertexArrayObject를 선택
            GL.BindVertexArray(VertexArrayObject);
            // 그래픽카드에 Bind된 Buffer의 해석 방법을 Bind된 VertexArrayObject에 저장
            // 0번 속성, 데이터는 3개, float 타입, ...
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            // 0번 속성을 Enable
            GL.EnableVertexAttribArray(0);

            int VertexBufferObject2;
            // 그래픽카드에 Buffer (저장공간)를 생성
            VertexBufferObject2 = GL.GenBuffer();
            // 생성한 Buffer를 어떤 특성(ArrayBuffer)으로 선택
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject2);
            // 선택한 Buffer에 데이터를 보내기
            GL.BufferData(BufferTarget.ArrayBuffer, vertices2.Length * sizeof(float), vertices2, BufferUsageHint.StaticDraw);

            // 그래픽카드에 VertexArrayObject를 저장할 공간 할당
            VertexArrayObject2 = GL.GenVertexArray();
            // VertexArrayObject를 선택
            GL.BindVertexArray(VertexArrayObject2);
            // 그래픽카드에 Bind된 Buffer의 해석 방법을 Bind된 VertexArrayObject에 저장
            // 0번 속성, 데이터는 3개, float 타입, ...
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            // 0번 속성을 Enable
            GL.EnableVertexAttribArray(0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.BindVertexArray(VertexArrayObject2);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }
    }
}
