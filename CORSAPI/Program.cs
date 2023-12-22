using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(x=>
{
    //x.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    x.AddPolicy("AllowSites", builder =>
    builder.WithOrigins("https://localhost:7273", "https://www.mywebsite.com").AllowAnyHeader().AllowAnyMethod()
    );

    //x.AddPolicy("AllowSitesVariant", builder =>
    //{
    //    builder.WithOrigins("https://www.aaaaa.com").WithHeaders(HeaderNames.ContentType,"x-custom-header");

    //});
    x.AddPolicy("AllowSitesVIP", builder =>
    {
        builder.WithOrigins("https://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSites");
app.UseAuthorization();

app.MapControllers();

app.Run();
