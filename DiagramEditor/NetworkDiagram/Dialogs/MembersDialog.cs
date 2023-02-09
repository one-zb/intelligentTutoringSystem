﻿
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using KRLab.Core;
using KRLab.Translations;

namespace KRLab.DiagramEditor.NetworkDiagram.Dialogs
{
	public partial class MembersDialog : Form
	{
		CompositeNode parent = null;
		Member member = null;
		bool locked = false;
		int attributeCount = 0;
		bool error = false;

		public event EventHandler ContentsChanged;

		public MembersDialog()
		{
			InitializeComponent();
			lstMembers.SmallImageList = Icons.IconList;
			toolStrip.Renderer = ToolStripSimplifiedRenderer.Default;
		}

		private void OnContentsChanged(EventArgs e)
		{
			if (ContentsChanged != null)
				ContentsChanged(this, e);
		}

		private void UpdateTexts()
		{
			lblSyntax.Text = Strings.Syntax;
			lblName.Text = Strings.Name;
			lblType.Text = Strings.Type;
			lblAccess.Text = Strings.Access;
			lblInitValue.Text = Strings.InitialValue;
			grpOperationModifiers.Text = Strings.Modifiers;
			grpFieldModifiers.Text = Strings.Modifiers;
			toolNewField.Text = Strings.NewField;
			toolNewMethod.Text = Strings.NewMethod;
			toolNewConstructor.Text = Strings.NewConstructor;
			toolNewDestructor.Text = Strings.NewDestructor;
			toolNewProperty.Text = Strings.NewProperty;
			toolNewEvent.Text = Strings.NewEvent;
			toolOverrideList.Text = Strings.OverrideMembers;
			toolImplementList.Text = Strings.Implementing;
			toolSortByKind.Text = Strings.SortByKind;
			toolSortByAccess.Text = Strings.SortByAccess;
			toolSortByName.Text = Strings.SortByName;
			toolMoveUp.Text = Strings.MoveUp;
			toolMoveDown.Text = Strings.MoveDown;
			toolDelete.Text = Strings.Delete;
			lstMembers.Columns[1].Text = Strings.Name;
			lstMembers.Columns[2].Text = Strings.Type;
			lstMembers.Columns[3].Text = Strings.Access;
			lstMembers.Columns[4].Text = Strings.Modifiers;
			btnClose.Text = Strings.ButtonClose;
		}

        public void ShowDialog(CompositeNode parent)
		{
			if (parent == null)
				return;

			this.parent = parent;
			this.Text = string.Format(Strings.MembersOfType, parent.Name);
             
			FillMembersList();
			if (lstMembers.Items.Count > 0) {
				lstMembers.Items[0].Focused = true;
				lstMembers.Items[0].Selected = true;
			}             

			base.ShowDialog();
		}
         

