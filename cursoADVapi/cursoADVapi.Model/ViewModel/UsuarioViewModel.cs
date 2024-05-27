﻿using cursoADVapi.Model._Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursoADVapi.Model.ViewModel
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string CpfCnpj { get; set; }

        public string Senha { get; set; }

        public string Src { get; set; }

        public string Cargo { get; set; }

        public List<cursoContratado> ListaCursos { get; set; }


    }
}
