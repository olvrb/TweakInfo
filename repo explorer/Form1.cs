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
        class package
        {
            public string name;
            public string display;
            public string section;
            public string summary;
            public string version;
            public string url;

            public package(string name, string display, string section, string summary, string version) {
                this.name = (name != null ? name : "");
                this.display = display;
                this.section = section;
                this.summary = summary;
                this.version = version;
                this.url = "http://cydia.saurik.com/package/" + this.name;
            }
            public override string ToString() {
                return this.display;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            //richTextBox1.Text = "Getting information, please wait...";
            string response = Get("https://cydia.saurik.com/api/macciti?query=" + Uri.EscapeUriString(textBox1.Text));
            JObject packages = JObject.Parse(response);
            JToken data = packages["results"];
            listBox1.BeginUpdate(); // speed
            listBox1.Items.Clear();
            foreach (JToken obj in data) {
                package pack = new package(
                    obj["name"].Value<string>(),
                    obj["display"].Value<string>(),
                    obj["section"].Value<string>(),
                    obj["summary"].Value<string>(),
                    obj["version"].Value<string>()
                );
                listBox1.Items.Add(pack);
            }
            listBox1.EndUpdate(); // speed
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            package selected = listBox1.SelectedItem as package;

            Console.WriteLine(selected);
            
            /*
             * Access using selected.name, selected.display, selected.section, etc
             * Have fun oliver lol
             */

        }
    }
}
