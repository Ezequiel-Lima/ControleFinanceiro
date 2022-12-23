﻿using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.ViewModels;
using ControleFinanceiro.ViewModels.Transacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ControleFinanceiro.Controllers
{
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        [HttpGet("transacao")]
        public async Task<IActionResult> GetAsync(
            [FromServices] ControleFinanceiroDataContext context,
            [FromQuery] int skip = 0, 
            [FromQuery] int take = 25) 
        {
            try
            {
                var entrada = await context
                    .Transacoes
                    .AsNoTracking()
                    .Where(x => x.Tipo == true)
                    .SumAsync(x => x.Valor);

                var saida = await context
                    .Transacoes
                    .AsNoTracking()
                    .Where(x => x.Tipo == false)
                    .SumAsync(x => x.Valor);

                double saldo = (entrada - saida);

                var transacoes = await context
                    .Transacoes
                    .AsNoTracking()
                    .Skip(skip * take)
                    .Take(take)
                    .ToListAsync();           

                return Ok(new ResultViewModel<dynamic>(new
                {
                    skip,
                    take,
                    saldo,
                    transacoes
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X01 - Falha interna no servidor"));
            }
        }

        [HttpGet("transacao/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] ControleFinanceiroDataContext context,
            [FromRoute] Guid id)
        {
            try
            {
                var transacao = await context
                    .Transacoes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (transacao == null)
                    return NotFound(new ResultViewModel<Transacao>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<dynamic>(new
                {
                    transacao
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X02 - Falha interna no servidor"));
            }
        }

        [HttpPost("transacao")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateTransacaoViewModel model,
            [FromServices] ControleFinanceiroDataContext context)
        {
            try
            {
                var transacao = new Transacao
                {
                    Id = Guid.NewGuid(),
                    Descricao = model.Descricao,
                    Valor = model.Valor,
                    Tipo = model.Tipo
                };

                await context.Transacoes.AddAsync(transacao);
                await context.SaveChangesAsync();

                return Created($"transacao{transacao.Id}", new ResultViewModel<Transacao>(transacao));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X03 - Não foi possivel inserir uma transação"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X04 - Falha interna no servidor"));
            }
        }

        [HttpPut("transacao/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] EditorTransacaoViewModel model,
            [FromServices] ControleFinanceiroDataContext context)
        {
            try
            {
                var transacao = await context.Transacoes.FirstOrDefaultAsync(x => x.Id == id);

                if (transacao == null)
                    return NotFound(new ResultViewModel<Transacao>("Conteudo não encontrado"));

                transacao.Descricao = model.Descricao;
                transacao.Valor = model.Valor;
                transacao.Tipo = model.Tipo;
                transacao.DataCriacao = transacao.DataCriacao;
                transacao.DataAtualizacao = DateTime.UtcNow;

                context.Transacoes.Update(transacao);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Transacao>(transacao));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X05 - Não foi possivel alterar uma transação"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X06 - Falha interna no servidor"));
            }
        }

        [HttpDelete("transacao/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] ControleFinanceiroDataContext context)
        {
            try
            {
                var transacao = await context.Transacoes.FirstOrDefaultAsync(x => x.Id == id);

                if (transacao == null)
                    return NotFound(new ResultViewModel<Transacao>("Conteudo não encontrado"));

                context.Transacoes.Remove(transacao);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Transacao>(transacao));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X07 - Não foi possivel deletar uma transação"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Transacao>("01X08 - Falha interna no servidor"));
            }
        }
    }
}
