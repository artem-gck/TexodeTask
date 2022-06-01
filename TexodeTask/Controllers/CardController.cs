using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Service;
using TexodeTask.Service.Model;

namespace TexodeTask.Controllers
{
    /// <summary>
    /// Controller for cards.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("api/cards/")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardController"/> class.
        /// </summary>
        /// <param name="cardService">The card service.</param>
        public CardController(ICardService cardService)
            => _cardService = cardService;

        /// <summary>
        /// Adds the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>Id of added card.</returns>
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

        /// <summary>
        /// Gets all cards asynchronous.
        /// </summary>
        /// <returns>All cards.</returns>
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

        /// <summary>
        /// Gets the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Card by id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCardAsync(int id)
        {
            try
            {
                var card = await _cardService.GetCardAsync(id);

                if (card is null)
                    return NotFound();

                return Json(card);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Updates the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>Id of updated card.</returns>
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

        /// <summary>
        /// Deletes the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Id of deleted card.</returns>
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

        /// <summary>
        /// Deletes the list of cards asynchronous.
        /// </summary>
        /// <param name="listOfId">The list of identifier.</param>
        /// <returns>Number of deleted cards.</returns>
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

        /// <summary>
        /// Sorts the cards by name asynchronous.
        /// </summary>
        /// <returns>Sorted cards.</returns>
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
