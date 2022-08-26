using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            if (string.IsNullOrEmpty(context.Principal.FindFirst("id").Value))
                context.Fail("Unauthorized");

            return Task.CompletedTask;
        }
    };

    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secret"]))
    };
});



var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
app.UseAuthentication();    // VEM ÄR DU? - INLOGGNING - Behörighet     401
app.UseAuthorization();     // VAD FÅR DU GÖRA?  - Rättigheter          403

app.MapControllers();

app.Run();


/* 
    HTTP METHODS                            CRUD
    POST, GET, PUT, DELETE          =       CREATE READ UPDATE DELETE
  
    Origin : 194.103.157.90
  
    fetch("https://domain.com/api/accounts", {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6Induc3N1all6TmYxRzVqOGRtbk1PQktHTktrNFg4SzRxWGJrUUZHUGR6ZEUiLCJhbGciOiJSUzI1NiIsIng1dCI6ImpTMVhvMU9XRGpfNTJ2YndHTmd2UU8yVnpNYyIsImtpZCI6ImpTMVhvMU9XRGpfNTJ2YndHTmd2UU8yVnpNYyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9iYjY4NzdlNS1mZDI0LTQ5NDUtYWMyYS0xNzQ1YTRmMzQ5MTEvIiwiaWF0IjoxNjU1NzE0OTIzLCJuYmYiOjE2NTU3MTQ5MjMsImV4cCI6MTY1NTcyMDA3MSwiYWNjdCI6MCwiYWNyIjoiMSIsImFnZUdyb3VwIjoiMyIsImFpbyI6IkFWUUFxLzhUQUFBQXU3TEUwSjhrb3R5cW12dEJpL3FFQ2JPdGVuS2E4TmY3WkhEMFZqRU5UWTFTYXJHUUZXLzBsdVhmRVVBYjBIY1pTVHRSbFFFZkw0ZWNVVTNtcHovRG9yQW1BTnpGR2tQbTBjU29oQklMZVNZPSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiR3JhcGggRXhwbG9yZXIiLCJhcHBpZCI6ImRlOGJjOGI1LWQ5ZjktNDhiMS1hOGFkLWI3NDhkYTcyNTA2NCIsImFwcGlkYWNyIjoiMCIsImRldmljZWlkIjoiNTljZjhiODItN2M0NC00MGFkLWI0ZDctOGZkOTdiODc0NTFiIiwiZmFtaWx5X25hbWUiOiJNYXR0aW4tTGFzc2VpIiwiZ2l2ZW5fbmFtZSI6IkhhbnMiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIxODUuMTc2LjI0Ny4xNzQiLCJuYW1lIjoiSGFucyBNYXR0aW4tTGFzc2VpIiwib2lkIjoiMDdkOWI4NGEtMTkxZC00ZTljLWI5ODgtNWQ3M2UyYmE0ZTVhIiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMzRkZGOUE5QUI1RUUiLCJyaCI6IjAuQVhNQTVYZG91eVQ5UlVtc0toZEZwUE5KRVFNQUFBQUFBQUFBd0FBQUFBQUFBQUJ6QUFrLiIsInNjcCI6IkNhbGVuZGFycy5SZWFkV3JpdGUgQ29udGFjdHMuUmVhZFdyaXRlIEZpbGVzLlJlYWRXcml0ZS5BbGwgTWFpbC5SZWFkV3JpdGUgTm90ZXMuUmVhZFdyaXRlLkFsbCBvcGVuaWQgUGVvcGxlLlJlYWQgcHJvZmlsZSBTaXRlcy5SZWFkV3JpdGUuQWxsIFRhc2tzLlJlYWRXcml0ZSBUZWFtc0FjdGl2aXR5LlJlYWQgVGVhbXNBY3Rpdml0eS5TZW5kIFRlYW1zQXBwLlJlYWQgVGVhbXNBcHAuUmVhZC5BbGwgVGVhbXNBcHAuUmVhZFdyaXRlIFRlYW1zQXBwLlJlYWRXcml0ZS5BbGwgVGVhbXNBcHBJbnN0YWxsYXRpb24uUmVhZEZvckNoYXQgVGVhbXNBcHBJbnN0YWxsYXRpb24uUmVhZEZvclRlYW0gVGVhbXNBcHBJbnN0YWxsYXRpb24uUmVhZEZvclVzZXIgVGVhbXNBcHBJbnN0YWxsYXRpb24uUmVhZFdyaXRlRm9yQ2hhdCBUZWFtc0FwcEluc3RhbGxhdGlvbi5SZWFkV3JpdGVGb3JUZWFtIFRlYW1zQXBwSW5zdGFsbGF0aW9uLlJlYWRXcml0ZUZvclVzZXIgVGVhbXNBcHBJbnN0YWxsYXRpb24uUmVhZFdyaXRlU2VsZkZvckNoYXQgVGVhbXNBcHBJbnN0YWxsYXRpb24uUmVhZFdyaXRlU2VsZkZvclRlYW0gVGVhbXNBcHBJbnN0YWxsYXRpb24uUmVhZFdyaXRlU2VsZkZvclVzZXIgVGVhbVNldHRpbmdzLlJlYWQuQWxsIFRlYW1TZXR0aW5ncy5SZWFkV3JpdGUuQWxsIFRlYW1zVGFiLkNyZWF0ZSBUZWFtc1RhYi5SZWFkLkFsbCBUZWFtc1RhYi5SZWFkV3JpdGUuQWxsIFVzZXIuUmVhZCBVc2VyLlJlYWRCYXNpYy5BbGwgVXNlci5SZWFkV3JpdGUgZW1haWwiLCJzdWIiOiJwMm56UjVnVG1QTHY0S0szLWh6MnNSM3VNcXZKZS1pMml5dEpHZy1WVGZNIiwidGVuYW50X3JlZ2lvbl9zY29wZSI6IkVVIiwidGlkIjoiYmI2ODc3ZTUtZmQyNC00OTQ1LWFjMmEtMTc0NWE0ZjM0OTExIiwidW5pcXVlX25hbWUiOiJoYW5zLm1hdHRpbi1sYXNzZWlAZXBuZGF0YS5jb20iLCJ1cG4iOiJoYW5zLm1hdHRpbi1sYXNzZWlAZXBuZGF0YS5jb20iLCJ1dGkiOiJXVVJRX3VsSUpVNl9HTWtzSHNjTEFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6InhISTduYWMzS1RPZ09tZFhnaW1jNGpxVHlua2wzT2FtZ09jWjBmVlBSbGMifSwieG1zX3RjZHQiOjE0NzM3MDI3ODh9.eP5FEC3uP_6ku3IotXDEVwmgsBIRFEJFyF1d9kHf2LmEML36qsQCDNw7HE8CeZxuY6wH5Fhqr1kBUOq4In6Sw-Mh_2EYDYffSyL18-OlJWOEXurCr9wrQHqxqyToWOjWJ9c80M9LLAGgtX5cJP3i4poPN3PTjBbMRIDJkcArWSaHtn4vETGRuqZZvhSKN2YJMzFya12hggaacizNbC-PYeHaZT2A87T-qjL5jxuIsYukIEpWgRqw5XvF-4FK2CI12XBeLH4eYI8Q7VF_au__I9_rBfGZeVwBPQxk0Fc4K6pWceQYFCBmCp7dIEQOxeKVwaA3PPMKL3Sfiu2VsilwYw'
        }

    }) 
 
 
 
 
*/