namespace iTalentBootcamp_Blog.Core.Dtos
{
    public class MerchantBaseModel
    {
        public string MerchantCode { get; set; }
    }

    public class UpdateMerchantRequestModel : MerchantBaseModel
    {
        public string Name { get; set; }
    }
}
