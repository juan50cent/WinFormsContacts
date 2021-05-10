using System;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class ContactDetails : Form
    {
        //Variables globales
        private BusinessLogicLayer _businessLogicLayer;
        private Contact _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region EVENTS
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e) //Boton Save
        {
            SaveContact();
            this.Close();
            //Se castea de tipo Main para poder acceder al metodo PopulateContacts();
            //Al darle click en Save automaticamente carga en el grid en tiempo real el ultimo registro.
            ((Main)this.Owner).PopulateContacts();
        }
        #endregion

        #region PRIVATE METHODS
        private void SaveContact()
        {
            Contact contact = new Contact();

            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;

            //Si _contact es distinto de null entonces usamos el Id de _contact sino pongamos 0.
            contact.Id = _contact != null ? _contact.Id : 0;

            _businessLogicLayer.SaveContact(contact);
        }
        private void ClearForm()
        {
            //Limpia los cuadros de texto
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
        #endregion

        #region PUBLIC METHODS
        public void LoadContact(Contact contact)
        {
            //variable global
            _contact = contact;
            if (contact != null)
            {
                ClearForm();

                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;
            }
        }

        #endregion
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


      
    }
}
