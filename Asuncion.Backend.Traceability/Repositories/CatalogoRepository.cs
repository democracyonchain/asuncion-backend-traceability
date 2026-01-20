using Dapper;
using Asuncion.Backend.Traceability.Dtos;
using Asuncion.Backend.Traceability.Utils;
using Microsoft.AspNetCore.Connections;

namespace Asuncion.Backend.Traceability.Repositories
{
    public class CatalogoRepository
    {
        private readonly DbConnectionFactory _db;
        public CatalogoRepository(DbConnectionFactory db) => _db = db;

        public async Task<List<OptionDto>> GetProvincias()
        {
            const string sql = @"SELECT id::text AS id, nombre AS name FROM provincia ORDER BY nombre;";
            using var cn = _db.Create();
            return (await cn.QueryAsync<OptionDto>(sql)).ToList();
        }

        public async Task<List<OptionDto>> GetCantones(int provinciaId)
        {
            const string sql = @"SELECT id::text AS id, nombre AS name FROM canton WHERE provincia_id=@provinciaId ORDER BY nombre;";
            using var cn = _db.Create();
            return (await cn.QueryAsync<OptionDto>(sql, new { provinciaId })).ToList();
        }

        public async Task<List<OptionDto>> GetParroquias(int cantonId)
        {
            const string sql = @"SELECT id::text AS id, nombre AS name FROM parroquia WHERE canton_id=@cantonId ORDER BY nombre;";
            using var cn = _db.Create();
            return (await cn.QueryAsync<OptionDto>(sql, new { cantonId })).ToList();
        }

        public async Task<List<OptionDto>> GetZonas(int parroquiaId)
        {
            // tu tabla parece tener: parroquia_id y zona_id (PK compuesta) + nombre
            const string sql = @"SELECT zona_id::text AS id, nombre AS name FROM zona WHERE parroquia_id=@parroquiaId ORDER BY zona_id;";
            using var cn = _db.Create();
            return (await cn.QueryAsync<OptionDto>(sql, new { parroquiaId })).ToList();
        }

        public async Task<List<OptionDto>> GetJuntas(int parroquiaId, int zonaId)
        {
            // junta: id, parroquia_id, zona_id, junta, sexo, electores
            const string sql = @"
            SELECT
              id::text AS id,
              ('Junta ' || junta::text || ' - ' || sexo || COALESCE(' (' || electores::text || ' electores)', '')) AS name
            FROM junta
            WHERE parroquia_id=@parroquiaId AND zona_id=@zonaId
            ORDER BY junta, sexo;";
            using var cn = _db.Create();
            return (await cn.QueryAsync<OptionDto>(sql, new { parroquiaId, zonaId })).ToList();
        }

        public async Task<List<OptionDto>> GetDignidadesByJunta(int juntaId)
        {
            // para que el usuario vea solo dignidades existentes para esa junta (via acta)
            const string sql = @"
            SELECT DISTINCT d.id::text AS id, d.nombre AS name
            FROM acta a
            JOIN dignidad d ON d.id = a.dignidad_id
            WHERE a.junta_id=@juntaId
            ORDER BY d.nombre;";
            using var cn = _db.Create();
            return (await cn.QueryAsync<OptionDto>(sql, new { juntaId })).ToList();
        }
    }
}
