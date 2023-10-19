using FluentValidation;

namespace BidIt_Saitynai.Data.Dtos
{
    public class UserDtos
    {
        public record UserDto(int id, string firstName, string lastName, string email);
        public record CreateUserDto(string firstName, string lastName, string email);
        public record UpdateUserDto(string firstName, string lastName, string email);
        public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
        {
            public UpdateUserDtoValidator()
            {
                RuleFor(dto => dto.firstName).NotEmpty().NotNull().Length(2, 100);
                RuleFor(dto => dto.lastName).NotEmpty().NotNull().Length(2, 100);
                RuleFor(dto => dto.email).NotEmpty().NotNull().Length(3, 30);
            }
        }
        public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
        {
            public CreateUserDtoValidator()
            {
                RuleFor(dto => dto.firstName).NotEmpty().NotNull().Length(2, 100);
                RuleFor(dto => dto.lastName).NotEmpty().NotNull().Length(2, 100);
                RuleFor(dto => dto.email).NotEmpty().NotNull().Length(3, 30);
            }
        }
    }
}
