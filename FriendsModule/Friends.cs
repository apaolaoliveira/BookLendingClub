using BookLendingClub.Share;

namespace BookLendingClub.FriendsModule
{
    public class Friends : Entity
    {
        public string Name { get; set; }
        public string Guardian { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; } 

        public Friends(int friendId, string name, string guardian, string address, string phone)
        {
            id = friendId;
            Name = name;
            Guardian = guardian;
            Address = address;
            Phone = phone;
        }
    }
}
