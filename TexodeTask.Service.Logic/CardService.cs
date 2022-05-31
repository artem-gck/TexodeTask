using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexodeTask.Access;
using TexodeTask.Access.Entity;
using TexodeTask.Service.Model;

namespace TexodeTask.Service.Logic
{
    public class CardService : ICardService
    {
        private readonly ICardAccess _cardAccess;
        private readonly IMapper _mapperCard;
        private readonly IMapper _mapperCardEntity;

        public CardService(ICardAccess cardAccess)
        { 
            _cardAccess = cardAccess;

            var configCard = new MapperConfiguration(cfg => cfg.CreateMap<CardEntity, Card>());
            var configCardEntity = new MapperConfiguration(cfg => cfg.CreateMap<Card, CardEntity>());

            _mapperCard = new Mapper(configCard);
            _mapperCardEntity = new Mapper(configCardEntity);
        }

        public async Task<int> AddCardAsync(Card card)
        {
            card = card ?? throw new ArgumentNullException(nameof(card), "Card is null");

            var cardEntity = _mapperCardEntity.Map<CardEntity>(card);

            return await _cardAccess.AddCardAsync(cardEntity);
        }

        public async Task<int> DeleteCardAsync(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            return await _cardAccess.DeleteCardAsync(id);
        }

        public async Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId)
        {
            listOfId = listOfId ?? throw new ArgumentNullException(nameof(listOfId), "List of id is null");

            foreach (var id in listOfId)
                if (id < 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            return await _cardAccess.DeleteListOFCardsAsync(listOfId);
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            var cardsEntity = await _cardAccess.GetAllCardsAsync();

            return cardsEntity.Select(card => _mapperCard.Map<Card>(card)).AsEnumerable();
        }

        public async Task<Card> GetCardAsync(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            var card = await _cardAccess.GetCardAsync(id);

            return _mapperCard.Map<Card>(card);
        }

        public async Task<IEnumerable<Card>> SortCardsByNameAsync()
        {
            var cardsEntity = await _cardAccess.SortCardsByNameAsync();

            return cardsEntity.Select(card => _mapperCard.Map<Card>(card)).AsEnumerable();
        }

        public async Task<int> UpdateCardAsync(Card card)
        {
            card = card ?? throw new ArgumentNullException(nameof(card), "Card is null");

            var cardEntity = _mapperCardEntity.Map<CardEntity>(card);

            return await _cardAccess.UpdateCardAsync(cardEntity);
        }
    }
}
