using AutoMapper;
using cursoADVapi.Model.ViewModel;
using MongoDB.Bson;
using System;
using System.Linq;

namespace ProAdvCore.Model.Mappers
{
    public static class AMapper
    {
        public static IMapper RegisterMappings()
        {
            var config = new MapperConfiguration(map =>
            {
                map.CreateMap<CursoViewModel, CursoViewModel>();

                //map.CreateMap<UsuarioModel, UsuarioViewModel>()
                //.AfterMap((model, view) => {
                //    view.DataNascimento = model.DtNascimento ?? DateTime.MinValue;
                //    view.TelefonePrincipal = model.Admin != null && !string.IsNullOrEmpty(model.Admin.TelefonePrincipal) ? model.Admin.TelefonePrincipal : string.Empty;
                //    view.EmailAtivo = model.GetEmailAtivo();
                //    view.Nome = model.Nome.Split(' ')[0]?.Trim();
                //    view.NomeCompleto = model.Nome.Trim();
                //    view.UsuarioAdminId = model.Admin.UsuarioAdminId;
                //    view.IntegracaoTifluxId = model.Admin.IntegracaoTifluxId;
                //    view.VisualizarModalIntimacao = model.Admin.VisualizarModalIntimacao;

                //    if (string.IsNullOrEmpty(view.Sobrenome))
                //        view.Sobrenome = model.Nome.Replace(model.Nome.Split(' ')[0], "")?.Trim();
                //});
                //.ForMember(x => x.Senha, opt => opt.Ignore());

                //map.CreateMap<ProcessoViewModel, ProcessoModel>()
                //.ForMember(model => model.Autores,
                //    m => m.MapFrom(view => view.Autores.Select(x => new ParteModel { Id = ObjectId.GenerateNewId().ToString(), Nome = x.ToString() }).ToList()))
                //.ForMember(model => model.Reus,
                //    m => m.MapFrom(view => view.Reus.Select(x => new ParteModel { Id = ObjectId.GenerateNewId().ToString(), Nome = x.ToString() }).ToList()))
                //.ForMember(view => view.ProcessosVinculados,
                //    m => m.MapFrom(model => model.ProcessosVinculados.Select(x => new ProcessoVinculado { NumeroProcesso = x.NumeroProcesso, DataCadastro = x.DataCadastro, Status = x.Status }).ToList()))
                //.AfterMap((model, view) =>
                //{
                //    view.Status = model.Status;
                //});


            });

            return config.CreateMapper();
        }
    }
}
