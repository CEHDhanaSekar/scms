using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace SCMS.API.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddCustomLogging(
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

            lc.WriteTo.Logger(glob => glob
                    .WriteTo.PostgreSQL(
                        connectionString: scms_connection,
                        tableName: logsTableName,
                        columnOptions: writers,
                        schemaName: schemaName,
                        needAutoCreateTable: true,
                        batchSizeLimit: 1));
        });

        return builder;
    }
}

