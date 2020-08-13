using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace MathEx.ExternalBallistic
{
    public class AmmoInfoLibrary
    {
        public class AmmoInfoList : IEnumerable<AmmoInfoEx>
        {
            private List<AmmoInfoEx> mList = new List<AmmoInfoEx>();

            public IEnumerator<AmmoInfoEx> GetEnumerator()
            {
                return mList.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return mList.GetEnumerator();
            }

            public int Count
            {
                get
                {
                    return mList.Count;
                }

            }

            public AmmoInfoEx this[int index]
            {
                get
                {
                    return mList[index];
                }

            }

            internal void Add(AmmoInfoEx info)
            {
                mList.Add(info);
            }

            internal void Remove(int index)
            {
                mList.RemoveAt(index);
            }

            internal int Find(AmmoInfo info)
            {
                for (int i = 0; i < mList.Count; i++)
                    if (object.ReferenceEquals(info, mList[i]))
                        return i;
                return -1;
            }

            internal bool Remove(AmmoInfo info)
            {
                int index = Find(info);
                if (index >= 0)
                {
                    Remove(index);
                    return true;
                }
                else
                    return false;
            }
        }

        public class KeyCollection : IEnumerable<string>
        {
            private ICollection<string> mCollection;

            public IEnumerator<string> GetEnumerator()
            {
                return mCollection.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return mCollection.GetEnumerator();
            }

            public int Count
            {
                get
                {
                    return mCollection.Count;
                }

            }

            internal KeyCollection(ICollection<string> collection)
            {
                mCollection = collection;
            }
        }

        public class AmmoInfoDictionary
        {
            private SortedDictionary<string, AmmoInfoList> mDict = new SortedDictionary<string, AmmoInfoList>();

            public KeyCollection Keys
            {
                get
                {
                    return new KeyCollection(mDict.Keys);
                }
            }

            public AmmoInfoList this[string key]
            {
                get
                {
                    return mDict[key];
                }
            }

            internal void Add(string key, AmmoInfoEx info)
            {
                if (!mDict.ContainsKey(key))
                    mDict[key] = new AmmoInfoList();
                mDict[key].Add(info);
            }

            internal void Remove(string key, AmmoInfoEx info)
            {
                if (!mDict.ContainsKey(key))
                    return ;
                AmmoInfoList list = mDict[key];
                list.Remove(info);
                if (list.Count == 0)
                    mDict.Remove(key);
            }
        }


        private AmmoInfoList mAllAmmo = new AmmoInfoList();
        private AmmoInfoDictionary mCaliberDict = new AmmoInfoDictionary();
        private AmmoInfoDictionary mSourceDict = new AmmoInfoDictionary();

        public AmmoInfoLibrary()
        {
        }

        public void Add(AmmoInfoEx ammoInfo)
        {
            mAllAmmo.Add(ammoInfo);
            mCaliberDict.Add(ammoInfo.Caliber, ammoInfo);
            mSourceDict.Add(ammoInfo.Source, ammoInfo);
        }

        public AmmoInfoList CompleteList
        {
            get
            {
                return mAllAmmo;
            }
        }

        public AmmoInfoDictionary ByCaliber
        {
            get
            {
                return mCaliberDict;
            }
        }

        public AmmoInfoDictionary BySource
        {
            get
            {
                return mSourceDict;
            }
        }

        public void Remove(AmmoInfoEx ammoInfo)
        {
            mAllAmmo.Remove(ammoInfo);
            mCaliberDict.Remove(ammoInfo.Caliber, ammoInfo);
            mSourceDict.Remove(ammoInfo.Source, ammoInfo);
        }
    }
}