using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace blockchain.Model
{
    public class Web3Client
    {
        Web3 web3Connect = new Web3("http://192.168.168.178:8545");
        public async Task<BlockWithTransactions> BlockWithTransactions(int requested = -1)
        {
            try
            {
                HexBigInteger blockNo;

                if (requested == -1)
                {
                    blockNo = await web3Connect.Eth.Blocks.GetBlockNumber.SendRequestAsync();
                }
                else
                {
                    var requestedBI = (BigInteger)requested;
                    blockNo = new HexBigInteger(new BigInteger(requested));
                }

                return await web3Connect.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(blockNo);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Updating block info failed: {e}");

                return null;
            }
        }

        public async Task<BlockWithTransactionHashes> BlockWithTransactionHashes(int requested = -1)
        {
            try
            {
                BlockWithTransactionHashes block;

                if (requested == -1)//Send back the current block
                {
                    var blockNo = await web3Connect.Eth.Blocks.GetBlockNumber.SendRequestAsync();
                    block = await web3Connect.Eth.Blocks.GetBlockWithTransactionsHashesByNumber.SendRequestAsync(blockNo);
                }
                else
                {
                    block = await web3Connect.Eth.Blocks.GetBlockWithTransactionsHashesByNumber.SendRequestAsync(new HexBigInteger(new BigInteger(requested)));
                }
                return block;
            }
            catch (Exception)
            {
                Debug.WriteLine("Crashed getting block with transaction hashes");
                return null;
            }
        }

        public async Task<BlockWithTransactions> BlockByHash(string hash)
        {
            try
            {
                return await web3Connect.Eth.Blocks.GetBlockWithTransactionsByHash.SendRequestAsync(hash);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<string> DeciperTypeOfHash(string hash)
        {
            var trans = await GetTransaction(hash);

            try
            {
                if (!string.IsNullOrEmpty(trans.From))
                {
                    return "Transaction";
                }
            }
            catch (Exception)
            {

            }
            return "Block";
        }

        public async Task<decimal> GetAccountBalance(string hash)
        {
            try
            {
                var account = await web3Connect.Eth.GetBalance.SendRequestAsync(hash);
                return Nethereum.Web3.Web3.Convert.FromWei(account.Value);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error getting account balance: {e}");

                return 0;
            }
        }

        public async Task<Transaction> GetTransaction(string hash)
        {
            try
            {
                var transaction = await web3Connect.Eth.Transactions.GetTransactionByHash.SendRequestAsync(hash);

                return transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
