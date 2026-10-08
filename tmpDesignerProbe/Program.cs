using System;
using System.Windows.Forms;
using SIMS_WinFormsApp.Forms.Auth;

class P
{
    static void Dump(Control c, string indent = "")
    {
        Console.WriteLine($"{indent}{c.GetType().Name} Visible={c.Visible} Size={c.Size} Loc={c.Location} Parent={c.Parent?.GetType().Name}");
        foreach (Control child in c.Controls)
            Dump(child, indent + "  ");
    }

    static void Main()
    {
        Application.EnableVisualStyles();
        var f = new frmLogin();
        Dump(f);
        Console.WriteLine("Controls count = " + f.Controls.Count);
        Console.ReadLine();
    }
}
