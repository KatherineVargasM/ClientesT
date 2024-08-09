using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Clientes.Config;

namespace Clientes.Models
{
    internal class ClientesModel
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        private ConexionBDD conexionBDD = new ConexionBDD();

        public string NombreCompleto
        {
            get { return Apellidos + " " + Nombres; }
        }

        public List<ClientesModel> Todos()
        {
            string cadena = "select * from Clientes";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexionBDD.AbrirConexion());
            DataTable data = new DataTable();
            adapter.Fill(data);

            List<ClientesModel> listaClientes = new List<ClientesModel>();

            foreach (DataRow fila in data.Rows)
            {
                ClientesModel cliente = new ClientesModel
                {
                    IdCliente = Convert.ToInt32(fila["IdCliente"]),
                    Nombres = fila["Nombres"].ToString(),
                    Apellidos = fila["Apellidos"].ToString(),
                    Direccion = fila["Direccion"].ToString(),
                    Telefono = fila["Telefono"].ToString(),
                    Correo = fila["Correo"].ToString()
                };
                listaClientes.Add(cliente);
            }

            conexionBDD.CerrarConexion();
            return listaClientes;
        }

        public ClientesModel Obtener(int idCliente)
        {
            string cadena = "select * from Clientes where IdCliente = @IdCliente";
            SqlCommand cmd = new SqlCommand(cadena, conexionBDD.AbrirConexion());
            cmd.Parameters.AddWithValue("@IdCliente", idCliente);
            SqlDataReader lector = cmd.ExecuteReader();

            ClientesModel cliente = null;
            if (lector.Read())
            {
                cliente = new ClientesModel
                {
                    IdCliente = Convert.ToInt32(lector["IdCliente"]),
                    Nombres = lector["Nombres"].ToString(),
                    Apellidos = lector["Apellidos"].ToString(),
                    Direccion = lector["Direccion"].ToString(),
                    Telefono = lector["Telefono"].ToString(),
                    Correo = lector["Correo"].ToString()
                };
            }
            lector.Close();
            conexionBDD.CerrarConexion();
            return cliente;
        }

        public string Insertar(ClientesModel cliente)
        {
            try
            {
                string cadena = "insert into Clientes (Nombres, Apellidos, Direccion, Telefono, Correo) values (@Nombres, @Apellidos, @Direccion, @Telefono, @Correo)";
                SqlCommand cmd = new SqlCommand(cadena, conexionBDD.AbrirConexion());
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                conexionBDD.CerrarConexion();
            }
        }

        public string Editar(ClientesModel cliente)
        {
            try
            {
                string cadena = "update Clientes set Nombres = @Nombres, Apellidos = @Apellidos, Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo where IdCliente = @IdCliente";
                SqlCommand cmd = new SqlCommand(cadena, conexionBDD.AbrirConexion());
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                conexionBDD.CerrarConexion();
            }
        }

        public string Eliminar(ClientesModel cliente)
        {
            try
            {
                string cadena = "delete from Clientes where IdCliente = @IdCliente";
                SqlCommand cmd = new SqlCommand(cadena, conexionBDD.AbrirConexion());
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                conexionBDD.CerrarConexion();
            }
        }
    }
}
