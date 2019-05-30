using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nethereum.RPC.Eth.DTOs;

namespace blockchain.Pages
{
    public class BlockModel : PageModel
    {
        public BlockWithTransactions block { get; set; }
        public void OnGet()
        {
            var query = Request.QueryString.Value.Replace("?", "");
            Model.Web3Client w3client = new Model.Web3Client();
            if (int.TryParse(query, out int blockNumber))//If the query is an int, assume it's a block number
            {
                block = w3client.BlockWithTransactions(blockNumber).GetAwaiter().GetResult();
            }
            else if (query.Length == 66)
            {
                block = w3client.BlockByHash(query).GetAwaiter().GetResult();
            }
            else
            {
                block = w3client.BlockWithTransactions().GetAwaiter().GetResult();
            }

        }
    }
}