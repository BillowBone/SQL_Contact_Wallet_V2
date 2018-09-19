using System;
using System.Data;
using System.Windows.Forms;

namespace SQL_Contact_Wallet_V2
{
    //This class manages every interactions with the society's card form
    public partial class SocietyCard : Form
    {
        private DataRow society_data_row;

        public SocietyCard()
        {
            InitializeComponent();
        }

        //This is the only constructor used here
        //Gets the whole row of the society thanks to its ID and fills all textboxes with
        public SocietyCard(int id)
        {
            InitializeComponent();
            DisableBoxes();
            society_data_row = SocietiesData.GetSocietiesRowByID(id);
            boxID.Text = society_data_row[0].ToString();
            boxName.Text = society_data_row[1].ToString();
            boxAddress.Text = society_data_row[2].ToString();
            boxAddress2.Text = society_data_row[3].ToString();
            boxPostalCode.Text = society_data_row[4].ToString();
            boxCity.Text = society_data_row[5].ToString();
            boxStandard.Text = society_data_row[6].ToString();
        }

        //Sends modifications to the database
        private void SaveSocietyEdition()
        {
            society_data_row[1] = boxName.Text;
            society_data_row[2] = boxAddress.Text;
            society_data_row[3] = boxAddress2.Text;
            society_data_row[4] = boxPostalCode.Text;
            society_data_row[5] = boxCity.Text;
            society_data_row[6] = boxStandard.Text;
            SocietiesData.UpdateSociety(society_data_row);
        }

        private void EnableBoxes()
        {
            boxName.Enabled = true;
            boxAddress.Enabled = true;
            boxAddress2.Enabled = true;
            boxPostalCode.Enabled = true;
            boxCity.Enabled = true;
            boxStandard.Enabled = true;
        }

        private void DisableBoxes()
        {
            boxID.Enabled = false;
            boxName.Enabled = false;
            boxAddress.Enabled = false;
            boxAddress2.Enabled = false;
            boxPostalCode.Enabled = false;
            boxCity.Enabled = false;
            boxStandard.Enabled = false;
        }

        //Checks if the non-nullables values aren't null and numbers values are numbers
        private void EnableSave()
        {
            if (boxName.Text != "" && boxAddress.Text != "" && boxPostalCode.Text != ""
                && StringLib.StrIsNumSpace(boxPostalCode.Text) && boxCity.Text != "")
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
            }
            else if (buttonEdit.Text == "Save")
            {
                SaveSocietyEdition();
                buttonEdit.Text = "Edit";
                DisableBoxes();
            }
        }

        private void boxName_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        private void boxAddress_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        private void boxPostalCode_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        private void boxCity_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }
    }
}
