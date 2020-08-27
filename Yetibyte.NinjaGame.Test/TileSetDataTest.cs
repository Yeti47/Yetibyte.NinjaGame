using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Serialization;
using Yetibyte.NinjaGame.Pipeline.TileSets;
using System.IO;

namespace Yetibyte.NinjaGame.Test
{
    [TestClass]
    public class TileSetDataTest
    {
        private const string TEST_TILESET_PATH = "./Resources/Test.tsx";

        [TestMethod]
        public void DeserializeTileSetData()
        {
            //string fileContent = System.IO.File.ReadAllText(TEST_TILESET_PATH);

            XmlSerializer serializer = new XmlSerializer(typeof(TileSetData));

            TileSetData tileSetData = null;

            using (FileStream stream = new FileStream(TEST_TILESET_PATH, FileMode.Open))
            {
                 tileSetData = serializer.Deserialize(stream) as TileSetData;
            }

            Assert.IsTrue(tileSetData != null && tileSetData.Name == "Test" && tileSetData.TileWidth == 64);

        }
    }
}
