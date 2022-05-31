using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Access;
using TexodeTask.Access.Entity;
using TexodeTask.Service.Model;

namespace TexodeTask.Service.Logic
{
    public class CardService : ICardService
    {
        private readonly ICardAccess _cardAccess;
        private readonly IMapper _mapperIEnumerable;
        private readonly IMapper _mapperCard;
        private readonly IMapper _mapperCardEntity;

        public CardService(ICardAccess cardAccess)
        { 
            _cardAccess = cardAccess;

            var configIEnumerable = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<CardEntity>, IEnumerable<Card>>());
            var configCard = new MapperConfiguration(cfg => cfg.CreateMap<CardEntity, Card>());
            var configCardEntity = new MapperConfiguration(cfg => cfg.CreateMap<Card, CardEntity>());

            _mapperIEnumerable = new Mapper(configIEnumerable);
            _mapperCard = new Mapper(configCard);
            _mapperCardEntity = new Mapper(configCardEntity);
        }

        public async Task<int> DeleteCard(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            return await _cardAccess.DeleteCard(id);
        }

        public async Task<int> DeleteListOFCards(IEnumerable<int> listOfId)
        {
            listOfId = listOfId ?? throw new ArgumentNullException(nameof(listOfId), "List of id is null");

            foreach (var id in listOfId)
                if (id < 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            return await _cardAccess.DeleteListOFCards(listOfId);
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            var cardsEntity = await _cardAccess.GetAllCardsAsync();

            return _mapperIEnumerable.Map<IEnumerable<Card>>(cardsEntity);
        }

        public async Task<Card> GetCardAsync(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            var card = await _cardAccess.GetCardAsync(id);

            return _mapperCard.Map<Card>(card);
        }

        public async Task<IEnumerable<Card>> SortCardsByName()
        {
            var cardsEntity = await _cardAccess.SortCardsByName();

            return _mapperIEnumerable.Map<IEnumerable<Card>>(cardsEntity);
        }

        public async Task<int> UpdateCard(Card card)
        {
            card = card ?? throw new ArgumentNullException(nameof(card), "Card is null");

            var cardEntity = _mapperCardEntity.Map<CardEntity>(card);

            return await _cardAccess.UpdateCard(cardEntity);
        }
    }
}
