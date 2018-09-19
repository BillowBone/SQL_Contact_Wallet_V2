using System;
using System.Windows.Forms;

namespace SQL_Contact_Wallet_V2
{
    //This class manages the form that let you add a society to the table
    public partial class NewSociety : Form
    {
        public NewSociety()
        {
            InitializeComponent();
            buttonAdd.Enabled = false;
        }

        //Checks if the non-nullables values aren't null and numbers values are numbers
        private void ManageBoxes()
        {
            if (boxName.Text != "" && boxAddress.Text != "" && boxPostalCode.Text != ""
                && StringLib.StrIsNumSpace(boxPostalCode.Text) && boxCity.Text != "")
                buttonAdd.Enabled = true;
            else
                buttonAdd.Enabled = false;
        }

        //Puts each textboxes' content in a tab and add it to the society table before closing the form
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string[] new_society = new string[6];

            new_society[0] = boxName.Text;
            new_society[1] = boxAddress.Text;
            new_society[2] = boxAddress2.Text;
            new_society[3] = boxPostalCode.Text;
            new_society[4] = boxCity.Text;
            new_society[5] = boxStandard.Text;
            SocietiesData.AddSociety(new_society);
            this.Close();
        }

        private void boxName_TextChanged(object sender, EventArgs e)
        {
            ManageBoxes();
        }

        private void boxAddress_TextChanged(object sender, EventArgs e)
        {
            ManageBoxes();
        }

        private void boxPostalCode_TextChanged(object sender, EventArgs e)
        {
            ManageBoxes();
        }

        private void boxCity_TextChanged(object sender, EventArgs e)
        {
            ManageBoxes();
        }
    }
}
