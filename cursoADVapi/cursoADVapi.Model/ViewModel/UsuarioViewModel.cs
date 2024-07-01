using cursoADVapi.Model._Models.Usuario;
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

        public string idAsaas { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }
        
        public string Ip { get; set; }

        public string CpfCnpj { get; set; }

        public string Senha { get; set; }

        public string Src { get; set; }

        public string Cargo { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public CartaoCreditoViewModel Cartao { get; set; }

        public List<cursoContratado> ListaCursos { get; set; }


    }

    public class EnderecoViewModel
    {
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Rua { get; set; }

    }

    public class CartaoCreditoViewModel
    {
        public string Id { get; set; }

        public string NomeCartao { get; set; }

        public string NumeroCartao { get; set; }

        public string MesExpira { get; set; }

        public string AnoExpira { get; set; }

        public string Cvv { get; set; }

        public string NumeroFinal { get; set; }

        public string Bandeira { get; set; }

        public string Token { get; set; }

    }


}
