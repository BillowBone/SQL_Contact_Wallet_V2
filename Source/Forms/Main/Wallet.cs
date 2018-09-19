using System;
using System.Windows.Forms;

namespace SQL_Contact_Wallet_V2
{
    //This class manages the main form, which displays rather the contacts table or the societies table
    //and their 3 options : Edit, Delete & Add
    public partial class Wallet : Form
    {
        //Equals 1 when the contacts button has the control and 2 for the societies button
        private int list_control = 0;

        public Wallet()
        {
            InitializeComponent();
            buttonDetails.Enabled = false;
            buttonDelete.Enabled = false;
            buttonAdd.Enabled = false;
        }

        //Gets the table of contacts from the DB and add it to the listbox
        private void InsertContacts()
        {
            string[] contacts = ContactsData.GetContactsTab();

            list_control = 1;
            buttonDetails.Enabled = false;
            buttonDelete.Enabled = false;
            buttonDetails.Text = "";
            buttonDelete.Text = "";
            listBox1.Items.Clear();
            foreach (string str in contacts)
            {
                listBox1.Items.Add(str);
            }
        }

        //Gets the table of societies from the DB and add it to the listbox
        private void InsertSocieties()
        {
            string[] societies = SocietiesData.GetSocietiesTab();

            list_control = 2;
            buttonDetails.Enabled = false;
            buttonDelete.Enabled = false;
            buttonDetails.Text = "";
            buttonDelete.Text = "";
            listBox1.Items.Clear();
            foreach (string str in societies)
            {
                listBox1.Items.Add(str);
            }
        }

        //Displays the contact table and its options
        private void buttonContacts_Click(object sender, EventArgs e)
        {
            InsertContacts();
            buttonAdd.Enabled = true;
            buttonAdd.Text = "Add a contact";
        }

        //Displays the society table and its options
        private void buttonSocieties_Click(object sender, EventArgs e)
        {
            InsertSocieties();
            buttonAdd.Enabled = true;
            buttonAdd.Text = "Add a society";
        }

        //Enables details and delete options for the current control option
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_control == 1 && listBox1.SelectedIndex != -1) {
                buttonDetails.Enabled = true;
                buttonDelete.Enabled = true;
                buttonDetails.Text = "Contact full card";
                buttonDelete.Text = "Delete this contact";
            } else if (list_control == 2 && listBox1.SelectedIndex != -1) {
                buttonDetails.Enabled = true;
                buttonDelete.Enabled = true;
                buttonDetails.Text = "Society full card";
                buttonDelete.Text = "Delete this society";
            }
        }

        //Opens a new form which contains details about the contact or society and let you edit it
        private void buttonDetails_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if (buttonDetails.Text == "Contact full card") {
                int[] contacts_id = ContactsData.GetContactsIDTab();
                ContactCard contact_details = new ContactCard(contacts_id[index]);
                contact_details.ShowDialog();
                InsertContacts();
            } else if (buttonDetails.Text == "Society full card") {
                int[] societies_id = SocietiesData.GetSocietiesIDTab();
                SocietyCard society_details = new SocietyCard(societies_id[index]);
                society_details.ShowDialog();
                InsertSocieties();
            }
        }

        //Deletes the selected item rather if it's a contact or a society
        //Asks you if you really wants to delete the selected item before
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            DialogResult res = DialogResult.No;

            if (buttonDelete.Text == "Delete this contact")
                res = MessageBox.Show("Are you sure you want to delete the contact " + listBox1.Text + " ?",
                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            else if (buttonDelete.Text == "Delete this society")
                res = MessageBox.Show("Are you sure you want to delete the society " + listBox1.Text + " ?" +
                " This will also delete all your contacts working here !",
                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (buttonDelete.Text == "Delete this contact" && res == DialogResult.Yes) {
                int[] contacts_id = ContactsData.GetContactsIDTab();
                ContactsData.DeleteContact(contacts_id[index]);
                InsertContacts();
            } else if (buttonDelete.Text == "Delete this society" && res == DialogResult.Yes) {
                int[] societies_id = SocietiesData.GetSocietiesIDTab();
                SocietiesData.DeleteSociety(societies_id[index]);
                InsertSocieties();
                ContactsData.DeleteContactsBySocietyID(societies_id[index]);
            }
        }

        //Opens a new form which let you add a new contact or society
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (buttonAdd.Text == "Add a contact") {
                NewContact add_contact = new NewContact();
                add_contact.ShowDialog();
                InsertContacts();
            } else if (buttonAdd.Text == "Add a society") {
                NewSociety add_society = new NewSociety();
                add_society.ShowDialog();
                InsertSocieties();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if (list_control == 1 && index != -1)
            {
                int[] contacts_id = ContactsData.GetContactsIDTab();
                ListWorkers my_list = new ListWorkers(ContactsData.GetSocietyIDByContactID(contacts_id[index]));
                my_list.ShowDialog();
            }
            if (list_control == 2 && index != -1)
            {
                int[] societies_id = SocietiesData.GetSocietiesIDTab();
                ListWorkers my_list = new ListWorkers(societies_id[index]);
                my_list.ShowDialog();
            }
        }
    }
}
