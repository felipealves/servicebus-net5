using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.ServiceBusTeste
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("Iniciando configurações Service Bus");
            Console.WriteLine("==============================================");

            var host = CreateDefaultBuilder().Build();

            // Invoke Worker
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var workerInstance = provider.GetRequiredService<Worker>();
            await workerInstance.DoWork();

            //host.Run();

            Console.WriteLine("==============================================");
            Console.WriteLine("Processamento Finalizando. Aperte qualquer tecla para fechar.");
            var sair = Console.ReadKey();
        }
        static IHostBuilder CreateDefaultBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<Worker>();
                });
        }
    }
}
