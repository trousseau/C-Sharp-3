using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class PointList : IList<Point>
    {
        private List<Point> _Points = new List<Point>();
        public event EventHandler<PointListChangedEventArgs> Changed;


        //todo: modify other members to fire Changed event
        public Point this[int index]
        {
            get
            {
                return ((IList<Point>)_Points)[index];
            }

            set
            {
                ((IList<Point>)_Points)[index] = value;
                Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointsListChangedOperations.Update));
            }
        }

        public int Count
        {
            get
            {
                return ((IList<Point>)_Points).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<Point>)_Points).IsReadOnly;
            }
        }

        public void Add(Point item)
        {
            ((IList<Point>)_Points).Add(item);
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointsListChangedOperations.Add));
        }

        public void Clear()
        {
            ((IList<Point>)_Points).Clear();
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointsListChangedOperations.Clear));
        }

        public bool Contains(Point item)
        {
            return ((IList<Point>)_Points).Contains(item);
        }

        public void CopyTo(Point[] array, int arrayIndex)
        {
            ((IList<Point>)_Points).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return ((IList<Point>)_Points).GetEnumerator();
        }

        public int IndexOf(Point item)
        {
            return ((IList<Point>)_Points).IndexOf(item);
        }

        public void Insert(int index, Point item)
        {
            ((IList<Point>)_Points).Insert(index, item);
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointsListChangedOperations.Insert));
        }

        public bool Remove(Point item)
        {
            return ((IList<Point>)_Points).Remove(item);
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointsListChangedOperations.Remove));
        }

        public void RemoveAt(int index)
        {
            ((IList<Point>)_Points).RemoveAt(index);
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointsListChangedOperations.Remove));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Point>)_Points).GetEnumerator();
        }
    }
}
