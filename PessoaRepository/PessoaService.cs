

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PessoaRepository
{
    public class PessoaService : IPessoaRepository
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

        public PessoaService()            
        {
            pessoas = Arquivo.LerPessoas();
        }

        public List<Pessoa> ListaPessoas()
        {
            return pessoas;         
        }

        public void AdicionarPessoa(string nome, string sobrenome, DateTime aniversario)
        {
            Pessoa pessoa = new Pessoa(nome, sobrenome, aniversario);
            pessoa.Id = GerarId();
            pessoas.Add(pessoa);      
        }

        public List<Pessoa> BuscarPessoa(string nome)
        {
            var lista = pessoas.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            return lista;
        }

        public Pessoa? BuscarPessoaPorId(int id)
        {            
            var pessoa = pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                throw new Exception("Pessoa não encontrada");
            }
            return pessoa;
        }
        
        public void EditarPessoa(Pessoa pessoa)
        {
            var pessoaEditada = BuscarPessoaPorId(pessoa.Id);
            if (pessoaEditada != null)
            {
                pessoaEditada.Nome = pessoa.Nome;
                pessoaEditada.Sobrenome = pessoa.Sobrenome;
                pessoaEditada.Aniversario = pessoa.Aniversario;               
            }
        }
              

        public void RemoverPessoa(Pessoa pessoa)
        {      
            pessoas.Remove(pessoa);            
        }

        public int DiasProxAniversario(DateTime aniversario)
        {

            DateTime proxAniversario = new(DateTime.Today.Year, aniversario.Month, aniversario.Day);
            if (DateTime.Today.Month > aniversario.Month)
            {
                proxAniversario = new(DateTime.Today.Year + 1, aniversario.Month, aniversario.Day);
            }
            if (DateTime.Today.Day > aniversario.Day && DateTime.Today.Month == aniversario.Month)
            {
                proxAniversario = new(DateTime.Today.Year + 1, aniversario.Month, aniversario.Day);
            }           
            return (int)proxAniversario.Subtract(DateTime.Today).TotalDays;
        }

        public List<Pessoa> AniversariantesDoDia()
        {
            var lista = pessoas.Where(p => p.Aniversario.Day == DateTime.Now.Day && p.Aniversario.Month == DateTime.Now.Month).ToList();
            return lista;
        }

        private int GerarId()
        {
            Random random = new Random();
            int id = random.Next(1, 500);
            while (pessoas.Any(p => p.Id == id)) {
                id = random.Next(1, 500);
            }         
            return id;
        }

        public void SalvarArquivo() {
            
            Arquivo.atualizarArquivo(pessoas);
        }

    }
}
