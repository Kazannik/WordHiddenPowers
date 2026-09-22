using LLMConnectorLibrary.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using WordHiddenPowers.Controls.ComboControls;

namespace WordHiddenPowers.Controls
{
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(ComboBox))]
	[ComVisible(false)]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]

	public class ToolStripModelsBox : ToolStripControlHost
	{
		public ToolStripModelsBox() : base(new ModelsComboBox())
		{
			AutoSize = false;
			ModelsComboBoxControl.SelectedIndexChanged += new EventHandler(ModelsComboBoxControl_SelectedIndexChanged);
		}

		private void ModelsComboBoxControl_SelectedIndexChanged(object sender, EventArgs e) => DoSelectedModelChanged();

		public ModelsComboBox ModelsComboBoxControl => (ModelsComboBox)Control;

		#region SelectedModelChanged

		public IModel SelectedModel
		{
			get => ModelsComboBoxControl?.SelectedItem.Model;
			set => ModelsComboBoxControl.SelectModel(value);
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ModelEventArgs> SelectedModelChanged;
		protected virtual void OnSelectedModelChanged(ModelEventArgs e) => SelectedModelChanged?.Invoke(this, e);

		public void DoSelectedModelChanged() => OnSelectedModelChanged(new ModelEventArgs(ModelsComboBoxControl?.SelectedItem.Model));

		#endregion

		public class ModelEventArgs(IModel model) : EventArgs
		{
			public IModel Model { get; } = model;
		}
	}
}
