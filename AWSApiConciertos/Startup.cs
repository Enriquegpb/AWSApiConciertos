using AWSApiConciertos.Data;
using AWSApiConciertos.Helpers;
using AWSApiConciertos.Models;
using AWSApiConciertos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AWSApiConciertos;

public class Startup
{
    

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public async void ConfigureServices(IServiceCollection services)
    {
        string secret = HelperSecretManager.GetSecretAsync().GetAwaiter().GetResult();
        KeyModel model = JsonConvert.DeserializeObject<KeyModel>(secret);
        string connectionString = model.ServerConnection;
        services.AddTransient<RepositoryEventos>();
        services.AddDbContext<EventosContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Conciertos AWS",
                Version = "v1"
            });
        });
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint
            ("/swagger/v1/swagger.json", "Api Conciertos AWS v1");
            options.RoutePrefix = "";
        });
        app.UseCors(options => options.AllowAnyOrigin());
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}