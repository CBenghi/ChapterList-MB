using System;

namespace MessageControlDemo
{
	/// <summary>
	/// 
	/// </summary>
	public class ParseMessageEventArgs : System.EventArgs
	{
		private int m_LineNumber;
		private string m_MessageText;
		private string m_ParseSource;
		private ParseMessageType m_Type = ParseMessageType.None;

		public ParseMessageEventArgs() : base()
		{		}

		public ParseMessageEventArgs(ParseMessageType type, int LineNumber, string MessageText) : this()
		{		
			m_LineNumber = LineNumber;
			m_MessageText = MessageText;
			m_Type = type;
		}

		public ParseMessageEventArgs(ParseMessageType type, int LineNumber, string MessageText, string Source) : this(type,LineNumber,MessageText)
		{		
			m_ParseSource = Source;			
		}

		public string MessageText
		{
			get { return m_MessageText; }
			set { m_MessageText = value; }
		}

		public string Source
		{
			get { return m_ParseSource; }
			set { m_ParseSource = value; }
		}

		public int LineNumber
		{
			get { return m_LineNumber; }
			set { m_LineNumber = value; }
		}

		public ParseMessageType MessageType
		{
			get { return m_Type; }
			set { m_Type = value; }
		}
	}

	public enum ParseMessageType  {	None = -1, Info = 0, Warning = 1, Error = 2, Question = 3 };
}
