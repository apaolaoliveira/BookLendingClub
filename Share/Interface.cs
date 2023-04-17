
namespace BookLendingClub.Share
{
    public class Interface
    {
        public static void ColorfulMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        public static void SetMenu (string title, string fieldOne, string fieldTwo, string fieldThree, string fieldFour, string fieldFive)
        {
            ColorfulMessage(
              $"\n{title}"
            + $"\n-------------------"
            + $"\n[1] {fieldOne}"
            + $"\n[2] {fieldTwo}"
            + $"\n[3] {fieldThree}"
            + $"\n[4] {fieldFour}"
            + $"\n[5] {fieldFive}"
            + "\n\n→ "
            , ConsoleColor.DarkYellow);
        }
    }
}
