using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gehtsoft.BallisticCalculator.Connectivity
{
    public class TraceInfoCollection : IEnumerable<TraceInfo>
    {
        private List<TraceInfo> mList = new List<TraceInfo>();

        public int Count
        {
            get
            {
                return mList.Count;
            }
        }

        public TraceInfo this[int index]
        {
            get
            {
                return mList[index];
            }
            set
            {
                mList[index] = null;
            }
        }

        public TraceInfoCollection()
        {
        }

        public TraceInfoCollection(IEnumerable<TraceInfo> collection)
        {
            if (collection != null)
                foreach (TraceInfo info in collection)
                    mList.Add(info);
        }

        public void Add(TraceInfo info)
        {
            mList.Add(info);
        }

        public void RemoveAt(int index)
        {
            mList.RemoveAt(index);
        }

        public void Clear()
        {
            mList.Clear();
        }

        public int Find(string name)
        {
            for (int i = 0; i < mList.Count; i++)
                if (mList[i].TraceName == name)
                    return i;
            return -1;
        }

        public IEnumerator<TraceInfo> GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mList.GetEnumerator();
        }
    }
}
