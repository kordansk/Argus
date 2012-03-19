﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;

namespace Argus
{
    public class AdvancedList<T> : BindingList<T>, IBindingListView
    {
        protected override bool IsSortedCore
        {
            get { return sorts != null; }
        }
        protected override void RemoveSortCore()
        {
            sorts = null;
        }
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }
        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return sorts == null ? ListSortDirection.Ascending :
                    sorts.PrimaryDirection;
            }
        }
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sorts == null ? null : sorts.PrimaryProperty; }
        }
        protected override void ApplySortCore(PropertyDescriptor prop,
        ListSortDirection direction)
        {
            ListSortDescription[] arr = {new ListSortDescription(prop, direction)};
            ApplySort(new ListSortDescriptionCollection(arr));
        }
        PropertyComparerCollection<T> sorts;
        public void ApplySort(ListSortDescriptionCollection
        sortCollection)
        {
            bool oldRaise = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            try
            {
                PropertyComparerCollection<T> tmp
                = new PropertyComparerCollection<T>(sortCollection);
                List<T> items = new List<T>(this);
                items.Sort(tmp);
                int index = 0;
                foreach (T item in items)
                {
                    SetItem(index++, item);
                }
                sorts = tmp;
            }
            finally
            {
                RaiseListChangedEvents = oldRaise;
                ResetBindings();
            }
        }
        string IBindingListView.Filter
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        void IBindingListView.RemoveFilter()
        {
            throw new NotImplementedException();
        }
        ListSortDescriptionCollection IBindingListView.SortDescriptions
        {
            get { return sorts.Sorts; }
        }
        bool IBindingListView.SupportsAdvancedSorting
        {
            get { return true; }
        }
        bool IBindingListView.SupportsFiltering
        {
            get { return false; }
        }
    }
    public class PropertyComparerCollection<T> : IComparer<T>
    {
        private readonly ListSortDescriptionCollection sorts;
        private readonly PropertyComparer<T>[] comparers;
        public ListSortDescriptionCollection Sorts
        {
            get { return sorts; }
        }
        public PropertyComparerCollection(ListSortDescriptionCollection
        sorts)
        {
            if (sorts == null) throw new ArgumentNullException("sorts");
            this.sorts = sorts;
            List<PropertyComparer<T>> list = new
            List<PropertyComparer<T>>();
            foreach (ListSortDescription item in sorts)
            {
                list.Add(new PropertyComparer<T>(item.PropertyDescriptor,
                item.SortDirection == ListSortDirection.Descending));
            }
            comparers = list.ToArray();
        }
        public PropertyDescriptor PrimaryProperty
        {
            get
            {
                return comparers.Length == 0 ? null :
                comparers[0].Property;
            }
        }
        public ListSortDirection PrimaryDirection
        {
            get
            {
                return comparers.Length == 0 ? ListSortDirection.Ascending
                : comparers[0].Descending ?
                ListSortDirection.Descending
                : ListSortDirection.Ascending;
            }
        }

        int IComparer<T>.Compare(T x, T y)
        {
            int result = 0;
            for (int i = 0; i < comparers.Length; i++)
            {
                result = comparers[i].Compare(x, y);
                if (result != 0) break;
            }
            return result;
        }
    }
    public class PropertyComparer<T> : IComparer<T>
    {
        private readonly bool descending;
        public bool Descending { get { return descending; } }
        private readonly PropertyDescriptor property;
        public PropertyDescriptor Property { get { return property; } }
        public PropertyComparer(PropertyDescriptor property, bool
        descending)
        {
            if (property == null) throw new
            ArgumentNullException("property");
            this.descending = descending;
            this.property = property;
        }
        public int Compare(T x, T y)
        {
            // todo; some null cases
            int value = Comparer.Default.Compare(property.GetValue(x),
            property.GetValue(y));
            return descending ? -value : value;
        }
    }
}