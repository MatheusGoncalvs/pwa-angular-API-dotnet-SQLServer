using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            this._produtoAppService = produtoAppService;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert(IFormFile file)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _produtoAppService.AdicionarProdutos(file));
        }

        [HttpGet("importacoes")]
        public async Task<IActionResult> GetImportacoes()
        {
            try
            {
                var lotes = await _produtoAppService.GetImportacoes();
                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("importacao/{id}")]
        public async Task<IActionResult> GetImportacao(int id)
        {
            try
            {
                var lote = await _produtoAppService.GetImportacao(id);
                return Ok(lote);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete("importacao/{id}")]
        public IActionResult RemoverImportacao(int id)
        {
            try
            {
                var lote = _produtoAppService.RemoverImportacao(id);
                return Ok(lote);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}