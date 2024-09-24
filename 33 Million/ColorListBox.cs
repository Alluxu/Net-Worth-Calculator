using System.Windows.Forms;
using System.Drawing;

namespace _33_Million
{
    public class ColorListBox : ListBox
    {
        public ColorListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.ItemHeight = 20;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
                return;

            // Ensure the item is of type _33_Million.Tieto
            if (Items[e.Index] is Tieto item)
            {
                float nettovarallisuus = item.Arvo - item.Laina;

                e.DrawBackground();
                Brush textBrush = Brushes.Black;

                if (nettovarallisuus > 0)
                {
                    e.Graphics.FillRectangle(Brushes.LightGreen, e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.LightCoral, e.Bounds);
                }

                // Ensure e.Font is not null before calling DrawString
                if (e.Font != null)
                {
                    e.Graphics.DrawString(item.ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
                }

                e.DrawFocusRectangle();
            }
            else
            {
                base.OnDrawItem(e); // Fallback to base behavior if item type is not correct
            }
        }
    }
}
