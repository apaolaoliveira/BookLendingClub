using BookLendingClub.FriendsModule;
using BookLendingClub.MagazinesModule;
using BookLendingClub.Share;

namespace BookLendingClub.LoansModule
{
    internal class LoansRepository : Repository
    {
        public FriendsRepository friendsRepository = null;
        public MagazinesRepository magazinesRepository = null;

        public bool HasFriendsAndMagazines()
        {
            if (magazinesRepository.list.Count == 0 || friendsRepository.list.Count == 0)
            {
                if (magazinesRepository.list.Count == 0 && friendsRepository.list.Count == 0)
                    Interface.ColorfulMessage("\nYou need to have at least one friend and one magazine to make a loan.", ConsoleColor.Red);

                else if (magazinesRepository.list.Count == 0)
                    Interface.ColorfulMessage("\nYou need to have at least one magazine to make a loan.", ConsoleColor.Red);

                else
                    Interface.ColorfulMessage("\nYou need to have at least one friend to make a loan.", ConsoleColor.Red);

                return false;
            }
            else { return true; }
        }

        public string GetLoanStatus(int statusChoice)
        {
            switch (statusChoice)
            {
                case 1: return "ON LOAN";
                case 2: return "RETURNED";
                case 3: return "LATE";
                default: throw new ArgumentException("Invalid status choice.");
            }
        }

        public bool kidHasMagazine(int friendId)
        {
            bool kidHasMagazine = false; 
            
            foreach (Loans loan in list)
            {
                if (loan.Friend.id == friendId) 
                {
                    if (loan.Status != "RETURNED")
                    {
                        kidHasMagazine = true; 
                        break;
                    }
                }
            }

            return kidHasMagazine;
        }     

        public bool CanTakeMagazine(int magazineId)
        {
            bool CanTakeMagazine = true;

            foreach (Loans loan in list)
            {
                if (loan.Magazine.id == magazineId)
                {
                    if (loan.Status != "RETURNED")
                    {
                        CanTakeMagazine = false;
                        break;
                    }
                }
            }

            return CanTakeMagazine;
        }
    }
}
