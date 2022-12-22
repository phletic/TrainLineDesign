namespace TrainLineDesigner{

    public struct TerrainMapData{ 
        public Map<byte> ElevationMap {get; set;}
        public Map<Zones> ZoningMap {get; set;}

        public TerrainMapData(){

            ElevationMap = new Map<byte>();
            ZoningMap = new Map<Zones>();
        }
    }
    public enum Zones{
        NoZone,
        Residential,
        Commercial,
        Industrial,
        NatureReserves,
    }

    public class Terrain{
        public int MapWidth {get; set;}
        public int MapHeight {get; set;}
        public Terrain(int MapHeight, int MapWidth){
            this.MapWidth = MapWidth;
            this.MapHeight = MapHeight;
        }
        public TerrainMapData TerrainMaps {get; set;}

        public void WriteTerrain(TerrainMapData terrainMap){
            TerrainMapData tempMap = new TerrainMapData();
            tempMap.ZoningMap = terrainMap.ZoningMap;
            tempMap.ElevationMap = terrainMap.ElevationMap;
            TerrainMaps = tempMap;
        }

        public void ReadTerrain(int x, int y){
            Console.WriteLine(TerrainMaps.ZoningMap.ReadChunk(x,y));
            Console.WriteLine(TerrainMaps.ElevationMap.ReadChunk(x,y));
        }
    }
}