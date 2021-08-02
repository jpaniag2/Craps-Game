using System;
/**
 * author Julio Paniagua
 * 
 **/
namespace Craps
{
    class Driver
    {
        private static int startChips = 0;
        private static int wager = 0;
        private static int wins = 0;
        private static int losses = 0;
        private static string userName;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to online CRAPS!");
            Console.WriteLine();
            initializeCraps();
        }

        //Starts Craps
        public static void initializeCraps()
        {

            Console.WriteLine("Enter Your Name");
            userName = Console.ReadLine();

            if (userName != null)
            {
                Console.WriteLine();
                Console.WriteLine("Hello, " + userName);
                startChips = 100;
                Console.WriteLine("You have been awarded " + startChips + " Chips.");
                Console.WriteLine("LETS GAMBLE! ");
                Console.WriteLine();

                int currWager = placeWager();
                playRound(startChips, currWager);
            }


        }


        //Plays round of craps, returns dice roll result from round
        public static int playRound(int chips, int roundWager)
        {
            int result = 0;
            int chipsRemaining = chips - roundWager;//subtracting first placed wager from starting chips

            Console.WriteLine("You have " + chipsRemaining + " chips remaining.");
            Console.WriteLine("------------------------------------------");

            if (chips>0 || chipsRemaining > 0)//condition to make sure theres enough chips left
            {

                result = diceThrow(); //dice roll
                Console.WriteLine("You rolled a " + result);
                Console.WriteLine();
                analyzeResults(result, roundWager);


            }
            else //if not enought will be directed to a prompt
            {
                Console.WriteLine("Not Enough Chips");
                if (chipsRemaining < 0)
                {
                    gameOver();
                }
                else
                {
                    placeWager();
                    playRound(chips, roundWager);
                }

            }
            return result;
        }

        //Placing Wager
        public static int placeWager()
        {
            Console.WriteLine("Place your wager: ");
            wager = Convert.ToInt32(Console.ReadLine());

            if (wager > startChips)
            {
                Console.WriteLine("Not enough chips. Place new wager.");

                placeWager();
            }

            return wager;
        }

        //Rolls pair of dice;
        public static int diceThrow()
        {
            Random rand = new Random();
            int sum = rand.Next(1, 7) + rand.Next(1, 7);

            return sum;
        }

        //Analyzing NON-POINT round roll results
        public static void analyzeResults(int results, int currentWager)
        {

            if (results == 7 || results == 11)
            {
                //Automatic win
                Console.WriteLine("Luck is on your side, Automatic Win!");
                roundWin(currentWager);

            }
            else if (results == 2 || results == 3 || results == 12)
            {
                losses++;
                Console.WriteLine("Tough Luck, Automatic Loss.");
                startChips = startChips - currentWager;
                gameOver();
            }
            else
            {
                int point = results;

                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Roll a " + point + " before a 7 to win!");

                initializePointRounds(point, currentWager);
            }

        }

        //Begins players point rounds
        public static void initializePointRounds(int point, int wager)
        {
            Console.WriteLine("Press ENTER to ROLL DICE");
            Console.ReadLine();

            int roll = diceThrow();//Point throws
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("You rolled a " + roll);
            Console.WriteLine();
            Console.WriteLine("Players Point: " + point);

            if (roll == 7)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("OH NO! you rolled a 7 before " + point + ", Automatic Loss!");
                startChips = startChips - wager;
                losses++;
                gameOver();//Takes you to game over prompt
            }
            else if (roll == point)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("NICE! You hit your point!! , AUTOMATIC WIN!");
                Console.WriteLine("------------------------------------------");
                roundWin(wager);//Takes you to round win prompt, doubles wager chips.
            }
            else
            {
                Console.WriteLine("Rolling again!");
                initializePointRounds(point, wager);//Keeps rolling/throwing point rounds
            }
        }

        //Doubles chips if round win
        public static int roundWin(int roundWager)
        {
            wins++;
            startChips = startChips + roundWager;
            Console.WriteLine("You've doubled your chips, " + userName + "! you now have " + startChips + " chips.");

            playAgain();//Takes you to play again prompt

            return startChips;
        }

        //Handles round loss.
        public static void roundLoss(int roundWager)
        {
            losses++;
            Console.WriteLine("Round Lost!");
            if (startChips > 0)
            {
                int currWager = placeWager();
                playRound(startChips, currWager);
            }
            else
            {
                gameOver();
            }

        }

        //Play again prompt handler
        public static void playAgain()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Keep Playing?");
            Console.WriteLine("Press 1 for yes or 2 for no");
            Console.WriteLine("------------------------------------------");
            int continuing = Convert.ToInt32(Console.ReadLine());
            if (continuing == 1)
            {
                int currWager = placeWager();
                playRound(startChips, currWager);
            }
            else if (continuing == 2)
            {
                showStats();
            }
            else
            {

                playAgain();
            }
        }

        //Game over prompt with stats.
        public static void gameOver()
        {

            if (startChips > 0)
            {
                Console.WriteLine();
                Console.WriteLine(userName + ", you have " + startChips + " chips.");
                playAgain();
            }

            else
            {
                if (startChips == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("You have no more chips left to wager, " + userName + ".");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Here Are Your Final Stats:");
                    Console.WriteLine("Last Wager: " + wager);
                    Console.WriteLine("Total Round Wins: " + wins);
                    Console.WriteLine("Total Round Losses: " + losses);
                    Console.WriteLine("Final Chips Remaining: " + startChips);
                }
                else
                {
                    showStats();
                }



            }

        }

        public static void showStats() {

            Console.WriteLine("Game Over, " + userName + " , Collect your chips");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Here Are Your Final Stats:");
            Console.WriteLine("Last Wager: " + wager);
            Console.WriteLine("Total Round Wins: " + wins);
            Console.WriteLine("Total Round Losses: " + losses);
            Console.WriteLine("Final Chips Remaining: " + startChips);
        }
    }
}
