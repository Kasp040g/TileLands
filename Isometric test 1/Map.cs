using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
   public class Map
    {
        //Setup basic tile and map information variables
        private readonly Point MAP_SIZE = new(2, 2);
        private readonly Point TILE_SIZE;
        private readonly Vector2 MAP_OFFSET = new(2.5f, 2);
        private readonly Tile[,] _tiles;

        //Mouse interaction variables
        private Tile _mouseHovered;                 //Null means none has been hovered, else stores a reference to hovered tile instance
        private Tile _mouseGrabbed;                 //Null means none has been grabbed, else stores a reference to grabbed tile instance

        private Texture2D[] textures = new Texture2D[6];

        // Level States
        public enum Level
        {
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
        }

        private Level _levels;


        /// <summary>
        /// Map constructer to load, setup and create tiles on map
        /// </summary>
        public Map()
        {
            _levels = Level.Level1;
            
            //Create tile array from map size
            _tiles = new Tile[MAP_SIZE.X, MAP_SIZE.Y];

            //Load tile textures and add them to texture array
            this.textures[0] = Globals.Content.Load<Texture2D>("tile0");
            this.textures[1] = Globals.Content.Load<Texture2D>("tile1");
            this.textures[2] = Globals.Content.Load<Texture2D>("tile2");
            this.textures[3] = Globals.Content.Load<Texture2D>("tile3");
            this.textures[4] = Globals.Content.Load<Texture2D>("tile4");
            this.textures[5] = Globals.Content.Load<Texture2D>("tile5");

            //Update tile size variables
            TILE_SIZE.X = textures[0].Width;
            TILE_SIZE.Y = textures[0].Height / 2;

            // Level state machine
            switch(_levels)
            {
                case Level.Level1:
                    Level1();
                    SolutionFound();
                    break;
                case Level.Level2:
                    Level2();
                    break;
                case Level.Level3:
                    break;
                case Level.Level4:
                    break;
                case Level.Level5:
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Converts map coordinates (the location of tiles in the map) to screen coordinates
        /// </summary>
        /// <param name="mapX"></param>
        /// <param name="mapY"></param>
        /// <returns></returns>
        public Vector2 MapToScreen(int mapX, int mapY)
        {
            var screenX = ((mapX - mapY) * TILE_SIZE.X / 2) + (MAP_OFFSET.X * TILE_SIZE.X);
            var screenY = ((mapY + mapX) * TILE_SIZE.Y / 2) + (MAP_OFFSET.Y * TILE_SIZE.Y);

            return new(screenX, screenY);
        }


        /// <summary>
        /// Converts a set of screen coordinates to the map coordinates (the location of tiles in the map)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point ScreenToMap(Point point)
        {
            Vector2 vector = new(point.X - (int)(MAP_OFFSET.X * TILE_SIZE.X), point.Y - (int)(MAP_OFFSET.Y * TILE_SIZE.Y));

            var x = vector.X + (2 * vector.Y) - (TILE_SIZE.X / 2);
            int mapX = (x < 0) ? -1 : (int)(x / TILE_SIZE.X);
            var y = -vector.X + (2 * vector.Y) + (TILE_SIZE.X / 2);
            int mapY = (y < 0) ? -1 : (int)(y / TILE_SIZE.X);

            return new(mapX, mapY);
        }


        /// <summary>
        /// Is called every frame of the game and houses various functionalities of the map
        /// </summary>
        public void Update()
        {
            
            

            #region Tile Merging

            //Checks if a tile is stored in mouse hovered and then calls for it to be unhovered if there is
            _mouseHovered?.MouseUnhovered();

            //Converts the current mouse position into map coordinates
            var mouseMap = ScreenToMap(InputManager.MousePosition);

            //Get mouse state for inputs and mouse screen coordinates
            var _mouseState = Mouse.GetState();
            var _mousePosition = new Point(_mouseState.X, _mouseState.Y);

            //Checks if the mouse coordinates are within bounds of the tile, thereby hovering it
            if (mouseMap.X >= 0 && mouseMap.Y >= 0 && mouseMap.X < MAP_SIZE.X && mouseMap.Y < MAP_SIZE.Y)
            {
                //Save the hovered tile
                _mouseHovered = _tiles[mouseMap.X, mouseMap.Y];

                //Tell the hovered tile that it is being hovered
                _mouseHovered.MouseHovered();
            }
            else 
            {
                //No tile is being hovered
                _mouseHovered = null;
            }

            //If the player is hovering a tile and no other tile is being dragged, then they are able to drag the tile around
            if (_mouseHovered != null && _mouseGrabbed == null && _mouseState.LeftButton == ButtonState.Pressed)
            {
                //Transfers the hovered tile to grabbed
                _mouseGrabbed = _mouseHovered;

                //Tells the now grabbed tile that it is being grabbed
                _mouseGrabbed.MouseGrab();
            }

            //Release tile with mouse
            if (_mouseGrabbed != null && _mouseState.LeftButton == ButtonState.Released)
            {
                //Check if the tile is dropped on another tile
                if (_mouseHovered != null && _mouseHovered != _mouseGrabbed)
                {
                    //if (_mouseHovered._mapPosition.X < _mouseGrabbed._mapPosition.X + 1.05
                    //    && _mouseHovered._mapPosition.X > _mouseGrabbed._mapPosition.X - 1.05
                    //    && _mouseHovered._mapPosition.Y < _mouseGrabbed._mapPosition.Y + 1.05
                    //    && _mouseHovered._mapPosition.Y > _mouseGrabbed._mapPosition.Y - 1.05
                    //    )
                    var _hoveredTileVector = _mouseHovered._mapPosition.ToVector2();
                    var _grabbedTileVector = _mouseGrabbed._mapPosition.ToVector2();

                    if (Vector2.Distance(_hoveredTileVector,_grabbedTileVector) <= 1)
                    {
                        //_mouseGrabbed._texture = textures[5];
                        _mouseGrabbed.CheckTileMerge(_mouseHovered);
                    }
                }

                //Tell the grabbed tile that it is no longer grabbed
                _mouseGrabbed.MouseUngrab();

                //Reset grabbed variable
                _mouseGrabbed = null;
            }
            #endregion Tile merging            
        }


        /// <summary>
        /// Draw calls for tiles in map
        /// </summary>
        public void Draw()
        {
            //Loops through the map array and calls the individual tiles' draw method
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    _tiles[x, y].Draw();
                }
            }
        }

        private void SolutionFound()
        {
            
            
            switch(_levels)
            {
                case Level.Level1:
                    var treeCount = 0;
                    for(int y= 0; y < _tiles.GetLength(0); y++)
                    {
                        for(int x = 0; x < _tiles.GetLength(1); x++)
                        {
                            if(_tiles[x,y]._tileType == Tile.TileTypes.tree)
                            {
                                treeCount++;
                                
                            }
                        }
                    }
                    if(treeCount == 1)
                    {
                        if(Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            // Press Space to continue
                            ClearLevel();
                            _levels = Level.Level2;
                        }
                        
                    }
                    break;
                case Level.Level2:

                    break;
                case Level.Level3:
                    break;
                case Level.Level4:
                    break;
                case Level.Level5:
                    break;
                default:
                    break;
            }
        }

        private void Leveltemp()
        {
            _tiles[0, 0] = new(textures[2], new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.grass);
            _tiles[0, 1] = new(textures[1], new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);
            _tiles[0, 2] = new(textures[5], new Point(0, 2), MapToScreen(0, 2), Tile.TileTypes.grass);
            _tiles[1, 0] = new(textures[1], new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.grass);
            _tiles[1, 1] = new(textures[2], new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);
            _tiles[1, 2] = new(textures[1], new Point(1, 2), MapToScreen(1, 2), Tile.TileTypes.grass);
            _tiles[2, 0] = new(textures[5], new Point(2, 0), MapToScreen(2, 0), Tile.TileTypes.grass);
            _tiles[2, 1] = new(textures[1], new Point(2, 1), MapToScreen(2, 1), Tile.TileTypes.grass);
            _tiles[2, 2] = new(textures[1], new Point(2, 2), MapToScreen(2, 2), Tile.TileTypes.grass);
        }

        private void ClearLevel()
        {
           
            _tiles[0, 0] = new(textures[5], new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.empty);
            _tiles[0, 1] = new(textures[5], new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.empty);
            _tiles[1, 0] = new(textures[5], new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.empty);
            _tiles[1, 1] = new(textures[5], new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.empty);
        }

        public void Level1()
        {

            // Level 1
            _tiles[0, 0] = new(textures[2], new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.bush);
            _tiles[0, 1] = new(textures[1], new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);
            _tiles[1, 0] = new(textures[5], new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.empty);
            _tiles[1, 1] = new(textures[1], new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);           
            
        }

        private void Level2()
        {
            _tiles[0, 0] = new(textures[2], new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.grass);
            _tiles[0, 1] = new(textures[1], new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);
            _tiles[0, 2] = new(textures[5], new Point(0, 2), MapToScreen(0, 2), Tile.TileTypes.grass);
            _tiles[1, 0] = new(textures[1], new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.grass);
            _tiles[1, 1] = new(textures[2], new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);
            _tiles[1, 2] = new(textures[1], new Point(1, 2), MapToScreen(1, 2), Tile.TileTypes.grass);
            _tiles[2, 0] = new(textures[5], new Point(2, 0), MapToScreen(2, 0), Tile.TileTypes.grass);
            _tiles[2, 1] = new(textures[1], new Point(2, 1), MapToScreen(2, 1), Tile.TileTypes.grass);
            _tiles[2, 2] = new(textures[1], new Point(2, 2), MapToScreen(2, 2), Tile.TileTypes.grass);
        }

        private void Level3()
        {
            _tiles[0, 0] = new(textures[2], new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.grass);
            _tiles[0, 1] = new(textures[1], new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);
            _tiles[0, 2] = new(textures[5], new Point(0, 2), MapToScreen(0, 2), Tile.TileTypes.grass);
            _tiles[1, 0] = new(textures[1], new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.grass);
            _tiles[1, 1] = new(textures[2], new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);
            _tiles[1, 2] = new(textures[1], new Point(1, 2), MapToScreen(1, 2), Tile.TileTypes.grass);
            _tiles[2, 0] = new(textures[5], new Point(2, 0), MapToScreen(2, 0), Tile.TileTypes.grass);
            _tiles[2, 1] = new(textures[1], new Point(2, 1), MapToScreen(2, 1), Tile.TileTypes.grass);
            _tiles[2, 2] = new(textures[1], new Point(2, 2), MapToScreen(2, 2), Tile.TileTypes.grass);
        }
    }
}
