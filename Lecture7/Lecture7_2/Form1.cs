using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lecture7_2
{
    public partial class Form1 : Form
    {

        ImageList imageListSmall = new ImageList();
        ImageList imageListLarge = new ImageList();

        private TreeNode currentNode;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode(drive.Name, 0, 1);
                node.Tag = drive.Name;
                treeView1.Nodes.Add(node);
            }


            foreach (RadioButton rb in groupBox1.Controls)
            {
                rb.Click += Rb_Click;
            }
            listView1.SmallImageList = imageListSmall;
            listView1.LargeImageList = imageListLarge;
            imageListLarge.ImageSize = new Size(128, 86);
            imageListSmall.ImageSize = new Size(32, 32);

        }

        private void Rb_Click(object sender, EventArgs e)
        {
            string value = (sender as RadioButton).Text;
            listView1.View = (View)Enum.Parse(typeof(View), value);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                e.Node.Nodes.Clear();
                currentNode = e.Node;
                foreach (var folder in Directory.GetDirectories(currentNode.Tag.ToString()))
                {
                    TreeNode node = new TreeNode(Path.GetFileName(folder), 0, 1);
                    node.Tag = folder;
                    currentNode.Nodes.Add(node);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            listView1.Clear();
            listView1.Columns.Add("File", 200);
            listView1.Columns.Add("Remarks", 200);
            foreach (var file in Directory.GetFiles(currentNode.Tag.ToString()))
            {
                try
                {
                    using (Bitmap btm = new Bitmap(file))
                    {
                        imageListSmall.Images.Add(btm);
                        imageListLarge.Images.Add(btm);
                    }
                }
                catch (ArgumentException)
                {
                    imageListSmall.Images.Add(imageList1.Images[2]);
                    imageListLarge.Images.Add(imageList1.Images[2]);
                }
               
                ListViewItem item = new ListViewItem(Path.GetFileName(file), imageListSmall.Images.Count - 1);
                listView1.Items.Add(item);
            }
        }
    }
}
