using BookLendingClub.Share;

namespace BookLendingClub.FriendsModule
{
    public class FriendsInterface : Interface //Screen
    {
        public FriendsRepository friendsRepository = null;

        public void FriendsOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                Console.Clear();
                SetMenu("FRIENDS' OPTIONS", "Add new friend.", "View friends' list.", "Edit friends' information.", "Remove a friend.", "Go back.");

                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1: AddNewFriend(); break;
                    case 2: ViewFriends(); break;
                    case 3: EditFriend(); break;
                    case 4: RemoveFriend(); break;
                    case 5: proceed = false; break;
                }
            }
        }

        private void AddNewFriend()
        {
            Console.Clear();
            ColorfulMessage(
              "\n\tADD NEW FRIEND"
            + "\n------------------------------\n", ConsoleColor.Cyan);

            ColorfulMessage("\nName:" + "\n→ ", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ColorfulMessage("\nGuardian:" + "\n→ ", ConsoleColor.Cyan);
            string guardian = Console.ReadLine();

            ColorfulMessage("\nPhone:" + "\n→ ", ConsoleColor.Cyan);
            string phone = Console.ReadLine();

            ColorfulMessage("\nAddress:" + "\n→ ", ConsoleColor.Cyan);
            string address = Console.ReadLine();

            Friends newfriend = new Friends(friendsRepository.idCounter, name, guardian, address, phone);

            friendsRepository.AddNewFriend(newfriend);

            ColorfulMessage("\nNew friend successfully added!", ConsoleColor.Green);

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }

        public void ViewFriends()
        {
            Console.Clear();
            ColorfulMessage("\n\tFRIEND'S LIST :)\n\n", ConsoleColor.Cyan);

            ColorfulMessage(" ----------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
            ColorfulMessage("| ID | NAME                         | GUARDIAN                     | ADDRESS                      | PHONE         |\n", ConsoleColor.Cyan);
            ColorfulMessage(" ----------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

            bool hasFriends = friendsRepository.HasFriends();

            if (hasFriends == true)
            {
                foreach (Friends friend in friendsRepository.list)
                {
                    Console.WriteLine("| {0,-3}| {1,-29}| {2,-29}| {3,-29}| {4,-14}|", friend.id, friend.Name, friend.Guardian, friend.Address, friend.Phone);
                }
            }
            else
            {
                ColorfulMessage("\n                                       You don't have any friend yet :(\n\n", ConsoleColor.Gray);
            }
            Console.Write(" -----------------------------------------------------------------------------------------------------------------");

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        private void EditFriend()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\nEDIT FRIEND"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasFriends = friendsRepository.HasFriends();

            if (hasFriends == false)
            {
                ColorfulMessage("\nYou don't have any friend yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewFriends();

            ColorfulMessage("\n\nEnter your friend's ID:" + "\n→ ", ConsoleColor.Cyan);
            int selectedId = Convert.ToInt32(Console.ReadLine());

            while (selectedId <= 0 || selectedId > friendsRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedId = Convert.ToInt32(Console.ReadLine());
            }

            friendsRepository.GetFriendsId(selectedId);

            ColorfulMessage("\nWhat information would you like to change?\n", ConsoleColor.Cyan);

            ColorfulMessage(
                  "\n[1] Name"
                + "\n[2] Guardian"
                + "\n[3] Phone"
                + "\n[4] Address"
                + "\n\n→ "
                , ConsoleColor.Cyan);

            int selectedChange = Convert.ToInt32(Console.ReadLine());

            bool validOption = false;

            foreach (Friends friend in friendsRepository.list)
            {
                if (friend.id == selectedId)
                {
                    while (!validOption)
                    {
                        switch (selectedChange)
                        {
                            case 1:
                                ColorfulMessage("\nNew name:" + "\n→ ", ConsoleColor.Gray);
                                string newName = Console.ReadLine();

                                friend.Name = newName;

                                ColorfulMessage("\nName updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 2:
                                ColorfulMessage("\nNew Guardian:" + "\n→ ", ConsoleColor.Gray);
                                string newGuardian = Console.ReadLine();

                                friend.Guardian = newGuardian;

                                ColorfulMessage("\nGuardian updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 3:
                                ColorfulMessage("\nNew Phone:" + "\n→ ", ConsoleColor.Gray);
                                string newPhone = Console.ReadLine();

                                friend.Phone = newPhone;

                                ColorfulMessage("\nPhone updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 4:
                                ColorfulMessage("\nNew Address:" + "\n→ ", ConsoleColor.Gray);
                                string newAddress = Console.ReadLine();

                                friend.Address = newAddress;

                                ColorfulMessage("\nAddress updated!", ConsoleColor.Green);
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

        private void RemoveFriend()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\nREMOVE FRIEND"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasFriends = friendsRepository.HasFriends();

            if (hasFriends == false)
            {
                ColorfulMessage("\nYou don't have any friend yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewFriends();

            ColorfulMessage("\n\nEnter your friend's ID:" + "\n→ ", ConsoleColor.Cyan);
            int selectedId = Convert.ToInt32(Console.ReadLine());

            while (selectedId <= 0 || selectedId > friendsRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedId = Convert.ToInt32(Console.ReadLine());
            }

            friendsRepository.RemoveFriend(selectedId);

            ColorfulMessage("\nBye, bye friend!", ConsoleColor.Green);

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }
    }
}
