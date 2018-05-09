using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Gomoku {
    class Player {
        public readonly Image image;

        public Player(Image image) {
            this.image = image;
        }

        public static bool operator !=(Player player1, Player player2) {
            return player1.image != player2.image;
        }

        public static bool operator ==(Player player1, Player player2) {
            return player1.image == player2.image;
        }
    }
}
