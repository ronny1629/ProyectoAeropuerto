using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{

    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuentas tipoCuentas);
        Task Borrar(int id);
        Task Crear(TipoCuentas tipoCuentas);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuentas>> Obtener(int usuarioId);
        Task<TipoCuentas> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuentas> tipoCuentasOrdenados);
    }
    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        public readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration) 
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear( TipoCuentas tipoCuentas)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("TiposCuentas_Insertar",
                                                             new { usuarioId = tipoCuentas.UsuarioId,
                                                                 nombre = tipoCuentas.Nombre },
                                                                 commandType: System.Data.CommandType.StoredProcedure);
                                                             

            tipoCuentas.Id = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                  @"select 1
                                  from TiposCuentas
                                  where Nombre = @Nombre and UsuarioId = @UsuarioId",
                                  new {nombre, usuarioId});

            return existe == 1;
        }

         
        public async Task<IEnumerable<TipoCuentas>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuentas>(
                  @"SELECT Id, Nombre, Orden FROM TiposCuentas where
                    UsuarioId = @UsuarioId ORDER BY Orden", new {usuarioId});
        }

        public async Task Actualizar(TipoCuentas tipoCuentas)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                @"UPDATE TiposCuentas set Nombre = @Nombre
                where Id = @Id", tipoCuentas);
        }

        public async Task<TipoCuentas> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuentas>(
                @"Select Id, Nombre, Orden from TiposCuentas
                where Id = @Id and UsuarioId = @UsuarioId", new {id, usuarioId});
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Delete TiposCuentas where Id = @Id", new {id}); 
        }

        public async Task Ordenar(IEnumerable<TipoCuentas> tipoCuentasOrdenados)
        {
            var query = "UPDATE TiposCuentas Set Orden = @Orden where Id = @Id"; 
            using  var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentasOrdenados);
        }


    }
}
