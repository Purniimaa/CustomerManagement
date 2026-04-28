using CustomerManagement.Helper;
using CustomerManagement.Repositories.ICustomerRepositories;
using CustomerManagement.Repositories.IDbConnection;
using CustomerManagement.Repositories.ITransactionRepositories;
using CustomerManagement.Services;

namespace CustomerManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

        
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "customer";
                config.Title = "Customer API";
                config.ApiGroupNames = new[] { "customer" };
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Customer API";
                };
            });

            builder.Services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "transactions";
                config.Title = "Transactions API";
                config.ApiGroupNames = new[] { "transactions" };
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Transactions API";
                };
            });

            builder.Services.AddScoped<IConnection, DbConnectionServices>();
            builder.Services.AddScoped<ICustomer, CustomerServices>();
            builder.Services.AddScoped<DapperHelper>();
            builder.Services.AddScoped<ITransaction, TransactionServices>();
          

            var app = builder.Build();
            app.UseHttpsRedirection();

            app.UseOpenApi(settings =>
            {
                settings.Path = "/v1/swagger/{documentName}.json";
            });

            app.UseSwaggerUi(settings =>
            {
                settings.Path = "/v1/swagger/customer.html";
                settings.DocumentPath = "/v1/swagger/customer.json";
            });

            app.UseSwaggerUi(settings =>
            {
                settings.Path = "/v1/swagger/transactions.html";
                settings.DocumentPath = "/v1/swagger/transactions.json";
            });

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}


