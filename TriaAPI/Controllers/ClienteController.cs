using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriaAPI.Models;
using TriaAPI.Repositorio;
using TriaAPI.ViewModels;

namespace TriaAPI.Controllers
{
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ClienteRepositorio clienteRepositorio;
        private readonly ProdutoServicoRepositorio produtoServicoRepositorio;

        public ClienteController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            clienteRepositorio = new ClienteRepositorio(_context);
            produtoServicoRepositorio = new ProdutoServicoRepositorio(_context);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteViewModel model)
        {
            try
            {
                Cliente obj = _mapper.Map<Cliente>(model);
                obj.Ativo = true;
                obj.Excluido = false;

                obj.Produto = await produtoServicoRepositorio.FindAsync(model.idProduto);

                await clienteRepositorio.AddAndSaveAsync(obj);

                model.Id = obj.Id;

                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("Busca")]
        public async Task<IActionResult> GetF([FromBody] Filtro filtro)
        {
            try
            {
                List<Cliente> list = clienteRepositorio.FindBy(p => p.Excluido != true).ToList();
                if (filtro != null && !string.IsNullOrEmpty(filtro.Busca))
                {
                    list = list.Where(p => p.Empresa.Contains(filtro.Busca) || p.NomeCompleto.Contains(filtro.Busca)).ToList();
                }
                List<ClienteViewModel> modelList = new List<ClienteViewModel>();

                foreach (Cliente item in list)
                {
                    ClienteViewModel model = _mapper.Map<ClienteViewModel>(item);
                    model.Id = item.Id;
                    modelList.Add(model);
                }
                return Ok(modelList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                Cliente cliente = clienteRepositorio.FindBy(p => p.Id == id && p.Excluido != true).FirstOrDefault();

                if (cliente == null)
                    return NotFound();

                ClienteViewModel model = _mapper.Map<ClienteViewModel>(cliente);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Cliente obj = await clienteRepositorio.FindAsync(id);

                if (obj == null)
                    return NotFound();

                obj.Excluido = true;

                await clienteRepositorio.EditAndSaveAsync(obj);

                return Ok("Excluido!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ClienteViewModel model)
        {
            try
            {
                Cliente obj = await clienteRepositorio.FindAsync(id);

                if (obj == null)
                    return NotFound("Erro ao atualizar");

                obj.Empresa = model.Empresa;
                obj.NomeCompleto = model.NomeCompleto;
                obj.Telefone = model.Telefone;
                obj.Email = model.Email;
                obj.Conversa = model.Conversa;
                obj.Data = model.Data;
                obj.HoraAtendimento = model.HoraAtendimento;
                obj.Ativo = true;
                obj.Excluido = false;

                await clienteRepositorio.EditAndSaveAsync(obj);

                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetByDate()
        {
            try
            {
                List<Cliente> obj = clienteRepositorio.GetAll().OrderBy(p => p.Data).ToList();
                if (obj == null)
                    return NotFound();

                List<ClienteViewModel> model = _mapper.Map<List<ClienteViewModel>>(obj);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("Relatorio")]
        public async Task<IActionResult> GetRelarorio()
        {
            try
            {
                List<Cliente> obj = clienteRepositorio.GetAll().OrderBy(p => p.NomeCompleto).ToList();
                if (obj == null)
                    return NotFound();

                List<ClienteViewModel> model = _mapper.Map<List<ClienteViewModel>>(obj);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
    }
}
