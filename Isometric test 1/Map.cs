using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
   public class Map
    {
        private readonly Point MAP_SIZE = new(2, 2);
        private readonly Point TILE_SIZE;
        private readonly Vector2 MAP_OFFSET = new(2.5f, 2);
        private readonly Tile[,] _tiles;
        private Tile _lastMouseSelected;
        public Map()
        {
            _tiles = new Tile[MAP_SIZE.X, MAP_SIZE.Y];

            Texture2D[] textures =
            {
                Globals.Content.Load<Texture2D>("tile0"),
                Globals.Content.Load<Texture2D>("tile1"),
                Globals.Content.Load<Texture2D>("tile2"),
                Globals.Content.Load<Texture2D>("tile3"),
                Globals.Content.Load<Texture2D>("tile4"),
                Globals.Content.Load<Texture2D>("tile5"),
            };
            TILE_SIZE.X = textures[0].Width;
            TILE_SIZE.Y = textures[0].Height / 2;

            //Random random = new();

            //for (int y = 0; y < MAP_SIZE.Y; y++)
            //{
            //    for (int x = 0; x < MAP_SIZE.X; x++)
            //    {
            //        int r = random.Next(0, textures.Length);
            //        _tiles[x, y] = new(textures[r], MapToScreen(x, y));
            //    }
            //}

            _tiles[0, 0] = new(textures[2], MapToScreen(0, 0));
            _tiles[0, 1] = new(textures[1], MapToScreen(0, 1));
            _tiles[1, 0] = new(textures[5], MapToScreen(1, 0));
            _tiles[1, 1] = new(textures[3], MapToScreen(1, 1));
        }

        private Vector2 MapToScreen(int mapX, int mapY)
        {
            var screenX = ((mapX - mapY) * TILE_SIZE.X / 2) + (MAP_OFFSET.X * TILE_SIZE.X);
            var screenY = ((mapY + mapX) * TILE_SIZE.Y / 2) + (MAP_OFFSET.Y * TILE_SIZE.Y);

            return new(screenX, screenY);
        }

        private Point ScreenToMap(Point mousePos)
        {
            Vector2 cursor = new(mousePos.X - (int)(MAP_OFFSET.X * TILE_SIZE.X), mousePos.Y - (int)(MAP_OFFSET.Y * TILE_SIZE.Y));

            var x = cursor.X + (2 * cursor.Y) - (TILE_SIZE.X / 2);
            int mapX = (x < 0) ? -1 : (int)(x / TILE_SIZE.X);
            var y = -cursor.X + (2 * cursor.Y) + (TILE_SIZE.X / 2);
            int mapY = (y < 0) ? -1 : (int)(y / TILE_SIZE.X);

            return new(mapX, mapY);
        }

        public void Update()
        {
            _lastMouseSelected?.MouseDeselect();

            var map = ScreenToMap(InputManager.MousePosition);

            if (map.X >= 0 && map.Y >= 0 && map.X < MAP_SIZE.X && map.Y < MAP_SIZE.Y)
            {
                _lastMouseSelected = _tiles[map.X, map.Y];
                _lastMouseSelected.MouseSelect();
            }
        }
        public void Draw()
        {
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    _tiles[x, y].Draw();
                }
            }
        }
    }
}
