using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Service;
using TexodeTask.Service.Model;

namespace TexodeTask.Controllers
{
    [ApiController]
    [Route("api/cards/")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
            => _cardService = cardService;

        [HttpPost]
        public async Task<IActionResult> AddCardAsync(Card card)
        {
            try
            {
                var answer = await _cardService.AddCardAsync(card);

                return Ok(answer);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetAllCardsAsync()
        {
            try
            {
                var cards = await _cardService.GetAllCardsAsync();

                return Json(cards);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCardAsync(int id)
        {
            try
            {
                var card = await _cardService.GetCardAsync(id);

                return Json(card);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCardAsync(Card card)
        {
            try
            {
                var answer = await _cardService.UpdateCardAsync(card);

                return Ok(answer);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardAsync(int id)
        {
            try
            {
                var answer = await _cardService.DeleteCardAsync(id);

                return Ok(answer);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListOFCardsAsync(IEnumerable<int> listOfId)
        {
            try
            {
                var answer = await _cardService.DeleteListOFCardsAsync(listOfId);

                return Ok(answer);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<Card>>> SortCardsByNameAsync()
        {
            try
            {
                var cards = await _cardService.SortCardsByNameAsync();

                return Json(cards);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
