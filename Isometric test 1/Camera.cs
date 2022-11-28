
using System;

namespace Isometric_test_1
{
    //Camera ned arver fra singleton og kan derved selv instantieres
    public class Camera : Singleton<Camera>
    {
        private Matrix viewMatrix;

        public Matrix ViewMatrix => viewMatrix;

        private Vector2 position;
           
        public Vector2 Position1 { get => position; set => position = value; }

        public void UpdateCamera()
        {
            viewMatrix =
                Matrix.CreateTranslation((int)MathF.Round(-position.X), (int)MathF.Round(-position.Y), 0) * //position
                Matrix.CreateTranslation(GameWorld.WindowWidth / 2, GameWorld.WindowHeight / 2, 1) *
                Matrix.CreateScale(1.0f) *                                                                  //Scale
                Matrix.CreateRotationZ(0f);                                                                 //Rotation
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(viewMatrix));
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, viewMatrix);
        }
    }
}
