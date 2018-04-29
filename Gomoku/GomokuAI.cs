using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gomoku {
    struct Template {
        public int weight;
        public string template;

        public Template(string template, int weight) {
            this.template = template;
            this.weight = weight;
        }
    }

    class GomokuAI {
        const int fiveScore = 1000;
        const int openedFourScore = 80;
        const int halfClosedFourScore = 40;
        const int openedThreeScore = 30;
        const int halfClosedFourWithBreachScore = 20;
        const int halfClosedThreeScore = 15;
        const int halfClosedThreeWithBreachScore = 8;
        const int openedTwoScore = 2;

        List<Move> availableMoves;
        int n, m;
        Random rnd;

        Template[] templates = {
            new Template("#####", fiveScore), // 5 в ряд

            new Template(" #### ", openedFourScore), // открытая четвёрка

            new Template(" ####", halfClosedFourScore), // полузакрытая четвёрка
            new Template("#### ", halfClosedFourScore), // полузакрытая четвёрка

            new Template(" # ###", halfClosedFourWithBreachScore), // полузакрытая четвёрка с брешью
            new Template(" ## ##", halfClosedFourWithBreachScore), // полузакрытая четвёрка с брешью
            new Template(" ### #", halfClosedFourWithBreachScore), // полузакрытая четвёрка с брешью
            new Template("### # ", halfClosedFourWithBreachScore), // полузакрытая четвёрка с брешью
            new Template("## ## ", halfClosedFourWithBreachScore), // полузакрытая четвёрка с брешью
            new Template("# ### ", halfClosedFourWithBreachScore), // полузакрытая четвёрка с брешью

            new Template(" ### ", openedThreeScore), // открытая тройка

            new Template(" ###", halfClosedThreeScore), // полузакрытая тройка
            new Template("### ", halfClosedThreeScore), // полузакрытая тройка

            new Template(" ## #", halfClosedThreeWithBreachScore), // полузакрытая тройка с брешью
            new Template(" # ##", halfClosedThreeWithBreachScore), // полузакрытая тройка с брешью
            new Template("## # ", halfClosedThreeWithBreachScore), // полузакрытая тройка с брешью
            new Template("# ## ", halfClosedThreeWithBreachScore), // полузакрытая тройка с брешью

            new Template(" ## ", openedTwoScore), // открытая двойка
        };

        public GomokuAI(int n, int m, bool isUserFirst) {
            availableMoves = new List<Move>();
            this.n = n;
            this.m = m;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    AddMove(new Move(i, j));
            
            if (!isUserFirst) {
                RemoveMove(n / 2, m / 2);
            }            

            rnd = new Random();
        }

        public bool haveMove(Move move) {
            for (int i = 0; i < availableMoves.Count; i++)
                if (availableMoves[i].i == move.i && availableMoves[i].j == move.j)
                    return true;

            return false;
        }

        public void AddMove(Move move) {
            if (!haveMove(move))
                availableMoves.Add(move);
        }

        public void RemoveMove(int i0, int j0) {
            for (int i = 0; i < availableMoves.Count; i++) {
                if (availableMoves[i].i == i0 && availableMoves[i].j == j0) {
                    availableMoves.RemoveAt(i);
                    i--;
                }
            }
        }

        string[] getLines(int i0, int j0, int radius, Player player, Board board) {
            string[] lines = new string[4];

            for (int i = i0 - radius; i <= i0 + radius; i++) {
                if (i < 0 || i >= n)
                    continue;

                if (i == i0) {
                    lines[0] += "0";
                }
                else if (board[i, j0].value == "") {
                    lines[0] += " ";
                }
                else if (board[i, j0].value == player.character) {
                    lines[0] += "#";
                }
                else {
                    lines[0] += "@";
                }
            }

            for (int j = j0 - radius; j <= j0 + radius; j++) {
                if (j < 0 || j >= m)
                    continue;

                if (j == j0) {
                    lines[1] += "0";
                }
                else if (board[i0, j].value == "") {
                    lines[1] += " ";
                }
                else if (board[i0, j].value == player.character) {
                    lines[1] += "#";
                }
                else {
                    lines[1] += "@";
                }
            }

            for (int k = -radius; k <= radius; k++) {
                if (i0 + k < 0 || i0 + k >= n || j0 + k < 0 || j0 + k >= m)
                    continue;

                if (k == 0) {
                    lines[2] += "0";
                }
                else if (board[i0 + k, j0 + k].value == "") {
                    lines[2] += " ";
                }
                else if (board[i0 + k, j0 + k].value == player.character) {
                    lines[2] += "#";
                }
                else {
                    lines[2] += "@";
                }
            }

            for (int k = -radius; k <= radius; k++) {
                if (i0 + k < 0 || i0 + k >= n || j0 - k < 0 || j0 - k >= m)
                    continue;

                if (k == 0) {
                    lines[3] += "0";
                }
                else if (board[i0 + k, j0 - k].value == "") {
                    lines[3] += " ";
                }
                else if (board[i0 + k, j0 - k].value == player.character) {
                    lines[3] += "#";
                }
                else {
                    lines[3] += "@";
                }
            }

            return lines;
        }

        private void CalcWeights(Board board, Player huPlayer, Player aiPlayer, double probably) {
            for (int i = 0; i < availableMoves.Count; i++) {
                string[] lines = getLines(availableMoves[i].i, availableMoves[i].j, 4, huPlayer, board);
                int huImportance = 0;

                for (int line = 0; line < lines.Length; line++) {
                    string testLine = lines[line].Replace("0", "#");
                    
                    for (int t = 0; t < templates.Length; t++) {
                        int index = -1;

                        while ((index = testLine.IndexOf(templates[t].template, index + 1)) > -1) {
                            int j = index;

                            while (j < templates[t].template.Length && lines[line][j] != '0')
                                j++;

                            if (lines[line][j] == '0') {
                                if (templates[t].weight == fiveScore || rnd.NextDouble() < probably)
                                    huImportance += templates[t].weight;
                            }
                        }
                    }
                }

                lines = getLines(availableMoves[i].i, availableMoves[i].j, 4, aiPlayer, board);
                int aiImportance = 0;
                
                for (int line = 0; line < lines.Length; line++) {
                    string testLine = lines[line].Replace("0", "#");

                    for (int t = 0; t < templates.Length; t++) {
                        int index = -1;

                        while ((index = testLine.IndexOf(templates[t].template, index + 1)) > -1) {
                            int j = index;

                            while (j < templates[t].template.Length && lines[line][j] != '0')
                                j++;

                            if (lines[line][j] == '0')
                                if (templates[t].weight == fiveScore || rnd.NextDouble() < probably)
                                    aiImportance += templates[t].weight;
                        }
                    }
                }

                availableMoves[i] = new Move(availableMoves[i].i, availableMoves[i].j, Math.Max(huImportance, aiImportance));
            }
        }

        public Move MakeMove(ref Board board, Player player, Player huPlayer, Player aiPlayer, double probably) {
            CalcWeights(board, huPlayer, aiPlayer, probably);

            int index = 0;

            for (int i = 1; i < availableMoves.Count; i++)
                if (availableMoves[i].importance > availableMoves[index].importance)
                    index = i;

            Move move = availableMoves[index];
            RemoveMove(move.i, move.j);

            board.SetStep(move.i, move.j, player);

            return move;
        }
    }
}