using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EmailAppTester
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for our tester.
        /// Initially, this program just attempts to send one email to a single user
        /// Second, it will read a list of email addresses from a file, and send a message to each recipient named in the file
        /// Finally, we'll add more robust error-checking and configuration
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}