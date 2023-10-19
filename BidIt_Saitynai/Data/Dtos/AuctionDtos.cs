using BidIt_Saitynai.Data.Entities;
using FluentValidation;

namespace BidIt_Saitynai.Data.Dtos
{
    public class AuctionDtos
    {
        public record AuctionDto(int id, string name, string city, DateTime creationDate, DateTime startingTime, DateTime endingTime, string company);
        public record CreateAuctionDto(string name, string city, int company);
        public record UpdateAuctionDto(string name, string city, DateTime creationDate, DateTime startingTime, DateTime endingTime, int company);
        public class UpdateAuctionDtoValidator : AbstractValidator<UpdateAuctionDto>
        {
            public UpdateAuctionDtoValidator()
            {
                RuleFor(dto => dto.name).NotEmpty().NotNull().Length(2, 200);
                RuleFor(dto => dto.city).NotEmpty().NotNull().Length(1, 100);
                RuleFor(dto => dto.creationDate).NotEmpty().NotNull();
                RuleFor(dto => dto.startingTime).NotEmpty().NotNull();
                RuleFor(dto => dto.endingTime).NotEmpty().NotNull();
                RuleFor(dto => dto.company).NotEmpty().NotNull();
            }
        }
        public class CreateAuctionDtoValidator : AbstractValidator<CreateAuctionDto>
        {
            public CreateAuctionDtoValidator()
            {
                RuleFor(dto => dto.name).NotEmpty().NotNull().Length(2, 200);
                RuleFor(dto => dto.city).NotEmpty().NotNull().Length(1, 100);
                RuleFor(dto => dto.company).NotEmpty().NotNull();
            }
        }
    }
}
