
namespace PessoaRepository
{
    public interface IPessoaRepository
    {
        public List<Pessoa> ListaPessoas();
        public void AdicionarPessoa(string nome, string sobrenome, DateTime aniversario);
        public List<Pessoa> BuscarPessoa(string nome);
        public Pessoa? BuscarPessoaPorId(int id);
        public void EditarPessoa(Pessoa pessoa);
        public void RemoverPessoa(Pessoa pessoa);
    }
}