using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_05_10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ToolStripMenuItem file = new ToolStripMenuItem("File");

            ToolStripMenuItem neww = new ToolStripMenuItem("New");
            neww.Click += Neww_Click;
            ToolStripMenuItem close = new ToolStripMenuItem("Close");
            close.Click += Close_Click;
            ToolStripMenuItem exit = new ToolStripMenuItem("Exit");
            exit.Click += Exit_Click;

            file.DropDownItems.Add(neww);
            file.DropDownItems.Add(close);
            file.DropDownItems.Add(exit);

            ToolStripMenuItem edit = new ToolStripMenuItem("Edit");

            ToolStripMenuItem copy = new ToolStripMenuItem("Copy");
            copy.Click += Copy_Click;
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste");
            paste.Click += Paste_Click;

            edit.DropDownItems.Add(copy);
            edit.DropDownItems.Add(paste);

            ToolStripMenuItem window = new ToolStripMenuItem("Window");


            ToolStripMenuItem style = new ToolStripMenuItem("Arrange");

            ToolStripMenuItem cascade = new ToolStripMenuItem("Cascade");
            cascade.Click += Cascade_Click;
            ToolStripMenuItem horizontal = new ToolStripMenuItem("Horizontally Tile");
            horizontal.Click += Horizontal_Click;
            ToolStripMenuItem vertical = new ToolStripMenuItem("Vertically Tile");
            vertical.Click += Vertical_Click;

            style.DropDownItems.Add(cascade);
            style.DropDownItems.Add(horizontal);
            style.DropDownItems.Add(vertical);

            menuStrip1.Items.Add(file);
            menuStrip1.Items.Add(edit);
            menuStrip1.Items.Add(window);
            menuStrip1.Items.Add(style);


        }

        private void Vertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void Horizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void Cascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            if (activeChild != null)
            {
                RichTextBox editBox = (RichTextBox)activeChild.ActiveControl;

                if (editBox != null)
                {
                    IDataObject data = Clipboard.GetDataObject();

                    if (data.GetDataPresent(DataFormats.Text))
                    {
                        editBox.SelectedText =
                          data.GetData(DataFormats.Text).ToString();
                    }
                }
            }

        }

        private void Copy_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            if (activeChild != null)
            {
                RichTextBox editBox = (RichTextBox)activeChild.ActiveControl;

                if (editBox != null)
                {
                    Clipboard.SetDataObject(editBox.SelectedText);
                }
            }

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            if (activeChild != null)
            {
                activeChild.Close(); 
            }
            UpdateWindowMenu();
        }

        private int windowCount = 1;

        private void Neww_Click(object sender, EventArgs e)
        {
            Form2 mdiChild = new Form2();
            mdiChild.MdiParent = this;
            mdiChild.Text = $"{windowCount}. {mdiChild.Text}"; 
            mdiChild.Show();

            UpdateWindowMenu();

            windowCount++;
        }

        private void UpdateWindowMenu()
        {

            ToolStripMenuItem windowMenu = null;
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item.Text == "Window" && item is ToolStripMenuItem)
                {
                    windowMenu = (ToolStripMenuItem)item;
                    break;
                }
            }

            if (windowMenu != null)
            {
                windowMenu.DropDownItems.Clear();


                foreach (Form mdiChild in MdiChildren)
                {
                    ToolStripMenuItem childMenuItem = new ToolStripMenuItem(mdiChild.Text);
                    childMenuItem.Tag = mdiChild;
                    childMenuItem.Click += ChildMenuItem_Click;


                    if (mdiChild == ActiveMdiChild)
                        childMenuItem.Checked = true;

                    windowMenu.DropDownItems.Add(childMenuItem);
                }
            }
        }

        private void ChildMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            Form mdiChild = (Form)menuItem.Tag;

            if (mdiChild != null)
            {
                mdiChild.Focus(); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            UpdateWindowMenu();
        }

        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            
            UpdateWindowMenu();
        }
    }
}
