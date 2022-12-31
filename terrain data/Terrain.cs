namespace TrainLineDesigner
{
    public enum Zones
    {
        NoZone, // Default -- not the rest is this
        DistrictZone, // Land that contributes to form a district
        Border, // The land between usable and unusable land
        RestrictedLand, // Land in usable land that cannot be used/high cost to use 
    }

    public class TerrainMapData
    {
        public Map<byte> ElevationMap { get; set; }
        public Map<Zones> ZoningMap { get; set; }

        public TerrainMapData()
        {
            ElevationMap = new Map<byte>();
            ZoningMap = new Map<Zones>();
        }

        public LandData GetChunkData(Location L)
        {
            return new LandData(ElevationMap.ReadChunk(L), ZoningMap.ReadChunk(L));
        }

    }
    public struct LandData
    {
        public byte Elevation;
        public Zones Zone;

        public LandData(byte Elevation, Zones Zone)
        {
            this.Elevation = Elevation;
            this.Zone = Zone;
        }
    }
    public class Terrain
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public TerrainMapData TerrainMaps { get; set; }
        public Terrain(int MapHeight, int MapWidth)
        {
            this.MapWidth = MapWidth;
            this.MapHeight = MapHeight;
        }

        public void WriteTerrain(TerrainMapData terrainMap)
        {
            Console.WriteLine(
            "hiiiii!!"
            );
            this.TerrainMaps = terrainMap;
            Console.WriteLine(IsRegionEnclosed());
        }

        public class BorderCell
        {
            public bool Visited;
            public int[]? Neighbours;
            public Location position;

            public BorderCell(Location position)
            {
                this.position = position;
                this.Visited = false;
            }
        }
        //TODO: Prune the region
        public bool IsRegionEnclosed()
        {
            Dictionary<Location, BorderCell> BorderCells = new Dictionary<Location, BorderCell>();
            foreach (KeyValuePair<Location, Zones> l in TerrainMaps.ZoningMap.ChunkMap)
            {
                if (l.Value == Zones.Border)
                {
                    Location position = l.Key;
                    BorderCells.Add(position, new BorderCell(position));
                }
            }
            BorderCell InitialCell = BorderCells.Values.ToArray()[0];
            BorderCell CurrentCell;
            Stack<BorderCell> CellStack = new Stack<BorderCell>();
            CellStack.Push(InitialCell);
            int Path = 0;
            while (CellStack.Count() > 0)
            {
                CurrentCell = CellStack.Pop();
                bool BackTracking = true;
                Console.WriteLine(CurrentCell.position);
                if (CurrentCell.Neighbours == null)
                {
                    Location[] possibleNeighbours = {
                                            Location.Shift(CurrentCell.position,1,0,MapWidth),
                                            Location.Shift(CurrentCell.position,-1,0,MapWidth),
                                            Location.Shift(CurrentCell.position,0,1,MapWidth),
                                            Location.Shift(CurrentCell.position,0,-1, MapWidth),
                                            Location.Shift(CurrentCell.position,1,1, MapWidth),
                                            Location.Shift(CurrentCell.position,1,-1, MapWidth),
                                            Location.Shift(CurrentCell.position,-1,1,MapWidth),
                                            Location.Shift(CurrentCell.position,-1,1,MapWidth),
                                    };
                    List<int> neighbours = new List<int>();
                    foreach (Location possibleNeighbour in possibleNeighbours)
                    {
                        if (BorderCells.ContainsKey(possibleNeighbour))
                        {
                            BorderCell Target = BorderCells[possibleNeighbour];
                            if (Target.Visited == true)
                            {
                                if (Target == InitialCell)
                                {
                                    if (Path == BorderCells.Count())
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                CellStack.Push(CurrentCell);
                                Target.Visited = true;
                                Path++;
                                CellStack.Push(Target);
                                BackTracking = false;
                                break;
                            }
                        }
                    }
                }
                if (BackTracking)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
/*
struct bordercell
position
neighbours
visited

Create stack 
Add initial cell and push it to stack
array of all cells
stack=
initial cell
currentcell =
path = 0
While stack is not empty 
pop cell from stack and make it current cell
if there are unvisited cell:
    neighbour is initial cell:
        if length of path is length of all cell		
            return true
        else	
                return false
    push cell to stack
    choose one of the unvisited cell
    Mark chosen cell and push it to stack
return false 
*/
