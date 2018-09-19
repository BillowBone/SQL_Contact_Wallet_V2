using System;
using System.Data;
using System.Windows.Forms;

namespace SQL_Contact_Wallet_V2
{
    //This class manages every interactions with the contact's card form
    public partial class ContactCard : Form
    {
        private DataRow contact_data_row;

        public ContactCard()
        {
            InitializeComponent();
        }

        //This is the only constructor used here
        //Gets the whole row of the contact thanks to its ID and fills all textboxes with
        public ContactCard(int id)
        {
            InitializeComponent();
            DisableBoxes();
            contact_data_row = ContactsData.GetContactsRowByID(id);
            boxID.Text = contact_data_row[0].ToString();
            boxSocietyID.Text = contact_data_row[1].ToString();
            ManageSocietyName((int)contact_data_row[1]);
            boxTitle.Text = contact_data_row[2].ToString();
            boxSurname.Text = contact_data_row[3].ToString();
            boxName.Text = contact_data_row[4].ToString();
            boxFunction.Text = contact_data_row[5].ToString();
        }

        //Displays the name of a society thanks to its ID given by the user
        //If the ID is not in the society table it writes an error message
        private void ManageSocietyName(int id_society)
        {
            if (SocietiesData.GetSocietyNameByID(id_society) != null)
                boxSociety.Text = SocietiesData.GetSocietyNameByID(id_society);
            else
                boxSociety.Text = "Not in the database";
        }

        //Sends modifications to the database
        private void SaveContactEdition()
        {
            contact_data_row[1] = boxSocietyID.Text;
            contact_data_row[2] = boxTitle.Text;
            contact_data_row[3] = boxSurname.Text;
            contact_data_row[4] = boxName.Text;
            contact_data_row[5] = boxFunction.Text;
            ContactsData.UpdateContact(contact_data_row);
        }

        private void EnableBoxes()
        {
            boxSocietyID.Enabled = true;
            boxTitle.Enabled = true;
            boxSurname.Enabled = true;
            boxName.Enabled = true;
            boxFunction.Enabled = true;
        }

        private void DisableBoxes()
        {
            boxID.Enabled = false;
            boxSocietyID.Enabled = false;
            boxSociety.Enabled = false;
            boxTitle.Enabled = false;
            boxSurname.Enabled = false;
            boxName.Enabled = false;
            boxFunction.Enabled = false;
        }

        //Checks if the non-nullables values aren't null and numbers values are numbers
        //Also checks if the ID of society is valable
        private void EnableSave()
        {
            int society_id = -1;

            Int32.TryParse(boxSocietyID.Text, out society_id);
            if (boxSocietyID.Text != "" && SocietiesData.FindSocietyByID(society_id) != -1
                && StringLib.StrIsNum(boxSocietyID.Text) && boxTitle.Text != ""
                && boxSurname.Text != "" && boxName.Text != "")
                buttonEdit.Enabled = true;
            else
                buttonEdit.Enabled = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Manage the edit and save process
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Text == "Edit")
            {
                buttonEdit.Text = "Save";
                buttonEdit.Enabled = false;
                EnableBoxes();
                EnableSave();
            } else if (buttonEdit.Text == "Save") {
                SaveContactEdition();
                buttonEdit.Text = "Edit";
                DisableBoxes();
            }
        }

        //Sends the ID given by the user to the ManageSocietyName function
        private void boxSocietyID_TextChanged(object sender, EventArgs e)
        {
            int id_society = -1;

            Int32.TryParse(boxSocietyID.Text, out id_society);
            ManageSocietyName(id_society);
            EnableSave();
        }

        private void boxTitle_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        private void boxSurname_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        private void boxName_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }
    }
}
