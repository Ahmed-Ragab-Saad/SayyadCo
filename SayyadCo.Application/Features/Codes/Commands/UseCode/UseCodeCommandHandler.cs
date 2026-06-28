using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Codes.Commands.UseCode
{
    public class UseCodeCommandHandler : IRequestHandler<UseCodeCommand, Result<UseCodeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UseCodeCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Result<UseCodeResponseDto>> Handle(UseCodeCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<UseCodeResponseDto>.Unauthorized("Unauthorized");

            var code = await _unitOfWork.Codes.GetByValueAsync(request.Value);
            if (code is null)
                return Result<UseCodeResponseDto>.NotFound("Invalid code");

            if (code.IsUsed)
                return Result<UseCodeResponseDto>.Failure("Code already used");

            if (code.ExpiresAt.HasValue && code.ExpiresAt.Value < DateTime.UtcNow)
                return Result<UseCodeResponseDto>.Failure("Code has expired");

            if (request.SectionId != code.SectionId || request.GameId != code.GameId)
                return Result<UseCodeResponseDto>.Failure("This code does not belong to this game");

            var existing = await _unitOfWork.UserGames.GetAsync(userId, code.SectionId, code.GameId);

            if (existing is not null)
                return Result<UseCodeResponseDto>.Failure("You are already subscribed to this game");

            var startDate = DateTime.UtcNow;
            var expirationDate = startDate.AddDays(code.SectionGamePlan.Plan.DurationInDays);

            await _unitOfWork.UserGames.AddAsync(new UserGame
            {
                UserId = userId,
                SectionId = code.SectionId,
                GameId = code.GameId,
                StartDate = startDate,
                ExpirationDate = expirationDate,
                GameRoleId = code.GameRoleId
            });

            code.IsUsed = true;
            code.UsedByUserId = userId;
            code.UsedAt = DateTime.UtcNow;
            _unitOfWork.Codes.Update(code);

            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<UseCodeResponseDto>(code);
            response.StartDate = startDate;
            response.ExpirationDate = expirationDate;

            return Result<UseCodeResponseDto>.Success(response);
        }
    }
}
