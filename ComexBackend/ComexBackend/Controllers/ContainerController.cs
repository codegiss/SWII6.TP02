using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ComexBackend.Models;

namespace ComexBackend.Controllers
{
    // Giselle Souza - CB3020339
    // Lucas Gomes - CB3021777

    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private static List<Container> containers = new List<Container>
        {
            new Container { Id = 1, Numero = "CONT-1", Tipo = "Dry", Tamanho = 30 },
            new Container { Id = 2, Numero = "CONT-2", Tipo = "Refeer", Tamanho = 25 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Container>> Get()
        {
            return Ok(containers);
        }

        [HttpGet("{id}")]
        public ActionResult<Container> GetById(int id)
        {
            var container = containers.FirstOrDefault(c => c.Id == id);
            if (container == null) return NotFound($"Container com ID {id} não encontrado.");
            return Ok(container);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Container newContainer)
        {
            if (newContainer == null) return BadRequest("Dados inválidos.");

            newContainer.Id = containers.Max(c => c.Id) + 1;
            containers.Add(newContainer);

            return CreatedAtAction(nameof(GetById), new { id = newContainer.Id }, newContainer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Container updatedContainer)
        {
            var container = containers.FirstOrDefault(c => c.Id == id);
            if (container == null) return NotFound($"Container com ID {id} não encontrado.");

            container.Numero = updatedContainer.Numero;
            container.Tipo = updatedContainer.Tipo;
            container.Tamanho = updatedContainer.Tamanho;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var container = containers.FirstOrDefault(c => c.Id == id);
            if (container == null) return NotFound($"Container com ID {id} não encontrado.");

            containers.Remove(container);
            return NoContent();
        }
    }
}
