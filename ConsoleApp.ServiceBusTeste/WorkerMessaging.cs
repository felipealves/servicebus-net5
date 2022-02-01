using Azure.Messaging.ServiceBus;
using ConsoleApp.ServiceBusTeste.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp.ServiceBusTeste
{
    internal class WorkerMessaging
    {
        static ServiceBusClient client;

        static ServiceBusSender sender;

        private readonly IConfiguration configuration;

        public WorkerMessaging(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task DoWork()
        {
            //Utilizando a classe Microsoft.Azure.ServiceBus - Depreciada 11/2020
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==============================================");
            Console.WriteLine("Iniciando envio de Mensagem no Service Bus");
            Console.WriteLine("==============================================");

            //Codigo chamando a fila e inserindo mensagem.
            var mensagens = Mensagem.GetMensagens();

            client = new ServiceBusClient(configuration.GetConnectionString("ServiceBus"));

            await SendMessageAsync(mensagens, configuration.GetValue<string>("Topic:NomeTopic"));

            Console.WriteLine("==============================================");
            Console.WriteLine("Mensagens envidas ao Service Bus com sucesso!!!!!!!");
            Console.WriteLine("==============================================");
            Console.ResetColor();
        }

        public async Task SendMessageAsync(List<Mensagem> mensagens, string queueOrTopicName)
        {
            sender = client.CreateSender(queueOrTopicName);

            foreach (var item in mensagens)
            {
                var mensagemBody = JsonSerializer.Serialize(item);

                ServiceBusMessage message = new ServiceBusMessage(mensagemBody);

                await sender.SendMessageAsync(message);
            }
        }
    }
}
