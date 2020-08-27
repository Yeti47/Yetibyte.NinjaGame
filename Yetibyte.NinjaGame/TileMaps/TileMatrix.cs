using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMatrix : IEnumerable<TileMapTile>
    {
        private readonly TileMapTile[,] _tiles;

        public TileMapTile this[int x, int y]
        {
            get => GetTile(x, y);
            set => SetTile(x, y, value);
        }

        public int Width => _tiles.GetLength(0);
        public int Height => _tiles.GetLength(1);

        public TileMatrix(int width, int height)
        {
            _tiles = new TileMapTile[width, height];
            
        }

        public TileMatrix(IEnumerable<IEnumerable<TileMapTile>> tiles)
        {
            if (tiles == null)
                throw new ArgumentNullException(nameof(tiles));

            int x = 0;
            int y = 0;

            _tiles = new TileMapTile[tiles.Max(t => t.Count()), tiles.Count()];

            foreach(IEnumerable<TileMapTile> tileRow in tiles)
            {
                foreach (TileMapTile t in tileRow)
                {
                    SetTile(x, y, t);
                    x++;
                }
                x = 0;
                y++;
            }

        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < _tiles.GetLength(0) 
                && y >= 0 && y < _tiles.GetLength(1);
        }

        public void SetTile(int x, int y, TileMapTile tile)
        {
            _tiles[x, y] = tile;

        }

        public void ClearTile(int x, int y)
        {
            _tiles[x, y] = null;

        }

        public TileMapTile GetTile(int x, int y)
        {
            return IsInBounds(x, y) ? _tiles[x, y] : null;
        }

        public IEnumerator<TileMapTile> GetEnumerator()
        {
            List<TileMapTile> tiles = new List<TileMapTile>();

            int width = _tiles.GetLength(0);
            int height = _tiles.GetLength(1);

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    tiles.Add(_tiles[x, y]);
                }
            }

            return tiles.GetEnumerator();
        } 

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }
        public static TileMatrix ParseCsv(string csv)
        {
            string[] csvLines = csv.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<List<TileMapTile>> tiles = new List<List<TileMapTile>>();

            for (int i = 0; i < csvLines.Length; i++)
            {
                var tileList = new List<TileMapTile>();

                string[] ids = csvLines[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                tileList.AddRange(ids.Select(x => new TileMapTile(Convert.ToInt32(x))));
                tiles.Add(tileList);

            }

            return new TileMatrix(tiles);

        }

        public void ForEachTile(Action<int, int> action)
        {
            int width = _tiles.GetLength(0);
            int height = _tiles.GetLength(1);

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    action?.Invoke(x, y);
                }
            }

        }

    }
}
