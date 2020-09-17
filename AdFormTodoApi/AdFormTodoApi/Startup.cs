using AdFormTodoApi.Core;
using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using AdFormTodoApi.Data;
using AdFormTodoApi.Middleware;
using AdFormTodoApi.Service;
using AdFormTodoApi.Services;
using AdFormTodoApi.v1.GraphiQL.Mutation;
using AdFormTodoApi.v1.GraphiQL.Queries;
using AdFormTodoApi.v1.GraphiQL.Types;
using AdFormTodoApi.v1.Middleware;
using AutoMapper;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
namespace AdFormTodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddGraphQL(s => SchemaBuilder.New()
                .AddServices(s)
                .AddType<TodoItemType>()
                .AddType<TodoListType>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .Create());
            // To Enable EF with SQL Server 
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TodoDatabase"), x => x.MigrationsAssembly("AdFormTodoApi.Data")));
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true; // false by default
                
            }).AddNewtonsoftJson().AddXmlSerializerFormatters(); ;
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                
            }).AddXmlSerializerFormatters();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                // sepcify our operation filter here.  
                c.OperationFilter<AddParametersToSwagger>();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = $"TODO v1 API",
                    Description = "TODO v1 API",
                    Contact = new OpenApiContact
                    {
                        Name = "Priyanka Kapoor",
                        Email = "priyanka.kapoor@nagarro.com",
                    }
                });
            });
            // Registering custom Authentication service
            services.AddScoped<IAuthenicateService, AuthenticateService>();
            services.AddScoped<CorrelationID, CorrelationID>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddTransient<ITodoListService, TodoListService>();
            services.AddTransient<ILabelService, LabelService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddSession();
            services.AddDistributedMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground();
            }
            app.UseCors();
            app.UseGraphQL("/graphql").UsePlayground("/graphql");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My TODO API V1");
               
            });
            app.UseMiddleware<CorrelationIdToResponseMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<ErrorLoggingMiddleware>();
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
