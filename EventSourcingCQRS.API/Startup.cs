using EventSourcingCQRS.Application.Commands.CreateOrderCommand;
using EventSourcingCQRS.Domain.Models.Orders;
using EventSourcingCQRS.Infrastructure.EventSourcing;
using EventSourcingCQRS.Infrastructure.ReadModel;
using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventSourcingCQRS.API
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
            services.AddControllers();

            services.AddTransient<IEventSourcingRepository<Order, OrderId>, EventSourcingRepository<Order, OrderId>>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EventSourcingCQRS.EventStore;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddDbContext<ReadModelContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EventSourcingCQRS.ReadModel;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddTransient<IReadRepository<OrderReadModel>, Repository<OrderReadModel>>();
            services.AddTransient<IRepository<OrderReadModel>, Repository<OrderReadModel>>();

            services.AddMediatR(typeof(CreateOrderCommand));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
