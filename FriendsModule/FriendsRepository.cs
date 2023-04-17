using BookLendingClub.Share;

namespace BookLendingClub.FriendsModule
{
    public class FriendsRepository : Repository//Repository
    {
        public bool HasFriends()
        {
            if (list.Count == 0) { return false; }
            else { return true; }
        }

        public void AddNewFriend(Friends friend)
        {
            list.Add(friend);
            friend.id = idCounter;
            increaseId();
        }

        public void RemoveFriend(int selectedId) 
        { 
            Friends friend = GetFriendsId(selectedId);
            list.Remove(friend); 
        }

        public Friends GetFriendsId(int id)
        {
            Friends friend = null;

            foreach (Friends friendAdded in list)
            {
                if (friendAdded.id == id)
                {
                    friend = friendAdded;
                    break;
                }
            }
            return friend;
        }
    }
}
