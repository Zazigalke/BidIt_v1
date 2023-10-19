using FluentValidation;

namespace BidIt_Saitynai.Data.Dtos
{
    public class BookDtos
    {
        public record BookDto(int id, string title, int condition, int pageCount, double startingPrice, int user);
        public record CreateBookDto(string title, int condition, int pageCount, double startingPrice, int user);
        public record UpdateBookDto(string title, int condition, int pageCount, double startingPrice, int user);
        public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
        {
            public UpdateBookDtoValidator()
            {
                RuleFor(dto => dto.title).NotEmpty().NotNull().Length(2, 200);
                RuleFor(dto => dto.condition).NotEmpty().NotNull().InclusiveBetween(0, 10);
                RuleFor(dto => dto.pageCount).NotEmpty().NotNull();
                RuleFor(dto => dto.startingPrice).NotEmpty().NotNull();
                RuleFor(dto => dto.user).NotEmpty().NotNull();
            }
        }
        public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
        {
            public CreateBookDtoValidator()
            {
                RuleFor(dto => dto.title).NotEmpty().NotNull().Length(2, 200);
                RuleFor(dto => dto.condition).NotEmpty().NotNull().InclusiveBetween(0, 10);
                RuleFor(dto => dto.pageCount).NotEmpty().NotNull();
                RuleFor(dto => dto.startingPrice).NotEmpty().NotNull();
                RuleFor(dto => dto.user).NotEmpty().NotNull();
            }
        }
    }
    
}
