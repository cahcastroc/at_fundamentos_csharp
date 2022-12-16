using PessoaRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PessoaRepository
{
    static class Arquivo
    {
        private static string arquivo = "registros_pessoas.txt";       
                  

        public static void atualizarArquivo(List<Pessoa> pessoa)
        {

            if (!File.Exists(arquivo))
            {
                File.Create(arquivo);
            }
            using (StreamWriter fluxo = new StreamWriter(arquivo))
            {
                foreach (var item in pessoa)
                {
                    string linha = item.Id + "|" + item.Nome + "|" + item.Sobrenome + "|" + item.Aniversario.ToString("dd/MM/yyyy");
                    fluxo.WriteLine(linha);
                }
            }
        }

       
        public static List<Pessoa> LerPessoas()
        {

            if (!File.Exists(arquivo))
            {
                File.Create(arquivo);
            }
            List<Pessoa> pessoas = new List<Pessoa>();
            using (StreamReader fluxo = new StreamReader(arquivo))
            {
                
                string linha;
                while ((linha = fluxo.ReadLine()) != null) {                    
                    var infoPessoa = linha.Split('|');
                    Pessoa pessoa = new Pessoa(infoPessoa[1], infoPessoa[2], DateTime.Parse(infoPessoa[3]));
                    pessoa.Id = int.Parse(infoPessoa[0]);
                    pessoas.Add(pessoa);
                }              
            }

            return pessoas;
        }     
    }
}