		private void FillMembersList()
		{
			lstMembers.Items.Clear();
			attributeCount = 0;

            //foreach (Field field in parent.Fields)
            //    AddFieldToList(field);

            //foreach (Operation operation in parent.Operations)
            //    AddOperationToList(operation);

			DisableFields();
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="field"/> is null.
		/// </exception>
        //private ListViewItem AddFieldToList(Field field)
        //{
        //    if (field == null)
        //        throw new ArgumentNullException("field");

        //    ListViewItem item = lstMembers.Items.Insert(attributeCount, "");

        //    item.Tag = field;
        //    item.ImageIndex = Icons.GetImageIndex(field);
        //    item.SubItems.Add(field.Name);
        //    item.SubItems.Add(field.Type); 
        //    item.SubItems.Add("");
        //    attributeCount++;

        //    return item;
        //}         

		private void ShowNewMember(Member actualMember)
		{
			if (locked || actualMember == null)
				return;
			else
				member = actualMember;

			RefreshValues();
		}

		private void RefreshValues()
		{
			if (member == null)
				return;

			locked = true;

			txtSyntax.Enabled = true;
			txtName.Enabled = true;
            //txtSyntax.ReadOnly = (member is Destructor);
            //txtName.ReadOnly = (member == null || member.IsNameReadonly);
            //cboType.Enabled = (member != null && !member.IsTypeReadonly);
            //cboAccess.Enabled = (member != null && member.IsAccessModifiable);
            //txtInitialValue.Enabled = (member is Field);
			toolSortByKind.Enabled = true;
			toolSortByAccess.Enabled = true;
			toolSortByName.Enabled = true;

			if (lstMembers.Items.Count > 0) {
				toolSortByKind.Enabled = true;
				toolSortByAccess.Enabled = true;
				toolSortByName.Enabled = true;
			}

			txtSyntax.Text = member.ToString();
			txtName.Text = member.Name;
            //cboType.Text = member.Type;

	
            //if (member is Field) {
            //    Field field = (Field) member;

            //    grpFieldModifiers.Enabled = true;
            //    grpFieldModifiers.Visible = true;
            //    grpOperationModifiers.Visible = false;

            //    chkFieldStatic.Checked = field.IsStatic;
            //    chkReadonly.Checked = field.IsReadonly;
            //    chkConstant.Checked = field.IsConstant;
            //    chkFieldHider.Checked = field.IsHider;
            //    chkVolatile.Checked = field.IsVolatile;
            //    txtInitialValue.Text = field.InitialValue;
            //}

			RefreshMembersList();

			locked = false;

			errorProvider.SetError(txtSyntax, null);
			errorProvider.SetError(txtName, null);
			errorProvider.SetError(cboType, null);
			errorProvider.SetError(cboAccess, null);
			error = false;
		}

		private void RefreshMembersList()
		{
			ListViewItem item = null;

            //if (lstMembers.FocusedItem != null)
            //    item = lstMembers.FocusedItem;
            //else if (lstMembers.SelectedItems.Count > 0)
            //    item = lstMembers.SelectedItems[0];

            //if (item != null && member != null) {
            //    item.ImageIndex = Icons.GetImageIndex(member);
            //    item.SubItems[1].Text = txtName.Text;
            //    item.SubItems[2].Text = cboType.Text;
            //    item.SubItems[3].Text = cboAccess.Text;
            //    if (member is Field) {
            //        item.SubItems[4].Text = member.Language.GetFieldModifierString(
            //            ((Field) member).Modifier);
            //    }
            //    else if (member is Operation) {
            //        item.SubItems[4].Text = member.Language.GetOperationModifierString(
            //            ((Operation) member).Modifier);
            //    }
            //}
		}

		private void DisableFields()
		{
			member = null;

			locked = true;

			txtSyntax.Text = null;
			txtName.Text = null;
			cboType.Text = null;
			cboAccess.Text = null;
			txtInitialValue.Text = null;

			txtSyntax.Enabled = false;
			txtName.Enabled = false;
			cboType.Enabled = false;
			cboAccess.Enabled = false;
			txtInitialValue.Enabled = false;

			grpFieldModifiers.Enabled = false;
			grpOperationModifiers.Enabled = false;

			if (lstMembers.Items.Count == 0) {
				toolSortByKind.Enabled = false;
				toolSortByAccess.Enabled = false;
				toolSortByName.Enabled = false;
			}
			toolMoveUp.Enabled = false;
			toolMoveDown.Enabled = false;
			toolDelete.Enabled = false;

			locked = false;
		}

		private void SwapListItems(ListViewItem item1, ListViewItem item2)
		{
			int image = item1.ImageIndex;
			item1.ImageIndex = item2.ImageIndex;
			item2.ImageIndex = image;

			object tag = item1.Tag;
			item1.Tag = item2.Tag;
			item2.Tag = tag;

			for (int i = 0; i < item1.SubItems.Count; i++) {
				string text = item1.SubItems[i].Text;
				item1.SubItems[i].Text = item2.SubItems[i].Text;
				item2.SubItems[i].Text = text;
			}
			OnContentsChanged(EventArgs.Empty);
		}

		private void DeleteSelectedMember()
		{
			if (lstMembers.SelectedItems.Count > 0) {
				ListViewItem item = lstMembers.SelectedItems[0];
				int index = item.Index;

                //if (item.Tag is Field)
                //    attributeCount--;
                //parent.RemoveMember(item.Tag as Member);
                //lstMembers.Items.Remove(item);
                //OnContentsChanged(EventArgs.Empty);

				int count = lstMembers.Items.Count;
				if (count > 0) {
					if (index >= count)
						index = count - 1;
					lstMembers.Items[index].Selected = true;
				}
				else {
					DisableFields();
				}
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			UpdateTexts();
			errorProvider.SetError(grpFieldModifiers, null);
			errorProvider.SetError(grpOperationModifiers, null);
			error = false;
		}

		private void PropertiesDialog_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) {
				if (error)
					RefreshValues();
				else
					this.Close();
			}
			else if (e.KeyCode == Keys.Enter) {
				lstMembers.Focus();
			}
		}

