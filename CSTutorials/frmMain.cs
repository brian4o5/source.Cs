using System;
using System.Diagnostics;
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
            Thread t = new Thread(() => Debug.WriteLine("Hello world from a thread."));
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
                Debug.WriteLine(tc.Msg);
            }
            Debug.WriteLine(tc.Msg);
        }

        delegate void myAction(int s, int t);
        private void button3_Click(object sender, EventArgs e)
        {
            Action<int, int> add = (s, t) => Debug.WriteLine((s + t).ToString());
            // Actions don't return a value, Func does
            Action<int, int> mul = (s, t) => Debug.WriteLine((s * t).ToString());
            Action<int, int> x;
            x = add;
            x += mul;
            x(1,2);

            myAction y = (s,t) => Debug.WriteLine((s - t).ToString()); ;
            y(1, 2);

        }

        [Conditional("DEBUG")]
        private void testing_attrib()
        {
            Debug.WriteLine("I only run in DEBUG build");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            testing_attrib();
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
            double dv = 10 / 4;
            int iv = 10 / 4;
            var rv = dv + iv;
            System.Diagnostics.Debug.WriteLine(rv + " " + rv.GetType());
           Thread.Sleep(1000);
            Msg = "end";
        }
    }
}
