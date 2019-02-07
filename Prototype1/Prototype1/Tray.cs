namespace Prototype1//TODO definir utilite
{
    public class Tray
    {
        private Tile[] tray;

        public Tray(Tile[] t)
        {
            tray = t;
        }
    }

    public enum Tile
    {
        DISABLE,
        ENABLE,
        GENOS
    }
}