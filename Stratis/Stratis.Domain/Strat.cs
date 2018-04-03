using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratis.Domain
{
    public class Strat
    {
        private readonly QBitNinjaClient client;
        public readonly Network network;
        public readonly Key privateKey;
        public readonly BitcoinSecret secret;
        public readonly string PubKey;
        public Strat(string privateKey, Network network)
        {
            this.network = network;
            this.privateKey = Key.Parse(privateKey, network);
            secret = new BitcoinSecret(privateKey, network);
            var address = secret.GetAddress();
            client = new QBitNinjaClient(network);
            PubKey = this.privateKey.PubKey.GetAddress(network).ToString();
        }
        public BitcoinPubKeyAddress GetAddress()
        {
            return secret.GetAddress();
        }
        public async Task<BroadcastResponse> Send(string to, Money amount, Money minerfee, string message = "")
        {

            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            var xxx = client.GetBalance(privateKey.ScriptPubKey, true).Result;
            
                
            OutPoint outPointToSpend = null;
            var trans = xxx.Operations.First().ReceivedCoins;

            foreach (var coin in trans)
            {
                if (coin.TxOut.ScriptPubKey == privateKey.ScriptPubKey)
                {
                    outPointToSpend = coin.Outpoint;
                }
            }
            Money txInAmount = null;
            if (trans.Count == 1)
                txInAmount = (Money)trans.First().Amount;
            else
                txInAmount = (Money)trans[(int)outPointToSpend.N].Amount;
            var changeAmount = txInAmount - amount - minerfee;
            var hallOfTheMakersAddress = BitcoinAddress.Create(to, network);

            TxOut hallOfTheMakersTxOut = new TxOut()
            {
                Value = amount,
                ScriptPubKey = hallOfTheMakersAddress.ScriptPubKey
            };

            TxOut changeTxOut = new TxOut()
            {
                Value = changeAmount,
                ScriptPubKey = privateKey.ScriptPubKey
            };
            var transaction = new Transaction();
            transaction.Inputs.Add(new TxIn()
            {
                PrevOut = outPointToSpend
            });
            transaction.Outputs.Add(hallOfTheMakersTxOut);
            transaction.Outputs.Add(changeTxOut);

            var bytes = Encoding.UTF8.GetBytes(message);
            transaction.Outputs.Add(new TxOut()
            {
                Value = Money.Zero,
                ScriptPubKey = TxNullDataTemplate.Instance.GenerateScriptPubKey(bytes)
            });
            transaction.Inputs[0].ScriptSig = secret.ScriptPubKey;
            transaction.Sign(privateKey, trans.ToArray());


            BroadcastResponse broadcastResponse = await client.Broadcast(transaction);
            
            return broadcastResponse;
        }
        public async Task<IEnumerable<Money>> GetBalance()
        {
            var entities = client.GetBalance(privateKey.ScriptPubKey).Result;
            var trans = entities.Operations.First().ReceivedCoins;           
            return entities.Operations.Select(px => px.Amount);
        
        }
        public async Task<BalanceModel> GetTransactions()
        {
            var response = await client.GetBalance(privateKey.ScriptPubKey);
            return response;
                
        }
        public async Task<string> GetBalanceSummaryString()
        {
            var x = await client.GetBalanceSummary(privateKey.ScriptPubKey);
            return x.Spendable.Amount.ToString();
        }
        /// <summary>
        /// Generate privateKey
        /// </summary>
        /// <returns></returns>
        public static BitcoinSecret CreateKey(Network network)
        {
            var key = new Key();
            return key.GetBitcoinSecret(network);
        }
    }
}
