using BookLendingClub.FriendsModule;
using BookLendingClub.MagazinesModule;
using BookLendingClub.Share;
using System.Globalization;

namespace BookLendingClub.LoansModule
{
    internal class LoansInterface : Interface
    {
        public LoansRepository loansRepository = null;
        public FriendsRepository friendsRepository = null;
        public FriendsInterface friendsInterface = null;
        public MagazinesRepository magazinesRepository = null;
        public MagazinesInterface magazinesInterface = null;

        public void LoansOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                Console.Clear();
                SetMenu("LOANS' OPTIONS", "Add new loan.", "View loans' list.", "Edit loans' information.", "Remove a loan.", "Go back.");

                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1: AddNewLoan(); break;
                    case 2: ViewLoan(); break;
                    case 3: EditLoan(); break;
                    case 4: RemoveLoan(); break;
                    case 5: proceed = false; break;
                }
            }
        }

        private void AddNewLoan()
        {
            Console.Clear();
            ColorfulMessage(
              "\n\nADD NEW LOAN"
            + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasFriendsAndMagazines = loansRepository.HasFriendsAndMagazines();

            if (hasFriendsAndMagazines == false)
            {
                Console.ReadKey();
                return;
            }

            friendsInterface.ViewFriends();

            ColorfulMessage("\n\nFriend's id:" + "\n→ ", ConsoleColor.Cyan);
            int friendId = Convert.ToInt32(Console.ReadLine());

            Friends friend = friendsRepository.GetFriendsId(friendId);

            bool alreadyLoaned = loansRepository.CanTakeMagazine(friendId);

            if (alreadyLoaned == true)
            {
                ColorfulMessage("\nThis friend has already got a magazine!", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            magazinesInterface.ViewMagazines();

            ColorfulMessage("\n\nLoaned Magazine id:" + "\n→ ", ConsoleColor.Cyan);
            int loanedMagazineId = Convert.ToInt32(Console.ReadLine());

            Magazines magazine = magazinesRepository.GetMagazineId(loanedMagazineId);

            ColorfulMessage("\nLoan date:" + "\n→ ", ConsoleColor.Cyan);
            string loanDate = Console.ReadLine();

            ColorfulMessage("\nDue date:" + "\n→ ", ConsoleColor.Cyan);
            string dueDate = Console.ReadLine();

            ColorfulMessage("\nStatus:", ConsoleColor.Cyan);
            ColorfulMessage(
                    "\n[1] ON LOAN"
                  + "\n[2] RETURNED"
                  + "\n[3] LATE"
                  + "\n\n→ "
                   , ConsoleColor.Cyan);
            int statusChoice = Convert.ToInt32(Console.ReadLine());

            string status = loansRepository.GetLoanStatus(statusChoice);

            Loans newLoan = new Loans(loansRepository.idCounter, friend, magazine, loanDate, dueDate, status);

            loansRepository.AddNewLoan(newLoan);

            ColorfulMessage("\nNew loan successfully added!", ConsoleColor.Green);
            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }

        private void ViewLoan()
        {
            Console.Clear();

            ColorfulMessage(
                    "\n[1] Daily view"
                  + "\n[2] Monthly view"
                  + "\n\n→ "
            , ConsoleColor.Cyan);

            int selectedOption = Convert.ToInt32(Console.ReadLine());

            switch (selectedOption)
            {
                case 1:

                    Console.Clear();
                    ColorfulMessage("\n\tDAILY VIEW - LOANS' LIST :)\n\n", ConsoleColor.Cyan);

                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
                    ColorfulMessage("| ID | FRIEND                       | MAGAZINE                     | LOAN DATE  | DUE DATE   | STATUS   |\n", ConsoleColor.Cyan);
                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

                    bool hasLoans = loansRepository.HasLoans();

                    if (hasLoans == true)
                    {
                        foreach (Loans loan in loansRepository.list)
                        {
                            if (loan.Status == "ON LOAN")
                                Console.WriteLine("| {0,-3}| {1,-29}| {2,-29}| {3,-11}| {4,-11}| {5,-9}|", loan.id, loan.Friend.Name, loan.Magazine.Title, loan.LoanDate, loan.DueDate, loan.Status);
                        }
                    }
                    else
                    {
                        ColorfulMessage("\n                                     You don't have any loan yet :(\n\n", ConsoleColor.Gray);
                    }
                    Console.Write(" -------------------------------------------------------------------------------------------------------");

                    ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                    Console.ReadKey();

                    break;

                case 2:

                    Console.Clear();
                    ColorfulMessage("\n\tMONTHLY VIEW - LOANS' LIST :)\n\n", ConsoleColor.Cyan);

                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
                    ColorfulMessage("| ID | FRIEND                       | MAGAZINE                     | LOAN DATE  | DUE DATE   | STATUS   |\n", ConsoleColor.Cyan);
                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

                    bool hassLoans = loansRepository.HasLoans();

                    if (hassLoans == true)
                    {
                        foreach (Loans loan in loansRepository.list)
                        {
                            Console.WriteLine("| {0,-3}| {1,-29}| {2,-29}| {3,-11}| {4,-11}| {5,-9}|", loan.id, loan.Friend.Name, loan.Magazine.Title, loan.LoanDate, loan.DueDate, loan.Status);
                        }
                    }
                    else
                    {
                        ColorfulMessage("\n                                     You don't have any loan yet :(\n\n", ConsoleColor.Gray);
                    }
                    Console.Write(" -------------------------------------------------------------------------------------------------------");

                    ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                    Console.ReadKey();

                    break;

                default:
                    ColorfulMessage("\nInvalid option selected!", ConsoleColor.Red);
                    ColorfulMessage("\n\n<-'", ConsoleColor.Red);
                    Console.ReadKey();
                    return;
            }
        }

        private void EditLoan()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\nEDIT LOAN"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasLoans = loansRepository.HasLoans();

            if (hasLoans == false)
            {
                ColorfulMessage("\nYou don't have any loan yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewLoan();

            ColorfulMessage("\n\nEnter the loan's ID:" + "\n→ ", ConsoleColor.Cyan);
            int selectedId = Convert.ToInt32(Console.ReadLine());

            while (selectedId <= 0 || selectedId > loansRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedId = Convert.ToInt32(Console.ReadLine());
            }

            loansRepository.GetLoanId(selectedId);

            ColorfulMessage("\nWhat information would you like to change?\n", ConsoleColor.Cyan);

            ColorfulMessage(
              "\n[1] Friend"
            + "\n[2] Magazine"
            + "\n[3] Loan Date"
            + "\n[4] Due Date"
            + "\n[5] Status"
            + "\n\n→ "
                , ConsoleColor.Cyan);

            int selectedChange = Convert.ToInt32(Console.ReadLine());

            bool validOption = false;

            foreach (Loans loan in loansRepository.list)
            {
                if (loan.id == selectedId)
                {
                    while (!validOption)
                    {
                        switch (selectedChange)
                        {
                            case 1:
                                ColorfulMessage("\nNew friend, enter the Id:" + "\n→ ", ConsoleColor.Gray);
                                int newFriend = Convert.ToInt32(Console.ReadLine());

                                Friends friend = friendsRepository.GetFriendsId(newFriend);

                                ColorfulMessage("\nFriend updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 2:
                                ColorfulMessage("\nNew Magazine:" + "\n→ ", ConsoleColor.Gray);
                                int newMagazine = Convert.ToInt32(Console.ReadLine());

                                Magazines magazines = magazinesRepository.GetMagazineId(newMagazine);

                                ColorfulMessage("\nMagazine updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 3:
                                ColorfulMessage("\nNew Loan Date:" + "\n→ ", ConsoleColor.Gray);
                                string newLoanDate = Console.ReadLine();

                                loan.LoanDate = newLoanDate;

                                ColorfulMessage("\nLoan Date updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 4:
                                ColorfulMessage("\nNew Due Date:" + "\n→ ", ConsoleColor.Gray);
                                string newDueDate = Console.ReadLine();

                                loan.DueDate = newDueDate;

                                ColorfulMessage("\nDue Date updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 5:
                                ColorfulMessage("\nNew status:", ConsoleColor.Gray);
                                ColorfulMessage(
                                        "\n[1] ON LOAN"
                                      + "\n[2] RETURNED"
                                      + "\n[3] LATE"
                                      + "\n\n→ "
                                       , ConsoleColor.Cyan);

                                int statusChoice = Convert.ToInt32(Console.ReadLine());

                                string newStatus = loansRepository.GetLoanStatus(statusChoice);                                

                                loan.Status = newStatus;

                                ColorfulMessage("\nStatus updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            default:
                                ColorfulMessage("\nInvalid option selected! Try again:" + "\n→ ", ConsoleColor.Red);
                                selectedChange = Convert.ToInt32(Console.ReadLine());
                                break;
                        }
                        break;
                    }
                }
            }
            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        private void RemoveLoan()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\nREMOVE LOAN"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasLoans = loansRepository.HasLoans();

            if (hasLoans == false)
            {
                ColorfulMessage("\nYou don't have any loan yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewLoan();

            ColorfulMessage("\nEnter the loan's ID:" + "\n→ ", ConsoleColor.Cyan);
            int selectedId = Convert.ToInt32(Console.ReadLine());

            while (selectedId <= 0 || selectedId > loansRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedId = Convert.ToInt32(Console.ReadLine());

            }

            friendsRepository.RemoveFriend(selectedId);

            ColorfulMessage("\nLoan sucessfully removed!", ConsoleColor.Green);

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }
    }
}
