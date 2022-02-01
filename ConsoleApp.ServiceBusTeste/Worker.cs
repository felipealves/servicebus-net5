using ConsoleApp.ServiceBusTeste.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp.ServiceBusTeste
{
    internal class Worker
    {
        private readonly IConfiguration configuration;

        public Worker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task DoWork()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==============================================");
            Console.WriteLine("Iniciando envio de Mensagem no Service Bus");
            Console.WriteLine("==============================================");

            //Codigo chamando a fila e inserindo mensagem.
            var mensagens = Mensagem.GetMensagens();

            await SendMessageAsync(mensagens, configuration.GetValue<string>("Topic:NomeTopic"));

            Console.WriteLine("==============================================");
            Console.WriteLine("Mensagens envidas ao Service Bus com sucesso!!!!!!!");
            Console.WriteLine("==============================================");
            Console.ResetColor();
        }

        public async Task SendMessageAsync(List<Mensagem> mensagens, string queueName)
        {
            var topicClient = new TopicClient(configuration.GetConnectionString("ServiceBus"), queueName);

            foreach (var item in mensagens)
            {
                var mensagemBody = JsonSerializer.Serialize(item);

                var mensagem = new Message(Encoding.UTF8.GetBytes(mensagemBody));

                await topicClient.SendAsync(mensagem);
            }
        }
    }
}