		private void txtSyntax_Validating(object sender, CancelEventArgs e)
		{
			if (!locked && member != null) {
				try {
					string oldValue = member.ToString();

                    //member.InitFromString(txtSyntax.Text);
                    //errorProvider.SetError(txtSyntax, null);
                    //error = false;

					RefreshValues();
					if (oldValue != txtSyntax.Text)
						OnContentsChanged(EventArgs.Empty);
				}
				catch (BadSyntaxException ex) {
					e.Cancel = true;
					errorProvider.SetError(txtSyntax, ex.Message);
					error = true;
				}
			}
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
			if (!locked && member != null) {
				try {
					string oldValue = member.Name;

					member.Name = txtName.Text;
					errorProvider.SetError(txtName, null);
					error = false;

					RefreshValues();
					if (oldValue != txtName.Text)
						OnContentsChanged(EventArgs.Empty);
				}
				catch (BadSyntaxException ex) {
					e.Cancel = true;
					errorProvider.SetError(txtName, ex.Message);
					error = true;
				}
			}
		}

		private void cboType_Validating(object sender, CancelEventArgs e)
		{
			if (!locked && member != null) {
				try {
                    //string oldValue = member.Type;

                    //member.Type = cboType.Text;
					if (!cboType.Items.Contains(cboType.Text))
						cboType.Items.Add(cboType.Text);
					errorProvider.SetError(cboType, null);
					error = false;
					cboType.Select(0, 0);

					RefreshValues();
                    //if (oldValue != cboType.Text)
                    //    OnContentsChanged(EventArgs.Empty);
				}
				catch (BadSyntaxException ex) {
					e.Cancel = true;
					errorProvider.SetError(cboType, ex.Message);
					error = true;
				}
			}
		}

		private void cboAccess_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = cboAccess.SelectedIndex;

			if (!locked && member != null) {
				try {
					string selectedModifierString = cboAccess.SelectedItem.ToString();

                    //foreach (AccessModifier modifier in member.Language.ValidAccessModifiers.Keys)
                    //{
                    //    if (member.Language.ValidAccessModifiers[modifier] == selectedModifierString)
                    //    {
                    //        member.AccessModifier = modifier;
                    //        RefreshValues();
                    //        OnContentsChanged(EventArgs.Empty);
                    //        break;
                    //    }
                    //}
				}
				catch (BadSyntaxException ex) {
					errorProvider.SetError(cboAccess, ex.Message);
					error = true;
				}
			}
		}

