using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blockchain.Model
{
    public static class Recherche
    {
        public static string recherche(string textSearch)
        {
            int blockNumber;
            if (int.TryParse(textSearch, out blockNumber))
            {
                return $"/Block?{textSearch}";
            }

            Web3Client w3client = new Web3Client();

            switch (textSearch.Length)
            {
                case 42:
                    return $"/Address?{textSearch}";

                case 66:
                    return $"/{w3client.DeciperTypeOfHash(textSearch).GetAwaiter().GetResult()}?{textSearch}";

                default:
                    return "/Index";
            }
        }
    }
}
