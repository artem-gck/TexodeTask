using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Access.Entity;

namespace TexodeTask.Access
{
    public interface ICardAccess
    {
        public Task<int> AddCardAsync(CardEntity cardEntity);
        public Task<IEnumerable<CardEntity>> GetAllCardsAsync();
        public Task<CardEntity> GetCardAsync(int id);
        public Task<int> UpdateCardAsync(CardEntity cardEntity);
        public Task<int> DeleteCardAsync(int id);
        public Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId);
        public Task<IEnumerable<CardEntity>> SortCardsByNameAsync();
    }
}
