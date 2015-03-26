using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfLogger
{
    public class ObservableLimitedQueue<T> : Queue<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public int limit;

        public ObservableLimitedQueue(int limit)
        {
            this.limit = limit;
        }

        public ObservableLimitedQueue(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                base.Enqueue(item);
        }

        public ObservableLimitedQueue(List<T> list)
        {
            foreach (var item in list)
                base.Enqueue(item);
        }

        public ObservableLimitedQueue(IEnumerable<T> collection, int limit)
            : this(collection)
        {
            this.limit = limit;
        }

        public ObservableLimitedQueue(List<T> list, int limit)
            : this(list)
        {
            this.limit = limit;
        }
        
        public new void Clear()
        {
            base.Clear();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public new T Dequeue()
        {
            var item = base.Dequeue();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return item;
        }

        new public void Enqueue(T item)
        {
            if (base.Count >= limit)
            {
              //  this.Dequeue();
            }
            base.Enqueue(item);
            
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }


        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this.RaiseCollectionChanged(e);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(e);
        }


        protected virtual event PropertyChangedEventHandler PropertyChanged;


        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
                this.CollectionChanged(this, e);
        }

        private void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }


        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { this.PropertyChanged += value; }
            remove { this.PropertyChanged -= value; }
        }
    }
}
