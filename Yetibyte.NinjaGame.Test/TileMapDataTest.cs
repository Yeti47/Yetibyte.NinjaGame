using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yetibyte.NinjaGame.Pipeline.TileMaps;

namespace Yetibyte.NinjaGame.Test
{
    [TestClass]
    public class TileMapDataTest
    {
        private const string TEST_TILEMAP_PATH = "./Resources/TestTiledMap1.tmx";

        [TestMethod]
        public void Deserialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TileMapData));

            TileMapData mapData = null;

            using (FileStream stream = new FileStream(TEST_TILEMAP_PATH, FileMode.Open))
            {
                mapData = serializer.Deserialize(stream) as TileMapData;
            }
        }
    }
}
