namespace Domain.Models.Entities.Association
{
    public class ProductTypeParameterProductType
    {
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

        public int ProductTypeParameterId { get; set; }
        public virtual ProductTypeParameter ProductTypeParameter { get; set; }
    }
}