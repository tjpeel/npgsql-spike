using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsqlDataSource(
    "Host=127.0.0.1; Database=npgsql-spike; Username=npgsql-spike; Password=password; Include Error Detail=true; Timeout=5; Command Timeout=5; Max Auto Prepare=10;",
    options => options.EnableParameterLogging());

var app = builder.Build();

app.MapGet("/", async (string name, NpgsqlConnection connection) =>
{
    await connection.OpenAsync();

    await using var command = new NpgsqlCommand("select assignment_id from assignment where name = $1", connection)
    {
        Parameters = {new NpgsqlParameter {Value = name}}
    };

    var assignmentId = await command.ExecuteScalarAsync();
    assignmentId ??= -1;

    return "assignmentId: " + assignmentId;
});

app.Run();
