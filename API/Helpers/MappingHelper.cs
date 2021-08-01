using AutoMapper;
using Core.Entities;
using Core.Dtos;

namespace API.Helpers
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<Stock, StockToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.Modality, o => o.MapFrom(s => s.Modality.Label))
                .ForMember(d => d.Segment, o => o.MapFrom(s => s.Segment.Label))
                .ForMember(d => d.TypeOfStock, o => o.MapFrom(s => s.Type.Label));
            
            CreateMap<StockToCreateDto, Stock>().ReverseMap();
            CreateMap<StockToEditDto, Stock>().ReverseMap();

            CreateMap<Category, CategoryToReturnDto>().ReverseMap();
            CreateMap<CategoryToCreateDto, Category>().ReverseMap();
            CreateMap<CategoryToEditDto, Category>().ReverseMap();

            CreateMap<Modality, ModalityToReturnDto>().ReverseMap();
            CreateMap<ModalityToCreateDto, Modality>().ReverseMap();
            CreateMap<ModalityToEditDto, Modality>().ReverseMap();

            CreateMap<Segment, SegmentToReturnDto>().ReverseMap();
            CreateMap<SegmentToCreateDto, Segment>().ReverseMap();
            CreateMap<SegmentToEditDto, Segment>().ReverseMap();            

            CreateMap<TypeOfStock, TypeOfStockToReturnDto>().ReverseMap();
            CreateMap<TypeOfStockToCreateDto, TypeOfStock>().ReverseMap();
            CreateMap<TypeOfStockToEditDto, TypeOfStock>().ReverseMap();
            
            CreateMap<Surtax, SurtaxToReturnDto>().ReverseMap();
            CreateMap<SurtaxToCreateDto, Surtax>().ReverseMap();
            CreateMap<SurtaxToEditDto, Surtax>().ReverseMap();
        }
    }
}