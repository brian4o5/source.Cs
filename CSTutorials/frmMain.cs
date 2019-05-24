using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSTutorials
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        // basic thread implementation
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => System.Diagnostics.Debug.WriteLine("Hello world from a thread."));
            t.Start();
            // wait for it to end            
            t.Join();
        }
        // more advanced thread with thread-safe property
        private void button2_Click(object sender, EventArgs e)
        {
            thread_class tc = new thread_class();
            tc.Msg = "start";
            Thread t = new Thread(new ThreadStart(tc.Execute));
            t.Start();
            // poll a thread every 100 ms 
            while(!t.Join(100))
            {
                System.Diagnostics.Debug.WriteLine(tc.Msg);
            }
            System.Diagnostics.Debug.WriteLine(tc.Msg);
        }
    }

    public class thread_class
    {
        // a thread safe get/set
        static readonly object _locker = new object();
        private string msg;
        public string Msg
        {
            get { lock (_locker) { return msg; } }
            set { lock (_locker) { msg = value; } }
        }

        public void Execute()
        {
            Thread.Sleep(1000);
            Msg = "end";
        }
    }
}
