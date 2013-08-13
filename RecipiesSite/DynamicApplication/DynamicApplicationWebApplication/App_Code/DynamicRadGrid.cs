using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess.Web;

namespace Telerik.Web.UI.DynamicData
{
    [ControlValueProperty("DataKeySelectedValue")]
    public sealed class DynamicRadGrid : RadGrid, IControlParameterTarget, IPersistedSelector, IDataBoundControl
    {
        private string selectedIndex;
        private System.Web.UI.WebControls.DataKey dataKey;

        public DynamicRadGrid()
        {
            this.selectedIndex = null;
        }

        [Browsable(false)]
        public object DataKeySelectedValue
        {
            get
            {
                if (this.DataKey != null)
                {
                    return this.DataKey.Value;
                }

                return this.SelectedValue;
            }
        }

        public System.Web.UI.WebControls.DataKey DataKey
        {
            get
            {
                return this.dataKey;
            }
            set
            {
                this.dataKey = value;
                if (this.dataKey != null)
                {
                    ((IStateManager)this.dataKey).TrackViewState();
                }
            }
        }

        MetaTable IControlParameterTarget.Table
        {
            get
            {
                return this.FindMetaTable();
            }
        }

        public MetaColumn FilteredColumn
        {
            get
            {
                return null;
            }
        }

        string[] IDataBoundControl.DataKeyNames
        {
            get
            {
                return this.MasterTableView.DataKeyNames;
            }
            set
            {
                this.MasterTableView.DataKeyNames = value;
            }
        }

        string IDataBoundControl.DataMember
        {
            get
            {
                return this.MasterTableView.DataMember;
            }
            set
            {
                this.MasterTableView.DataMember = value;
            }
        }

        object IDataBoundControl.DataSource
        {
            get
            {
                return this.DataSource;
            }
            set
            {
                this.DataSource = value;
            }
        }

        string IDataBoundControl.DataSourceID
        {
            get
            {
                return this.DataSourceID;
            }
            set
            {
                this.DataSourceID = value;
            }
        }

        IDataSource IDataBoundControl.DataSourceObject
        {
            get
            {
                return this.DataSourceObject;
            }
        }

        public string GetPropertyNameExpression(string columnName)
        {
            return "DataKeySelectedValue";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.AllowAutomaticUpdates = true;
            this.AllowAutomaticDeletes = true;

            // I added this

            //this.AllowFilteringByColumn = true;
            //this.AllowSorting = true;

            //var test = this.DataSource as OpenAccessLinqDataSource;
               

            //


            //MetaTable metaTable = DynamicDataRouteHandler.GetRequestMetaTable(this.Context);
            //List<string> list = new List<string>();
            //foreach (MetaColumn column in metaTable.PrimaryKeyColumns)
            //{
            //    list.Add(column.Name);
            //}

            //this.MasterTableView.DataKeyNames = list.ToArray();
            //foreach (MetaColumn column in metaTable.Columns)
            //{
            //    if (!column.Scaffold || column.IsLongString || column is MetaChildrenColumn)
            //    {
            //        continue;
            //    }

            //    DynamicGridBoundColumn gridColumn = new DynamicGridBoundColumn();
            //    gridColumn.DataField = column.Name;
            //    gridColumn.ConvertEmptyStringToNull = column.ConvertEmptyStringToNull;
            //    gridColumn.DataFormatString = column.DataFormatString;
            //    gridColumn.UIHint = column.UIHint;
            //    gridColumn.HtmlEncode = column.HtmlEncode;
            //    gridColumn.NullDisplayText = column.NullDisplayText;
            //    gridColumn.ApplyFormatInEditMode = column.ApplyFormatInEditMode;
            //    gridColumn.HeaderText = column.DisplayName;

            //    this.MasterTableView.Columns.Add(gridColumn);
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.SelectedIndexes.Count > 0)
            {
                this.selectedIndex = this.SelectedIndexes[0];
            }

