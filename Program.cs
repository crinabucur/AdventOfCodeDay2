namespace AdventOfCodeDay2
{
    internal class Program
    {
        public static void Main()
        {
            const bool interpretSecondColumnAsDesiredOutcome = true; // false for 1st part of the problem, true for the 2nd
            
            long totalGameScore = 0;
            
            foreach (var line in File.ReadLines(@"C:\Users\CrinaBucur\input.txt"))
            {
                var opponentMove = (int)line[0];

                int myMove;
                if (interpretSecondColumnAsDesiredOutcome)
                {
                    var desiredOutcome = AsciiReduceToAbc(line[2]);
                    myMove = GetIndicatedMoveForOutcome(opponentMove, desiredOutcome);
                }
                else
                { 
                    myMove = AsciiReduceToAbc(line[2]); 
                }
                
                totalGameScore += GetMoveScore(myMove) + GetOutcomeScore(opponentMove, myMove);
            }
            
            Console.WriteLine("Your total game score is {0} ", totalGameScore); 
            Console.ReadLine();  
        }

        private static int AsciiReduceToAbc(int secondColumnCharacter)
        {
            return secondColumnCharacter - 23; // ASCII diff to reduce input to {A, B, C}
        }

        private static int GetMoveScore(int move)
        {
            return move switch
            {
                65 => 1, // A - Rock
                66 => 2, // B - Paper
                67 => 3, // C - Scissors
                _ => throw new InvalidOperationException($"Invalid move {move}")
            };
        }

        private static int GetOutcomeScore(int opponentMove, int myMove)
        {
            if (opponentMove == myMove) return 3; // Draw

            return myMove switch
            {
                65 => // A - Rock
                    opponentMove == 67 ? 6 : 0, // Win if opponent used C - Scissors, lose otherwise
                66 => // B - Paper
                    opponentMove == 65 ? 6 : 0, // Win if opponent used A - Rock, lose otherwise
                67 => // C - Scissors
                    opponentMove == 66 ? 6 : 0, // Win if opponent used B - Paper, lose otherwise
                _ => throw new InvalidOperationException($"Invalid move {myMove}")
            };
        }

        private static int GetIndicatedMoveForOutcome(int opponentMove, int desiredOutcome)
        {
            if (desiredOutcome == 66) return opponentMove; // Draw

            return opponentMove switch
            {
                65 => // A - Rock
                    desiredOutcome == 65 ? 67 : 66, // If we need to lose, show C - Scissors, otherwise B - Paper
                66 => // B - Paper
                    desiredOutcome == 65 ? 65 : 67, // If we need to lose, show A - Rock, otherwise C - Scissors
                67 => // C - Scissors
                    desiredOutcome == 65 ? 66 : 65, // If we need to lose, show B - Paper, otherwise A - Rock
                _ => throw new InvalidOperationException($"Invalid opponent move {opponentMove}")
            };
        }
    }
}