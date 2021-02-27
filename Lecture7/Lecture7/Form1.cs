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

namespace Lecture7
{
    public partial class Form1 : Form
    {
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
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode currentNode = e.Node;
                foreach (var folder in Directory.GetDirectories(currentNode.Tag.ToString()))
                {
                    TreeNode node = new TreeNode(Path.GetFileName(folder), 0, 1);
                    node.Tag = folder;
                    currentNode.Nodes.Add(node);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }
    }
}
