using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong_CS
{
    class Program : GameWindow
    {
        int xDaBola = 0;
        int yDaBola = 0;
        int tamanhoDaBola = 20;
        int velocidadeDaBolaEmX = 1;
        int velocidadeDaBolaEmY = 1;

        int yDoJogador1 = 0;
        int yDoJogador2 = 0;

        int xDoJogador1()
        {
            //-metadeJanela+metadeLarguraJogador
            return -ClientSize.Width / 2 + larguraDoJogadores() / 2;
        }

        int xDoJogador2()
        {
            return ClientSize.Width / 2 - larguraDoJogadores() / 2;
        }

        int larguraDoJogadores()
        {
            return tamanhoDaBola;
        }

        int alturaDoJogadores()
        {
            return 4 * tamanhoDaBola;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            xDaBola = xDaBola + velocidadeDaBolaEmX;
            yDaBola = yDaBola + velocidadeDaBolaEmY;
            if (xDaBola + tamanhoDaBola / 2 > xDoJogador2() - larguraDoJogadores() / 2
                && yDaBola - tamanhoDaBola / 2 < yDoJogador2 + larguraDoJogadores() / 2
                && yDaBola + tamanhoDaBola / 2 > yDoJogador2 - larguraDoJogadores() / 2)
            {
                velocidadeDaBolaEmX--;
            }
            if (xDaBola - tamanhoDaBola / 2 < xDoJogador1() + larguraDoJogadores() / 2
                && yDaBola - tamanhoDaBola / 2 < yDoJogador1 + larguraDoJogadores() / 2
                && yDaBola + tamanhoDaBola / 2 > yDoJogador1 - larguraDoJogadores() / 2)
            {
                velocidadeDaBolaEmX++;
            }            
            if (yDaBola + tamanhoDaBola / 2 > ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY--;
            }
            if (yDaBola - tamanhoDaBola / 2 < -ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY++;
            }
            //marca ponto
            if (xDaBola < -ClientSize.Width / 2 || xDaBola > ClientSize.Width / 2)
            {
                xDaBola = 0;
                yDaBola = 0;
            }

            //jogador
            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                yDoJogador1 = yDoJogador1 + 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                yDoJogador1 = yDoJogador1 - 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                yDoJogador2 = yDoJogador2 + 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                yDoJogador2 = yDoJogador2 - 5;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Console.WriteLine(ClientSize.Width + " x " + ClientSize.Height);

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DesenharRetangulo(xDaBola, yDaBola, tamanhoDaBola, tamanhoDaBola, 1.0f, 1.0f, 0.0f);//bola amarelo
            DesenharRetangulo(xDoJogador1(), yDoJogador1, larguraDoJogadores(), alturaDoJogadores(), 1.0f, 0.0f, 0.0f);//jogadorA vermelho
            DesenharRetangulo(xDoJogador2(), yDoJogador2, larguraDoJogadores(), alturaDoJogadores(), 0.0f, 0.0f, 1.0f);//jogadorB azul

            GL.End();

            SwapBuffers();

        }

        void DesenharRetangulo(int x, int y, int largura, int altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
