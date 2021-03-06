﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotMachine
{
    /**
     * Karl Kovacs
     * 200327966
     * December 16
     * Slot machine game. Adds your bet to the jackpot if you lose, Otherwise the money is yours!
     */
    public partial class SlotMachineForm : Form
    {
        private int playerMoney = 1000;
        private int winnings = 0;
        private int jackpot = 5000;
        private float turn = 0.0f;
        private int playerBet = 0;
        private float winNumber = 0.0f;
        private float lossNumber = 0.0f;
        private string[] spinResult;
        private string fruits = "";
        private float winRatio = 0.0f;
        private float lossRatio = 0.0f;
        private int grapes = 0;
        private int bananas = 0;
        private int oranges = 0;
        private int cherries = 0;
        private int bars = 0;
        private int bells = 0;
        private int sevens = 0;
        private int blanks = 0;

        private Random random = new Random();

        public SlotMachineForm()
        {
            InitializeComponent();
        }

        /* Utility function to show Player Stats */
        private void showPlayerStats()
        {
            winRatio = winNumber / turn;
            lossRatio = lossNumber / turn;
            string stats = "";
            stats += ("Jackpot: " + jackpot + "\n");
            stats += ("Player Money: " + playerMoney + "\n");
            stats += ("Turn: " + turn + "\n");
            stats += ("Wins: " + winNumber + "\n");
            stats += ("Losses: " + lossNumber + "\n");
            stats += ("Win Ratio: " + (winRatio * 100) + "%\n");
            stats += ("Loss Ratio: " + (lossRatio * 100) + "%\n");
            //MessageBox.Show(stats, "Player Stats");
            TotalCreditsTextBox.Text = Convert.ToString(playerMoney);
        }

        /* Utility function to reset all fruit tallies*/
        private void resetFruitTally()
        {
            grapes = 0;
            bananas = 0;
            oranges = 0;
            cherries = 0;
            bars = 0;
            bells = 0;
            sevens = 0;
            blanks = 0;
        }

        /* Utility function to reset the player stats */
        private void resetAll()
        {
            playerMoney = 1000;
            winnings = 0;
            jackpot = 5000;
            turn = 0;
            playerBet = 0;
            winNumber = 0;
            lossNumber = 0;
            winRatio = 0.0f;
        }

        /* Check to see if the player won the jackpot */
        private void checkJackPot()
        {
            /* compare two random values */
            var jackPotTry = this.random.Next(51) + 1;
            var jackPotWin = this.random.Next(51) + 1;
            if (jackPotTry == jackPotWin)
            {
                MessageBox.Show("You Won the $" + jackpot + " Jackpot!!","Jackpot!!");
                playerMoney += jackpot;
                jackpot = 1000;
            }
        }

        /* Utility function to show a win message and increase player money */
        private void showWinMessage()
        {
            playerMoney += winnings;
           // MessageBox.Show("You Won: $" + winnings, "Winner!");
            AmountWonTextBox.Text = Convert.ToString(winnings);
            resetFruitTally();
            checkJackPot();
        }

        /* Utility function to show a loss message and reduce player money */
        private void showLossMessage()
        {
            playerMoney -= playerBet;
            jackpot = playerBet + jackpot;
            //MessageBox.Show("You Lost!", "Loss!");
            AmountWonTextBox.Text = "0";
            resetFruitTally();
        }

        /* Utility function to check if a value falls within a range of bounds */
        private bool checkRange(int value, int lowerBounds, int upperBounds)
        {
            return (value >= lowerBounds && value <= upperBounds) ? true : false;
            
        }

        /* When this function is called it determines the betLine results.
    e.g. Bar - Orange - Banana */
        private string[] Reels()
        {
            string[] betLine = { " ", " ", " " };
            int[] outCome = { 0, 0, 0 };

            for (var spin = 0; spin < 3; spin++)
            {
                outCome[spin] = this.random.Next(65) + 1;

               if (checkRange(outCome[spin], 1, 27)) {  // 41.5% probability
                    betLine[spin] = "blank";
                    blanks++;
                    }
                else if (checkRange(outCome[spin], 28, 37)){ // 15.4% probability
                    betLine[spin] = "Grapes";
                    grapes++;
                }
                else if (checkRange(outCome[spin], 38, 46)){ // 13.8% probability
                    betLine[spin] = "Banana";
                    bananas++;
                }
                else if (checkRange(outCome[spin], 47, 54)){ // 12.3% probability
                    betLine[spin] = "Orange";
                    oranges++;
                }
                else if (checkRange(outCome[spin], 55, 59)){ //  7.7% probability
                    betLine[spin] = "Cherry";
                    cherries++;
                }
                else if (checkRange(outCome[spin], 60, 62)){ //  4.6% probability
                    betLine[spin] = "Bar";
                    bars++;
                }
                else if (checkRange(outCome[spin], 63, 64)){ //  3.1% probability
                    betLine[spin] = "Bell";
                    bells++;
                }
                else if (checkRange(outCome[spin], 65, 65)){ //  1.5% probability
                    betLine[spin] = "Seven";
                    sevens++;
                }

            }
            return betLine;
        }

        /* This function calculates the player's winnings, if any */
        private void determineWinnings()
        {
            if (blanks == 0)
            {
                if (grapes == 3)
                {
                    winnings = playerBet * 10;
                }
                else if (bananas == 3)
                {
                    winnings = playerBet * 20;
                }
                else if (oranges == 3)
                {
                    winnings = playerBet * 30;
                }
                else if (cherries == 3)
                {
                    winnings = playerBet * 40;
                }
                else if (bars == 3)
                {
                    winnings = playerBet * 50;
                }
                else if (bells == 3)
                {
                    winnings = playerBet * 75;
                }
                else if (sevens == 3)
                {
                    winnings = playerBet * 100;
                }
                else if (grapes == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (bananas == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (oranges == 2)
                {
                    winnings = playerBet * 3;
                }
                else if (cherries == 2)
                {
                    winnings = playerBet * 4;
                }
                else if (bars == 2)
                {
                    winnings = playerBet * 5;
                }
                else if (bells == 2)
                {
                    winnings = playerBet * 10;
                }
                else if (sevens == 2)
                {
                    winnings = playerBet * 20;
                }
                else if (sevens == 1)
                {
                    winnings = playerBet * 5;
                }
                else
                {
                    winnings = playerBet * 1;
                }
                winNumber++;
                showWinMessage();
            }
            else
            {
                lossNumber++;
                showLossMessage();
            }

        }

        private void SpinPictureBox_Click(object sender, EventArgs e)
        {
            JackPotTextBox.Text = Convert.ToString(jackpot);
            AmountWonTextBox.Text = "";
            if (BetTextBox.Text == "")
            {
                playerBet = 10; // default bet amount
            }
            else
            {
                playerBet = Convert.ToInt16(BetTextBox.Text);
                BetTextBox.Text = "";
            }
            

            if (playerMoney == 0)
            {
                if (MessageBox.Show("You ran out of Money! \nDo you want to play again?","Out of Money!",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    resetAll();
                    showPlayerStats();
                }
            }
            else if (playerBet > playerMoney)
            {
                MessageBox.Show("You don't have enough Money to place that bet.", "Insufficient Funds");
            }
            else if (playerBet < 0)
            {
                MessageBox.Show("All bets must be a positive $ amount.", "Incorrect Bet");
            }
            else if (playerBet <= playerMoney)
            {
                spinResult = Reels();
                fruits = spinResult[0] + " - " + spinResult[1] + " - " + spinResult[2];
                chooseImage();
                determineWinnings();
                //MessageBox.Show(fruits);
                turn++;
                showPlayerStats();
            }
            else
            {
                MessageBox.Show("Please enter a valid bet amount");
            }

        }
        public void chooseImage()
        {
            //Check first spin result for picturebox 1
            if (spinResult[0] == "Banana")
            {
                SlotPictureBox1.Image = Properties.Resources.banana;
            }
            if (spinResult[0] == "blank")
            {
                SlotPictureBox1.Image = Properties.Resources.blank;
            }
            if (spinResult[0] == "Grapes")
            {
                SlotPictureBox1.Image = Properties.Resources.grapes;
            }
            if (spinResult[0] == "Orange")
            {
                SlotPictureBox1.Image = Properties.Resources.orange;
            }
            if (spinResult[0] == "Cherry")
            {
                SlotPictureBox1.Image = Properties.Resources.cherry;
            }
            if (spinResult[0] == "Bar")
            {
                SlotPictureBox1.Image = Properties.Resources.bar;
            }
            if (spinResult[0] == "Bell")
            {
                SlotPictureBox1.Image = Properties.Resources.bell;
            }
            if (spinResult[0] == "Seven")
            {
                SlotPictureBox1.Image = Properties.Resources.seven;
            }

            //Check first spin result for picturebox 2
            if (spinResult[1] == "Banana")
            {
                SlotPictureBox2.Image = Properties.Resources.banana;
            }
            if (spinResult[1] == "blank")
            {
                SlotPictureBox2.Image = Properties.Resources.blank;
            }
            if (spinResult[1] == "Grapes")
            {
                SlotPictureBox2.Image = Properties.Resources.grapes;
            }
            if (spinResult[1] == "Orange")
            {
                SlotPictureBox2.Image = Properties.Resources.orange;
            }
            if (spinResult[1] == "Cherry")
            {
                SlotPictureBox2.Image = Properties.Resources.cherry;
            }
            if (spinResult[1] == "Bar")
            {
                SlotPictureBox2.Image = Properties.Resources.bar;
            }
            if (spinResult[1] == "Bell")
            {
                SlotPictureBox2.Image = Properties.Resources.bell;
            }
            if (spinResult[1] == "Seven")
            {
                SlotPictureBox2.Image = Properties.Resources.seven;
            }
            //Check first spin result for picturebox 3
            if (spinResult[2] == "Banana")
            {
                SlotPictureBox3.Image = Properties.Resources.banana;
            }
            if (spinResult[2] == "blank")
            {
                SlotPictureBox3.Image = Properties.Resources.blank;
            }
            if (spinResult[2] == "Grapes")
            {
                SlotPictureBox3.Image = Properties.Resources.grapes;
            }
            if (spinResult[2] == "Orange")
            {
                SlotPictureBox3.Image = Properties.Resources.orange;
            }
            if (spinResult[2] == "Cherry")
            {
                SlotPictureBox3.Image = Properties.Resources.cherry;
            }
            if (spinResult[2] == "Bar")
            {
                SlotPictureBox3.Image = Properties.Resources.bar;
            }
            if (spinResult[2] == "Bell")
            {
                SlotPictureBox3.Image = Properties.Resources.bell;
            }
            if (spinResult[2] == "Seven")
            {
                SlotPictureBox3.Image = Properties.Resources.seven;
            }
        }

        private void Bet5PictureBox_Click(object sender, EventArgs e)
        {
            BetTextBox.Text = "5";
        }

        private void Bet10PictureBox_Click(object sender, EventArgs e)
        {
            BetTextBox.Text = "10";
        }

        private void Bet25PictureBox_Click(object sender, EventArgs e)
        {
            BetTextBox.Text = "25";
        }

        private void Bet50PictureBox_Click(object sender, EventArgs e)
        {
            BetTextBox.Text = "50";
        }

        private void SlotMachineForm_Load(object sender, EventArgs e)
        {
            TotalCreditsTextBox.Text = Convert.ToString(playerMoney);
        }
    }
   

}
