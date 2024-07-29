using Microsoft.EntityFrameworkCore;
using AdminPortal.Data;
using AdminPortal.Endpoints;



var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Configure Database
var connString = builder.Configuration.GetConnectionString("AdminPortal");
builder.Services.AddSqlite<AdminPortalContext>(connString);

var app = builder.Build();

// Apply CORS policy
app.UseCors("AllowAll");

// Ensure database migration
app.MigrateDb();

// Add Middleware for logging and error handling
app.UseDeveloperExceptionPage();
app.UseExceptionHandler("/error"); // Customize as needed
app.UseHttpsRedirection();

app.MapClaimEndpoints();

app.MapTenantEndpoints();

app.MapRoleEndpoints();

app.MapUserGroupEndpoints();

app.MapUserEndpoints();

app.Run();

