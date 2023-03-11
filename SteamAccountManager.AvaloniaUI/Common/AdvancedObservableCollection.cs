using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace SteamAccountManager.AvaloniaUI.Common
{
    public class AdvancedObservableCollection<T> : ObservableCollection<T>
    {
        public AdvancedObservableCollection() : base()
        {

        }

        public AdvancedObservableCollection(IEnumerable<T> items) : base(items)
        {

        }

        public void SetItems(IList<T> items)
        {
            base.ClearItems();
            foreach (var item in items)
            {
                Items.Add(item);
            }
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
