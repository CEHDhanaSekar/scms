using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace SCMS.API.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddSeparatedSerilog(
        this WebApplicationBuilder builder,
        string logsTableName = "logs",
        string schemaName = "public")
    {
        Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));

        var scms_connection = Environment.GetEnvironmentVariable("SCMS_DEFAULT_CONNECTION");

        var writers = new Dictionary<string, ColumnWriterBase>
        {
            ["message"] = new RenderedMessageColumnWriter(),
            ["message_template"] = new MessageTemplateColumnWriter(),
            ["level"] = new LevelColumnWriter(),
            ["timestamp"] = new TimestampColumnWriter(),
            ["exception"] = new ExceptionColumnWriter(),
            ["log_event"] = new LogEventSerializedColumnWriter()
        };

        builder.Host.UseSerilog((ctx, services, lc) =>
        {
            lc.ReadFrom.Configuration(ctx.Configuration)
              .Enrich.FromLogContext();

            // Helper: true only for real tenant requests (TenantConn populated by middleware)
            static bool HasTenantConn(LogEvent e) =>
                e.Properties.TryGetValue("TenantConn", out var v)
                && v is ScalarValue sv
                && sv.Value is string s
                && !string.IsNullOrWhiteSpace(s);

            // GLOBAL sink (SCMS owner DB): exclude when a non-empty TenantConn is present
            if (!string.IsNullOrWhiteSpace(scms_connection))
            {
                lc.WriteTo.Logger(glob => glob
                    .Filter.ByExcluding(HasTenantConn)
                    .WriteTo.PostgreSQL(
                        connectionString: scms_connection,
                        tableName: logsTableName,
                        columnOptions: writers,
                        schemaName: schemaName,
                        needAutoCreateTable: true,
                        batchSizeLimit: 1));
            }

            // TENANT sink: route logs dynamically per tenant database connection
            lc.WriteTo.Logger(ten => ten
                .Filter.ByIncludingOnly(HasTenantConn)
                .WriteTo.Map(
                    keyPropertyName: "TenantConn",
                    configure: (tenantConn, wt) =>
                    {
                        if (tenantConn is not string cs || string.IsNullOrWhiteSpace(cs)) return;
                        wt.PostgreSQL(
                            connectionString: cs,
                            tableName: logsTableName,
                            columnOptions: writers,
                            schemaName: schemaName,
                            needAutoCreateTable: true,
                            batchSizeLimit: 1);
                    }));
        });

        return builder;
    }
}

