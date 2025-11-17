//--------------------------------------------------------------------- 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY 
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
//PARTICULAR PURPOSE. 
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace DataGridCustomColumns
{
    // This is our editable TextBox column.
    public class DataGridCustomNumericTextBoxColumn : DataGridCustomColumnBase
    {
        // Let's add this so user can access 
        public virtual NumericTextBox NumericTextBox
        {
            get { return this.HostedControl as NumericTextBox; }
        }

        protected override string GetBoundPropertyName()
        {
            return "Text";                                                          // Need to bount to "Text" property on TextBox
        }

        protected override Control CreateHostedControl()
        {
            NumericTextBox box = new NumericTextBox();                                            // Our hosted control is a TextBox           
            box.BorderStyle = BorderStyle.None;                                     // It has no border
            box.Multiline = true;                                                   // And it's multiline
            box.TextAlign = this.Alignment;                                         // Set up aligment.
            return box;
        }
    }
}
