using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms_Practice
{
    public partial class SinglePlayerUC : UserControl
    {
        public SinglePlayerUC()
        {
            InitializeComponent();

            InitializeCoins();
        }

        private static int[][] coinLocation = new int[7][];
        private static int pressRow = 0, pressCol = 0;
        private static int releaseRow = 0, releaseCol = 0;
        private static Control selectedCoin = new Control();
        private static Point screenPoint = new Point(), clientPoint = new Point();
        private static bool isPressed = false;
        private static int yourScore = 1, highScore = 0;

        private void ButtonSplitForm_MouseDown(object sender, MouseEventArgs e)
        {
            chessBoardPanel.ResumeLayout();
            selectedCoin = sender as Control;
            screenPoint = selectedCoin.PointToScreen(new Point(e.X, e.Y));
            clientPoint = chessBoardPanel.PointToClient(screenPoint);

            pressCol = clientPoint.X / (chessBoardPanel.Width / 7);
            pressRow = clientPoint.Y / (chessBoardPanel.Height / 7);

            isPressed = true;

        }


        private void ButtonSplitForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                isPressed = false;
                selectedCoin = sender as Control;
                screenPoint = selectedCoin.PointToScreen(new Point(e.X, e.Y));
                clientPoint = chessBoardPanel.PointToClient(screenPoint);

                releaseCol = clientPoint.X / (chessBoardPanel.Width / 7);
                releaseRow = clientPoint.Y / (chessBoardPanel.Height / 7);

                if (clientPoint.X > 0 && clientPoint.X < chessBoardPanel.Width && clientPoint.Y > 0 && clientPoint.Y < chessBoardPanel.Height && isInPath(releaseRow, releaseCol) && isValidMovement())
                {
                    chessBoardPanel.Controls.Add(selectedCoin, releaseCol, releaseRow);
                    coinLocation[pressRow][pressCol] = 0;
                    coinLocation[releaseRow][releaseCol] = 1;

                    yourScore = Convert.ToInt32(yourScoreDisplayPanel.Text); yourScore++;
                    yourScoreDisplayPanel.Text = Convert.ToString(yourScore);
                }

                isGameOver();
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            chessBoardPanel.SuspendLayout();
            RemoveCoins();
            InitializeCoins();
            chessBoardPanel.ResumeLayout();
            highScore = Math.Max(highScore, yourScore);
            highScoreDisplayPanel.Text = Convert.ToString(highScore);
            yourScore = 0;
            yourScoreDisplayPanel.Text = "0";
        }

        private bool isInPath(int ctr, int ptr)
        {
            if ((ctr <= 1 && ptr <= 4 && ptr >= 2) || ((ctr >= 5 && ptr <= 4 && ptr >= 2)) || (ctr <= 4 && ctr >= 2))
                return true;
            else
                return false;
        }

        private bool isValidMovement()
        {
            if (pressRow == releaseRow && pressCol + 2 == releaseCol && coinLocation[pressRow][pressCol + 1] == 1 && coinLocation[releaseRow][releaseCol] == 0)
            {
                coinLocation[pressRow][pressCol + 1] = 0;
                chessBoardPanel.Controls.Remove(chessBoardPanel.GetControlFromPosition(pressCol + 1, pressRow));
                return true;
            }
            else if (pressRow == releaseRow && pressCol - 2 == releaseCol && coinLocation[pressRow][pressCol - 1] == 1 && coinLocation[releaseRow][releaseCol] == 0)
            {
                coinLocation[pressRow][pressCol - 1] = 0;
                chessBoardPanel.Controls.Remove(chessBoardPanel.GetControlFromPosition(pressCol - 1, pressRow));
                return true;
            }
            else if (pressCol == releaseCol && pressRow - 2 == releaseRow && coinLocation[pressRow - 1][pressCol] == 1 && coinLocation[releaseRow][releaseCol] == 0)
            {
                coinLocation[pressRow - 1][pressCol] = 0;
                chessBoardPanel.Controls.Remove(chessBoardPanel.GetControlFromPosition(pressCol, pressRow - 1));
                return true;
            }
            else if (pressCol == releaseCol && pressRow + 2 == releaseRow && coinLocation[pressRow + 1][pressCol] == 1 && coinLocation[releaseRow][releaseCol] == 0)
            {
                coinLocation[pressRow + 1][pressCol] = 0;
                chessBoardPanel.Controls.Remove(chessBoardPanel.GetControlFromPosition(pressCol, pressRow + 1));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitializeCoins()
        {
            for (int ctr = 0; ctr < 7; ctr++)
            {
                coinLocation[ctr] = new int[7];
                for (int ptr = 0; ptr < 7; ptr++)
                {
                    if ((ctr <= 1 && ptr <= 4 && ptr >= 2) || ((ctr >= 5 && ptr <= 4 && ptr >= 2)) || (ctr <= 4 && ctr >= 2))
                    {
                        if (ctr == 3 && ptr == 3)
                            continue;

                        Control C = new Button();
                        C.BackColor = Color.Orange;
                        C.Margin = new Padding(0);
                        chessBoardPanel.Controls.Add(C, ptr, ctr);
                        C.Dock = DockStyle.Fill;

                        C.MouseDown += ButtonSplitForm_MouseDown;
                        C.MouseUp += ButtonSplitForm_MouseUp;

                        coinLocation[ctr][ptr] = 1;
                    }
                    else
                    {
                        coinLocation[ctr][ptr] = -1;
                    }
                }
            }
        }

        private void RemoveCoins()
        {
            for (int ctr = 0; ctr < 7; ctr++)
            {
                for (int ptr = 0; ptr < 7; ptr++)
                {
                    if ((ctr <= 1 && ptr <= 4 && ptr >= 2) || ((ctr >= 5 && ptr <= 4 && ptr >= 2)) || (ctr <= 4 && ctr >= 2))
                    {
                        if (chessBoardPanel.GetControlFromPosition(ptr, ctr) != null)
                        {
                            chessBoardPanel.Controls.Remove(chessBoardPanel.GetControlFromPosition(ptr, ctr));
                        }
                    }
                }
            }
        }

        private void isGameOver()
        {
            for (int ctr = 0; ctr < 7; ctr++)
            {
                for (int ptr = 0; ptr < 7; ptr++)
                {
                    if ((ctr <= 1 && ptr <= 4 && ptr >= 2) || ((ctr >= 5 && ptr <= 4 && ptr >= 2)) || (ctr <= 4 && ctr >= 2))
                    {
                        if ((ptr + 2) < 7 && ((coinLocation[ctr][ptr] == 1 && coinLocation[ctr][ptr + 1] == 1 && coinLocation[ctr][ptr + 2] == 0) || (coinLocation[ctr][ptr] == 0 && coinLocation[ctr][ptr + 1] == 1 && coinLocation[ctr][ptr + 2] == 1)))
                        {
                            return;
                        }
                    }

                }
            }

            for (int ptr = 0; ptr < 7; ptr++)
            {
                for (int ctr = 0; ctr < 7; ctr++)
                {
                    if ((ctr <= 1 && ptr <= 4 && ptr >= 2) || ((ctr >= 5 && ptr <= 4 && ptr >= 2)) || (ctr <= 4 && ctr >= 2))
                    {
                        if ((ctr + 2) < 7 && ((coinLocation[ctr][ptr] == 1 && coinLocation[ctr + 1][ptr] == 1 && coinLocation[ctr + 2][ptr] == 0) || (coinLocation[ctr][ptr] == 0 && coinLocation[ctr + 1][ptr] == 1 && coinLocation[ctr + 2][ptr] == 1)))
                        {
                            return;
                        }
                    }
                }
            }

            MessageBox.Show("Game Over", "Cross Chess Game");
            chessBoardPanel.SuspendLayout();
            RemoveCoins();
            InitializeCoins();
            chessBoardPanel.ResumeLayout();
            highScore = Math.Max(highScore, yourScore);
            highScoreDisplayPanel.Text = Convert.ToString(highScore);
            yourScore = 0;
            yourScoreDisplayPanel.Text = "0";
        }
    }
}
