using System;
using System.Windows.Forms;

namespace SQL_Contact_Wallet_V2
{
    //This class manages the form that let you add a contact to the table
    public partial class NewContact : Form
    {
        public NewContact()
        {
            InitializeComponent();
            buttonAdd.Enabled = false;
            boxSociety.Enabled = false;
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

        //Checks if the non-nullables values aren't null and numbers values are numbers
        //Also checks if the ID of society is valable
        private void ManageBoxes()
        {
            int society_id = -1;

            Int32.TryParse(boxSocietyID.Text, out society_id);
            if (boxSocietyID.Text != "" && SocietiesData.FindSocietyByID(society_id) != -1
                && StringLib.StrIsNum(boxSocietyID.Text) && boxTitle.Text != ""
                && boxSurname.Text != "" && boxName.Text != "")
                buttonAdd.Enabled = true;
            else
                buttonAdd.Enabled = false;
        }

        //Puts each textboxes' content in a tab and add it to the contact table before closing the form
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string[] new_contact = new string[5];

            new_contact[0] = boxSocietyID.Text;
            new_contact[1] = boxTitle.Text;
            new_contact[2] = boxSurname.Text;
            new_contact[3] = boxName.Text;
            new_contact[4] = boxFunction.Text;
            ContactsData.AddContact(new_contact);
            this.Close();
        }

        //Sends the ID given by the user to the ManageSocietyName function
        private void boxSocietyID_TextChanged(object sender, EventArgs e)
        {
            int id = -1;

            Int32.TryParse(boxSocietyID.Text, out id);
            ManageSocietyName(id);
            ManageBoxes();
        }

        private void boxTitle_TextChanged(object sender, EventArgs e)
        {
            ManageBoxes();
        }

        private void boxSurname_TextChanged(object sender, EventArgs e)
        {
            ManageBoxes();
        }

        private void boxName_TextChanged(object sender, EventArgs e)
        {
            ManageBoxes();
        }
    }
}
