using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriaAPI.Models;
using TriaAPI.Repositorio;
using TriaAPI.ViewModels;

namespace TriaAPI.Controllers
{
    [Route("api/produtoservico")]
    public class ProdutoServicoController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ProdutoServicoRepositorio produtoServicoRepositorio;

        public ProdutoServicoController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            produtoServicoRepositorio = new ProdutoServicoRepositorio(_context);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoServicoViewModel model)
        {
            try
            {
                ProdutoServico obj = _mapper.Map<ProdutoServico>(model);
                obj.Ativo = true;
                obj.Excluido = false;


                await produtoServicoRepositorio.AddAndSaveAsync(obj);
                model.Id = obj.Id;

                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<ProdutoServico> list = produtoServicoRepositorio.FindBy(p => p.Excluido != true).ToList();

                List<ProdutoServicoViewModel> modelList = new List<ProdutoServicoViewModel>();

                foreach (ProdutoServico item in list)
                {
                    ProdutoServicoViewModel model = _mapper.Map<ProdutoServicoViewModel>(item);
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
                ProdutoServico produtoServico = produtoServicoRepositorio.FindBy(p => p.Id == id && p.Excluido != true).FirstOrDefault();

                if (produtoServico == null)
                    return NotFound();

                ProdutoServicoViewModel model = _mapper.Map<ProdutoServicoViewModel>(produtoServico);

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
                ProdutoServico obj = await produtoServicoRepositorio.FindAsync(id);

                if (obj == null)
                    return NotFound();

                obj.Excluido = true;

                await produtoServicoRepositorio.EditAndSaveAsync(obj);

                return Ok("Excluido!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ProdutoServicoViewModel model)
        {
            try
            {
                ProdutoServico obj = await produtoServicoRepositorio.FindAsync(id);

                if (obj == null)
                    return NotFound("Erro ao atualizar");

                obj.Descricao = model.Descricao;
                obj.Ativo = true;
                obj.Excluido = false;

                await produtoServicoRepositorio.EditAndSaveAsync(obj);

                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
