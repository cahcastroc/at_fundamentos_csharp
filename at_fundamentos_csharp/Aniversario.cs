
using PessoaRepository;
using System;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace at_fundamentos_csharp
{
    class Aniversario
    {
        //private static readonly PessoaService pessoaService;


        static void Main(string[] args)
        {
            var pessoaService = new PessoaService();

            AniversariantesDia();
            Menu();


             void AniversariantesDia()
            {
                Console.WriteLine("-*-*-*-*-Registros de aniversários-*-*-*-*-");
                Console.WriteLine();
                Console.WriteLine("---------Aniversariantes do dia-----------");
                Console.WriteLine();
                try
                {
                    List<Pessoa> aniversariantes = pessoaService.AniversariantesDoDia();

                    foreach (var pessoa in aniversariantes)
                    {
                        Console.WriteLine(pessoa.Nome);
                    }
                }
                catch (NullReferenceException) {
                    Console.WriteLine("Sem aniversariantes no dia!");
                }




            }

             void Menu()
            {
                int opcao;

                do
                {
                    opcao = EscolherMenu();
                    switch (opcao)
                    {
                        case 1:
                            ListaPessoas();
                            break;
                        case 2:
                            BuscarPessoa();
                            break;
                        case 3:
                            AddPessoa();
                            break;
                        case 4:
                            //EditarPessoa()
                            break;
                        case 5:
                            //DeletarPessoa()
                            break;
                        case 0:
                            Console.WriteLine("Programa finalizado!");
                            break;
                    }
                }
                while (opcao != 0);
            }

            static int EscolherMenu()
            {
                int opcao;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Selecione uma das opções abaixo: ");
                    Console.WriteLine("1 - Listar todas as pessoas");
                    Console.WriteLine("2 - Pesquisar uma pessoa");
                    Console.WriteLine("3 - Adicionar nova pessoa");
                    Console.WriteLine("4 - Editar uma pessoa");
                    Console.WriteLine("5 - Remover uma pessoa");
                    Console.WriteLine("0 - Sair");
                    opcao = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                } while (opcao < 0 || opcao > 5);

                return opcao;
            }

             void ListaPessoas()
            {
                Console.WriteLine("*-*-*-*-Listar Pessoas:-*-*-*-*");
                List<Pessoa> lista = pessoaService.ListaPessoas();

                if (lista.Count == 0)
                    Console.WriteLine("Não há registros de pessoas no momento. Adicione pessoas através da opção 3 do Menu!");
                else
                    lista.ForEach(p => Console.WriteLine($"*-*-> Nome: {p.Nome}\nSobrenome: {p.Sobrenome}\n" +
                    $"Data de nascimento: {p.Aniversario.ToString("dd/MM/yyyy")}"));
            }

             void AddPessoa()
            {
                Regex regex = new Regex(@"[a-zA-Z]+");
                try
                {
                    Console.WriteLine("*-*-*-*-Adicionar Pessoa:-*-*-*-*");
                    Console.WriteLine("Digite o nome da pessoa:");
                    var nome = Console.ReadLine();
                    if (!regex.IsMatch(nome))
                        throw new FormatException();

                    Console.WriteLine("Digite o sobrenome da pessoa:");
                    var sobrenome = Console.ReadLine();
                    if (!regex.IsMatch(sobrenome))
                        throw new FormatException();

                    Console.WriteLine("Digite a data de nascimento da pessoa:");
                    var aniversario = Convert.ToDateTime(Console.ReadLine());
                    if (aniversario > DateTime.Now)
                        throw new ArgumentException();

                    ConfirmarOperacao(nome, sobrenome, aniversario);

                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato inválido. Repita a operação!");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Data de aniversário não pode ser maior que a data atual");
                }
            }

           void ConfirmarOperacao(string nome, string sobrenome, DateTime aniversario)
            {


                Console.WriteLine("*-*-*-*-*Os dados abaixo estão corretos?*-*-*-*-*");
                Console.WriteLine($"Nome: {nome}");
                Console.WriteLine($"Sobrenome: {sobrenome}");
                Console.WriteLine($"Aniversário/Data de nascimento: {aniversario.ToString("dd/MM/yyyy")}");
                Console.WriteLine("Digite 1 para Confirmar ou 2 para repetir a operação");
                int opcao = int.Parse(Console.ReadLine());

                if (opcao == 1)
                {
                    pessoaService.AdicionarPessoa(nome, sobrenome, aniversario);
                    Console.WriteLine("Registro salvo com sucesso!");
                }
                else
                {
                    Console.WriteLine("Repita a operação!");
                    Menu();
                }

            }
             void BuscarPessoa()
            {
                Console.WriteLine("*-*-*-*-Pesquisar Pessoa:-*-*-*-*");
                Console.WriteLine("Insira parte ou nome da pessoa que você deseja buscar: ");
                var nome = Console.ReadLine();
                List<Pessoa> pessoas = pessoaService.BuscarPessoa(nome);

                if (pessoas.Count > 0)
                {
                    foreach (var pessoa in pessoas)
                    {
                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");
                        Console.WriteLine($"Id pessoa: {pessoa.Id}");
                        Console.WriteLine($"Nome completo: {pessoa.Nome} {pessoa.Sobrenome}");
                        Console.WriteLine($"Data de aniaversário: {pessoa.Aniversario.Day}/{pessoa.Aniversario.Month}");
                        int proxAniversario = pessoaService.DiasProxAniversario(pessoa.Aniversario);
                        Console.WriteLine($"Dias restantes para o próximo aniversário : {proxAniversario}");

                    }

                }
                else
                {
                    Console.WriteLine("Não foram localizados resultados para a pesquisa");
                }



            }

        }
    }
}

