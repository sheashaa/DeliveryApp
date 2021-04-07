using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DeliveryApp.Models
{
    public class Order
    {
        [Display(Name = "ID")]
        public int OrderId { get; set; }
        [Display(Name = "Date")]
        public DateTime OrderDateTime { get; set; }
        [Display(Name = "Address")]
        public string OrderAdress { get; set; }
        [Display(Name = "Total Price")]
        public double OrderPrice
        {
            get
            {
                double totalPrice = 0.0;
                foreach (OrderItem item in OrderItems)
                {
                    totalPrice += (1.0 - item.OrderItemDiscount) * item.OrderItemPrice;
                }
                return totalPrice;
            }
        } 

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey("Delivery"), Display(AutoGenerateField = false)]
        public int DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public int CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }

    }

    public class OrderItem
    {
        [Display(Name = "ID")]
        public int OrderItemId { get; set; }
        [Display(Name = "Quantity")]
        public int OrderItemQuantity { get; set; }
        [Display(Name = "Price")]
        public double OrderItemPrice { get; set; }
        [Display(Name = "Discount")]
        public double OrderItemDiscount { get; set; }

        [ForeignKey("Order"), Display(AutoGenerateField = false)]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product"), Display(AutoGenerateField = false)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public class Product
    {
        [Display(Name = "ID")]
        public int ProductId { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Price")]
        public double ProductPrice { get; set; }
        [Display(AutoGenerateField = false)]
        public string ProductImage { get; set; }
        [Display(Name = "Count")]
        public int ProductCount { get; set; }

        [ForeignKey("ProductCategory"), Display(AutoGenerateField = false)]
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        [ForeignKey("Branch"), Display(AutoGenerateField = false)]
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }

    public class ProductCategory
    {
        [Display(Name = "ID")]
        public int ProductCategoryId { get; set; }
        [Display(Name = "Name")]
        public string ProductCategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class Establishment
    {
        [Display(Name = "ID")]
        public int EstablishmentId { get; set; }
        [Display(Name = "Name")]
        public string EstablishmentName { get; set; }
        [Display(Name = "Website")]
        public string EstablishmentWebsite { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }

        [ForeignKey("BusinessType"), Display(AutoGenerateField = false)]
        public int BusinessTypeId { get; set; }
        public virtual BusinessType BusinessType { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public int EstablishmentManagerId { get; set; }
        public virtual ApplicationUser EstablishmentManager { get; set; }
    }

    public class Branch
    {
        [Display(Name = "ID")]
        public int BranchId { get; set; }
        [Display(Name = "Name")]
        public string BranchName { get; set; }
        [Display(Name = "Governorate")]
        public string BranchGovernorate { get; set; }
        [Display(Name = "City")]
        public string BranchCity { get; set; }
        [Display(Name = "Street")]
        public string BranchStreet { get; set; }
        [Display(Name = "Latitude")]
        public double BranchLatitude { get; set; }
        [Display(Name = "Longitude")]
        public double BranchLongitude { get; set; }
        [Display(Name = "Phone")]
        public string BranchPhone { get; set; }

        [ForeignKey("Establishment"), Display(AutoGenerateField = false)]
        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }

    public class BusinessType
    {
        [Display(Name = "ID")]
        public int BusinessTypeId { get; set; }
        [Display(Name = "Type")]
        public string BusinessTypeName { get; set; }

        public virtual ICollection<Establishment> Establishments { get; set; }
    }

    public class Delivery
    {
        [Display(Name = "ID")]
        public int DeliveryId { get; set; }
        [Display(Name = "Date")]
        public DateTime DeliveryDateTime { get; set; }

        [ForeignKey("Branch"), Display(AutoGenerateField = false)]
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public int DeliveryAgentId { get; set; }
        public virtual ApplicationUser DeliveryAgent { get; set; }
        
    }
     
}