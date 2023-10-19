using FluentValidation;

namespace BidIt_Saitynai.Data.Dtos
{
    public class CompanyDtos
    {
        public record CompanyDto(int id, string name);
        public record CreateCompanyDto(string name);
        public record UpdateCompanyDto(string name);
        public class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
        {
            public UpdateCompanyDtoValidator()
            {
                RuleFor(dto => dto.name).NotEmpty().NotNull().Length(2, 100);
            }
        }
        public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDto>
        {
            public CreateCompanyDtoValidator()
            {
                RuleFor(dto => dto.name).NotEmpty().NotNull().Length(2, 100);
               
            }
        }
    }
}
