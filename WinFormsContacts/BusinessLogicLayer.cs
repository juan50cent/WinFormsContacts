using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    public class BusinessLogicLayer //capa de lógica empresarial
    {
        private DataAccessLayer _dataAccessLayer;

        public BusinessLogicLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }


        public Contact SaveContact(Contact contact)
        {
            if(contact.Id == 0)
            {
                _dataAccessLayer.InsertContact(contact);

            }
            else
            {
                _dataAccessLayer.UpdateContact(contact);
            }
            return contact;
           
        }
        //Cuando yo agrego un valor nulo a un parametro , esto significa que es opcional.
        public List<Contact> GetContacts(string search = null)
        {
            return _dataAccessLayer.GetContacts(search);
        }

        public void DeleteContact(int id)
        {
            _dataAccessLayer.DeleteContact(id);
        }



    }
}
