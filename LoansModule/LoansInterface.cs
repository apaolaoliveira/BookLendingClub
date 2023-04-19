using BookLendingClub.FriendsModule;
using BookLendingClub.MagazinesModule;
using BookLendingClub.Share;

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
                int selectedOption = SetMenu("loans' options", "Add new loan", "View loans' list", "Edit loans' information", "Remove a loan", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewLoan();    break;
                    case 2: ViewLoan();      break;
                    case 3: EditLoan();      break;
                    case 4: RemoveLoan();    break;
                    case 5: proceed = false; break;
                }
            }
        }

        private void AddNewLoan()
        {
            SetHeader("add new loan");

            bool hasFriendsAndMagazines = loansRepository.HasFriendsAndMagazines();

            if (hasFriendsAndMagazines == false)
            {
                SetFooter();
                return;
            }

            friendsInterface.ViewFriends();

            int friendId = SetIntField("\nFriend's id:", ConsoleColor.Cyan);
            int newfriendId = friendsRepository.isValidId(friendId, friendsRepository);

            Friends friend = (Friends)friendsRepository.GetId(newfriendId, friendsRepository);

            bool alreadyHasMagazine = loansRepository.kidHasMagazine(newfriendId);
            if (alreadyHasMagazine == true)
            {
                ColorfulMessage("\nThis friend has already got a magazine!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            magazinesInterface.ViewMagazines();

            int loanedMagazineId = SetIntField("\nLoaned Magazine id:", ConsoleColor.Cyan);
            int newloanedMagazineId = magazinesRepository.isValidId(loanedMagazineId, magazinesRepository);

            Magazines magazine = (Magazines)magazinesRepository.GetId(newloanedMagazineId, magazinesRepository);

            bool magazineIsLoaned = loansRepository.CanTakeMagazine(newloanedMagazineId);
            if (magazineIsLoaned == false)
            {
                ColorfulMessage($"\nThis magazine is currently unavailable." +
                                $"\nA friend has already borrowed it."
                                , ConsoleColor.Gray);
                SetFooter();
                return;
            }

            string loanDate = SetStringField("Loan date:", ConsoleColor.Cyan);

            string dueDate = SetStringField("Due date:", ConsoleColor.Cyan);

            ColorfulMessage("\nStatus:", ConsoleColor.Cyan);

            int statusChoice = SetIntField(
                                 "\n[1] ON LOAN"
                               + "\n[2] RETURNED"
                               + "\n[3] LATE"
                               , ConsoleColor.Cyan);

            string status = loansRepository.GetLoanStatus(statusChoice);

            Loans newLoan = new Loans(loansRepository.idCounter, friend, magazine, loanDate, dueDate, status);

            loansRepository.AddNewEntity(newLoan);

            ColorfulMessage("\nNew loan successfully added!", ConsoleColor.Green);
            SetFooter();
        }

        private void ViewLoan()
        {
            Console.Clear();

            int selectedOption = SetIntField(
                                   "\n[1] Daily view"
                                 + "\n[2] Monthly view"
                                 , ConsoleColor.Cyan);

            switch (selectedOption)
            {
                case 1:
                    Console.Clear();

                    ColorfulMessage("\n\tDAILY VIEW - LOANS' LIST :)\n\n", ConsoleColor.Cyan);

                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
                    ColorfulMessage("| ID | FRIEND                       | MAGAZINE                     | LOAN DATE  | DUE DATE   | STATUS   |\n", ConsoleColor.Cyan);
                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

                    bool hasLoans = loansRepository.HasEntity();

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

                    SetFooter();

                    break;

                case 2:
                    Console.Clear();

                    ColorfulMessage("\n\tMONTHLY VIEW - LOANS' LIST :)\n\n", ConsoleColor.Cyan);

                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
                    ColorfulMessage("| ID | FRIEND                       | MAGAZINE                     | LOAN DATE  | DUE DATE   | STATUS   |\n", ConsoleColor.Cyan);
                    ColorfulMessage(" ------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

                    bool hassLoans = loansRepository.HasEntity();

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

                    SetFooter();

                    break;

                default:
                    ColorfulMessage("\nInvalid option selected!", ConsoleColor.Red);
                    SetFooter();
                    return;
            }
        }

        private void EditLoan()
        {
            SetHeader("edit loan");

            bool hasLoans = loansRepository.HasEntity();

            if (hasLoans == false)
            {
                ColorfulMessage("\nYou don't have any loan yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewLoan();

            int selectedId = SetIntField("\nEnter the loan's ID:", ConsoleColor.Cyan);

            int newSelectedId = loansRepository.isValidId(selectedId, loansRepository);

            loansRepository.GetId(newSelectedId, loansRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);
                       
            int selectedChange = SetIntField(
                                   "\n[1] Friend"
                                 + "\n[2] Magazine"
                                 + "\n[3] Loan Date"
                                 + "\n[4] Due Date"
                                 + "\n[5] Status"
                                 , ConsoleColor.Cyan);
             
            bool validOption = false;

            foreach (Loans loan in loansRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            int newFriend = SetIntField("New friend, enter the Id:", ConsoleColor.Gray);

                            Friends friend = (Friends)friendsRepository.GetId(newFriend, friendsRepository);

                            ColorfulMessage("\nFriend updated!", ConsoleColor.Green);
                            validOption = true;
                            break;
                        case 2:
                            int newMagazine = SetIntField("New Magazine:", ConsoleColor.Gray);

                            Magazines magazines = (Magazines)magazinesRepository.GetId(newMagazine, magazinesRepository);

                            ColorfulMessage("\nMagazine updated!", ConsoleColor.Green);
                            validOption = true;
                            break;
                        case 3:
                            string newLoanDate = SetStringField("New Loan Date:", ConsoleColor.Gray);
                            loan.LoanDate = newLoanDate;

                            ColorfulMessage("\nLoan Date updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            string newDueDate = SetStringField("New Due Date:", ConsoleColor.Gray);
                            loan.DueDate = newDueDate;

                            ColorfulMessage("\nDue Date updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 5:
                            ColorfulMessage("\nNew status:", ConsoleColor.Gray);

                            int statusChoice = SetIntField(
                                                 "\n[1] ON LOAN"
                                               + "\n[2] RETURNED"
                                               + "\n[3] LATE"
                                               , ConsoleColor.Gray);

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
            SetFooter();
        }

        private void RemoveLoan()
        {
            SetHeader("remove loan");

            bool hasLoans = loansRepository.HasEntity();

            if (hasLoans == false)
            {
                ColorfulMessage("\nYou don't have any loan yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewLoan();

            int selectedId = SetIntField("\nEnter the loan's ID:", ConsoleColor.Cyan);

            int newSelectedId = loansRepository.isValidId(selectedId, loansRepository);

            loansRepository.RemoveEntity(newSelectedId, loansRepository);

            ColorfulMessage("\nLoan sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
