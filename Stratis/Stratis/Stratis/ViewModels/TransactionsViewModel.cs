using NBitcoin;
using QBitNinja.Client.Models;
using Stratis.Client.Resources;
using Stratis.Domain;
using Stratis.Models;
using Stratis.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Stratis.ViewModels
{
    public class TransactionsViewModel : CollectionViewModel<object>
    {
        public IDataStore<Contact> DataStore;
        private bool canStart = true;

        public TransactionsViewModel(IDataStore<Contact> dataStore)
        {
            this.DataStore = dataStore;
            Transaction();
        }
        public override async Task OnApperaing()
        {
            await Transaction();
        }
        private async Task Transaction()
        {
            IsBusy = true;
            var result = await App.Account.GetTransactions();
            foreach (var item in result.Operations)
            {
                Items.Add(new
                {
                    Amount = item.Amount.ToString(),
                    Confirmations = item.Confirmations.ToString(),
                    FirstSeen = item.FirstSeen.ToString(),
                    TransactionId = item.TransactionId.ToString()
                });              
            }
            IsBusy = false;
        }
    }        
    
}
