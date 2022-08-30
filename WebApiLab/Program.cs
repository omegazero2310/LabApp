using CommonClass.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.BusinessLayer;
using WebApiLab.Services.DataAccessLayer;
using WebApiLab.Services.Interfaces;

namespace WebApiLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddJWTTokenServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
                }
                    });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddDbContext<LabDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"))
            );
            builder.Services.AddScoped<IAdminParts<AdminParts>, AdminPartDAL>();
            builder.Services.AddScoped<IAdminPartService, AdminPartService>();

            builder.Services.AddScoped<IAdminStaffs<AdminStaff>, AdminStaffDAL>();
            builder.Services.AddScoped<IAdminStaffsService, AdminStaffsService>();

            builder.Services.AddScoped<IAdminUsers<AdminUser>, AdminUserDAL>();
            builder.Services.AddScoped<IAdminUsersService, AdminUsersService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}