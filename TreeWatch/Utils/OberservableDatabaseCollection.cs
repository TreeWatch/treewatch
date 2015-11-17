using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace TreeWatch
{
	public class OberservableDatabaseCollection<T> : ObservableCollection<T> where T : BaseModel
	{
		public OberservableDatabaseCollection (List<T> list)
			: base (list)
		{
		}

		public OberservableDatabaseCollection (IEnumerable<T> collection)
			: base (collection)
		{
		}

		protected override void OnCollectionChanged (NotifyCollectionChangedEventArgs e)
		{
			base.OnCollectionChanged (e);
			if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Count != 0) {
				new DBQuery<T> (App.Database).InsertAll ((IEnumerable<T>)e.NewItems);
			}
			if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Count != 0) {
				foreach (var item in e.OldItems) {
					new DBQuery<T> (App.Database).Delete ((T)item);	
				}
			}
			if (e.Action == NotifyCollectionChangedAction.Replace && e.OldItems.Count != 0) {
				new DBQuery<T> (App.Database).UpdateAllWithChildren ((IEnumerable<T>)e.OldItems);
			}
		}

	}
}

