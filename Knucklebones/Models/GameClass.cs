namespace Knucklebones.Models
{
    public class GameClass
    {
        public int[] CheckScore(int[,] pillars)
        {
            int [] score = new int[3];

            int scoreValue = 0;
            for(int i = 0; i< 3; i++)
            {


                //for (int j = 0; j < 3; j++)
                //{
                //    scoreValue += pillars[i,j];
                //}
                score[i] = scoreValue;
            }
            return score;
        }



        public int RollTheDice()
        {
            var dice = new Random();
            return dice.Next(1, 6);
        }
    }   
}
