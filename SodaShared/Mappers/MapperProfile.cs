using Size = SodaShared.Models.Size;

namespace SodaShared.Mappers;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<AddOnData, AddOn>();
		CreateMap<AddOn, AddOnData>()
            .ForMember(dest => dest.AddOnTypeId, opt => opt.MapFrom(src => src.AddOnType.Id));
		CreateMap<AddOnTypeData, AddOnType>().ReverseMap();
		CreateMap<BaseData, Base>();
		CreateMap<Base, BaseData>()
            .ForMember(dest => dest.BaseTypeId, opt => opt.MapFrom(src => src.BaseType.Id));
		CreateMap<BaseTypeData, BaseType>().ReverseMap();
		CreateMap<CategoryData, Category>().ReverseMap();
		CreateMap<CustomerData, Customer>().ReverseMap();
		CreateMap<ProductData, Product>();
		CreateMap<Product, ProductData>()
			.ForMember(dest => dest.BaseId, opt => opt.MapFrom(src => src.Base.Id));
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