            base.OnLoad(e);
        }

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);
            if (string.IsNullOrEmpty(this.selectedIndex) == false)
            {
                GridItem item = this.Items[this.selectedIndex];
                if (item != null)
                {
                    item.Selected = true;
                }
            }
            //else
            //{
            //    if (this.Items.Count > 0)
            //    {
            //        this.Items[0].Selected = true;
            //    }
            //}
        }
    }

    public class DynamicGridBoundColumn : GridBoundColumn
    {
        private bool? applyFormatInEditMode;
        private string dataFormatString;
        private bool htmlEncode = true;
        private string nullDisplayText;
        private MetaColumn column;

        [Category("Data"), DefaultValue("")]
        public override string DataFormatString
        {
            get
            {
                return this.dataFormatString ?? string.Empty;
            }
            set
            {
                this.dataFormatString = value;
            }
        }

        public override string HeaderText
        {
            get
            {
                object storedHeaderText = this.ViewState["HeaderText"];
                if (storedHeaderText != null)
                {
                    return (string)storedHeaderText;
                }

                if (this.Column != null)
                {
                    return this.Column.DisplayName;
                }

                return this.DataField;
            }
            set
            {
                base.HeaderText = value;
            }
        }

        [DefaultValue(true), Category("Behavior")]
        public override bool HtmlEncode
        {
            get
            {
                return this.htmlEncode;
            }
            set
            {
                this.htmlEncode = value;
            }
        }

        [Category("Behavior"), DefaultValue("")]
        public string NullDisplayText
        {
            get
            {
                return this.nullDisplayText ?? string.Empty;
            }
            set
            {
                this.nullDisplayText = value;
            }
        }

        public override string SortExpression
        {
            get
            {
                object storedSortExpression = this.ViewState["SortExpression"];
                if (storedSortExpression != null)
                {
                    return (string)storedSortExpression;
                }

                if (this.Column != null)
                {
                    return this.Column.SortExpression;
                }

                return string.Empty;
            }
        }

        [DefaultValue(""), Category("Behavior")]
        public virtual string UIHint
        {
            get
            {
                object storedUIHint = this.ViewState["UIHint"];
                if (storedUIHint != null)
                {
                    return (string)storedUIHint;
                }

                return string.Empty;
            }
            set
            {
                if (object.Equals(value, this.ViewState["UIHint"]) == false)
                {
                    this.ViewState["UIHint"] = value;
                    this.OnColumnChanged();
                }
            }
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool ApplyFormatInEditMode
        {
            get
            {
                if (this.applyFormatInEditMode.HasValue)
                {
                    return this.applyFormatInEditMode.Value;
                }

                return false;
            }
            set
            {
                this.applyFormatInEditMode = new bool?(value);
            }
        }

        private MetaColumn Column
        {
            get
            {
                if (this.DesignMode || (this.Owner == null))
                {
                    return null;
                }

                if (this.column == null)
                {
                    MetaTable table = this.Owner.OwnerGrid.FindMetaTable();
                    if (table == null)
                    {
                        throw new InvalidOperationException(string.Empty);
                    }

                    this.column = table.GetColumn(this.DataField);
                }

                return this.column;
            }
        }

        public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
        {
            if (newValues == null)
            {
                throw new ArgumentNullException("newValues");
            }

            OrderedDictionary values = new OrderedDictionary();
            DynamicGridBoundColumn.ExtractValuesFromBindableControls(values, editableItem);
            foreach (DictionaryEntry entry in values)
            {
                if (newValues.Contains(entry.Key) == false)
                {
                    newValues.Add(entry.Key, entry.Value);
                }
            }
        }

        public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
        {
            if (cell == null)
            {
                throw new ArgumentNullException("cell");
            }

            if (inItem == null)
            {
                throw new ArgumentNullException("inItem");
            }

            if (inItem is GridEditableItem)
            {
                DynamicControl dynamicControl = new DynamicControl();
                dynamicControl.DataField = this.DataField;
                bool isInsert = inItem is GridEditFormInsertItem || inItem is GridDataInsertItem;

                if (isInsert)
                {
                    dynamicControl.Mode = DataBoundControlMode.Insert;
                }

                if (inItem.IsInEditMode)
                {
                    dynamicControl.Mode = DataBoundControlMode.Edit;
                }

                dynamicControl.UIHint = this.UIHint;
                dynamicControl.HtmlEncode = this.HtmlEncode;
                dynamicControl.DataFormatString = this.DataFormatString;
                dynamicControl.NullDisplayText = this.NullDisplayText;
                if (this.ConvertEmptyStringToNull)
                {
                    dynamicControl.ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
                }

                if (this.applyFormatInEditMode.HasValue)
                {
                    dynamicControl.ApplyFormatInEditMode = this.ApplyFormatInEditMode;
                }

                cell.Controls.Add(dynamicControl);
            }
            else
            {
                base.InitializeCell(cell, columnIndex, inItem);
            }
        }

        public override GridColumn Clone()
        {
            DynamicGridBoundColumn column = new DynamicGridBoundColumn();
            column.CopyBaseProperties(this);

            return column;
        }

        internal static void ExtractValuesFromBindableControls(IOrderedDictionary dictionary, Control container)
        {
            IBindableControl control = container as IBindableControl;
            if (control != null)
            {
                control.ExtractValues(dictionary);
            }

            foreach (Control control2 in container.Controls)
            {
                DynamicGridBoundColumn.ExtractValuesFromBindableControls(dictionary, control2);
            }
        }

        protected override void CopyBaseProperties(GridColumn fromColumn)
        {
            base.CopyBaseProperties(fromColumn);

            DynamicGridBoundColumn source = fromColumn as DynamicGridBoundColumn;
            if (source == null)
            {
                throw new ArgumentException("fromColumn");    
            }

            this.ConvertEmptyStringToNull = source.ConvertEmptyStringToNull;
            this.DataFormatString = source.DataFormatString;
            this.UIHint = source.UIHint;
            this.HtmlEncode = source.HtmlEncode;
            this.NullDisplayText = source.NullDisplayText;
            this.ApplyFormatInEditMode = source.ApplyFormatInEditMode;
        }
    }
}