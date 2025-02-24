using FilerNS;
using System;
using LevelDesignView;

namespace LevelDesignNS
{
    public class ChessMazeLevel : ILevel
    {
        private Part[,] boardGrid;//create the 2d board
        private LevelDesignForm levelDesignForm;

        public void CreateLevel(int width, int height)
        {
            int minWidth = 3;
            int minHeight = 3;
            if (width >= minWidth && height >= minHeight)
            {
                boardGrid = new Part[width, height];
                
                //Make the whole grid empty ready for placement
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        //levelDesignForm.MakeAButton("btn_", "#", x, y);
                        boardGrid[x, y] = Part.Empty;
                    }
                }
                //Set the goal (currently forced to have it bottom right)
                AddGoal(GetLevelWidth() - 1, GetLevelHeight() - 1);
            }
            else
            {
                //Console.WriteLine("Board dimensions must be at least 3x3.");
                throw new ArgumentOutOfRangeException("The board must be at least 3 by 3");
            }
        }

        public int GetLevelWidth() => boardGrid.GetLength(0);

        public int GetLevelHeight() => boardGrid.GetLength(1);

        public void AddPiece(int gridX, int gridY, Part piece)
        {
            if (ValidPosition(gridX, gridY))
            {
                boardGrid[gridX, gridY] = piece;
            }
            else
            {
                Console.WriteLine("The piece {0} was not added",piece);
            }
        }

        public void AddEmpty(int gridX, int gridY) => AddPiece(gridX, gridY, Part.Empty);
        public void AddKing(int gridX, int gridY) => AddPiece(gridX, gridY, Part.King);
        public void AddRook(int gridX, int gridY) => AddPiece(gridX, gridY, Part.Rook);
        public void AddBishop(int gridX, int gridY) => AddPiece(gridX, gridY, Part.Bishop);
        public void AddKnight(int gridX, int gridY) => AddPiece(gridX, gridY, Part.Knight);
        public void AddPlayerOnEmpty(int gridX, int gridY) => AddPiece(gridX, gridY, Part.PlayerOnEmpty);
        public void AddPlayerOnKing(int gridX, int gridY) => AddPiece(gridX, gridY, Part.PlayerOnKing);
        public void AddPlayerOnRook(int gridX, int gridY) => AddPiece(gridX, gridY, Part.PlayerOnRook);
        public void AddPlayerOnBishop(int gridX, int gridY) => AddPiece(gridX, gridY, Part.PlayerOnBishop);
        public void AddPlayerOnKnight(int gridX, int gridY) => AddPiece(gridX, gridY, Part.PlayerOnKnight);

        public void AddGoal(int gridX, int gridY) => AddPiece(gridX, gridY, Part.Goal);

        public Part GetPartAtIndex(int gridX, int gridY) => boardGrid[gridX, gridY];

        public void SaveMe()
        {
            throw new NotImplementedException();//This is not for me
            //ISaver mockSaver = new MockSaver();
            //mockSaver.Save(_currentLevel.Name, _currentLevel);
        }

        public string CheckValid()
        {
            int countPlayerPieces = 0;
            int countGoalPiece = 0;
            int width = GetLevelWidth();
            int height = GetLevelHeight();

            for (int x = 0; x < width; x++)//height
            {
                for (int y = 0; y < height; y++)//row
                {
                    if (PlayerPiece(boardGrid[x, y]))//if the piece at the current location is a player one
                    {
                        countPlayerPieces++;//add one to the count
                    }
                    if (GetPartAtIndex(x, y) == Part.Goal)
                    {
                        countGoalPiece++;
                    }
                }
            }
            if (countPlayerPieces != 1)
            {
                return "Incorrect amount of player pieces. Please include exactly 1 player piece.";
            }
            if (countGoalPiece != 1)
            {
                return "Incorrect amount of Goal pieces. Please include exactly 1 Goal piece.";
            }
            else
            {
                return "Level is valid.";
            }
        }

        public bool PlayerPiece(Part piece) =>
            piece == Part.PlayerOnEmpty ||
            piece == Part.PlayerOnKing ||
            piece == Part.PlayerOnRook ||
            piece == Part.PlayerOnBishop ||
            piece == Part.PlayerOnKnight;

        ///*
        public void PrintGrid()//used this for testing to see output
        {
            int width = GetLevelWidth();
            int height = GetLevelHeight();

            for (int y = 0; y < height; y++)//Go through the Height
            {
                for (int x = 0; x < width; x++)// Go through the width -- For each value in height all value in width
                {
                    Part part = GetPartAtIndex(x, y);//gets the part at the current x, y values
                    char displayChar = (char)part; // gets the enum value from Part as a character
                    Console.Write(displayChar + " ");// prints the value with a space after
                }
                Console.WriteLine(); //After the row is done new line
            }
        }
        //*/

        public bool ValidPosition(int gridX, int gridY)
        {
            int maxWidth = GetLevelWidth();
            int maxHeight = GetLevelHeight();

            if (boardGrid[gridX,gridY] == Part.Goal)
            {
                //throw new InvalidOperationException("Cant place pieces on the goal");//This is for if the user tries to but a piece on the goal
                //Console.WriteLine("Don't place pieces on the Goal");
                return false;
            }
            
            else
            {
                return true;
            }
        }
    }
}
