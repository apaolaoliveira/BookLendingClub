using BookLendingClub.FriendsModule;
using BookLendingClub.MagazinesModule;
using BookLendingClub.Share;
using System.Collections;

namespace BookLendingClub.LoansModule
{
    internal class Loans : Entity
    {
        public Friends Friend { get; set; }
        public Magazines Magazine { get; set; }
        public string LoanDate { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }

        public Loans(int loanId, Friends friend, Magazines magazine, string loanDate, string dueDate, string status)
        {
            id = loanId;
            Friend = friend;
            Magazine = magazine;
            LoanDate = loanDate;
            DueDate = dueDate;
            Status = status;
        }
    }
}
