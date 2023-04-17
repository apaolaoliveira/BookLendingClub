using BookLendingClub.FriendsModule;
using BookLendingClub.MagazinesModule;
using System.Collections;

namespace BookLendingClub.LoansModule
{
    internal class Loans
    {
        public Friends Friend { get; set; }
        public Magazines Magazine { get; set; }
        public string LoanDate { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }

        public int id;

        public Loans(int id, Friends friend, Magazines magazine, string loanDate, string dueDate, string status)
        {
            this.id = id;
            Friend = friend;
            Magazine = magazine;
            LoanDate = loanDate;
            DueDate = dueDate;
            Status = status;
        }
    }
}
