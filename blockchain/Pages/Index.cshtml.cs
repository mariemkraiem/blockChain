using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blockchain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nethereum.RPC.Eth.DTOs;

namespace blockchain.Pages
{
    public class IndexModel : PageModel
    {
        public List<BlockWithTransactionHashes> BlockList = new List<BlockWithTransactionHashes>();

        Web3Client w3client = new Web3Client();

        public void OnGet()
        {
            var block = w3client.BlockWithTransactionHashes().GetAwaiter().GetResult();
            
            int startBlock = Math.Max((int)block.Number.Value - 10, 0);

            BlockList.Clear();
            BlockList.TrimExcess();

            for (int i = startBlock; i < block.Number.Value; i++)
            {
                var returned = w3client.BlockWithTransactionHashes(i).GetAwaiter().GetResult();
                BlockList.Add(returned);
            }
        }

    }
}

