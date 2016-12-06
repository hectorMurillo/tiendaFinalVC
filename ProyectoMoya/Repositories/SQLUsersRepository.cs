using ProyectoMoya.Interfaces;
using ProyectoMoya.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoMoya.Repositories
{
    public class SQLUsersRepository : IUsersInterface
    {
        public void insertarRol(int usuarioId, int rolId)
        {
            //Aqui vas hacer el metodo para insertar el usuario
            throw new NotImplementedException();
        }

        public List<Roles> obtenerRoles()
        {
            var lista = new List<Roles>();

            var sql = "SELECT ID, Name from dbo.AspNetRoles";
            var cmd = new SqlCommand(sql);

            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString()))
            {
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rol = new Roles();

                        rol.ID = reader.GetString(0);
                        rol.Name= reader.GetString(1);
                        lista.Add(rol);
                    }
                }
                sqlConnection.Close();
                return lista;
            }
        }

        public  List<User> obtenerUsuarios()
        {
            var lista = new List<User>();

            var sql = "SELECT ID, Email from dbo.AspNetUsers";
            var cmd = new SqlCommand(sql);

            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString()))
            {
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User();

                        user.ID = reader.GetString(0);
                        user.Email = reader.GetString(1);                       
                        lista.Add(user);
                    }
                }
                sqlConnection.Close();
                return lista;
            }
        }      
    }
}