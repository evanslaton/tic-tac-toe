
namespace tic_tac_toe
{
    class Position
    {
        internal int Row { get; }
        internal int Column { get; }

        internal Position(int userInput)
        {
            Row = getRow(userInput);
            Column = getColumn(userInput);
        }

        private int getRow(int userInput)
        {
            if (userInput <= 3)
            {
                return 0;
            }
            else if (userInput <= 6)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private int getColumn(int userInput)
        {
            if (userInput == 1 || userInput == 4 || userInput == 7)
            {
                return 0;
            }
            else if (userInput == 2 || userInput == 5 || userInput == 8)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
