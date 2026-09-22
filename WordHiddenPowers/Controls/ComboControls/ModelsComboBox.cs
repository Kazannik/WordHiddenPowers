using ControlLibrary.Controls.ComboControls;
using LLMConnectorLibrary.Authentication;
using LLMConnectorLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using static WordHiddenPowers.Controls.ComboControls.ModelsComboBox;

namespace WordHiddenPowers.Controls.ComboControls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	[ComVisible(false)]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public class ModelsComboBox : ComboControl<ComboBoxModelItem>
	{
		#region Initialize

		public ModelsComboBox() : base()
		{
			PrefixUnique = false;
		}

		[DebuggerNonUserCode()]
		public ModelsComboBox(IContainer container) : base(container: container)
		{
			PrefixUnique = false;
		}

		#endregion
				
		public IEnumerable<IModel> Models => from ComboBoxModelItem item
											 in Items
											 select item.Model;

		public IEnumerable<IAuthenticationProfile> Profiles => Models
			.GroupBy(x => x.Profile.GetHashCode() ^ x.Profile.Caption.GetHashCode())
			.Select(x => x.First().Profile);
						
		public void InitializeSource(ModelsCollection models)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => InitializeSource(models)));
			}
			else
			{
				Items.Clear();
				if (models != null)
				{
					foreach (IModel model in models.OrderBy(x => x.Id).OrderBy(x => x.Profile.ApiType))
					{
						Add(new ComboBoxModelItem(parent: this, model: model));
					}
				}
			}
		}

		public void AddRange(ModelsCollection models)
		{
			foreach (IModel model in models
				.OrderBy(x => x.Id)
				.OrderBy(x => x.Profile.ApiType))
			{
				Add(new ComboBoxModelItem(parent: this, model: model));
			}
		}

		public void RemoveRange(IEnumerable<IAuthenticationProfile> profiles)
		{
			foreach (IAuthenticationProfile item in profiles)
			{
				RemoveRange(item);
			}
		}

		public void RemoveRange(IAuthenticationProfile profile)
		{
			List<ComboBoxModelItem> removedItems = [.. from ComboBoxModelItem item
				in Items
				where item.Model.Profile.Equals(profile)
				select item];

			foreach (ComboBoxModelItem item in removedItems)
			{
				Items.Remove(item);
			}
		}

		public void RemoveRange(string guid)
		{
			List<ComboBoxModelItem> removedItems = [.. from ComboBoxModelItem item
				in Items
				where item.Guid.Equals(guid)
				select item];

			foreach (ComboBoxModelItem item in removedItems)
			{
				Items.Remove(item);
			}
		}

		public void RefreshRange(string guid, IAuthenticationProfile profile)
		{
			List<ComboBoxModelItem> refreshItems = [.. from ComboBoxModelItem item
				in Items
				where item.Guid.Equals(guid)
				select item];

			foreach (ComboBoxModelItem item in refreshItems)
			{
				item.Model.Profile.Caption = profile.Caption;
			}
			Invalidate();
		}

		protected override Size OnMeasurePrefixBound(Graphics graphics, Font font)
		{			
			int length = Models.Count() > 0 ? Models.Max(model => model.Profile.Caption.Length) : 8;
			return graphics.MeasureString(new string('w', length), font).ToSize();
		}

		public void SelectModel(IModel model)
		{
			SelectedItem = Items
				.Cast<ComboBoxModelItem>()
				.FirstOrDefault(item => item.Model.Equals(model));
		}

		public void SelectModel(string modelName, string modelProfileEndpoint)
		{
			SelectedItem = Items
				.Cast<ComboBoxModelItem>()
				.FirstOrDefault(item => item.Model.Id.Equals(modelName) &&
				item.Model.Profile.ClientOptions.Endpoint.OriginalString.Equals(modelProfileEndpoint));
		}

		public class ComboBoxModelItem(ModelsComboBox parent, IModel model) : IComboItem
		{
			private readonly ModelsComboBox parent = parent;

			public string Guid { get; } = model.Profile.ClientOptions.Endpoint.OriginalString + "//" + model.Id;

			public long Id => parent.Items.IndexOf(this);

			public string Prefix => string.IsNullOrEmpty(Model.Profile.Caption) ? Model.Profile.ApiType.ToString() : Model.Profile.Caption;

			public string Text => Model.Id;

			public IModel Model { get; } = model;

			object IComboItem.Tag { get; } = model.Profile.ClientOptions.Endpoint.OriginalString;

		}
	}
}
