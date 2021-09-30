using Projeto22.Exceptions;
using Projeto22.Input_Model;
using Projeto22.Services;
using Projeto22.View_Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto22.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class livrosController : ControllerBase
    {
        private readonly livroser _livroService;

        public LivrosController(ILivroServices livroService)
        {
            _livroService = livroService;
        }

        ///<summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="pagina"></param>
        /// <param name="quantidade"></param>
        /// <response code="500"></response>
        /// <response code="504"></response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livros_View_Model>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var livros = await _livroService.Obter(pagina, quantidade);

            if (livros.Count() == 0)
                return NoContent();

            return Ok(livros);
        }

        /// <summary>
        /// </summary>
        /// <param name="idLivro"></param>
        /// <response code="500"></response>
        /// <response code="504"></response>   
        [HttpGet("{idLivro:guid}")]
        public async Task<ActionResult<Livros_View_Model>> Obter([FromRoute] Guid idLivro)
        {
            var livro = await _livroService.Obter(idLivro);

            if (livro == null)
                return NoContent();

            return Ok(livro);
        }

        /// <summary>
        /// </summary>
        /// <param name="livroInputModel"></param>
        /// <response code="500"></response>
        /// <response code="422">Caso já exista livros com o mesmo autor</response>   
        [HttpPost]
        public async Task<ActionResult<Livro_View_Model>> InserirLivro([FromBody] Livros_Input_Model livroInputModel)
        {
            try
            {
                var livro = await _livroService.Inserir(livroInputModel);

                return Ok(livro);
            }
            catch (Livro_Cadastrado_Exception ex)
            {
                return UnprocessableEntity("Já existe um livro com este nome para este autor");
            }
        }

        /// <summary>
        /// Atualizar um livro no catálogo
        /// </summary>
        /// /// <param name="idLivro"></param>
        /// <param name="livroInputModel"></param>
        /// <response code="500"></response>
        /// <response code="404"></response>   
        [HttpPut("{idLivro:guid}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromBody] Livros_Input_Model livroInputModel)
        {
            try
            {
                await _livroService.Atualizar(idLivro, livroInputModel);

                return Ok();
            }
            catch (Livro_Nao_Cadastrado_Exception ex)
            {
                return NotFound("Não existe este livro");
            }
        }

        /// <summary>
        /// </summary>
        /// /// <param name="idLivro"></param>
        /// <param name="preco"></param>
        /// <response code="500"></response>
        /// <response code="404"></response>   
        [HttpPatch("{idLivro:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromRoute] double preco)
        {
            try
            {
                await _livroService.Atualizar(idLivro, preco);

                return Ok();
            }
            catch (Livro_Nao_Cadastrado_Exception ex)
            {
                return NotFound("Não existe este livro");
            }
        }

        /// <summary>
        /// </summary>
        /// /// <param name="idLivro"></param>
        /// <response code="500"></response>
        /// <response code="404"></response>   
        [HttpDelete("{idLivro:guid}")]
        public async Task<ActionResult> ApagarLivro([FromRoute] Guid idLivro)
        {
            try
            {
                await _livroService.Remover(idLivro);

                return Ok();
            }
            catch (Livro_Nao_Cadastrado_Exception ex)
            {
                return NotFound("Não existe este livro");
            }
        }

    }
}
