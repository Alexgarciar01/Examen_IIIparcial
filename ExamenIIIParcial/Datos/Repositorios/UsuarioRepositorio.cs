using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private string CadenaConexion;

    public UsuarioRepositorio(string cadenaConexion)
    {
        CadenaConexion = cadenaConexion;
    }

    private MySqlConnection Conexion()
    {
        return new MySqlConnection(CadenaConexion);
    }
        

    public Task<bool> Actualizar(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Usuario>> GetLista()
    {
        IEnumerable<Usuario> lista = new List<Usuario>();

        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT * FROM usuario";
            lista = await conexion.QueryAsync<Usuario>(sql);
        }
        catch (Exception ex)
        {

        }
        return lista;
    }

    public Task<Usuario> GetPorCodigo(string codigo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Nuevo(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ValidarUsuario(Login login)
    {
        bool valido = false; 
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT 1 FROM usuario WHERE codigo = @Codigo AND Clave = @Clave;";
            valido = await conexion.ExecuteScalarAsync<bool>(sql,new {login.Codigo, login.Clave});
        }
        catch(Exception ex)
        {

        }
        return valido;
    }

   
}
