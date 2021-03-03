using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TriaAPI.Models;
using TriaAPI.ViewModels;

namespace TriaAPI.Configuration
{
    public class ConfigMapping: Profile
    {
        public ConfigMapping()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<ProdutoServico, ProdutoServicoViewModel>().ReverseMap();
           
           
        }
    }
}
