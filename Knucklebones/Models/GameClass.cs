namespace Knucklebones.Models
{
    public class GameClass
    {
        public static int RollTheDice()
        {
            var dice = new Random();
            return dice.Next(1, 6);
        }

    }   
}
