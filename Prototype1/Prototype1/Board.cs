namespace Prototype1//TODO definir utilite
{
    public class Board
    {
        private Tile[] board;

        public Board(Tile[] b)
        {
            board = b;
        }
    }

    public enum Tile
    {
        DISABLE,
        ENABLE,
        GENOS
    }
}