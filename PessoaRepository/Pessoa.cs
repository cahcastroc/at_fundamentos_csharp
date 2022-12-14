﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PessoaRepository
{
    public class Pessoa
    {
        

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Aniversario { get; set; }


        public Pessoa(string nome, string sobrenome, DateTime aniversario)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Aniversario = aniversario;
        }

        public override string? ToString()
        {
            return $"Nome: {Nome}\nSobrenome: {Sobrenome}\nAniversário: {Aniversario.ToString("dd/MM")}";
        }
    }
}
