﻿using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IUsersApplicationService : IApplicationService
{
    public Task<GetUserReviewsDto> GetReviewsAsync([Required] Guid userId, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);
}