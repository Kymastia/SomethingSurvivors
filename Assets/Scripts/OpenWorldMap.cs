using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenWorldMap : MonoBehaviour
{
    [SerializeField, Min(0)]
    private int _width = 10;

    [SerializeField, Min(0)]
    private int _height = 10;

    [SerializeField]
    private Sprite[] _groundSprites;

    [SerializeField]
    private Tilemap _tilemap;

    [SerializeField, Tooltip("Random seed. Use 0 to disable"), Min(0)]
    private int _seed = 1;
    
    private TileBase[] _groundTiles;

    private void Awake()
    {
        Debug.Assert(_groundSprites.Length > 0);
        Debug.Assert(_tilemap);
        if (_seed != 0)
        {
            Random.InitState(_seed);
        }

        _groundTiles = _groundSprites
            .Select(sprite => {
                var tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = sprite;
                return tile;
            }).ToArray();
    }

    private void OnValidate()
    {
        if (!_tilemap)
        {
            _tilemap = GetComponentInChildren<Tilemap>();
        }
    }

    private TileBase GetTile()
    {
        var index = Random.Range(0, _groundTiles.Length);
        return _groundTiles[index];
    }

    private void Start()
    {
        TileBase[] tiles = new Tile[_width * _height];

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = GetTile();
        }

        int halfWidth = _width / 2;
        int halfHeight = _height / 2;
        var position = new Vector3Int(-halfWidth, -halfHeight);
        var bounds = new BoundsInt(position, new(_width, _height, 1));

        _tilemap.SetTilesBlock(bounds, tiles);
    }

}