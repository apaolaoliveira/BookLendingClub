
using System.Drawing;

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

        public static int SetMenu(string title, string fieldOne, string fieldTwo, string fieldThree, string fieldFour, string fieldFive)
        {
            Console.Clear();

            ColorfulMessage(
              $"\n{title.ToUpper()}"
            + $"\n-------------------"
            + $"\n[1] {fieldOne}."
            + $"\n[2] {fieldTwo}."
            + $"\n[3] {fieldThree}."
            + $"\n[4] {fieldFour}."
            + $"\n[5] {fieldFive}."
            + "\n\n→ "
            , ConsoleColor.DarkYellow);

            int selectedOption = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            return selectedOption;
        }

        public static void SetHeader(string header)
        {
            Console.Clear();

            ColorfulMessage(
              $"\n\n{header.ToUpper()}"
            + "\n------------------------------\n"
            , ConsoleColor.Cyan);
        }

        public static void SetFooter()
        {
            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }

        public static string SetStringField(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(
                $"\n{message}"
                + "\n→ ");
            Console.ResetColor();

            string reply = Console.ReadLine();

            return reply;
        }

        public static int SetIntField(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(
                $"\n{message}"
                + "\n→ ");
            Console.ResetColor();

            int reply = Convert.ToInt32(Console.ReadLine());

            return reply;
        }
    }
}
