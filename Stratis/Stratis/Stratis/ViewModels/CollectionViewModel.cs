using System.Collections.ObjectModel;

namespace Stratis.ViewModels
{
    public class CollectionViewModel<T> : BaseViewModel
    {
        public ObservableCollection<T> Items { get; set; }
        public CollectionViewModel()
        {
            Items = new ObservableCollection<T>();
        }
    }
}