		private void cboAccess_Validated(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(errorProvider.GetError(cboAccess))) {
				errorProvider.SetError(cboAccess, null);
				RefreshValues();
				error = false;
			}
		}
 

		private void grpFieldModifiers_Validated(object sender, EventArgs e)
		{
			errorProvider.SetError(grpFieldModifiers, null);
			error = false;
		}

		private void grpOperationModifiers_Validated(object sender, EventArgs e)
		{
			errorProvider.SetError(grpOperationModifiers, null);
			error = false;
		}

		private void txtInitialValue_Validating(object sender, CancelEventArgs e)
		{
            //if (!locked && member is Field) {
            //    if (txtInitialValue.Text.Length > 0 && txtInitialValue.Text[0] == '"' &&
            //        !txtInitialValue.Text.EndsWith("\""))
            //    {
            //        txtInitialValue.Text += '"';
            //    }
            //    string oldValue = ((Field) member).InitialValue;
            //    ((Field) member).InitialValue = txtInitialValue.Text;

            //    RefreshValues();
            //    if (oldValue != txtInitialValue.Text)
            //        OnContentsChanged(EventArgs.Empty);
            //}
		}

		private void lstMembers_ItemSelectionChanged(object sender,
			ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected && e.Item.Tag is Member) {
				ShowNewMember((Member) e.Item.Tag);

				toolDelete.Enabled = true;
				if (e.ItemIndex < attributeCount) {
					toolMoveUp.Enabled = (e.ItemIndex > 0);
					toolMoveDown.Enabled = (e.ItemIndex < attributeCount - 1);
				}
				else {
					toolMoveUp.Enabled = (e.ItemIndex > attributeCount);
					toolMoveDown.Enabled = (e.ItemIndex < lstMembers.Items.Count - 1);
				}
			}
			else {
				toolMoveUp.Enabled = false;
				toolMoveDown.Enabled = false;
				toolDelete.Enabled = false;
			}
		}

		private void lstMembers_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				DeleteSelectedMember();
		}

		private void AddNewItem(ListViewItem item)
		{
			item.Focused = true;
			item.Selected = true;
			txtName.SelectAll();
			txtName.Focus();
		}

        //private void AddNewField(Field field)
        //{
        //    ListViewItem item = AddFieldToList(field);
        //    AddNewItem(item);
        //    OnContentsChanged(EventArgs.Empty);
        //}


		private void toolNewField_Click(object sender, EventArgs e)
		{
            //if (parent.SupportsFields) {
            //    Field field = parent.AddField();
            //    AddNewField(field);
            //}
		}

		private void toolNewMethod_Click(object sender, EventArgs e)
		{
            //Method method = parent.AddMethod();
            //AddNewOperation(method);
		}

        
		private void toolSortByKind_Click(object sender, EventArgs e)
		{
            //parent.SortMembers(SortingMode.ByKind);
            //FillMembersList();
            //OnContentsChanged(EventArgs.Empty);
		}

		private void toolSortByName_Click(object sender, EventArgs e)
		{
            //parent.SortMembers(SortingMode.ByName);
            //FillMembersList();
            //OnContentsChanged(EventArgs.Empty);
		}

		private void toolMoveUp_Click(object sender, EventArgs e)
		{
			if (lstMembers.SelectedItems.Count > 0) {
				ListViewItem item1 = lstMembers.SelectedItems[0];
				int index = item1.Index;

				if (index > 0) {
					ListViewItem item2 = lstMembers.Items[index - 1];

                    //if (item1.Tag is Field && item2.Tag is Field ||
                    //    item1.Tag is Operation && item2.Tag is Operation)
                    //{
                    //    locked = true;
                    //    parent.MoveUpItem(item1.Tag);
                    //    SwapListItems(item1, item2);
                    //    item2.Focused = true;
                    //    item2.Selected = true;
                    //    locked = false;
                    //    OnContentsChanged(EventArgs.Empty);
                    //}
				}
			}
		}

		private void toolMoveDown_Click(object sender, EventArgs e)
		{
			if (lstMembers.SelectedItems.Count > 0) {
				ListViewItem item1 = lstMembers.SelectedItems[0];
				int index = item1.Index;

				if (index < lstMembers.Items.Count - 1) {
					ListViewItem item2 = lstMembers.Items[index + 1];

                    //if (item1.Tag is Field && item2.Tag is Field ||
                    //    item1.Tag is Operation && item2.Tag is Operation)
                    //{
                    //    locked = true;
                    //    parent.MoveDownItem(item1.Tag);
                    //    SwapListItems(item1, item2);
                    //    item2.Focused = true;
                    //    item2.Selected = true;
                    //    locked = false;
                    //    OnContentsChanged(EventArgs.Empty);
                    //}
				}
			}
		}

		private void toolDelete_Click(object sender, EventArgs e)
		{
			DeleteSelectedMember();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtSyntax_KeyDown(object sender, KeyEventArgs e)
		{
		}
	}
}