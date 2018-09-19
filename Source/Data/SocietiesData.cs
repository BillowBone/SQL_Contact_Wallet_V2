using System;
using System.Data;
using System.Data.SqlClient;

namespace SQL_Contact_Wallet_V2
{
    //This class manages every interactions with the society table
    class SocietiesData
    {
        private static SqlConnection my_connection = new SqlConnection(Constants.Connection);
        private static SqlDataAdapter society_adapter = Initialize.SocietyAdapter(my_connection);
        private static DataSet my_dataset = new DataSet(Constants.Society_table);
        private static string[] societies;
        private static int[] societies_id;

        //Fills the dataset with the society table
        private static void GetSocietiesData()
        {
            my_connection.Open();
            my_dataset.Clear();
            society_adapter.Fill(my_dataset, Constants.Society_table);
            my_connection.Close();
        }

        //Fill a tab of strings with the name of each society in the dataset
        private static void FillSocietiesTabs()
        {
            GetSocietiesData();
            societies = new string[my_dataset.Tables[Constants.Society_table].Rows.Count];
            societies_id = new int[my_dataset.Tables[Constants.Society_table].Rows.Count];
            for (int i = 0; i < my_dataset.Tables[Constants.Society_table].Rows.Count; i++)
            {
                societies[i] = my_dataset.Tables[Constants.Society_table].Rows[i][1].ToString();
                societies_id[i] = (int)my_dataset.Tables[Constants.Society_table].Rows[i][0];
            }
        }

        //Returns the indice of a society in the table thanks to its ID
        public static int FindSocietyByID(int id)
        {
            for (int i = 0; i < my_dataset.Tables[Constants.Society_table].Rows.Count; i++)
            {
                if ((int)my_dataset.Tables[Constants.Society_table].Rows[i][0] == id)
                    return (i);
            }
            return (-1);
        }

        //Adds a new row to the table
        public static void AddSociety(string[] new_society)
        {
            DataRow to_add = my_dataset.Tables[Constants.Society_table].NewRow();

            for (int i = 0; i < 6; i++)
                to_add[i + 1] = new_society[i];
            my_dataset.Tables[Constants.Society_table].Rows.Add(to_add);
            my_connection.Open();
            society_adapter.Update(my_dataset, Constants.Society_table);
            my_connection.Close();
        }

        //Updates a given row in the table
        public static void UpdateSociety(DataRow update_row)
        {
            int index = FindSocietyByID((int)update_row[0]);

            if (index != -1)
            {
                my_dataset.Tables[Constants.Society_table].Rows[index][1] = update_row[1];
                my_dataset.Tables[Constants.Society_table].Rows[index][2] = update_row[2];
                my_dataset.Tables[Constants.Society_table].Rows[index][3] = update_row[3];
                my_dataset.Tables[Constants.Society_table].Rows[index][4] = update_row[4];
                my_dataset.Tables[Constants.Society_table].Rows[index][5] = update_row[5];
                my_dataset.Tables[Constants.Society_table].Rows[index][6] = update_row[6];
                my_connection.Open();
                society_adapter.Update(my_dataset, Constants.Society_table);
                my_connection.Close();
            }
        }

        //Delete a given row in the table
        public static void DeleteSociety(int id)
        {
            int index = FindSocietyByID(id);

            if (index != -1)
            {
                my_dataset.Tables[Constants.Society_table].Rows[index].Delete();
                my_connection.Open();
                society_adapter.Update(my_dataset, Constants.Society_table);
                my_connection.Close();
            }
        }

        //Returns a tab that contains each society's name
        public static string[] GetSocietiesTab()
        {
            FillSocietiesTabs();
            return (societies);
        }

        //Returns a tab taht contains each society's ID
        public static int[] GetSocietiesIDTab()
        {
            FillSocietiesTabs();
            return (societies_id);
        }

        //Returns the whole row of a given society thanks to its ID
        public static DataRow GetSocietiesRowByID(int id)
        {
            GetSocietiesData();
            for (int i = 0; i < my_dataset.Tables[Constants.Society_table].Rows.Count; i++)
            {
                if ((int)my_dataset.Tables[Constants.Society_table].Rows[i][0] == id)
                    return (my_dataset.Tables[Constants.Society_table].Rows[i]);
            }
            return (null);
        }

        //Returns the name of a society thanks to its ID
        public static string GetSocietyNameByID(int id)
        {
            GetSocietiesData();
            int index = FindSocietyByID(id);

            if (index != -1)
                return (my_dataset.Tables[Constants.Society_table].Rows[index][1].ToString());
            else
                return (null);
        }
    }
}
