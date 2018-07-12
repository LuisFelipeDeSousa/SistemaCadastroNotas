using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroNotas
{
    class Alunos
    {
        public string Nome { get; set; }

        public string CodigoMatricula { get; set; }

        public double Nota1 { get; set; }

        public double Nota2 { get; set; }

        public double Nota3 { get; set; }

        public byte Frequencia { get; set; }

        public int Id { get; set; }

        internal void Add(Alunos alunos)
        {
            throw new NotImplementedException();
        }
    }
}
