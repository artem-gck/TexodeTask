using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Service.Model;

namespace TexodeTask.Service
{
    public interface ICardService
    {
        public Task<int> AddCardAsync(Card card);
        public Task<IEnumerable<Card>> GetAllCardsAsync();
        public Task<Card> GetCardAsync(int id);
        public Task<int> UpdateCardAsync(Card card);
        public Task<int> DeleteCardAsync(int id);
        public Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId);
        public Task<IEnumerable<Card>> SortCardsByNameAsync();
    }
}
