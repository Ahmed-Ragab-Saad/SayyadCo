using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;
using System.Security.Cryptography;

namespace SayyadCo.Application.Features.Codes.Commands.GenerateCode
{
    public class GenerateCodeCommandHandler : IRequestHandler<GenerateCodeCommand, Result<GenerateCodeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateCodeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GenerateCodeResponseDto>> Handle(GenerateCodeCommand request, CancellationToken cancellationToken)
        {
            var sectionGame = await _unitOfWork.SectionGames.GetAsync(request.SectionId, request.GameId);
            if (sectionGame is null)
                return Result<GenerateCodeResponseDto>.NotFound("SectionGame not found");

            var sectionGamePlan = await _unitOfWork.SectionGamePlans.GetByIdWithPlanAsync(request.SectionGamePlanId);
            if (sectionGamePlan is null)
                return Result<GenerateCodeResponseDto>.NotFound("Plan not found");

            if (sectionGamePlan.SectionId != request.SectionId || sectionGamePlan.GameId != request.GameId)
                return Result<GenerateCodeResponseDto>.Failure("Plan does not belong to this SectionGame");

            var codeValue = GenerateUniqueCode();

            var code = new Code
            {
                Value = codeValue,
                SectionId = request.SectionId,
                GameId = request.GameId,
                GameRoleId = sectionGamePlan.GameRoleId,
                SectionGamePlanId = request.SectionGamePlanId,
                ExpiresAt = request.ExpiresAt
            };

            await _unitOfWork.Codes.AddAsync(code);
            await _unitOfWork.SaveChangesAsync();

            return Result<GenerateCodeResponseDto>.Success(new GenerateCodeResponseDto
            {
                Id = code.Id,
                Value = code.Value,
                SectionId = code.SectionId,
                GameId = code.GameId,
                PlanId = sectionGamePlan.PlanId,
                PlanTitleEn = sectionGamePlan.Plan.TitleEn,
                PlanTitleAr = sectionGamePlan.Plan.TitleAr,
                Price = sectionGamePlan.Plan.Price,
                DurationInDays = sectionGamePlan.Plan.DurationInDays,
                ExpiresAt = code.ExpiresAt!.Value,
                GameRole = await _unitOfWork.GameRoles.GetRoleNameAsync(sectionGamePlan.GameRoleId) ?? string.Empty
            });
        }

        private static string GenerateUniqueCode()
        {
            var bytes = new byte[6];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)
                .Replace("/", "")
                .Replace("+", "")
                .Replace("=", "")
                .ToUpper()[..8];
        }
    }
}
