﻿namespace BidIt_Saitynai.Helpers
{
    public record ResourceDto<T>(T Resource, IReadOnlyCollection<LinkDto> Links);
   
}
