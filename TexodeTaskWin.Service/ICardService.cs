using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexodeTaskWin.Service.Model;

namespace TexodeTaskWin.Service
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();
    }
}
