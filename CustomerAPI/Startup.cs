using AutoMapper;
using CustomerAPI.Data.Database;
using CustomerAPI.Data.Repository.v1;
using CustomerAPI.Messaging.Send.Options.v1;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace CustomerAPI
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
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            //services.AddDbContext<CustomerContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer ", Version = "v1",
                    Description = "A simple API to create or update customers",
                    Contact = new OpenApiContact
                    {
                        Name = "Wolfgang Ofner",
                        Email = "Wolfgang@programmingwithwolfgang.com",
                        Url = new Uri("https://www.programmingwithwolfgang.com/")
                    }
                });
            });

            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.Load("CustomerAPI.Service"));
            //services.AddMediatR(Assembly.Load("CustomerAPI.Service"));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            //services.AddTransient<IValidator<CreateCustomerModel>, CreateCustomerModelValidator>();
            //services.AddTransient<IValidator<UpdateCustomerModel>, UpdateCustomerModelValidator>();

            //services.AddSingleton<ICustomerUpdateSender, CustomerUpdateSender>();

            //services.AddTransient<IRequestHandler<CreateCustomerCommand, Customer>, CustomerHandler>();
            //services.AddTransient<IRequestHandler<UpdateCustomerCommand, Customer>, CustomerHandler>();
            //services.AddTransient<IRequestHandler<GetCustomerByIdQuery, Customer>, GetCustomerByIdQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerTest v1"));
            }

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
            //    c.RoutePrefix = string.Empty;
            //});

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
