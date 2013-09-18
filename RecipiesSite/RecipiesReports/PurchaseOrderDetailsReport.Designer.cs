namespace RecipiesReports
{
    partial class PurchaseOrderDetailsReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseOrderDetailsReport));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.productIdCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.orderQuantityCaptionTextBox = new Telerik.Reporting.TextBox();
            this.unitPriceCaptionTextBox = new Telerik.Reporting.TextBox();
            this.lineTotalCaptionTextBox = new Telerik.Reporting.TextBox();
            this.dsSalesOrderDetails = new Telerik.Reporting.OpenAccessDataSource();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.productIdDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.orderQuantityDataTextBox = new Telerik.Reporting.TextBox();
            this.unitPriceDataTextBox = new Telerik.Reporting.TextBox();
            this.lineTotalDataTextBox = new Telerik.Reporting.TextBox();
            this.odsSalesOrderDetails = new Telerik.Reporting.ObjectDataSource();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.47996059060096741D);
            this.labelsGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox4});
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.PrintOnEveryPage = true;
            this.labelsGroupFooterSection.Style.Visible = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.0790872573852539D), Telerik.Reporting.Drawing.Unit.Inch(0.47996059060096741D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.StyleName = "Total";
            this.textBox3.Value = "Total";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:C2}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.47988176345825195D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Italic = false;
            this.textBox4.Style.Visible = true;
            this.textBox4.Value = "= Sum(Fields.LineTotal)";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.productIdCaptionTextBox,
            this.textBox1,
            this.orderQuantityCaptionTextBox,
            this.unitPriceCaptionTextBox,
            this.lineTotalCaptionTextBox,
            this.textBox5});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // productIdCaptionTextBox
            // 
            this.productIdCaptionTextBox.CanGrow = true;
            this.productIdCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.productIdCaptionTextBox.Name = "productIdCaptionTextBox";
            this.productIdCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.productIdCaptionTextBox.StyleName = "Caption";
            this.productIdCaptionTextBox.Value = "Product Code";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3376177549362183D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Product Name";
            // 
            // orderQuantityCaptionTextBox
            // 
            this.orderQuantityCaptionTextBox.CanGrow = true;
            this.orderQuantityCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4248433113098145D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.orderQuantityCaptionTextBox.Name = "orderQuantityCaptionTextBox";
            this.orderQuantityCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.orderQuantityCaptionTextBox.StyleName = "Caption";
            this.orderQuantityCaptionTextBox.Value = "Order Quantity";
            // 
            // unitPriceCaptionTextBox
            // 
            this.unitPriceCaptionTextBox.CanGrow = true;
            this.unitPriceCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.76242208480835D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.unitPriceCaptionTextBox.Name = "unitPriceCaptionTextBox";
            this.unitPriceCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.unitPriceCaptionTextBox.StyleName = "Caption";
            this.unitPriceCaptionTextBox.Value = "Unit Price";
            // 
            // lineTotalCaptionTextBox
            // 
            this.lineTotalCaptionTextBox.CanGrow = true;
            this.lineTotalCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.lineTotalCaptionTextBox.Name = "lineTotalCaptionTextBox";
            this.lineTotalCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.lineTotalCaptionTextBox.StyleName = "Caption";
            this.lineTotalCaptionTextBox.Value = "Line Total";
            // 
            // dsSalesOrderDetails
            // 
            this.dsSalesOrderDetails.ConnectionString = "Connection";
            this.dsSalesOrderDetails.Name = "dsSalesOrderDetails";
            this.dsSalesOrderDetails.ObjectContext = "DynamicApplicationModel.RecipiesModel, DynamicApplicationModel, Version=1.0.0.0, " +
    "Culture=neutral, PublicKeyToken=null";
            this.dsSalesOrderDetails.ObjectContextMember = "PurchaseOrderDetails";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.pictureBox1});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3958332538604736D), Telerik.Reporting.Drawing.Unit.Inch(0.099960647523403168D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6874996423721314D), Telerik.Reporting.Drawing.Unit.Inch(0.2999606728553772D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Order Details";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5D), Telerik.Reporting.Drawing.Unit.Inch(0.69996070861816406D));
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.productIdDataTextBox,
            this.textBox2,
            this.orderQuantityDataTextBox,
            this.unitPriceDataTextBox,
            this.lineTotalDataTextBox,
            this.textBox6});
            this.detail.Name = "detail";
            // 
            // productIdDataTextBox
            // 
            this.productIdDataTextBox.CanGrow = true;
            this.productIdDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.productIdDataTextBox.Name = "productIdDataTextBox";
            this.productIdDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.productIdDataTextBox.StyleName = "Data";
            this.productIdDataTextBox.Value = "=Fields.Product.Code";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3584117889404297D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=Fields.Product.Name";
            // 
            // orderQuantityDataTextBox
            // 
            this.orderQuantityDataTextBox.CanGrow = true;
            this.orderQuantityDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4248433113098145D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.orderQuantityDataTextBox.Name = "orderQuantityDataTextBox";
            this.orderQuantityDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.orderQuantityDataTextBox.StyleName = "Data";
            this.orderQuantityDataTextBox.Value = "=Fields.OrderQuantity";
            // 
            // unitPriceDataTextBox
            // 
            this.unitPriceDataTextBox.CanGrow = true;
            this.unitPriceDataTextBox.Format = "{0:C2}";
            this.unitPriceDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.76242208480835D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.unitPriceDataTextBox.Name = "unitPriceDataTextBox";
            this.unitPriceDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.unitPriceDataTextBox.StyleName = "Data";
            this.unitPriceDataTextBox.Value = "=Fields.UnitPrice";
            // 
            // lineTotalDataTextBox
            // 
            this.lineTotalDataTextBox.CanGrow = true;
            this.lineTotalDataTextBox.Format = "{0:C2}";
            this.lineTotalDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.lineTotalDataTextBox.Name = "lineTotalDataTextBox";
            this.lineTotalDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3374996185302734D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.lineTotalDataTextBox.StyleName = "Data";
            this.lineTotalDataTextBox.Value = "=Fields.LineTotal";
            // 
            // odsSalesOrderDetails
            // 
            this.odsSalesOrderDetails.DataMember = "PurchaseOrderDetails";
            this.odsSalesOrderDetails.DataSource = "DynamicApplicationModel.RecipiesModel, DynamicApplicationModel, Version=1.0.0.0, " +
    "Culture=neutral, PublicKeyToken=null";
            this.odsSalesOrderDetails.Name = "odsSalesOrderDetails";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6751964092254639D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7495683431625366D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "Unit";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6959903240203857D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7287744283676148D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "=Fields.UnitMeasure.Name";
            // 
            // PurchaseOrderDetailsReport
            // 
            this.DataSource = this.dsSalesOrderDetails;
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.reportHeader,
            this.detail});
            this.Name = "SalesOrderDetails";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(103)))), ((int)(((byte)(109)))));
            styleRule1.Style.Font.Name = "Book Antiqua";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(103)))), ((int)(((byte)(109)))));
            styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(185)))), ((int)(((byte)(102)))));
            styleRule2.Style.Font.Name = "Book Antiqua";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Book Antiqua";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            styleRule4.Style.Font.Name = "Book Antiqua";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.4583330154418945D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.OpenAccessDataSource dsSalesOrderDetails;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox productIdCaptionTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox orderQuantityCaptionTextBox;
        private Telerik.Reporting.TextBox unitPriceCaptionTextBox;
        private Telerik.Reporting.TextBox lineTotalCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox productIdDataTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox orderQuantityDataTextBox;
        private Telerik.Reporting.TextBox unitPriceDataTextBox;
        private Telerik.Reporting.TextBox lineTotalDataTextBox;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.ObjectDataSource odsSalesOrderDetails;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;

    }
}