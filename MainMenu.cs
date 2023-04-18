using BookLendingClub.BoxesModule;
using BookLendingClub.FriendsModule;
using BookLendingClub.LoansModule;
using BookLendingClub.MagazinesModule;
using BookLendingClub.Share;

namespace BookLendingClub
{
    internal class MainMenu : Interface
    {
        public FriendsInterface friendsInterface = null;
        public BoxesInterface boxesInterface = null;
        public MagazinesInterface magazinesInterface = null;
        public LoansInterface loansInterface = null;

        List<string> byeMessages = new List<string>()
            {
                "Closing time, take care!",
                "Program closed, time to go take a byte out of the world!",
                "Au revoir!"
            };

        int lastIndex = -1;
        Random random = new Random();

        public void ShowMainMenu()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("main menu", "Friends", "Boxes", "Magazines", "Loans", "Exit");

                switch (selectedOption)
                {
                    case 1: friendsInterface.FriendsOptions();     break;
                    case 2: boxesInterface.BoxesOptions();         break;
                    case 3: magazinesInterface.MagazinesOptions(); break;
                    case 4: loansInterface.LoansOptions();         break;
                    case 5: proceed = false;                       break;
                }
            }

            int index = lastIndex;

            while (index == lastIndex)
                index = random.Next(byeMessages.Count);

            lastIndex = index;

            Console.Clear();

            ColorfulMessage("\n\n" + byeMessages[index], ConsoleColor.DarkCyan);
            ColorfulMessage("\n\n<-'", ConsoleColor.DarkCyan);

            Console.ReadKey();
        }
    }
}
