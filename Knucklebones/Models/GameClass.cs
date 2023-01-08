namespace Knucklebones.Models
{
    public class GameClass
    {
        public static int[][] CheckScore(int[][] arr1, int[][] arr2)
        {
            int[][] newarr2= new int[3][];
            int i = 0;
            do
            {
                foreach (int x in arr1[i])
                {
                    foreach(int y in arr2[i])
                    {
                        if(x != y)
                        {
                            newarr2[i][newarr2[i].Length] = y;
                        }
                    }
                }
                i++;
            }
            while (i < 3);
            return newarr2;   
        }



        public static int RollTheDice()
        {
            var dice = new Random();
            return dice.Next(1, 6);
        }

        public static int PickFirstTurn()
        {
            var rand = new Random();
            return rand.Next(0, 1);
        }
    }   
}
