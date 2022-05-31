using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexodeTask.Access.Entity;
using TexodeTask.Access.Entity.Context;

namespace TexodeTask.Access.SqlServer
{
    public class CardAccess : ICardAccess
    {
        private readonly CardContext _cardContext;

        public CardAccess(CardContext cardContext)
            => _cardContext = cardContext;

        public async Task<int> DeleteCard(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            var card = await _cardContext.Cards.FindAsync(id);
            _cardContext.Cards.Remove(card);

            return await _cardContext.SaveChangesAsync();
        }

        public async Task<int> DeleteListOFCards(IEnumerable<int> listOfId)
        {
            listOfId = listOfId ?? throw new ArgumentNullException(nameof(listOfId), "List of id is null");

            foreach (var id in listOfId)
                if (id < 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            var cards = await _cardContext.Cards.Where(card => listOfId.Contains(card.Id)).ToListAsync();
            _cardContext.Cards.RemoveRange(cards);

            return await _cardContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CardEntity>> GetAllCardsAsync()
            => await _cardContext.Cards.ToListAsync();

        public async Task<CardEntity> GetCardAsync(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            return await _cardContext.Cards.FindAsync(id);
        }

        public async Task<IEnumerable<CardEntity>> SortCardsByName()
            => await _cardContext.Cards.OrderBy(card => card.Name).ToListAsync();

        public async Task<int> UpdateCard(CardEntity cardEntity)
        {
            cardEntity = cardEntity ?? throw new ArgumentNullException(nameof(cardEntity), "Card is null");

            var card = await _cardContext.Cards.FindAsync(cardEntity.Id);

            card.Name = cardEntity.Name;
            card.Photo = cardEntity.Photo;

            return await _cardContext.SaveChangesAsync();
        }
    }
}
