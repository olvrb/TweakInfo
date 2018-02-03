using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;

namespace repo_explorer {

    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        
        public string Get(string uri) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
        

        private void button1_Click(object sender, EventArgs e) {
            richTextBox1.Text = "Getting information, please wait...";
            string response = Get("https://cydia.saurik.com/api/macciti?query=" + Uri.EscapeUriString(textBox1.Text));
            Object packages = JObject.Parse(response);
            richTextBox1.Text = packages.results[0];
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
