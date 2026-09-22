using LLMConnectorLibrary.Authentication;
using System;
using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class ConnectionTemplatesBrowser : Form
	{

		public IAuthenticationProfile AuthenticationProfile { get; private set; }

		public ConnectionTemplatesBrowser()
		{
			InitializeComponent();

			okButton.Enabled = browserListView.SelectedItems.Count > 0;

			okButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			cancelButton.Size = Const.Globals.ACTION_BUTTON_SIZE;

			browserListView.Items.Clear();

			ListViewItem item = new()
			{
				Text = "Пустой шаблон",
				ImageIndex = 0,
			};
			item.SubItems.Add("Пустой шаблон, подходит для начальной настройки подключения.");
			browserListView.Items.Add(item);

			item = new()
			{
				Text = "Шалон Ollama",
				ImageIndex = 1,
			};
			item.SubItems.Add("Шаблон с настройками по умолчанию для подключения к локальной Ollama.");
			browserListView.Items.Add(item);

			item = new()
			{
				Text = "Шаблон GigaChat",
				ImageIndex = 2,
			};
			item.SubItems.Add("Шаблон с настройками по умолчанию для подключения к GigaChat");
			browserListView.Items.Add(item);

			item = new()
			{
				Text = "Шаблон к стойке ПАО \"Ростелеком\"",
				ImageIndex = 3,
			};
			item.SubItems.Add("Шаблон с настройками для подключения к стойке РТК в ЗС ЕЗСПД.");
			browserListView.Items.Add(item);
			
			item = new()
			{
				Text = "Шаблон к стойке ПАО Сбербанк",
				ImageIndex = 4,
			};
			item.SubItems.Add("Шаблон с нстройками для подключения к стойке Сбера в ЗС ЕЗСПД.");
			browserListView.Items.Add(item);
			
			AuthenticationProfile = AuthenticationProfileFactory.Default;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		protected virtual void OnControlsResize()
		{
			cancelButton.Location = new System.Drawing.Point((int)(AutoScaleFactor.Width * (this.Width - cancelButton.Width - SystemInformation.BorderSize.Width * 10)), Height - cancelButton.Height - SystemInformation.BorderSize.Height * 10);
			okButton.Location = new System.Drawing.Point(Width - okButton.Width - SystemInformation.BorderSize.Width * 2, (int)(Height * AutoScaleFactor.Height)  - okButton.Height - SystemInformation.BorderSize.Height);
		}

		private void BrowserListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			okButton.Enabled = browserListView.SelectedItems.Count > 0;
			if (browserListView.SelectedItems.Count > 0)
			{
				AuthenticationProfile = browserListView.SelectedIndices[0] switch
				{
					0 => AuthenticationProfileFactory.Default,
					1 => AuthenticationProfileFactory.OllamaLocal,
					2 => AuthenticationProfileFactory.GigaChat,
					3 => AuthenticationProfileFactory.RTK,
					4 => AuthenticationProfileFactory.Sber,
					_ => AuthenticationProfileFactory.Default,
				};
			}
		}

		private void BrowserListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo hitInfo = browserListView.HitTest(e.Location);
			ListViewItem clickedItem = hitInfo.Item;

			if (clickedItem != null)
			{
				AuthenticationProfile = clickedItem.Index switch
				{
					0 => AuthenticationProfileFactory.Default,
					1 => AuthenticationProfileFactory.OllamaLocal,
					2 => AuthenticationProfileFactory.GigaChat,
					3 => AuthenticationProfileFactory.RTK,
					4 => AuthenticationProfileFactory.Sber,
					_ => AuthenticationProfileFactory.Default,
				};
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}
