
using PokeSync.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using RestSharp.Extensions.MonoHttp;
using System.Threading;

namespace PokeSync {
    public partial class Form1 : MetroFramework.Forms.MetroForm {
        //Callback
        private const String client_id = "460asm0wd4vwg6p";
        private const string rootDomain = "https://panjaco.com";
        private const string LoopbackHost = rootDomain + "/pokesync/";
        private readonly Uri RedirectUri = new Uri(LoopbackHost + "authorize"); // URL to receive OAuth 2 redirect from Dropbox server.
        private readonly Uri JSRedirectUri = new Uri(LoopbackHost + "token"); // URL to receive access token from JS.
        ChromiumWebBrowser browser;
        MetroFramework.Forms.MetroForm authWindow;
        private static readonly HttpClient client = new HttpClient();

        //API
        //String url = "https://www.dropbox.com/1/oauth2/authorize?client_id=460asm0wd4vwg6p&response_type=code&redirect_uri=http://localhost&state=abc";
        String url_code = "https://www.dropbox.com/1/oauth2/authorize?response_type=code&client_id=" + client_id + "&state=";
        String url_token = "https://api.dropboxapi.com/oauth2/token";
        String proxy_token = "https://panjaco.com/pokesync/proxy.php";
        private String state = "";

        public Form1() {
            InitializeComponent();
        }


        //Create a random string to be used as a state for OAuth2
        private static Random random = new Random();
        public static string RandomString(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Initial Link w/ Dropbox account
        public void auth() {
            state = RandomString(32);
            String url = url_code + state;
            url += "&redirect_uri=" + LoopbackHost;
            initializeBrowser(url);
        }

        public void initializeBrowser(String url) {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser(url);
            this.Controls.Add(browser);

            authWindow = new MetroFramework.Forms.MetroForm();
            authWindow.StartPosition = FormStartPosition.CenterScreen;
            authWindow.Height = 800;
            authWindow.Width = 500;
            authWindow.Show();
            browser.Width = 500;
            browser.Height = 800;
            authWindow.Controls.Add(browser);
            //browser.Dock = DockStyle.Fill;

            //Event Handler
            browser.AddressChanged += onBrowserAddressChanged;
        }

        private void onBrowserAddressChanged(object sender, AddressChangedEventArgs args) {
            String url = args.Address;
            Uri site = new Uri(url);
            if (site.GetLeftPart(UriPartial.Authority) == rootDomain) {
                Console.WriteLine("CALLBACK RECEIVED!");
                String pState = HttpUtility.ParseQueryString(site.Query).Get("state");
                String pCode = HttpUtility.ParseQueryString(site.Query).Get("code");
                if(pState != this.state){
                    Console.WriteLine("States do not match! SECURITY BREACH!?!?!?");
                    return;
                }
                browser.AddressChanged -= onBrowserAddressChanged;

                PokeSync.Properties.Settings.Default.AuthorizationCode = pCode;
                PokeSync.Properties.Settings.Default.Save();
                PokeSync.Properties.Settings.Default.Reload();
                
                BeginInvoke(new Action(() => {
                    this.Controls.Remove(browser);
                    authWindow.Close();
                    refreshLinkStatus();
                    new Task(getAccessToken).Start();
                }));
            }
        }

        private void LinkAccountButton_Click(object sender, EventArgs e) {
            auth();
        }
        private void refreshLinkStatus() {
            if (String.IsNullOrEmpty(PokeSync.Properties.Settings.Default.AuthorizationCode)) {
                LinkStatusLabel.Text = "Not Linked!";
                LinkStatusLabel.ForeColor = Color.Red;
            } else {
                LinkStatusLabel.Text = "Linked!";
                LinkStatusLabel.ForeColor = Color.Green;
                LinkAccountButton.Enabled = false;
            }
        }
        //Convert Authorization Code to Access Token
        private async void getAccessToken() {
            Console.WriteLine(Properties.Settings.Default.AuthorizationCode);
            var parameters = new Dictionary<String, String> {
                //{"client_secret", "<CLIENT SECRET>"},
                {"code", Properties.Settings.Default.AuthorizationCode},
                //{"grant_type", "authorization_code"},
                {"client_id", client_id},
                {"redirect_uri", LoopbackHost},
            };
            var content = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(proxy_token, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
    }
}
