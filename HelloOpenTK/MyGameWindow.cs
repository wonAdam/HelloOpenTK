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
        Vertex[] vertices = {
            new Vertex(-0.5f, -0.5f, 0.0f), //Bottom-left vertex
            new Vertex(0.5f, -0.5f, 0.0f), //Bottom-right vertex
            new Vertex(-0.5f,  0.5f, 0.0f),  //Top-left vertex
        };

        Vertex[] vertices2 = {
            new Vertex(-0.5f, 0.5f, 0.0f), //top-left vertex
            new Vertex(0.5f, 0.5f, 0.0f), //top-right vertex
            new Vertex(0.5f, -0.5f, 0.0f), //bottom-right vertex
        };

        Vertex[] vertices3 = {
            new Vertex(-0.5f, 0.5f, 0.0f), //top-left vertex
            new Vertex(0.5f, 0.5f, 0.0f), //top-right vertex
            new Vertex(0.5f, -0.5f, 0.0f), //bottom-right vertex
            new Vertex(-0.5f, -0.5f, 0.0f), //Bottom-left vertex
        };

        uint[] indices =
        {
            0, 1, 2,
            0, 2, 3,
        };

        Shader shader;

        Triangle t1;
        Triangle t2;

        int VertexBufferObject;
        int ElementBufferObject;
        int VertexArrayObject;

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
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);

            //t1 = new Triangle(vertices[0], vertices[1], vertices[2]);
            //t2 = new Triangle(vertices2[0], vertices2[1], vertices2[2]);

            //////////////////////////////////////////////////////////
            /// VBO (Vertex Buffer Object)
            // 그래픽카드에 Buffer (저장공간)를 생성
            VertexBufferObject = GL.GenBuffer();
            // 생성한 Buffer를 어떤 특성(ArrayBuffer)으로 선택
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            // 선택한 Buffer에 데이터를 보내기
            GL.BufferData(BufferTarget.ArrayBuffer, vertices3.Length * sizeof(float) * 3, vertices3, BufferUsageHint.StaticDraw);

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

            //////////////////////////////////////////////////////////
            /// EBO (Vertex Buffer Object)
            // 그래픽카드에 Buffer (저장공간)를 생성
            ElementBufferObject = GL.GenBuffer();
            // 생성한 Buffer를 어떤 특성(ArrayBuffer)으로 선택
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            // 선택한 Buffer에 데이터를 보내기
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            //////////////////////////////////////////////////////////
            /// Shader
            string ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string vertexPath = Path.Join(ProjectDirectory, "vertex.glsl").ToString();
            string fragmentPath = Path.Join(ProjectDirectory, "fragment.glsl").ToString();
            shader = new Shader(vertexPath, fragmentPath);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            //t1.Draw();
            //t2.Draw();

            SwapBuffers();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }
    }
}
