using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            this._produtoAppService = produtoAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(IFormFile file)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _produtoAppService.AdicionarProdutos(file));
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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
    }
}