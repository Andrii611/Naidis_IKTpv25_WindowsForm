using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naidis_IKTpv25_Windows_Forms
{
    
    public partial class Avavorm : Form
    {
        TreeView tree;
        Button nupp;
        Label silt;
        PictureBox pilt;
        CheckBox mruut1, mruut2;
        RadioButton rnupp1, rnupp2;
        TextBox tbox;
        TabControl tabs;
        TabPage tab1, tab2, tab3;
        public Avavorm()
        {
            Height = 600;
            Width = 1000;
            Text = "Naidis IKTpv25 Windows Forms";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Elemendid");
            tn.Nodes.Add(new TreeNode("Nupp"));
            tn.Nodes.Add(new TreeNode("Silt"));
            tn.Nodes.Add(new TreeNode("Pilt"));
            tn.Nodes.Add(new TreeNode("Märkeruut"));
            tn.Nodes.Add(new TreeNode("Radionupp"));
            tn.Nodes.Add(new TreeNode("Tekstiväli"));
            tn.Nodes.Add(new TreeNode("Vahekaardid"));
            tree.Nodes.Add(tn);

            //nupp,silt ja pilt
            nupp = new Button();
            nupp.Text = "Vajuta Siia"; 
            nupp.Location = new Point(300, 100);
            nupp.Height = 50;
            nupp.Width = 100;
            nupp.Click += (sender, e) => { MessageBox.Show("Nupp vajutati!"); };

            silt = new Label();
            silt.Text = "See on silt";
            silt.Location = new Point(300,200);
            silt.Size = new Size(200, 30);
            silt.Font = new Font("Arial", 16, FontStyle.Bold);
            silt.AutoSize = true;
            silt.MouseLeave += Silt_MouseLeave;
            silt.MouseHover += Silt_MouseHover;

            pilt = new PictureBox();
            pilt.Image = Image.FromFile(@"..\..\Pildid\images.jpg");
            pilt.Location = new Point(300, 300);
            pilt.Size = new Size(200, 200);
            pilt.SizeMode = PictureBoxSizeMode.StretchImage;
            pilt.MouseDoubleClick += Pilt_MouseDoubleClick;


            Controls.Add(tree);
        }

        private void Pilt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Size väike = new Size(200, 200);
            Size suur = new Size(500, 150);
            if (pilt.Size == suur)
                pilt.Size = väike;

            else
                pilt.Size = suur;
            
        }

        private void Silt_MouseHover(object sender, EventArgs e)
        {
            silt.BackColor = Color.LightGray;
            silt.BorderStyle = BorderStyle.Fixed3D;
        }

        private void Silt_MouseLeave(object sender, EventArgs e)
        {
            silt.BackColor = Color.Black;
            
        }

        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Nupp")
            {
                Controls.Add(nupp);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Silt")
            {
                Controls.Add(silt);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Pilt")
            {
                Controls.Add(pilt);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Märkeruut")
            {
                mruut1 = new CheckBox();
                mruut1.Text = "Märkeruut 1";
                mruut1.Location = new Point(200, 300);
                mruut1.CheckedChanged += Mruut1_CheckedChanged;
                mruut2 = new CheckBox();
                mruut2.Text = "Märkeruut 2";
                mruut2.Location = new Point(300, 350);
                mruut2.CheckedChanged += Mruut2_CheckedChanged;
                Controls.Add(mruut1);
                Controls.Add(mruut2);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Radionupp")
            {
                rnupp1 = new RadioButton();
                rnupp1.Text = "Bisque";
                rnupp1.Location = new Point(200, 400);
                rnupp1.CheckedChanged += Rnupp1_CheckedChanged;
                rnupp2 = new RadioButton();
                rnupp2.Text = "Hall";
                rnupp2.Location = new Point(200, 450);
                rnupp2.CheckedChanged += Rnupp1_CheckedChanged;
                Controls.Add(rnupp1);
                Controls.Add(rnupp2);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Tekstiväli")
            {
                tbox = new TextBox();
                tbox.Location = new Point(200, 500);
                tbox.Width = 200;
                tbox.TextChanged += (s, arg) =>
                {
                    Controls.Add(silt);
                    if (tbox.Text.Length > 0)
                    {
                        silt.Text = tbox.Text;
                    }
                    if (tbox.Text.Length == 0)
                    {
                        silt.Text = " See on silt";
                    }
                };
                Controls.Add(tbox);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Vahekaardid")
            {
                tabs = new TabControl();
                tabs.Location = new Point(500, 100);
                tabs.Size = new Size(1000, 500);
                tab1 = new TabPage("Techno+TLN");
                WebBrowser brauser = new WebBrowser();
                brauser.Dock = DockStyle.Fill;
                brauser.ScriptErrorsSuppressed = true;
                brauser.Url = new Uri("https://techno.ee/");
                tab1.Controls.Add(brauser);

                tab2 = new TabPage("ChatikGPs");
                WebBrowser browser = new WebBrowser();
                browser.Dock = DockStyle.Fill;
                browser.ScriptErrorsSuppressed = true;
                browser.Url = new Uri("https://chatgpt.com/");
                tab2.Controls.Add(browser);
                tab3 = new TabPage("+");

                // Otsingukast
                TextBox otsing = new TextBox();
                otsing.Location = new Point(20, 20);
                otsing.Size = new Size(300, 25);

                // Otsingu nupp
                Button otsi = new Button();
                otsi.Text = "Otsi";
                otsi.Location = new Point(330, 20);
                otsi.Size = new Size(70, 25);

                // Veebibrauser
                WebBrowser uusBrauser = new WebBrowser();
                uusBrauser.Location = new Point(20, 60);
                uusBrauser.Size = new Size(360, 200);
                uusBrauser.ScriptErrorsSuppressed = true;

                tab3.Controls.Add(otsing);
                tab3.Controls.Add(otsi);
                tab3.Controls.Add(uusBrauser);

                // Otsi Google'ist
                otsi.Click += (s, arg) =>
                {
                    string tekst = otsing.Text.Trim();

                    if (string.IsNullOrWhiteSpace(tekst))
                        return;

                    string url;

                    // Kui sisestatakse veebiaadress
                    if (tekst.StartsWith("http://") || tekst.StartsWith("https://"))
                    {
                        url = tekst;
                    }
                    // Kui sisestatakse näiteks youtube.com
                    else if (tekst.Contains("."))
                    {
                        url = "https://" + tekst;
                    }
                    // Muidu otsib Google'ist
                    else
                    {
                        url = "https://www.google.com/search?q=" +
                              Uri.EscapeDataString(tekst);
                    }

                    uusBrauser.Navigate(url);
                };
            }
        }
        private void Rnupp1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton nupp = sender as RadioButton;
            if (nupp == rnupp1 && nupp.Checked)
            {
                BackColor = Color.Bisque;
            }
            else if (nupp == rnupp2 && nupp.Checked)
            {
                BackColor = Color.Gray;
            }
        }

        private void Mruut2_CheckedChanged(object sender, EventArgs e)
        {
            Controls.Add(pilt);
            if (mruut2.Checked)
            {
                pilt.Visible = true;
                mruut1.Text = "Peida pilt";

            }
            else
            {
                pilt.Visible = false;
                mruut1.Text = "Näita pilt";
            }
        }

        private void Mruut1_CheckedChanged(object sender, EventArgs e)
        {
            if (mruut1.Checked)
            {
                Size = new Size(500, 300);
                mruut1.Text = "Tee suuremaks";

            }
            else
            {
                Size = new Size(1000, 600);
                mruut1.Text = "Tee väiksemaks";
            }
        }
    }
}
