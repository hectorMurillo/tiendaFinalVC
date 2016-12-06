using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoMoya.Models;

namespace ProyectoMoya.Interfaces
{
    public interface IUsersInterface
    {
        List<User> obtenerUsuarios();
        List<Roles> obtenerRoles();
        void insertarRol(int usuarioId, int rolId);
    }
}
