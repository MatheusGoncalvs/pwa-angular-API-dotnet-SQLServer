using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.ViewModels;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Reader.Domain;

namespace api.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAppService(IMapper mapper, IProdutoRepository produtoRepository)
        {
            this._mapper = mapper;
            this._produtoRepository = produtoRepository;
        }
        public async Task<ValidationResult> AdicionarProdutos(IFormFile file)
        {
            var produtos = await this.TranformarTabelaExcelParaListaProdutos(file);
            var lote = new Lote(produtos);

            if (!ValidarProdutos(produtos))
            {
                return RetornarErrosValidacao(produtos);
            }

            await _produtoRepository.AdicionarLote(lote);
            await _produtoRepository.Commit();

            return RetornarErrosValidacao(produtos);
        }

        private ValidationResult RetornarErrosValidacao(List<Produto> produtos)
        {
            foreach (var produto in produtos)
            {
                if (!produto.EhValido()) return produto.ValidationResult;
            }

            return produtos.FirstOrDefault().ValidationResult;
        }

        public async Task<List<ProdutoViewModel>> GetImportacao(int id)
        {
            return _mapper.Map<List<ProdutoViewModel>>(await _produtoRepository.GetImportacao(id));
        }

        public async Task<List<LoteViewModel>> GetImportacoes()
        {
            return _mapper.Map<List<LoteViewModel>>(await _produtoRepository.GetImportacoes());
        }

        public async Task<List<Produto>> TranformarTabelaExcelParaListaProdutos(IFormFile file)
        {
            List<Produto> produtos = new List<Produto>();

            if (file == null || file.Length == 0)
            {
                throw new Exception("Arquivo passado está nulo.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var totalLinhas = worksheet.Dimension?.Rows;
                    var totalColunas = worksheet.Dimension?.Columns;

                    for (int row = 2; row <= totalLinhas; row++)
                    {
                        var Nome = worksheet.Cells[row, 2].Value.ToString();
                        var Quantidade = Int32.Parse(worksheet.Cells[row, 3].Value.ToString());
                        var ValorUnitario = Double.Parse(worksheet.Cells[row, 4].Value.ToString());
                        var DataEntrega = DateTime.Parse(worksheet.Cells[row, 1].Value.ToString());

                        var produto = new Produto(Nome, Quantidade, ValorUnitario, DataEntrega);

                        produtos.Add(produto);
                    }
                    return produtos;
                }
            }
        }

        public bool ValidarProdutos(List<Produto> produtos)
        {
            foreach (var produto in produtos)
            {
                if (!produto.EhValido()) return false;
            }

            return true;
        }
    }
}