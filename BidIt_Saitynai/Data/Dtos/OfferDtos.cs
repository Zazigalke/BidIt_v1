using BidIt_Saitynai.Data.Entities;
using FluentValidation;

namespace BidIt_Saitynai.Data.Dtos
{
    public class OfferDtos
    {
    
        public record OfferDto(int id, double price, DateTime creationDate, string book, string auction);
        public record CreateOfferDto(double price, int book, int auction);
        public record UpdateOfferDto(double price, DateTime creationDate, int book, int auction);
        public class UpdateOfferDtoValidator : AbstractValidator<UpdateOfferDto>
        {
            public UpdateOfferDtoValidator()
            {
                RuleFor(dto => dto.price).NotEmpty().NotNull();
                RuleFor(dto => dto.creationDate).NotEmpty().NotNull();
                RuleFor(dto => dto.book).NotEmpty().NotNull();
                RuleFor(dto => dto.auction).NotEmpty().NotNull();
            }
        }
        public class CreateOfferDtoValidator : AbstractValidator<CreateOfferDto>
        {
            public CreateOfferDtoValidator()
            {
                RuleFor(dto => dto.price).NotEmpty().NotNull();
                RuleFor(dto => dto.book).NotEmpty().NotNull();
                RuleFor(dto => dto.auction).NotEmpty().NotNull();
            }
        }
    }
}
