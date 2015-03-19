using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class DataTable : IEnumerable, INotifyCollectionChanged, IEnumerable<DataRow>
    {
        private ObservableCollection<DataColumn> columns;
        private ObservableCollection<DataRow> rows;
        private IList internalView;
        private Type elementType;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableCollection<DataColumn> Columns
        {
            get
            {
                if (columns == null)
                {
                    columns = new ObservableCollection<DataColumn>();
                }

                return columns;
            }
            set
            {
                columns = value;
            }
        }


        public IList<DataRow> Rows
        {
            get
            {
                if (this.rows == null)
                {
                    this.rows = new ObservableCollection<DataRow>();
                    this.rows.CollectionChanged += OnRowsCollectionChanged;
                }

                return rows;
            }
        }

        public DataRow NewRow()
        {
            return new DataRow(this);
        }


        private void OnRowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.InternalView.Insert(e.NewStartingIndex, ((DataRow)e.NewItems[0]).RowObject);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.InternalView.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.InternalView.Remove(((DataRow)e.OldItems[0]).RowObject);
                    this.InternalView.Insert(e.NewStartingIndex, ((DataRow)e.NewItems[0]).RowObject);
                    break;
                case NotifyCollectionChangedAction.Reset:
                default:
                    {
                        this.InternalView.Clear();

                        var temp = this.rows.Select(r => r.RowObject).ToList();

                        foreach (var temp2 in temp)
                        {
                            this.InternalView.Add(temp2);
                        }

                        //this.Rows.Select(r => r.RowObject).ToList().ForEach(o => this.InternalView.Add(o));
                        break;
                    }
            }
        }

        private IList InternalView
        {
            get
            {
                if (this.internalView == null)
                {
                    this.CreateInternalView();
                }

                return this.internalView;
            }
        }

        private void CreateInternalView()
        {
            this.internalView = (IList)Activator.CreateInstance(typeof(ObservableCollection<>).MakeGenericType(this.ElementType));
            ((INotifyCollectionChanged)internalView).CollectionChanged += (s, e) => { this.OnCollectionChanged(e); };
        }

        internal Type ElementType
        {
            get
            {
                if (this.elementType == null)
                {
                    this.InitializeElementType();
                }

                return this.elementType;
            }
        }

        private void InitializeElementType()
        {
            this.Seal();
            this.elementType = typeof(System.Dynamic.ExpandoObject);
            //DynamicObjectBuilder.GetDynamicObjectBuilderType(this.Columns);
        }

        private void Seal()
        {
            this.columns = new ObservableCollection<DataColumn>(this.Columns);
        }

        public IEnumerator GetEnumerator()
        {
            return this.InternalView.GetEnumerator();
        }

        public IList ToList()
        {
            return this.InternalView;
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = this.CollectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        IEnumerator<DataRow> IEnumerable<DataRow>.GetEnumerator()
        {
            return this.rows.GetEnumerator();
        }
    }
}
