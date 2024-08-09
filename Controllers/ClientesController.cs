using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clientes.Models;

namespace Clientes.Controllers
{
    internal class ClientesController
    {
        private ClientesModel clientesModel = new ClientesModel();

        public List<ClientesModel> todos()
        {
            return clientesModel.Todos();
        }

        public ClientesModel obtener(int idCliente)
        {
            return clientesModel.Obtener(idCliente);
        }

        public string insertar(ClientesModel cliente)
        {
            return clientesModel.Insertar(cliente);
        }

        public string editar(ClientesModel cliente)
        {
            return clientesModel.Editar(cliente);
        }

        public string eliminar(ClientesModel cliente)
        {
            return clientesModel.Eliminar(cliente);
        }
    }
}

