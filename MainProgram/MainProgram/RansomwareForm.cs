using System;
using System.Windows.Forms;

public class RansomwareForm : Form
{
    public RansomwareForm()
    {
        this.Text = "Ransomware";
        this.Size = new System.Drawing.Size(400, 300);
        this.Controls.Add(new Label { Text = "Your files have been encrypted!", AutoSize = true, Location = new System.Drawing.Point(100, 100) });
        this.Controls.Add(new Button { Text = "Decrypt", AutoSize = true, Location = new System.Drawing.Point(100, 150) });
    }
}