using System;

namespace TrainLineDesigner // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Terrain Singapore = new Terrain(10, 10);
            TerrainSerializer TerrainSave = new TerrainSerializer("Singapore.json", Singapore);
            TerrainMapData terrainMapData = new TerrainMapData();
            terrainMapData.ElevationMap.WriteMap(new Location[] {
                new Location(2,3,10),
                new Location(5,6,10),
                new Location(6,2,10)
            }, new byte[] { 1, 3, 5 }, MapWidth : 10);
            terrainMapData.ZoningMap.WriteMap(new Location[] {
                new Location(1,1,10),
                new Location(0,0,10),
                new Location(0,1,10)
            }, new Zones[] {Zones.Border, Zones.Border, Zones.Border},MapWidth: 10);
            Singapore.WriteTerrain(terrainMapData);
            Console.WriteLine(Singapore.TerrainMaps.GetChunkData(new Location(2,3,10)).Elevation);
            TerrainSave.Write();
            Terrain Sg2 = TerrainSave.Read();
            TerrainSerializer TerrainSave2 = new TerrainSerializer("Singapore.json", Sg2);
            TerrainSave2.Write();
            Console.ReadLine();
        }
    }
}