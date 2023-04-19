using BookLendingClub.BoxesModule;
using BookLendingClub.FriendsModule;
using BookLendingClub.LoansModule;
using BookLendingClub.MagazinesModule;

namespace BookLendingClub.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Boxes -------------------------------------------------------------
            BoxesRepository boxesRepository = new BoxesRepository();

            BoxesInterface boxInterface = new BoxesInterface();
            boxInterface.boxesRepository = boxesRepository;

            // Friends -----------------------------------------------------------
            FriendsRepository friendsRepository = new FriendsRepository();

            FriendsInterface friendsInterface = new FriendsInterface();
            friendsInterface.friendsRepository = friendsRepository;

            // Magazines ----------------------------------------------------------
            MagazinesRepository magazinesRepository = new MagazinesRepository();
            magazinesRepository.boxesRepository = boxesRepository;

            MagazinesInterface magazinesInterface = new MagazinesInterface();
            magazinesInterface.magazinesRepository = magazinesRepository;
            magazinesInterface.boxesRepository = boxesRepository;
            magazinesInterface.boxesInterface = boxInterface;

            // Loans -------------------------------------------------------------
            LoansRepository loansRepository = new LoansRepository();
            loansRepository.magazinesRepository = magazinesRepository;
            loansRepository.friendsRepository = friendsRepository;

            LoansInterface loansInterface = new LoansInterface();
            loansInterface.loansRepository = loansRepository;
            loansInterface.friendsRepository = friendsRepository;
            loansInterface.friendsInterface = friendsInterface;
            loansInterface.magazinesRepository = magazinesRepository;
            loansInterface.magazinesInterface = magazinesInterface;

            // Main Menu ---------------------------------------------------------
            MainMenu mainMenu = new MainMenu();
            mainMenu.friendsInterface = friendsInterface;
            mainMenu.boxesInterface = boxInterface;
            mainMenu.magazinesInterface = magazinesInterface;
            mainMenu.loansInterface = loansInterface;

            mainMenu.ShowMainMenu();
        }
    }
}