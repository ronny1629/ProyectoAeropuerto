using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{

    public interface IRepositorioCuentas
    {
        Task Actualizar(CuentaCreacionViewModel cuenta);
        Task Borrar(int id);
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
        Task<Cuenta> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioCuentas: IRepositorioCuentas
    {
        private readonly string connectionString;
        public RepositorioCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                                      @"Insert into Cuentas(Nombre, TipoCuentaId, Balance, Descripcion)
                                      values (@Nombre, @TipoCuentaId, @Balance, @Descripcion);
                                      Select scope_identity();", cuenta);

            cuenta.Id = id;
        }

        public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Cuenta>(@"
                                     select Cuentas.Id, Cuentas.Nombre, Balance, tc.Nombre as tipoCuenta
                                     from Cuentas Inner Join TiposCuentas tc
                                     On tc.Id = Cuentas.TipoCuentaId
                                     where tc.UsuarioId = @UsuarioId
                                     order by tc.Orden", new {usuarioId});
        }

        public async Task<Cuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Cuenta>(@"
                                     select Cuentas.Id, Cuentas.Nombre, Balance, Descripcion, tipoCuentaId
                                     from Cuentas Inner Join TiposCuentas tc
                                     On tc.Id = Cuentas.TipoCuentaId
                                     where tc.UsuarioId = @UsuarioId And Cuentas.id = @Id", new { id, usuarioId });
        }

        public async Task Actualizar(CuentaCreacionViewModel cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"
                Update Cuentas
                set Nombre = @Nombre, Balance = @Balance, Descripcion = @Descripcion, TipoCuentaId = @TipoCuentaId
                where id = @id", cuenta);
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Cuentas where id = @id", new {id});
        }
    }
}
