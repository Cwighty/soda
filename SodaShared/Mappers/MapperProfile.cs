using Size = SodaShared.Models.Size;

namespace SodaShared.Mappers;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<AddOnData, AddOn>().ReverseMap();
		CreateMap<AddOnTypeData, AddOnType>().ReverseMap();
		CreateMap<BaseData, Base>().ReverseMap();
		CreateMap<BaseTypeData, BaseType>().ReverseMap();
		CreateMap<CategoryData, Category>().ReverseMap();
		CreateMap<CustomerData, Customer>().ReverseMap();
		CreateMap<ProductData, Product>().ReverseMap();
		CreateMap<SizeData, Size>().ReverseMap();
		CreateMap<PurchaseData, Purchase>().ReverseMap();
		CreateMap<PurchaseItemData, PurchaseItem>().ReverseMap();
		CreateMap<Product, PurchaseItem>()
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Product, opt => opt.MapFrom(src => src))
			.ReverseMap();
		CreateMap<Product, Product>().ReverseMap();
	}
}
