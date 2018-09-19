using System.Windows.Forms;

namespace SQL_Contact_Wallet_V2
{
    //This class is called when you double-click on a contact or society
    //It displays the list of contacts working in the same society as the one you double-clicked
    //Or the list of the contacts working in the society you double-cliked
    public partial class ListWorkers : Form
    {
        public ListWorkers(int society_id)
        {
            InitializeComponent();
            label1.Text = "Workers at " + SocietiesData.GetSocietyNameByID(society_id);
            string[] workers = ContactsData.GetContactsBySocietyID(society_id);
            foreach (string str in workers)
            {
                if (str != null)
                    listBox1.Items.Add(str);
            }
        }
    }
}
