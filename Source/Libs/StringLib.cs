namespace SQL_Contact_Wallet_V2
{
    //This class contains every string-related functions that I need for the program
    class StringLib
    {
        //Checks if a given string is only composed by numbers
        public static bool StrIsNum(string str)
        {
            foreach (char c in str)
            {
                if (c < 48 || c > 57)
                    return (false);
            }
            return (true);
        }

        //Checks if a given string is only composed by numbers or spaces
        public static bool StrIsNumSpace(string str)
        {
            foreach (char c in str)
            {
                if ((c < 48 || c > 57) && c != 32)
                    return (false);
            }
            return (true);
        }
    }
}
