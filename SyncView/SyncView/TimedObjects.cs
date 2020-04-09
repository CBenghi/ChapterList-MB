using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB.SyncView
{
    class TimedObjects<T> : IEnumerable<T> where T : ITimedObject
    {
        public T this[int index]    // Indexer declaration  
        {
            get
            {
                if (index >= images.Count)
                    return default;
                return images[index];
            }
            // get and set accessors  
        }
        int lastTimeReceived = -1;
        internal int LastObjectPolled { get; private set; } = -1;
        internal int NextObjectTime { get; private set; } = -1;

        internal List<T> images;

        public TimedObjects(List<T> list)
        {
            images = list;
        }

        public TimedObjects()
        {
            images = new List<T>();
        }

        internal int GetIndex(int seekTime)
        {
            if (images == null || !images.Any())
                return -1;

            // if we moved back start afresh                
            if (seekTime < lastTimeReceived || lastTimeReceived == -1)
                return FindAtTime(seekTime);
            if (seekTime < NextObjectTime)
            {
                lastTimeReceived = seekTime;
                return -1; // nothing to do
            }
            return FindAtTime(seekTime, LastObjectPolled + 1);
        }

        private int FindAtTime(int seekTime, int startAt = 0)
        {
            lastTimeReceived = seekTime;
            int retIndex = 0; // in any case return first image, if too early
            for (int i = startAt; i < images.Count; i++)
            {
                if (seekTime >= images[i].TimeStampMilliseconds)
                {
                    retIndex = i;
                }
                else
                {
                    break;
                }
            }
            LastObjectPolled = retIndex;
            if (retIndex < images.Count - 1)
                NextObjectTime = images[retIndex + 1].TimeStampMilliseconds;
            else
                NextObjectTime = int.MaxValue;
            return retIndex;
        }

        internal object[] ToArray()
        {
            return images.Select(x=>x as object).ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)images).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)images).GetEnumerator();
        }

        internal void Reset()
        {
            lastTimeReceived = -1;
            LastObjectPolled = -1;
            NextObjectTime = -1;
        }
    }
}
