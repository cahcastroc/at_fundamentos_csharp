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


        public List<Pessoa> ListaPessoas()
        {
            return pessoas;
        }

        public void AdicionarPessoa(string nome, string sobrenome, DateTime aniversario)
        {
            Pessoa pessoa = new Pessoa(nome, sobrenome, aniversario);
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
              

        public void RemoverPessoa(int id)
        {
            var pessoaDelete = BuscarPessoaPorId(id);
            pessoas.Remove(pessoaDelete);
        }

        public int DiasProxAniversario(DateTime aniversario)
        {

            DateTime proxAniversario = new(DateTime.Today.Year, aniversario.Month, aniversario.Day);


            if (DateTime.Today.Month > aniversario.Month)
            {

                proxAniversario = new(DateTime.Today.Year + 1, aniversario.Month, aniversario.Day);

            }
            return (int)proxAniversario.Subtract(DateTime.Today).TotalDays;
        }

        public List<Pessoa> AniversariantesDoDia()
        {
            pessoas.Add(new Pessoa("Camila", "N", DateTime.Now));

            var lista = pessoas.Where(p => p.Aniversario.Day == DateTime.Now.Day && p.Aniversario.Month == DateTime.Now.Month).ToList();
            return lista;
        }


    }
}
