using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clientes.Controllers;
using Clientes.Models;

namespace Clientes
{
    public partial class frm_Clientes : Form
    {
        ClientesController clientesController = new ClientesController();
        public string IdCliente = null;

        public frm_Clientes()
        {
            InitializeComponent();
        }


        private void frm_Clientes_Load(object sender, EventArgs e)
        {
            cargaLista();
        }

        public void cargaLista()
        {
            lst_Clientes.DataSource = clientesController.todos();
            lst_Clientes.DisplayMember = "NombreCompleto";
            lst_Clientes.ValueMember = "IdCliente";
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (lst_Clientes.SelectedItem != null)
            {
                IdCliente = lst_Clientes.SelectedValue.ToString();
                ClientesModel cliente = clientesController.obtener(Convert.ToInt32(IdCliente));
                if (cliente != null)
                {
                    txt_Nombres.Text = cliente.Nombres;
                    txt_Apellidos.Text = cliente.Apellidos;
                    txt_Direccion.Text = cliente.Direccion;
                    txt_Telefono.Text = cliente.Telefono;
                    txt_Correo.Text = cliente.Correo;
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente de la lista.");
            }
        }

        private void btn_Grabar_Click(object sender, EventArgs e)
        {
            string respuesta = "";
            ClientesModel cliente = new ClientesModel
            {
                IdCliente = Convert.ToInt32(IdCliente),
                Nombres = txt_Nombres.Text,
                Apellidos = txt_Apellidos.Text,
                Direccion = txt_Direccion.Text,
                Telefono = txt_Telefono.Text,
                Correo = txt_Correo.Text
            };

            if (Convert.ToInt32(IdCliente) > 0)
            {
                respuesta = clientesController.editar(cliente);
            }
            else
            {
                respuesta = clientesController.insertar(cliente);
            }

            if (respuesta == "ok")
            {
                IdCliente = null;
                cargaLista();
                MessageBox.Show("Se guardo con exito.");
            }
            else
            {
                IdCliente = null;
                MessageBox.Show("Error al guardar.");
            }


        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (lst_Clientes.SelectedItem != null)
            {
                ClientesModel cliente = new ClientesModel
                {
                    IdCliente = Convert.ToInt32(lst_Clientes.SelectedValue)
                };
                string respuesta = clientesController.eliminar(cliente);

                if (respuesta == "ok")
                {
                    cargaLista();
                    MessageBox.Show("Cliente eliminado con exito.");
                }
                else
                {
                    MessageBox.Show("Error al eliminar el cliente.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente de la lista.");
            }


        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_Nombres.Text = string.Empty;
            txt_Apellidos.Text = string.Empty;
            txt_Direccion.Text = string.Empty;
            txt_Telefono.Text = string.Empty;
            txt_Correo.Text = string.Empty;
            IdCliente = null;
            lst_Clientes.ClearSelected();
        }


    }
}
