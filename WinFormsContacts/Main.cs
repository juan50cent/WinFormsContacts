using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class Main : Form
    {
        BusinessLogicLayer _businessLogicLayer;

        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region EVENTS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailsDialog();
        }

        //Evento Load Insertado por propiedades y click en el rayito de las opciones.
        //Nos permitira que al momento de cargar la aplicaciòn , automatica mente cargue en el grid los datos de la base de datos
        private void Main_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }

        //Cuando hagan doble click en el contenido de la celda se activa este evento
        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Con esta Linea obtengo la celda que han cliqueado
            //Se debe castear para que el dato que devuelva sea del mismo tipo de la variable "cell".
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                contactDetails.ShowDialog(this);

            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeteleContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }

        #endregion

        #region PRIVATE METHODS

        private void OpenContactDetailsDialog()
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails.ShowDialog(this);
        }

        private void DeteleContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }


        #endregion

        #region PUBLIC METHODS

        //Cuando yo agrego un valor nulo a un parametro , esto significa que es opcional.
        public void PopulateContacts(string searchText = null)
        {
            //se utiliza la libreria System.Collections.Generic;
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText);

            //Se le agrega una fuente de datos, y esa fuente de datos es la lista contacts
            gridContacts.DataSource = contacts;

        }

        #endregion

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
