using AutoMapper;
using Size = CustomerApp.Models.Size;

namespace CustomerApp.Mappers;

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
	}
}
