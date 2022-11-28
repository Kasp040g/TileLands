
using System;

using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Audio;

namespace Isometric_test_1;

public class Tile
{
    //Eventhandler for WinCon
    public static EventHandler WinCon;

    
    //Tile visuals variables
    public Texture2D _texture;
    public readonly Point _mapPosition;
    public readonly Vector2 _coordinates;

    private readonly Texture2D[] _tileSprites = 
    { 
        Assets.Sprites.tileGrassBlock1,
        Assets.Sprites.tileGrassBlock2,
        Assets.Sprites.tileGrassBlock3,
        Assets.Sprites.tileGrassBlock4
    };


    //Mouse interaction variables
    private bool _mouseHovered;
    private bool _mouseGrabbed;

    //Create jagged array storing all recipies as their own arrays (hoveredTile,grabbedTile,resultingTile)
    private readonly TileTypes[][] _mergeRecipies = new TileTypes[][]
    {
        new TileTypes[] {TileTypes.grass,       TileTypes.grass,        TileTypes.bush},
        new TileTypes[] {TileTypes.bush,        TileTypes.bush,         TileTypes.tree},
    };


    /// <summary>
    /// An enum state machine that stores the current type of the tile
    /// </summary>
    public enum TileTypes
    {
        empty,
        grass,
        bush,
        tree,
    }

    public TileTypes _tileType;


    /// <summary>
    /// Tile constructer, used to instantiate a tile of a specific type
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="position"></param>
    /// <param name="coordinates"></param>
    /// <param name="tileType"></param>
    public Tile(Point position, Vector2 coordinates, TileTypes tileType)
    {
        //Select and random tile texture
        Random _rnd = new Random();
        int _number = _rnd.Next(0, _tileSprites.Length);

        _texture = _tileSprites[_number];

        _mapPosition = position;
        _coordinates = coordinates;
        _tileType = tileType;

        //Check if tile is empty instead
        if (_tileType == TileTypes.empty)
        {
            _texture = Assets.Sprites.tileEmpty;
        }
    }


    /// <summary>
    /// Tells the tile it is being hovered by the mouse
    /// </summary>
    public void MouseHovered()
    {
        _mouseHovered = true;
    }


    /// <summary>
    /// Tells the tile it is no longer hovered by the mouse
    /// </summary>
    public void MouseUnhovered()
    {
        _mouseHovered = false;
    }


    /// <summary>
    /// Tells the tile that it is now grabbed by the player
    /// </summary>
    public void MouseGrab()
    {
        _mouseGrabbed = true;
    }


    /// <summary>
    /// Tells the tile that it is no longer grabbed by the player
    /// </summary>
    public void MouseUngrab()
    {
        _mouseGrabbed = false;
    }


    /// <summary>
    /// Checks whether or not two tiles can be merged, if they can, give the resulting tile
    /// </summary>
    /// <param name="hoveredTile"></param>
    /// <param name="grabbedTile"></param>
    public void CheckTileMerge(Tile hoveredTile)
    {
        var recipeFound = false;            //Tells whether or not a merge recipe was found
        var recipeIndex = 0;                //Tracks which recipe is currently being checked for possible merge


        //Loop through the jagged array storing all the array recipies
        foreach (TileTypes[] tileArray in _mergeRecipies)
        {
            //Checks the first two colums for the grabbed tile type(this one)
            if (tileArray[0] == this._tileType)
            {
                //Checks if the hovered tile's tiletype is also a match inside the recipe
                if (tileArray[1] == hoveredTile._tileType)
                {
                    //A merge recipe is found containing both the hovered til and the grabbed tile
                    recipeFound = true;                    

                    //Break out of for each to keep recipe index and prevent further recipe checks
                    break;
                }
            }
            else if (tileArray[1] == this._tileType)
            {
                //Checks if the hovered tile's tiletype is also a match inside the recipe
                if (tileArray[0] == hoveredTile._tileType)
                {
                    //A merge recipe is found containing both the hovered til and the grabbed tile
                    recipeFound = true;

                    //Break out of for each to keep recipe index and prevent further recipe checks
                    break;
                }
            }

            //Update index/count of current recipe
              recipeIndex++;
        }

        //Only update anything if a merge recipe match was found
        if (recipeFound == true)
        {
            hoveredTile._tileType = _mergeRecipies[recipeIndex][2];
            //SoundEffect _mergeSound = Assets.Audio.mergeSound;
            //_mergeSound.Play();
            Assets.Audio.mergeSound.Play();

            // invoke Event
            WinCon?.Invoke(_mergeRecipies, new EventArgs());
        }
    }


    /// <summary>
    /// Draws the tile every frame depending on tile state
    /// </summary>
    public void Draw()
    {
        var color = Color.White;
        if (_mouseHovered) color = Color.LightGray;
        if (_mouseGrabbed) color = Color.LightSeaGreen;
        Globals.SpriteBatch.Draw(_texture, _coordinates, color);

        if (Globals.DebugModeToggled == true)
        {
            Globals.SpriteBatch.DrawString(Globals.FontTest, $"{_tileType}", _coordinates, Color.White);
        }
    }
}
