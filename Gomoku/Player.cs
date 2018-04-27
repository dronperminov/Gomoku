using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Gomoku {
    class Player {
        public string character;
        public Color color;

        public Player(string character, Color color) {
            this.character = character;
            this.color = color;
        }

        public static bool operator !=(Player player1, Player player2) {
            return player1.character != player2.character || player1.color != player2.color;
        }

        public static bool operator ==(Player player1, Player player2) {
            return player1.character == player2.character && player1.color == player2.color;
        }
    }
}
