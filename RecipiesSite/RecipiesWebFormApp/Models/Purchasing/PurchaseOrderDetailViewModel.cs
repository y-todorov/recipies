using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RecipiesModelNS;
using System.ComponentModel;
using InventoryManagementMVC.DataAnnotations;
using System.Web.Mvc;

namespace InventoryManagementMVC.Models
{
    public class PurchaseOrderDetailViewModel
    {
        [Key]
        public int PurchaseOrderDetailId { get; set; }

        [ReadOnly(true)]
        //[Relation(EntityType = typeof(PurchaseOrderHeader), DataFieldValue = "PurchaseOrderId",
        //    DataFieldText = "PurchaseOrderId")]
        [Display(Name = "Purchase Order")]
        [Editable(false)]
        [HiddenInput(DisplayValue = false)]
        public int? PurchaseOrderHeaderId { get; set; }

        [Relation(EntityType = typeof (Product), DataFieldValue = "ProductId", DataFieldText = "Name")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Relation(EntityType = typeof (UnitMeasure), DataFieldValue = "UnitMeasureId", DataFieldText = "Name")]
        [Display(Name = "Unit Measure")]
        public int? UnitMeasureId { get; set; }

        [Display(Name = "Order QTY")]
        public double? OrderQuantity { get; set; }

        public decimal? UnitPrice { get; set; }

        [ReadOnly(true)]
        public decimal LineTotal { get; set; }

        [Display(Name = "Received QTY")]
        public double? ReceivedQuantity { get; set; }

        [Display(Name = "Returned QTY")]
        public double? ReturnedQuantity { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Stocked QTY")]
        public double StockedQuantity { get; set; }

        //[ReadOnly(true)]
        //[Display(Name = "PO Total")]
        //public decimal? PoTotal { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public DateTime? PurchaseOrderHeaderShipDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Vendor { get; set; }

        //[HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Category { get; set; }

        [Display(Name = "Order Date")]
        [ReadOnly(true)]
        public DateTime? PurchaseOrderHeaderOrderDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Status { get; set; }

        public static PurchaseOrderDetailViewModel ConvertFromPurchaseOrderDetailEntity(
            PurchaseOrderDetail entity, PurchaseOrderDetailViewModel model)
        {
            if (entity == null)
            {
                throw new ApplicationException(
                    "PurchaseOrderDetail is null in method ConvertFromPurchaseOrderDetailEntity!");
            }
            if (model == null)
            {
                throw new ApplicationException(
                    "PurchaseOrderDetailViewModel is null in method ConvertFromPurchaseOrderDetailEntity!");
            }

            model.LineTotal = (decimal) entity.LineTotal;
            model.ModifiedByUser = entity.ModifiedByUser;
            model.ModifiedDate = entity.ModifiedDate;
            model.OrderQuantity = entity.OrderQuantity;
            model.ProductId = entity.ProductId;
            model.PurchaseOrderDetailId = entity.PurchaseOrderDetailId;
            model.PurchaseOrderHeaderId = entity.PurchaseOrderId;
            model.ReceivedQuantity = entity.ReceivedQuantity;
            model.ReturnedQuantity = entity.ReturnedQuantity;
            model.StockedQuantity = entity.StockedQuantity;
            //if (entity.PurchaseOrderHeader != null)
            //{
            //    model.PoTotal = entity.PurchaseOrderHeader.TotalDue;
            //}

            model.UnitMeasureId = entity.UnitMeasureId;
            model.UnitPrice = entity.UnitPrice;
            if (entity.PurchaseOrderHeader != null)
            {
                model.PurchaseOrderHeaderShipDate = entity.PurchaseOrderHeader.ShipDate;
                model.PurchaseOrderHeaderOrderDate = entity.PurchaseOrderHeader.OrderDate;
            }
            if (entity.PurchaseOrderHeader != null && entity.PurchaseOrderHeader.Vendor != null)
            {
                model.Vendor = entity.PurchaseOrderHeader.Vendor.Name;
            }
            if (entity.Product != null && entity.Product.ProductCategory != null)
            {
                model.Category = entity.Product.ProductCategory.Name;
            }
            if (entity.PurchaseOrderHeader != null &&
                entity.PurchaseOrderHeader.PurchaseOrderStatu != null)
            {
                model.Status = entity.PurchaseOrderHeader.PurchaseOrderStatu.Name;
            }

            return model;
        }

        public static PurchaseOrderDetail ConvertToPurchaseOrderDetailEntity(PurchaseOrderDetailViewModel model,
            PurchaseOrderDetail entity)
        {
            if (entity == null)
            {
                throw new ApplicationException(
                    "PurchaseOrderDetail is null in method ConvertToPurchaseOrderDetailEntity!");
            }
            if (model == null)
            {
                throw new ApplicationException(
                    "PurchaseOrderDetailViewModel is null in method ConvertToPurchaseOrderDetailEntity!");
            }

            //entity.LineTotal = (double)model.LineTotal;
            entity.ModifiedByUser = model.ModifiedByUser;
            entity.ModifiedDate = model.ModifiedDate;
            entity.OrderQuantity = model.OrderQuantity;
            entity.ProductId = model.ProductId;

            entity.PurchaseOrderDetailId = model.PurchaseOrderDetailId;
            if (model.PurchaseOrderHeaderId.HasValue)
            {
                entity.PurchaseOrderId = model.PurchaseOrderHeaderId;
            }
            entity.ReceivedQuantity = model.ReceivedQuantity;
            entity.ReturnedQuantity = model.ReturnedQuantity;
            entity.StockedQuantity = model.StockedQuantity;
            entity.UnitMeasureId = model.UnitMeasureId;
            entity.UnitPrice = model.UnitPrice;

            return entity;
        }
    }
}