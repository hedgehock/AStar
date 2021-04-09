using System;
using Raylib_cs;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            Rendering rendering = new Rendering(800, 50);
            Alghorithm alghorithm = new Alghorithm(rendering);
            Input input = new Input(rendering, alghorithm);

            Raylib.SetTraceLogLevel(TraceLogType.LOG_ERROR);

            Raylib.InitWindow(rendering.windowSize, rendering.windowSize, "AStar");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                rendering.RenderContext();
                input.HandleInput();

                Raylib.EndDrawing();
            }
        }
    }
}
