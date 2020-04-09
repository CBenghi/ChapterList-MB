using System;
using System.Collections.Generic;

namespace SyncView
{
    public class LongRunningRecognizeResponse
    {
        public static RecogniseParser Parser { get; internal set; }
        public List<SpeechRecognitionResult> Results { get; internal set; }
    }

    public class SpeechRecognitionResult
    {
        public List<SpeechRecognitionAlternative> Alternatives { get; internal set; }
    }

    public class SpeechRecognitionAlternative
    {
        public List<WordInfo> Words { get; internal set; }
    }

    public class WordInfo
    {
        public string Word { get; internal set; }

        internal TimeSpan LenghtTimeSpan()
        {
            throw new NotImplementedException();
        }

        internal int StartTimeMilliseconds()
        {
            throw new NotImplementedException();
        }
    }
}