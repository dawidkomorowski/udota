using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace UDota.CoreLib.OpenDota
{
    public interface IOpenDotaClient
    {
        Task<IEnumerable<Player>> SearchPlayers(string name);
    }

    public class OpenDotaClient : IOpenDotaClient
    {
        private readonly Uri _baseUrl = new("https://api.opendota.com/api/");

        public async Task<IEnumerable<Player>> SearchPlayers(string name)
        {
            var apiUrl = new Uri(_baseUrl, $"search?q={name}");
            Debug.WriteLine(apiUrl.AbsoluteUri);
            var client = new HttpClient();
            var result = await client.GetFromJsonAsync<IReadOnlyCollection<SearchPlayerDto>>(apiUrl);

            if (result == null)
            {
                throw new InvalidOperationException($"Unexpected null value from API: {apiUrl.AbsoluteUri}");
            }

            return result.Select(dto => new Player
            {
                AccountId = dto.Account_Id,
                Name = dto.PersonaName,
                AvatarFull = new Uri(dto.AvatarFull),
                LastMatchTime = dto.Last_Match_Time
            });
        }

        private sealed class SearchPlayerDto
        {
            // ReSharper disable once InconsistentNaming
            public int Account_Id { get; set; }
            public string PersonaName { get; set; }
            public string AvatarFull { get; set; }

            // ReSharper disable once InconsistentNaming
            public DateTime Last_Match_Time { get; set; }
        }
    }
}