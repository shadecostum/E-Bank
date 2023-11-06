using E_Bank.Data;
using E_Bank.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace E_Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            //adding connection to database as Mycontext-1
            builder.Services.AddDbContext<MyContext>( options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString"));
            }
                );

            builder.Services.AddControllers();

            //to avoid json cycle -2
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //next change based on registering

            //repository Normal
            // builder.Services.AddTransient<IUserRepo,UserRepo>();//old methodregistering needed must added or doesnt work

            //Genetic repository
            builder.Services.AddTransient(typeof(IRepository<>), typeof(EntityRepository<>));//note
            builder.Services.AddTransient<ICustomerService, CustomerService>();//new generatic used registering services
            builder.Services.AddTransient<IAccountService, AccountService>();

            //inbuild  only change in front connect
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}