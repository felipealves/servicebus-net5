using System.Collections.Generic;

namespace ConsoleApp.ServiceBusTeste.Models
{
    internal class Mensagem
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }

        public Mensagem(string nome, string sobrenome, int idade)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Idade = idade;
        }

        public static List<Mensagem> GetMensagens()
        {
            var mensagens = new List<Mensagem>
            {
                new Mensagem("Nome5", "NomeSobrenome5", 5),
                new Mensagem("Nome6", "NomeSobrenome6", 6),
                new Mensagem("Nome7", "NomeSobrenome7", 7),
                new Mensagem("Nome8", "NomeSobrenome8", 8),
                new Mensagem("Nome9", "NomeSobrenome9", 9),
                new Mensagem("Nome10", "NomeSobrenome10", 10),
                new Mensagem("Nome11", "NomeSobrenome11", 11),
                new Mensagem("Nome12", "NomeSobrenome12", 12),
                new Mensagem("Nome13", "NomeSobrenome13", 13),
                new Mensagem("Nome14", "NomeSobrenome14", 14)
            };

            return mensagens;
        }
    }
}
