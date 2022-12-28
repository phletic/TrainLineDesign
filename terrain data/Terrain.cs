namespace TrainLineDesigner
{

    // TODO: 
    // 1. Remove repetition of Terrain map data
    // 2. clean up the code
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

        public LandData GetChunkData(int x, int y)
        {
            return new LandData(ElevationMap.ReadChunk(x, y), ZoningMap.ReadChunk(x, y));
        }

        public LandData GetChunkData(int xy)
        {
            return new LandData(ElevationMap.ReadChunk(xy), ZoningMap.ReadChunk(xy));
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
            this.TerrainMaps = terrainMap;
            Console.WriteLine(IsRegionEnclosed());
        }

        public LandData ReadTerrain(int x, int y)
        {
            return TerrainMaps.GetChunkData(x, y);
        }

        public class BorderCell
        {
            public bool Visited;
            public int[]? Neighbours;
            public int position;

            public BorderCell(int position)
            {
                this.position = position;
                this.Visited = false;
            }
        }
			//todo: If backtrack -- immediately fail it 
        public bool IsRegionEnclosed()
        {
            Dictionary<int, BorderCell> BorderCells = new Dictionary<int, BorderCell>();
            foreach (KeyValuePair<int, Zones> location in TerrainMaps.ZoningMap.ChunkMap)
            {
                if (location.Value == Zones.Border)
                {
                    int position = location.Key;
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
                    int[] possibleNeighbours = {
                                            CurrentCell.position+1,
                                              CurrentCell.position-1,
                                                                           CurrentCell.position+MapWidth,
                                                                                        CurrentCell.position-MapWidth,
                                             CurrentCell.position+MapWidth+1,
                                             CurrentCell.position+MapWidth-1,
                                             CurrentCell.position-MapWidth+1,
                                             CurrentCell.position-MapWidth-1,
                                    };
                    List<int> neighbours = new List<int>();
                    foreach (int possibleNeighbour in possibleNeighbours)
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
