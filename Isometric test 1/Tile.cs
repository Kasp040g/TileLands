
using Microsoft.VisualBasic;

namespace Isometric_test_1;

public class Tile
{
    public Texture2D _texture;
    public readonly Point _mapPosition;
    public readonly Vector2 _coordinates;

    private bool _mouseHovered;
    private bool _mouseGrabbed;

    public enum TileTypes
    {
        grass,
        bush,
        tree,
    }

    private TileTypes _tileType;

    public Tile(Texture2D texture, Point position, Vector2 coordinates, TileTypes tileType)
    {
        _texture = texture;
        _mapPosition = position;
        _coordinates = coordinates;
        _tileType = tileType;
    }

    public void MouseSelect()
    {
        _mouseHovered = true;
    }

    public void MouseDeselect()
    {
        _mouseHovered = false;
    }

    public void MouseGrab()
    {
        _mouseGrabbed = true;
    }

    public void MouseUngrab()
    {
        _mouseGrabbed = false;
    }


    /// <summary>
    /// Checks whether or not two tiles can be merged, if they can, give the resulting tile
    /// </summary>
    /// <param name="hoveredTile"></param>
    /// <param name="grabbedTile"></param>
    public void CheckTileMerge(Tile hoveredTile, Tile grabbedTile)
    {
        //First check if merge is possible
        //Return the index in array if one is found
        //Merge them


    }

    public void Draw()
    {
        var color = Color.White;
        if (_mouseHovered) color = Color.LightSlateGray;
        if (_mouseGrabbed) color = Color.Red;
        Globals.SpriteBatch.Draw(_texture, _coordinates, color);
    }
}
