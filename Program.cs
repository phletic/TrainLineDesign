using System;

namespace TrainLineDesigner // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Terrain Singapore = new Terrain(10,10);
            TerrainSerializer TerrainSave = new TerrainSerializer("Singapore.json", Singapore);
            TerrainMapData terrainMapData = new TerrainMapData();
            terrainMapData.ElevationMap.WriteMap(new int[] {1,4,5,6,8},new int[] {3,5,6,8,9},new byte[] {1,3,5,6,7}, 10);
            terrainMapData.ZoningMap.WriteMap(new int[] {1,4,5,6,7}, new int[] {3,5,6,3,1}, new Zones[] {Zones.Residential, Zones.Industrial, Zones.NatureReserves,Zones.Commercial, Zones.Residential}, 10);
            Singapore.WriteTerrain(terrainMapData);
            Singapore.ReadTerrain(4,5);
            Singapore.ReadTerrain(6,8);
            Singapore.ReadTerrain(7,1);
            Singapore.ReadTerrain(5,5);

            TerrainSave.Write();
            Terrain Sg2 = TerrainSave.Read();
            TerrainSerializer TerrainSave2 = new TerrainSerializer("Singapore.json", Sg2);
            TerrainSave2.Write();          
        }
    }
}