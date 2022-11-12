using Data.DataAccess;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Sports Team Managment System - Sportly",
        Version = "v1"
    });
});

builder.Services.AddDbContext<STMSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SMTScs"));
});

builder.Services.AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} else
{
    app.UseHttpsRedirection();
}

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(@"serviceAccount.json"),
});

//app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
