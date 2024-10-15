using ComexBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComexBackend.Controllers
{
    // Giselle Souza - CB3020339
    // Lucas Gomes - CB3021777

    [Route("api/[controller]")]
    [ApiController]
    public class BillOfLadingController : ControllerBase
    {
        private static List<BillOfLading> billOfLadings = new List<BillOfLading>
        {
            new BillOfLading { Id = 1, Numero = "CONT-1", Consignee = "Empresa A", Navio = "SOS 1924" },
            new BillOfLading { Id = 1, Numero = "CONT-2", Consignee = "Empresa B", Navio = "SOS 2077" }
        };
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(billOfLadings);
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            var bl = billOfLadings.FirstOrDefault(b => b.Id == id);
            if (bl == null) return NotFound($"Container com ID {id} não encontrado.");
            return Ok(bl);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BillOfLading newBillOfLading)
        {
            if (newBillOfLading == null) return BadRequest("Dados inválidos.");

            newBillOfLading.Id = billOfLadings.Max(c => c.Id) + 1;
            billOfLadings.Add(newBillOfLading);

            return CreatedAtAction(nameof(GetById), new { id = newBillOfLading.Id }, newBillOfLading);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BillOfLading updatedBillOfLading)
        {
            var billOfLading = billOfLadings.FirstOrDefault(b => b.Id == id);
            if (billOfLading == null) return NotFound($"Container com ID {id} não encontrado.");

            billOfLading.Numero = updatedBillOfLading.Numero;
            billOfLading.Consignee = updatedBillOfLading.Consignee;
            billOfLading.Navio = updatedBillOfLading.Navio;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
