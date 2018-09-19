using System;
using System.Data;
using System.Data.SqlClient;

namespace SQL_Contact_Wallet_V2
{
    //This class manages every interactions with the contact table
    class ContactsData
    {
        private static SqlConnection my_connection = new SqlConnection(Constants.Connection);
        private static SqlDataAdapter contact_adapter = Initialize.ContactAdapter(my_connection);
        private static DataSet my_dataset = new DataSet(Constants.Contact_table);
        private static string[] contacts;
        private static int[] contacts_id;

        //Fills the dataset with the contact table
        private static void GetContactsData()
        {
            my_connection.Open();
            my_dataset.Clear();
            contact_adapter.Fill(my_dataset, Constants.Contact_table);
            my_connection.Close();
        }

        //Takes a whole dataset with the row's indice of a contact and returns its full name like {name surname}
        private static string GetContactFullName(int indice, DataSet my_dataset)
        {
            string full_name = my_dataset.Tables[Constants.Contact_table].Rows[indice][4] + " "
                                    + my_dataset.Tables[Constants.Contact_table].Rows[indice][3];

            return (full_name);
        }

        //Fill a tab of strings with the name of each contact in the dataset
        private static void FillContactsTabs()
        {
            GetContactsData();
            contacts = new string[my_dataset.Tables[Constants.Contact_table].Rows.Count];
            contacts_id = new int[my_dataset.Tables[Constants.Contact_table].Rows.Count];
            for (int i = 0; i < my_dataset.Tables[Constants.Contact_table].Rows.Count; i++)
            {
                contacts[i] = GetContactFullName(i, my_dataset);
                contacts_id[i] = (int)my_dataset.Tables[Constants.Contact_table].Rows[i][0];
            }
        }

        //Returns the indice of a contact in the table thanks to its ID
        private static int FindContactByID(int id)
        {
            for (int i = 0; i < my_dataset.Tables[Constants.Contact_table].Rows.Count; i++)
            {
                if ((int)my_dataset.Tables[Constants.Contact_table].Rows[i][0] == id)
                    return (i);
            }
            return (-1);
        }

        //Adds a new row to the table
        public static void AddContact(string[] new_contact)
        {
            DataRow to_add = my_dataset.Tables[Constants.Contact_table].NewRow();
            int nb = -1;

            Int32.TryParse(new_contact[0], out nb);
            to_add[1] = nb;
            for (int i = 1; i < 5; i++)
                to_add[i + 1] = new_contact[i];
            my_dataset.Tables[Constants.Contact_table].Rows.Add(to_add);
            my_connection.Open();
            contact_adapter.Update(my_dataset, Constants.Contact_table);
            my_connection.Close();
        }

        //Updates a given row in the table
        public static void UpdateContact(DataRow update_row)
        {
            int index = FindContactByID((int)update_row[0]);

            if (index != -1)
            {
                my_dataset.Tables[Constants.Contact_table].Rows[index][1] = update_row[1];
                my_dataset.Tables[Constants.Contact_table].Rows[index][2] = update_row[2];
                my_dataset.Tables[Constants.Contact_table].Rows[index][3] = update_row[3];
                my_dataset.Tables[Constants.Contact_table].Rows[index][4] = update_row[4];
                my_dataset.Tables[Constants.Contact_table].Rows[index][5] = update_row[5];
                my_connection.Open();
                contact_adapter.Update(my_dataset, Constants.Contact_table);
                my_connection.Close();
            }
        }

        //Deletes a given row in the table
        public static void DeleteContact(int id)
        {
            int index = FindContactByID(id);

            if (index != -1)
            {
                my_dataset.Tables[Constants.Contact_table].Rows[index].Delete();
                my_connection.Open();
                contact_adapter.Update(my_dataset, Constants.Contact_table);
                my_connection.Close();
            }
        }

        //Deletes all the contacts that works at the given society
        public static void DeleteContactsBySocietyID(int society_id)
        {
            GetContactsData();

            for (int i = 0; i < my_dataset.Tables[Constants.Contact_table].Rows.Count; i++)
            {
                if ((int)my_dataset.Tables[Constants.Contact_table].Rows[i][1] == society_id)
                    DeleteContact((int)my_dataset.Tables[Constants.Contact_table].Rows[i][0]);
            }
        }

        //Returns a tab that contains each contact's full name
        public static string[] GetContactsTab()
        {
            FillContactsTabs();
            return (contacts);
        }

        //Returns a tab that contains each contact's ID
        public static int[] GetContactsIDTab()
        {
            FillContactsTabs();
            return (contacts_id);
        }

        //Returns the whole row of a given contact thanks to its ID
        public static DataRow GetContactsRowByID(int id)
        {
            GetContactsData();
            for (int i = 0; i < my_dataset.Tables[Constants.Contact_table].Rows.Count; i++)
            {
                if ((int)my_dataset.Tables[Constants.Contact_table].Rows[i][0] == id)
                    return (my_dataset.Tables[Constants.Contact_table].Rows[i]);
            }
            return (null);
        }

        //Returns a tab that contains the name of each contact working in a given society thanks to its ID
        public static string[] GetContactsBySocietyID(int society_id)
        {
            GetContactsData();
            string[] coworkers = new string[my_dataset.Tables[Constants.Contact_table].Rows.Count];
            int j = 0;

            for (int i = 0; i < my_dataset.Tables[Constants.Contact_table].Rows.Count; i++)
            {
                if ((int)my_dataset.Tables[Constants.Contact_table].Rows[i][1] == society_id)
                {
                    coworkers[j] = GetContactFullName(i, my_dataset);
                    j++;
                }
            }
            return (coworkers);
        }

        //Returns the ID of a society thanks to the ID of a contact working here
        public static int GetSocietyIDByContactID(int contact_id)
        {
            GetContactsData();

            for (int i = 0; i < my_dataset.Tables[Constants.Contact_table].Rows.Count; i++)
            {
                if ((int)my_dataset.Tables[Constants.Contact_table].Rows[i][0] == contact_id)
                    return ((int)my_dataset.Tables[Constants.Contact_table].Rows[i][1]);
            }
            return (-1);
        }
    }
}
