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
    public class SalesOrderDetailViewModel
    {
        [Key]
        public int SalesOrderDetailId { get; set; }
        
        [ReadOnly(true)]    
        [Display(Name = "Sales Order")]
        [Editable(false)]
        [HiddenInput(DisplayValue = false)]
        public int? SalesOrderHeaderId { get; set; }

        [Relation(EntityType = typeof(Recipe), DataFieldValue = "RecipeId", DataFieldText = "Name")]
        [Display(Name = "Recipe")]
        public int? RecipeId { get; set; }
     

        [Display(Name = "Order QTY")]
        public double? OrderQuantity { get; set; }

        public decimal? UnitPrice { get; set; }


        public double? UnitPriceDiscount { get; set; }

        [ReadOnly(true)]
        public decimal LineTotal { get; set; }
     
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static SalesOrderDetailViewModel ConvertFromSalesOrderDetailEntity(
            SalesOrderDetail entity, SalesOrderDetailViewModel model)
        {
            model.LineTotal = (decimal)entity.LineTotal;
            model.ModifiedByUser = entity.ModifiedByUser;
            model.ModifiedDate = entity.ModifiedDate;
            model.OrderQuantity = entity.OrderQuantity;
            model.RecipeId = entity.RecipeId;
            model.SalesOrderDetailId = entity.SalesOrderDetailId;
            model.SalesOrderHeaderId = entity.SalesOrderHeaderId;
            model.UnitPrice = entity.UnitPrice;
            model.UnitPriceDiscount = entity.UnitPriceDiscount;

            return model;
        }

        public static SalesOrderDetail ConvertToSalesOrderDetailEntity(SalesOrderDetailViewModel model,
            SalesOrderDetail entity)
        {
            entity.LineTotal = (double)model.LineTotal;
            entity.ModifiedByUser = model.ModifiedByUser;
            entity.ModifiedDate = model.ModifiedDate;
            entity.OrderQuantity = model.OrderQuantity;
            entity.RecipeId = model.RecipeId;
            entity.SalesOrderDetailId = model.SalesOrderDetailId;
            entity.SalesOrderHeaderId = model.SalesOrderHeaderId;
            entity.UnitPrice = model.UnitPrice;
            entity.UnitPriceDiscount = model.UnitPriceDiscount;

            return entity;
        }
    }
}