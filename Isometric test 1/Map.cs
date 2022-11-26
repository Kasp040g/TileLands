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
        private Point _mapSize;
        private readonly Point _tileSize;
        private Vector2 _mapOffset = new(4.5f, 4f);
        private Tile[,] _tiles;
        private bool _shouldDrawMap = true;
        private bool _shouldShowWinText = false;

        //Mouse interaction variables
        private Tile _mouseHovered;                 //Null means none has been hovered, else stores a reference to hovered tile instance
        private Tile _mouseGrabbed;                 //Null means none has been grabbed, else stores a reference to grabbed tile instance

        //private Texture2D[] Assets.Assets. = new Texture2D[6];

        // Level States
        private Level _levels;

        public enum Level
        {
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
        }

        /// <summary>
        /// Map constructer to load, setup and create tiles on map
        /// </summary>
        public Map()
        {

            _levels = Level.Level1;

            

            ////Create tile array from map size
            //_tiles = new Tile[_mapSize.X, _mapSize.Y];

            //Load tile textures and add them to texture array
            /*
            this.Assets.Assets.[0] = Globals.Content.Load<Texture2D>("tile0");
            this.Assets.Assets.tileGrassBlockGrass = Globals.Content.Load<Texture2D>("tile1");
            this.Assets.Assets.tileGrassBlockBush = Globals.Content.Load<Texture2D>("tile2");
            this.Assets.Assets.[3] = Globals.Content.Load<Texture2D>("tile3");
            this.Assets.Assets.[4] = Globals.Content.Load<Texture2D>("tile4");
            this.Assets.Assets.[5] = Globals.Content.Load<Texture2D>("tile5");
            */

            //Update tile size variables
            _tileSize.X = Assets.Sprites.tileGrassBlock1.Width;
            _tileSize.Y = Assets.Sprites.tileGrassBlock1.Height / 2;

            //// Level state machine
            //switch(_levels)
            //{
            //    case Level.Level1:
            //        Level1();
            //        break;
            //    case Level.Level2:
            //        Level2();
            //        break;
            //    case Level.Level3:
            //        break;
            //    case Level.Level4:
            //        break;
            //    case Level.Level5:
            //        break;
            //    default:
            //        break;
            //}
        }


        /// <summary>
        /// Converts map coordinates (the location of tiles in the map) to screen coordinates
        /// </summary>
        /// <param name="mapX"></param>
        /// <param name="mapY"></param>
        /// <returns></returns>
        public Vector2 MapToScreen(int mapX, int mapY)
        {
            var screenX = ((mapX - mapY) * _tileSize.X / 2) + (_mapOffset.X * _tileSize.X);
            var screenY = ((mapY + mapX) * _tileSize.Y / 2) + (_mapOffset.Y * _tileSize.Y);

            return new(screenX, screenY);
        }


        /// <summary>
        /// Converts a set of screen coordinates to the map coordinates (the location of tiles in the map)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point ScreenToMap(Point point)
        {
            Vector2 vector = new(point.X - (int)(_mapOffset.X * _tileSize.X), point.Y - (int)(_mapOffset.Y * _tileSize.Y));

            var x = vector.X + (2 * vector.Y) - (_tileSize.X / 2);
            int mapX = (x < 0) ? -1 : (int)(x / _tileSize.X);
            var y = -vector.X + (2 * vector.Y) + (_tileSize.X / 2);
            int mapY = (y < 0) ? -1 : (int)(y / _tileSize.X);

            return new(mapX, mapY);
        }


        /// <summary>
        /// Is called every frame of the game and houses various functionalities of the map
        /// </summary>
        public void Update()
        {
            // Reset current level
            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                var _tempLevel = _levels;

                ClearLevel();
                _shouldDrawMap = true;
                _levels = _tempLevel;
            }

            if (_shouldDrawMap)
            {
                // Level state machine
                switch (_levels)
                {
                    case Level.Level1:
                        Level1();
                        break;
                    case Level.Level2:
                        Level2();
                        break;
                    case Level.Level3:
                        Level3();
                        break;
                    case Level.Level4:
                        break;
                    case Level.Level5:
                        break;
                    default:
                        break;
                }
                _shouldDrawMap = false;
            }


            #region Tile Merging

            //Checks if a tile is stored in mouse hovered and then calls for it to be unhovered if there is
            _mouseHovered?.MouseUnhovered();

            //Converts the current mouse position into map coordinates
            var mouseMap = ScreenToMap(InputManager.MousePosition);

            //Get mouse state for inputs and mouse screen coordinates
            var _mouseState = Mouse.GetState();
            var _mousePosition = new Point(_mouseState.X, _mouseState.Y);

            //Checks if the mouse coordinates are within bounds of the tile, thereby hovering it
            if (mouseMap.X >= 0 && mouseMap.Y >= 0 && mouseMap.X < _mapSize.X && mouseMap.Y < _mapSize.Y)
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

                    if (Vector2.Distance(_hoveredTileVector, _grabbedTileVector) <= 1)
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

            // check if a solution was found
            SolutionFound();
        }


        /// <summary>
        /// Draw calls for tiles in map
        /// </summary>
        public void Draw()
        {
            //Loops through the map array and calls the individual tiles' draw method
            for (int y = 0; y < _mapSize.Y; y++)
            {
                for (int x = 0; x < _mapSize.X; x++)
                {
                    _tiles[x, y].Draw();
                }
            }

            if (_shouldShowWinText)
            {
                Globals.SpriteBatch.DrawString(Globals.FontTest, $"Congratulationos \n Press 'Space' for next level", Vector2.Zero, Color.White);
                _shouldShowWinText = false;
            }
        }

        //Tile.WinCon = new EventHandler(solutio);

        private void SolutionFound()
        {
            //if (_levels == Level.Level1)
            //{
            //    var treeCount = 0;
            //    for (int y = 0; y < _tiles.GetLength(0); y++)
            //    {
            //        for (int x = 0; x < _tiles.GetLength(1); x++)
            //        {
            //            if (_tiles[x, y]._tileType == Tile.TileTypes.tree)
            //            {
            //                treeCount++;
            //            }
            //        }
            //    }
            //    if (treeCount == 1)
            //    {
            //        // Press Space to continue
            //        _shouldShowWinText = true;

            //        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //        {
            //            ClearLevel();
            //            _shouldDrawMap = true;
            //            _levels = Level.Level2;
            //        }
            //    }
            //}

            switch(_levels)
            {
                case Level.Level1:
                    var treeCount = 0;
                    for(int y = 0; y < _tiles.GetLength(0); y++)
                    {
                        for(int x = 0; x < _tiles.GetLength(1); x++)
                        {
                            if(_tiles[x, y]._tileType == Tile.TileTypes.tree)
                            {
                                treeCount++;
                            }
                        }
                    }
                    if(treeCount == 1)
                    {
                        // Press Space to continue
                        _shouldShowWinText = true;

                        if(Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            ClearLevel();
                            _shouldDrawMap = true;
                            _levels = Level.Level2;
                        }
                    }
                    break;
                case Level.Level2:
                    treeCount = 0;
                    for(int y = 0; y < _tiles.GetLength(0); y++)
                    {
                        for(int x = 0; x < _tiles.GetLength(1); x++)
                        {
                            if(_tiles[x, y]._tileType == Tile.TileTypes.tree)
                            {
                                treeCount++;
                            }
                        }
                    }
                    if(treeCount == 2)  // ****TEMP GOAL***
                    {
                        // Press Space to continue
                        _shouldShowWinText = true;

                        if(Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            ClearLevel();
                            _shouldDrawMap = true;
                            _levels = Level.Level3;
                        }
                    }
                    break;
                case Level.Level3:
                    treeCount = 0;
                    for(int y = 0; y < _tiles.GetLength(0); y++)
                    {
                        for(int x = 0; x < _tiles.GetLength(1); x++)
                        {
                            if(_tiles[x, y]._tileType == Tile.TileTypes.tree)
                            {
                                treeCount++;
                            }
                        }
                    }
                    if(treeCount == 3) // ****TEMP GOAL***
                    {
                        // Press Space to continue
                        _shouldShowWinText = true;

                        if(Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            ClearLevel();
                            _shouldDrawMap = true;
                            _levels = Level.Level4;
                        }
                    }
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
            _tiles[0, 0] = new( new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.grass);
            _tiles[0, 1] = new( new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);
            _tiles[0, 2] = new( new Point(0, 2), MapToScreen(0, 2), Tile.TileTypes.grass);
            _tiles[1, 0] = new( new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.grass);
            _tiles[1, 1] = new( new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);
            _tiles[1, 2] = new( new Point(1, 2), MapToScreen(1, 2), Tile.TileTypes.grass);
            _tiles[2, 0] = new( new Point(2, 0), MapToScreen(2, 0), Tile.TileTypes.grass);
            _tiles[2, 1] = new( new Point(2, 1), MapToScreen(2, 1), Tile.TileTypes.grass);
            _tiles[2, 2] = new( new Point(2, 2), MapToScreen(2, 2), Tile.TileTypes.grass);
        }

        private void ClearLevel()
        {

            _tiles[0, 0] = new( new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.empty);
            _tiles[0, 1] = new( new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.empty);
            _tiles[1, 0] = new( new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.empty);
            _tiles[1, 1] = new( new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.empty);
        }

        public void Level1()
        {
            _mapSize = new(2, 2);

            //Create tile array from map size
            _tiles = new Tile[_mapSize.X, _mapSize.Y];

            // Level 1
            _tiles[0, 0] = new(new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.bush);
            _tiles[0, 1] = new(new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);

            _tiles[1, 0] = new(new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.empty);
            _tiles[1, 1] = new(new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);

        }

        private void Level2()
        {
            _mapSize = new(3, 3);

            //Create tile array from map size
            _tiles = new Tile[_mapSize.X, _mapSize.Y];

            _tiles[0, 0] = new(new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.grass);
            _tiles[0, 1] = new(new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);
            _tiles[0, 2] = new(new Point(0, 2), MapToScreen(0, 2), Tile.TileTypes.grass);

            _tiles[1, 0] = new(new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.grass);
            _tiles[1, 1] = new(new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);
            _tiles[1, 2] = new(new Point(1, 2), MapToScreen(1, 2), Tile.TileTypes.grass);

            _tiles[2, 0] = new(new Point(2, 0), MapToScreen(2, 0), Tile.TileTypes.grass);
            _tiles[2, 1] = new(new Point(2, 1), MapToScreen(2, 1), Tile.TileTypes.grass);
            _tiles[2, 2] = new(new Point(2, 2), MapToScreen(2, 2), Tile.TileTypes.grass);
        }

        private void Level3() //Goal 10 træer
        {
            _mapSize = new(5, 5);
            _mapOffset = new(4.5f, 3f);

            //Create tile array from map size
            _tiles = new Tile[_mapSize.X, _mapSize.Y];

            _tiles[0, 0] = new(new Point(0, 0), MapToScreen(0, 0), Tile.TileTypes.empty);
            _tiles[0, 1] = new(new Point(0, 1), MapToScreen(0, 1), Tile.TileTypes.grass);
            _tiles[0, 2] = new(new Point(0, 2), MapToScreen(0, 2), Tile.TileTypes.grass);
            _tiles[0, 3] = new(new Point(0, 3), MapToScreen(0, 3), Tile.TileTypes.grass);
            _tiles[0, 4] = new(new Point(0, 4), MapToScreen(0, 4), Tile.TileTypes.empty);


            _tiles[1, 0] = new(new Point(1, 0), MapToScreen(1, 0), Tile.TileTypes.empty);
            _tiles[1, 1] = new(new Point(1, 1), MapToScreen(1, 1), Tile.TileTypes.grass);
            _tiles[1, 2] = new(new Point(1, 2), MapToScreen(1, 2), Tile.TileTypes.grass);
            _tiles[1, 3] = new(new Point(1, 3), MapToScreen(1, 3), Tile.TileTypes.grass);
            _tiles[1, 4] = new(new Point(1, 4), MapToScreen(1, 4), Tile.TileTypes.grass);


            _tiles[2, 0] = new(new Point(2, 0), MapToScreen(2, 0), Tile.TileTypes.grass);
            _tiles[2, 1] = new(new Point(2, 1), MapToScreen(2, 1), Tile.TileTypes.grass);
            _tiles[2, 2] = new(new Point(2, 2), MapToScreen(2, 2), Tile.TileTypes.grass);
            _tiles[2, 3] = new(new Point(2, 3), MapToScreen(2, 3), Tile.TileTypes.grass);
            _tiles[2, 4] = new(new Point(2, 4), MapToScreen(2, 4), Tile.TileTypes.grass);


            _tiles[3, 0] = new(new Point(3, 0), MapToScreen(3, 0), Tile.TileTypes.grass);
            _tiles[3, 1] = new(new Point(3, 1), MapToScreen(3, 1), Tile.TileTypes.grass);
            _tiles[3, 2] = new(new Point(3, 2), MapToScreen(3, 2), Tile.TileTypes.grass);
            _tiles[3, 3] = new(new Point(3, 3), MapToScreen(3, 3), Tile.TileTypes.grass);
            _tiles[3, 4] = new(new Point(3, 4), MapToScreen(3, 4), Tile.TileTypes.empty);


            _tiles[4, 0] = new(new Point(4, 0), MapToScreen(4, 0), Tile.TileTypes.empty);
            _tiles[4, 1] = new(new Point(4, 1), MapToScreen(4, 1), Tile.TileTypes.grass);
            _tiles[4, 2] = new(new Point(4, 2), MapToScreen(4, 2), Tile.TileTypes.grass);
            _tiles[4, 3] = new(new Point(4, 3), MapToScreen(4, 3), Tile.TileTypes.grass);
            _tiles[4, 4] = new(new Point(4, 4), MapToScreen(4, 4), Tile.TileTypes.empty);




        }
    }
}
