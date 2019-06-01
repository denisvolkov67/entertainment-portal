﻿using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class PostNewGameBoard : IRequest<int[,]>
    {
        public PostNewGameBoard(GameBoard gameBoard)
        {
            Rows = gameBoard.Row;
            Columns = gameBoard.Column;
        }

        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
