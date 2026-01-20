using Dapper;
using Asuncion.Backend.Traceability.Dtos;
using Asuncion.Backend.Traceability.Utils;

namespace Asuncion.Backend.Traceability.Repositories
{
    public class TraceabilityRepository
    {
        private readonly DbConnectionFactory _db;
        public TraceabilityRepository(DbConnectionFactory db) => _db = db;

        // Convierte tipos que pueden venir de Postgres (DATE -> DateOnly) a DateTime?
        private static DateTime? ToDateTime(object? v)
        {
            if (v is null) return null;

            // Por si en algún momento cambias a timestamp/timestamptz
            if (v is DateTime dt) return dt;

            // Postgres DATE en .NET suele mapearse a DateOnly
            if (v is DateOnly d) return d.ToDateTime(TimeOnly.MinValue); // 00:00:00

            // Fallback por si llega como string
            if (v is string s)
            {
                if (DateTime.TryParse(s, out var dts)) return dts;
                if (DateOnly.TryParse(s, out var dos)) return dos.ToDateTime(TimeOnly.MinValue);
            }

            // Si llega otro tipo inesperado, explícito
            throw new InvalidCastException($"No se puede convertir '{v.GetType().FullName}' a DateTime.");
        }

        public async Task<(int actaId, string codigo, string? txIcr, string? txDig, string? txCtrl,
                           DateTime? fIcr, DateTime? fDig, DateTime? fCtrl,
                           ActaScopeDto scope)> GetActaCore(int juntaId, int dignidadId)
        {
            const string sql = @"
            SELECT
              a.id                           AS actaId,
              a.id::text                     AS codigo,
              a.txicr                        AS txIcr,
              a.txdigitacion                 AS txDig,
              a.txcontrol                    AS txCtrl,
              a.fechaescaneo                 AS fIcr,
              a.fechadigitacion              AS fDig,
              a.fechacontrol                 AS fCtrl,

              p.nombre                       AS provincia,
              c.nombre                       AS canton,
              pr.nombre                      AS parroquia,
              j.zona_id::text                AS zona,
              ('Junta ' || j.junta::text || ' - ' || j.sexo ||
                 COALESCE(' (' || j.electores::text || ' electores)', '')
              )                              AS junta,
              d.nombre                       AS dignidad
            FROM acta a
            JOIN junta j        ON j.id = a.junta_id
            JOIN provincia p    ON p.id = j.provincia_id
            JOIN canton c       ON c.id = j.canton_id
            JOIN parroquia pr   ON pr.id = j.parroquia_id
            JOIN dignidad d     ON d.id = a.dignidad_id
            WHERE
              a.junta_id = @juntaId
              AND a.dignidad_id = @dignidadId
            ORDER BY a.id DESC
            LIMIT 1;";

            using var cn = _db.Create();

            var row = await cn.QuerySingleOrDefaultAsync(sql, new { juntaId, dignidadId });

            if (row == null)
                throw new Exception("No existe acta para la junta y dignidad seleccionadas.");

            var scope = new ActaScopeDto
            {
                provincia = row.provincia,
                canton = row.canton,
                parroquia = row.parroquia,
                zona = row.zona,
                junta = row.junta,
                dignidad = row.dignidad
            };

            return (
                (int)row.actaid,
                (string)row.codigo,
                (string?)row.txicr,
                (string?)row.txdig,
                (string?)row.txctrl,
                ToDateTime(row.ficr),
                ToDateTime(row.fdig),
                ToDateTime(row.fctrl),
                scope
            );
        }

        public async Task<List<ImagenActaDto>> GetImagenesActa(int actaId)
        {
            const string sql = @"
                select
                  acta_id  as ActaId,
                  nombre   as Nombre,
                  pagina   as Pagina,
                  hash     as Hash,
                  pathipfs as PathIpfs,
                  fecha    as Fecha
                from public.imagenacta
                where acta_id = @actaId
                  and pathipfs is not null
                  and length(trim(pathipfs)) > 0
                order by pagina asc, fecha desc;";


            using var cn = _db.Create();
            var rows = await cn.QueryAsync<ImagenActaDto>(sql, new { actaId });
            return rows.AsList();
        }



        public Task<ResultadosDto?> GetResultadosFinales(int actaId)
        {
            // TODO: implementar según tu modelo de resultados
            return Task.FromResult<ResultadosDto?>(null);
        }
    }
}
