using BookLendingClub.BoxesModule;
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

                Interface.ColorfulMessage("\n\n<-'", ConsoleColor.Red);
                return false;
            }
            else { return true; }
        }

        public bool HasLoans()
        {
            if (list.Count == 0) { return false; }
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

        public bool CanTakeMagazine(int friendId)
        {
            bool friendWithMagazine = false;
            
            foreach (Loans loan in list)
            {
                if (loan.Friend.id == friendId) 
                { 
                    friendWithMagazine = true; 
                    break;
                }
            }

            return friendWithMagazine;
        }

        public void AddNewLoan(Loans loan)
        {
            list.Add(loan);
            loan.id = idCounter;
            increaseId();
        }

        public void RemoveLoan(int selectedId) 
        { 
            Loans loan = GetLoanId(selectedId);
            list.Remove(loan); 
        }

        public Loans GetLoanId(int id)
        {
            Loans loan = null;

            foreach (Loans loanAdded in list)
            {
                if (loanAdded.id == id)
                {
                    loan = loanAdded;
                    break;
                }
            }
            return loan;
        }

    }
}
