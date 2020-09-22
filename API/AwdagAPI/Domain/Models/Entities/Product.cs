using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Cena podana w EUR, wyliczona na podstawie ceny typu produktu
        /// </summary>
        [Column(TypeName="Money")]
        public decimal PriceInEuro { get; set; }

        //Albo Order albo AnonymousOrder - nie mogą być dwa na raz null albo być dwa na raz przypisane
        public Guid? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Guid? AnonymousOrderId { get; set; }
        public virtual AnonymousOrder AnonymousOrder { get; set; }

        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

        public Guid ProductImageId { get; set; }
        public virtual ProductImage ProductImage { get; set; }

        public virtual ICollection<ProductTransformation> ProductTransformations { get; set; }
    }
}