﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        /// <summary>
        /// First player
        /// </summary>
        public Player Player1 { get; set; }

        /// <summary>
        /// Second player
        /// </summary>
        public Player Player2 { get; set; }

        /// <summary>
        /// Tells if the game is over
        /// </summary>
        public bool Finish { get; set; }
    }
}
