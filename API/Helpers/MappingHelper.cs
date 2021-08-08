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
            
            CreateMap<StockDto, Stock>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Modality, ModalityDto>().ReverseMap();

            CreateMap<Segment, SegmentDto>().ReverseMap();
            
            CreateMap<TypeOfStock, TypeOfStockDto>().ReverseMap();
            
            CreateMap<Surtax, SurtaxDto>().ReverseMap();

            CreateMap<StockTransaction, TransactionDto>().ReverseMap();
        }
    }
}