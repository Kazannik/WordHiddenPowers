using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class CertificatesBrowser : Form
	{
		public X509Certificate2 Certificate => browserListView.SelectedItems.Count > 0 ? (X509Certificate2)browserListView.SelectedItems[0].Tag : null;

		public string SubjectName => Certificate != null ? GetName(Certificate.Subject) : string.Empty;

		public string FriendlyName => Certificate != null ? Certificate.FriendlyName : string.Empty;

		public CertificatesBrowser()
		{
			InitializeComponent();

			okButton.Enabled = browserListView.SelectedItems.Count > 0;

			okButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			cancelButton.Size = Const.Globals.ACTION_BUTTON_SIZE;

			browserListView.Items.Clear();

			foreach (X509Certificate2 certificate in Services.CertificatesStore.GetSertificates())
			{
				ListViewItem item = new()
				{
					Text = GetName(certificate.Subject)
				};
				item.SubItems.Add(GetName(certificate.Issuer));
				item.SubItems.Add(certificate.NotAfter.ToShortDateString());
				item.SubItems.Add(certificate.FriendlyName);
				item.ImageIndex = certificate.HasPrivateKey ? 1 : 0;

				item.Tag = certificate;

				browserListView.Items.Add(item);
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			//OnControlsResize();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			//OnControlsResize();
		}

		protected virtual void OnControlsResize()
		{
			cancelButton.Location = new System.Drawing.Point((int)(AutoScaleFactor.Width * (this.Width - cancelButton.Width - SystemInformation.BorderSize.Width * 10)), Height - cancelButton.Height - SystemInformation.BorderSize.Height * 10);
			okButton.Location = new System.Drawing.Point(Width - okButton.Width - SystemInformation.BorderSize.Width * 2, (int)(Height * AutoScaleFactor.Height)  - okButton.Height - SystemInformation.BorderSize.Height);
		}

		private static readonly System.Text.RegularExpressions.Regex regexStart = new("CN\\s*=\\s*");

		public static string GetName(string name)
		{
			if (regexStart.IsMatch(name))
			{
				int startIndex = regexStart.Match(name).Index + regexStart.Match(name).Length;
				int separatorIndex = name.IndexOf(",", startIndex);
				if (separatorIndex > startIndex)
					return name[startIndex..separatorIndex];
				else
					return name.Substring(startIndex);
			}
			else
				return name;
		}

		private void BrowserListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			okButton.Enabled = browserListView.SelectedItems.Count > 0;
		}
	}
}
