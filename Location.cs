namespace TrainLineDesigner{

    public struct  Location{
        public int position;

        public Location (int x,int y, int MapWidth){
            this.position = Get1DCoordinate(x,y,MapWidth);
        }
        public Location (int xy){
            this.position = xy;
        }
        int Get1DCoordinate(int x, int y, int MapWidth)
        {
            return MapWidth * y + x;
        }
        public static Location Shift(Location L, int x, int y, int MapWidth){
            return new Location(L.position + y*MapWidth + x);
        }
        public static Location Create(string n){
            return new Location(int.Parse(n));
        }
    }

    
}