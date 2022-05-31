using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TexodeTaskWin.Service.Logic
{
    public class CardService : ICardService
    {
        private readonly HttpClient _httpClient;

        private CardService(HttpClient httpClient)
            => _httpClient = httpClient;
    }
}
