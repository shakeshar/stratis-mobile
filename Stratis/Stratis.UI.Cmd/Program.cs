using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using Stratis.Domain;
using System;
using System.Linq;
using System.Text;

namespace Stratis.UI.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {      
            Strat strat = new Strat("cPrJiV2qrzcg4FH4xv54F6Ko5Eq8QkbygLjUhbfHVEg9Zp2QpyzR", Network.TestNet);          
            Console.WriteLine("Press a key to exit");
            Console.Read();
        }
    }
}
