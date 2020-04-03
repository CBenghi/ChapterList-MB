using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MessageControlDemo
{
	/// <summary>
	/// 
	/// </summary>
	
	public class MessageListBox : ResizableListBox 
	{

		private const int	m_MainTextOffset = 50;
		private Font		m_HeadingFont;
		private System.Windows.Forms.ImageList IconList;
		private System.ComponentModel.IContainer components;

		public MessageListBox()
		{	
			InitializeComponent();		
			this.m_HeadingFont = new Font(this.Font, FontStyle.Bold);
			this.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.MeasureItemHandler);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MessageListBox));
			this.IconList = new System.Windows.Forms.ImageList(this.components);
			// 
			// IconList
			// 
			this.IconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.IconList.ImageSize = new System.Drawing.Size(16, 16);
			this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
			this.IconList.TransparentColor = System.Drawing.Color.Transparent;

		}

		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			
			m_HeadingFont.Dispose();

			base.Dispose( disposing );
		}

		#region overrides
		protected override void OnDrawItem( DrawItemEventArgs e)
		{

				e.DrawBackground();
				e.DrawFocusRectangle();			
				ParseMessageEventArgs item;
				Rectangle bounds = e.Bounds;
				Color TextColor = System.Drawing.SystemColors.ControlText;
			
				item =  (ParseMessageEventArgs) Items[e.Index];
			
				//draw selected item background
				if(e.State == DrawItemState.Selected)
				{
					using ( Brush b = new SolidBrush(System.Drawing.SystemColors.Highlight) )
					{
						// Fill background;
						e.Graphics.FillRectangle(b, e.Bounds);
					}	
					TextColor = System.Drawing.SystemColors.HighlightText;
				}
					
				//draw image
				IconList.Draw(e.Graphics, bounds.Left+1,bounds.Top+2,(int)item.MessageType); 
			
				using(SolidBrush TextBrush = new SolidBrush(TextColor))
				{
					//draw Headline
					e.Graphics.DrawString(String.Format("Line {0}",item.LineNumber) , m_HeadingFont, TextBrush, 
						bounds.Left+ IconList.ImageSize.Width + 5 , bounds.Top + IconList.ImageSize.Height - m_HeadingFont.Height);

				
					//draw main text
					int LinesFilled, CharsFitted=0, top;
					Size oneLine = new Size(this.Width - m_MainTextOffset,this.Font.Height);
					string TextToDraw = item.MessageText;
					string TextOfLine;

					top = bounds.Top + IconList.ImageSize.Height + 2;

					while(TextToDraw.Length > 0)
					{

						e.Graphics.MeasureString(item.MessageText,this.Font,oneLine,StringFormat.GenericDefault,out CharsFitted, out LinesFilled);
	

						if(TextToDraw.Length <= CharsFitted)
						{
							e.Graphics.DrawString(TextToDraw, this.Font, TextBrush , 
								bounds.Left + m_MainTextOffset, top);					
							break;
						}
						else				
						{
							TextOfLine = TextToDraw.Substring(0,CharsFitted);
							if(TextOfLine.LastIndexOf(" ") > 0)
								TextOfLine = TextOfLine.Substring(0,TextOfLine.LastIndexOf(" "));

							e.Graphics.DrawString(TextOfLine, this.Font, TextBrush, bounds.Left + m_MainTextOffset, top);
						}
						top += this.Font.Height;

						TextToDraw = TextToDraw.Substring(TextOfLine.Length + 1);
					}

				}

		}

		
		private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
		{

			int MainTextHeight;			
			ParseMessageEventArgs item;
			item =  (ParseMessageEventArgs) Items[e.Index];
			int LinesFilled, CharsFitted;
			
			//as we donot use the same algorithm to calculate the size of the text (for performance reasons)
			//we need to add some safety margin ( the 0.9 factor ) to ensure that always all text is displayed
			Size sz = new Size((int)((this.Width - m_MainTextOffset)*0.9),200);	

			e.Graphics.MeasureString(item.MessageText,this.Font,sz,StringFormat.GenericDefault,out CharsFitted, out LinesFilled);
			
			MainTextHeight = LinesFilled * this.Font.Height;

			e.ItemHeight = IconList.ImageSize.Height + MainTextHeight + 4;

		}
		#endregion
	}
}
