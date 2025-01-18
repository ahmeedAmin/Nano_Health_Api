
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nano_Health.Data;
using Nano_Health.Loc;
using Nano_Health.Models;
using Nano_Health.Services.Interfaces;
using Nano_Health.Services.Repositories;
using System.Text;

namespace Nano_Health
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            
            var defaultConnectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<NanoHealthDbContext>(options =>
            options.UseSqlServer(defaultConnectionString, b =>
            {
                b.MigrationsAssembly(typeof(NanoHealthDbContext).Assembly.FullName);
            }));


           
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
          
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Demo", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirment = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new string[]{"Bearer"}
                    }
                };

                c.AddSecurityRequirement(securityRequirment);
            });
            builder.Services.AddCors(options =>
            {
                
                options.AddPolicy("DefaultPolicy", builder => builder
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          );
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ILogEntryService, LogEntryService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddAutoMapper(typeof(Program));


            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<NanoHealthDbContext>();
                dbContext.Database.Migrate();
                Task.Run(async () => await SeedRolesAsync(dbContext)).GetAwaiter().GetResult();  
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.UseCors("DefaultPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();
           

            app.Run();
        }
        private static async Task SeedRolesAsync(NanoHealthDbContext dbContext)
        {
            
            if (!await dbContext.Roles.AnyAsync(r => r.NormalizedName == "USER"))
            {
                dbContext.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });
            }

            if (!await dbContext.Roles.AnyAsync(r => r.NormalizedName == "ADMIN"))
            {
                dbContext.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
