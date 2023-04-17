namespace BookLendingClub.FriendsModule
{
    public class Friends //Entity
    {
        public string Name { get; set; }
        public string Guardian { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public int id; 

        public Friends(int id, string name, string guardian, string address, string phone)
        {
            this.id = id;
            Name = name;
            Guardian = guardian;
            Address = address;
            Phone = phone;
        }
    }
}
