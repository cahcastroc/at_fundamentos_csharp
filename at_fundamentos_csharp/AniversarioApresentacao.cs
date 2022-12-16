using PessoaRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace at_fundamentos_csharp
{
    public class AniversarioApresentacao
    {
        PessoaService pessoaService = new PessoaService();
        
        private void AniversariantesDia()
        {
            Console.WriteLine("-*-*-*-*-Registros de aniversários-*-*-*-*-");
            Console.WriteLine();
            Console.WriteLine("---------Aniversariantes do dia-----------");            
            Console.WriteLine();
            List<Pessoa> aniversariantes = pessoaService.AniversariantesDoDia();

            if (aniversariantes.Count == 0)
            {
                Console.WriteLine("Nenhum aniversariante no dia de hoje");
            }
            else
            {
                foreach (var pessoa in aniversariantes)
                {
                    Console.WriteLine(pessoa.Nome, pessoa.Sobrenome);
                }
            }
            Console.WriteLine("------------------------------------------");
        }

        public void Menu()
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
                        EditarPessoa();                     
                        break;
                    case 5:
                        DeletarPessoa();                      
                        break;
                    case 0:
                        pessoaService.SalvarArquivo();
                        Console.WriteLine("Programa finalizado!");
                        break;
                }
            }
            while (opcao != 0);
        }

        private int EscolherMenu()
        {
            
            Console.WriteLine();
            AniversariantesDia();
            try
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
                    Console.WriteLine("0 - Sair e Salvar arquivo");
                    opcao = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                } while (opcao < 0 || opcao > 5);

                return opcao;
            }
            catch (Exception) {
                Console.WriteLine("Algo deu errado. Tente novamente");                
                return -1;
            }
        }

        private void ListaPessoas()
        {
            Console.Clear();
            Console.WriteLine("*-*-*-*-Listar Pessoas:-*-*-*-*");
            Console.WriteLine();
            List<Pessoa> lista = pessoaService.ListaPessoas();

            if (lista.Count == 0)
                Console.WriteLine("Não há registros de pessoas no momento. Adicione pessoas através da opção 3 do Menu!");
            else
                lista.ForEach(p => Console.WriteLine($"|Id: {p.Id}| -> Nome: {p.Nome} {p.Sobrenome}\n" +
                $"Aniversário: {p.Aniversario.ToString("dd/MM")}"));                     
        }

        private void AddPessoa()
        {
            Console.Clear();
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

                Console.WriteLine("Digite a data de nascimento da pessoa (dd/mm/aaaa):");
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
                Console.WriteLine("Data de nascimento não pode ser maior que a data atual");
            }
        }

        private void ConfirmarOperacao(string nome, string sobrenome, DateTime aniversario)
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

        private void BuscarPessoa()
        {
            Console.Clear();
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


        private void EditarPessoa()
        {
            Console.Clear();
            Console.WriteLine("Digite o ID da pessoa que deseja editar, caso não saiba retorne ao menu e escolha a opção 2 para pesquisar:");
            
            try
            {
                int id = int.Parse(Console.ReadLine());
                Pessoa pessoa = pessoaService.BuscarPessoaPorId(id);
                Console.WriteLine($"Nome completo da pessoa a ser editada: {pessoa.Nome} {pessoa.Sobrenome} ");
                Console.WriteLine("Digite o novo nome da pessoa:");
                pessoa.Nome = Console.ReadLine();
                Console.WriteLine("Digite o novo sobrenome da pessoa:");
                pessoa.Sobrenome = Console.ReadLine();
                Console.WriteLine("Digite a nova data de aniversário da pessoa:");
                pessoa.Aniversario = DateTime.Parse(Console.ReadLine());
                pessoaService.EditarPessoa(pessoa);
                Console.WriteLine("Pessoa editada com sucesso!");
            }
            catch (Exception e) {
                Console.WriteLine("Operação não concluída, verifique os dados e tente novamente");
            }            
        }

        private void DeletarPessoa()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Digite o ID da pessoa que deseja deletar, caso não saiba retorne ao menu e escolha a opção 2 para pesquisar:");
                int id = int.Parse(Console.ReadLine());
                Pessoa pessoa = pessoaService.BuscarPessoaPorId(id);
                Console.WriteLine($"Nome completo da pessoa a ser deletada: {pessoa.Nome} {pessoa.Sobrenome} ");
                Console.WriteLine("Deseja realmente deletar? Digite 1 para 'SIM' ou qualquer número para 'NÃO'");
                int opcao = int.Parse(Console.ReadLine());
                if (opcao == 1)
                {
                    pessoaService.RemoverPessoa(pessoa);
                    Console.WriteLine("Pessoa deletada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Operação cancelada!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro na operação.Tente novamente");
            }
        }        
    }
}
