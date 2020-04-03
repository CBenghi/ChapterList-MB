using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MessageControlDemo
{
	/// <summary>
	/// This is our base class. It tries to mimick the behaviour of a standard ListBox. It's
	/// not complete in that respect, but should do it for most cases.
	/// This class alone does NOT implement resizable listbox entries. It just provides the base
	/// for an inheriting class.
	/// </summary>
	public class ResizableListBox : System.Windows.Forms.Panel
	{
		//our data containers - exposed via properties
		private ArrayList	m_Items = new ArrayList();
		private ArrayList	m_SelectedItems = new ArrayList();
		private ArrayList	m_SelectedItemIndices = new ArrayList();

		//just for internal use
		private ArrayList	m_ItemHeights = new ArrayList();				
		private bool		m_CtrlPressed = false;
		private bool		m_AllowMultiSelect = true;

		/// <summary>
		/// The ctor.
		/// </summary>
		public ResizableListBox()
		{	
			
			// We are going to do all of the painting so better 
			// setup the control to use double buffering
			SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.ResizeRedraw|
				ControlStyles.Opaque|ControlStyles.UserPaint|ControlStyles.DoubleBuffer, true);
	

			//set some defaults			
			this.BackColor = System.Drawing.Color.White;
			this.AutoScroll = true;
			this.HScroll = false;
			
		}

		#region Events
		
		public event EventHandler SelectedIndexChanged;		
		public event MeasureItemEventHandler MeasureItem;
		public event DrawItemEventHandler DrawItem;
		
		#endregion


		#region Overrides

		/// <summary>
		/// This method is the core worker method. It calls the OnMeasureItem and OnDrawItem methods.
		/// The OnMeasureItem method is called *everytime* the OnDrawItem method is called. Not just once
		/// like in the original ListBox.
		/// </summary>
		/// <param name="pe"></param>
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			
			Graphics g = pe.Graphics;
			Rectangle bounds = new Rectangle();
			int posY = this.AutoScrollPosition.Y;

			//clear background
			using ( Brush b = new SolidBrush(this.BackColor) )
			{
				// Fill background;
				g.FillRectangle(b, this.ClientRectangle);
			}			
						
			m_ItemHeights.Clear();
			//draw our items
			for(int i=0; i<m_Items.Count; i++)
			{
				//measure
				MeasureItemEventArgs miea = new MeasureItemEventArgs(g,i);
				OnMeasureItem(miea);
				

				bounds.Location = new Point(0,posY);
				bounds.Size = new System.Drawing.Size(this.ClientRectangle.Right,(int)m_ItemHeights[i]);				

				//and draw
				DrawItemState state = (m_SelectedItemIndices.Contains(i)) ? DrawItemState.Selected : DrawItemState.Default;
				DrawItemEventArgs diea = new DrawItemEventArgs(g,this.Font,bounds,i,state,this.ForeColor,this.BackColor);
				
				OnDrawItem(diea);
				posY += (int)m_ItemHeights[i];
			}

			this.AutoScrollMinSize = new Size(this.Width - 50,posY);
		}

		/// <summary>
		/// Here we implement the selection handling of the listbox
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			//call base implementation
			base.OnMouseDown(e);
			
			//make sure we receive key events
			this.Focus();

			if(e.Button != MouseButtons.Left) 
				return;

			//determine which item was clicked
			int index = ItemHitTest(e.X,e.Y);

			if(index < 0)
				return;
						
			if((m_CtrlPressed) && m_AllowMultiSelect)
			{
				if(m_SelectedItemIndices.Contains(index))
				{
					 RemoveSelectedItem(index);
				}
				else
				{
					AddSelectedItem(index);
				}
			}
			else
			{
				if((m_SelectedItemIndices.Contains(index)) && (m_SelectedItemIndices.Count == 1))
					return;

				m_SelectedItemIndices.Clear();
				m_SelectedItems.Clear();

				AddSelectedItem(index);
			}

			this.Invalidate();
		
		}

		/// <summary>
		/// Handle the Ctrl-Key for multiple selections
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			m_CtrlPressed = e.Control;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			m_CtrlPressed = e.Control;
		}
		#endregion


		#region Private Methods

		/// <summary>
		/// Tests which item the user has clicked.
		/// </summary>
		/// <param name="X"></param>
		/// <param name="Y"></param>
		/// <returns>The index of the clicked item</returns>
		private int ItemHitTest(int X, int Y)
		{
			int posY = this.AutoScrollPosition.Y;
			for(int i=0; i< m_ItemHeights.Count; i++)
			{
				posY += (int)m_ItemHeights[i];

				if(Y < posY)
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Adds an item to the selected item ArrayList and fires the appropriate event.
		/// </summary>
		/// <param name="index"></param>
		private void AddSelectedItem(int index)
		{
			if(index == -1)
			{
				m_SelectedItemIndices.Clear();
				m_SelectedItems.Clear();
			}
			else
			{
				m_SelectedItemIndices.Add(index);
				m_SelectedItems.Add(m_Items[index]);
			}

			OnSelectedIndexChanged(new EventArgs());
		}

		
		/// <summary>
		/// Removes an item from the selected item ArrayList
		/// </summary>
		/// <param name="index"></param>
		private void RemoveSelectedItem(int index)
		{
			m_SelectedItemIndices.Remove(index);
			m_SelectedItems.Remove(m_Items[index]);
			OnSelectedIndexChanged(new EventArgs());
		}

		#endregion

		#region Properties		

		public ArrayList Items
		{
			get { return m_Items; }
		}

		public object SelectedItem
		{
			get 
			{
				if(m_SelectedItems.Count > 0)
					return m_SelectedItems[0];
				else
					return null;
			}
			set 
			{
				int pos = m_Items.IndexOf(value);
				if(pos >= 0)
				{
					//clear list
					m_SelectedItemIndices.Clear();
					m_SelectedItems.Clear();
					
					//add item
					AddSelectedItem(pos);					
				}
			}
		}

		public ArrayList SelectedItems
		{
			get { return m_Items; }
		}

		public int SelectedIndex
		{
			get 
			{
				if(m_SelectedItemIndices.Count > 0)
					return (int)m_SelectedItemIndices[0];
				else
					return -1;
			}
			set
			{
				if((value < m_Items.Count) && (value >= -1))
					AddSelectedItem(value);
			}
		}

		public ArrayList SelectedItemIndices
		{
			get { return m_SelectedItemIndices; }
		}

		#endregion

		#region Virtual Methods

		/// <summary>
		/// This is just a standard implementation without any resize-logic.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnDrawItem( DrawItemEventArgs e)
		{
			e.DrawBackground();
			e.DrawFocusRectangle();			
		
			Rectangle bounds = e.Bounds;
			Color TextColor = System.Drawing.SystemColors.ControlText;
					
			
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
			
			using (SolidBrush TextBrush = new SolidBrush(TextColor))
			{

				//draw item the standard way
				e.Graphics.DrawString(m_Items[e.Index].ToString(), this.Font, TextBrush, bounds.Left , bounds.Top);
			}

			//fire event
			if(DrawItem != null)
				DrawItem(this, e);
			
		}


		/// <summary>
		/// Just a standard implementation. Subscribe to the event in a derived class to implemet your logic
		/// to resize the items.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			//preset Height
			e.ItemHeight = this.Font.Height;
			
			if(MeasureItem != null)
				MeasureItem(this, e);
			
			m_ItemHeights.Add(e.ItemHeight);
		}
		
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if(SelectedIndexChanged != null)
				SelectedIndexChanged(this, e);
		}

		#endregion
	}
}
