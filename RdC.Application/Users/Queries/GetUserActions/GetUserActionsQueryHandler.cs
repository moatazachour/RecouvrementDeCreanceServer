using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.Litige;
using RdC.Domain.DTO.LitigeType;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.DTO.PlanDePaiement;
using RdC.Domain.DTO.User;
using RdC.Domain.Litiges;
using RdC.Domain.Paiements;
using RdC.Domain.PlanDePaiements;

namespace RdC.Application.Users.Queries.GetUserActions
{
    internal sealed class GetUserActionsQueryHandler
        : IRequestHandler<GetUserActionsQuery, UserActionsResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly ILitigeRepository _litigeRepository;
        private readonly IPaiementRepository _paiementRepository;

        public GetUserActionsQueryHandler(
            IUserRepository userRepository,
            IPlanDePaiementRepository planDePaiementRepository,
            ILitigeRepository litigeRepository,
            IPaiementRepository paiementRepository)
        {
            _userRepository = userRepository;
            _planDePaiementRepository = planDePaiementRepository;
            _litigeRepository = litigeRepository;
            _paiementRepository = paiementRepository;
        }

        public async Task<UserActionsResponse> Handle(GetUserActionsQuery request, CancellationToken cancellationToken)
        {
            var createdPlans = await _planDePaiementRepository.GetAllCreatedByUserIdAsync(request.userID);
            var validatedPlans = await _planDePaiementRepository.GetAllValidatedByUserId(request.userID);

            var createdPlansResponse = MapToPlanDePaiementResponseList(createdPlans);
            var validatedPlansResponse = MapToPlanDePaiementResponseList(validatedPlans);

            var declaredLitiges = await _litigeRepository.GetAllDeclaredByUserIdAsync(request.userID);
            var resolutedLitiges = await _litigeRepository.GetAllResolutedByUserIdAsync(request.userID);

            var declaredLitigesResponse = MapToLitigeResponseList(declaredLitiges);
            var resolutedLitigesResponse = MapToLitigeResponseList(resolutedLitiges);

            var paidPaiements = await _paiementRepository.GetAllByUserIdAsync(request.userID);

            var paidPaiementsResponse = MapToPaiementResponseList(paidPaiements);

            return new UserActionsResponse(
                createdPlansResponse,
                validatedPlansResponse,
                declaredLitigesResponse,
                resolutedLitigesResponse,
                paidPaiementsResponse);
        }

        private List<PlanDePaiementBasicResponse> MapToPlanDePaiementResponseList(List<PlanDePaiement> plans)
        {
            return plans.Select(plan => new PlanDePaiementBasicResponse(
                plan.Id,
                plan.MontantTotal,
                plan.NombreDeEcheances,
                plan.MontantRestant,
                plan.CreationDate,
                plan.PlanStatus,
                plan.IsLocked,
                plan.HasAdvance
            )).ToList();
        }

        private List<LitigeBasicResponse> MapToLitigeResponseList(List<Litige> litiges)
        {
            return litiges.Select(litige => new LitigeBasicResponse(
                                            litige.Id,
                                            new FactureResponse(
                                                litige.FactureID,
                                                litige.Facture.NumFacture,
                                                litige.Facture.DateEcheance,
                                                litige.Facture.MontantTotal,
                                                litige.Facture.MontantRestantDue,
                                                litige.Facture.AcheteurID,
                                                litige.Facture.Status),
                                            new LitigeTypeResponse(
                                                litige.LitigeTypeID,
                                                litige.LitigeType.Name,
                                                litige.LitigeType.Description),
                                            litige.LitigeStatus,
                                            litige.LitigeDescription,
                                            litige.CreationDate,
                                            litige.ResolutionDate
            )).ToList();
        }

        private List<PaiementResponse> MapToPaiementResponseList(List<Paiement> paiements)
        {
            return paiements.Select(p => new PaiementResponse(
                p.Id,
                p.PaiementDate.Id,
                p.MontantPayee,
                p.DateDePaiement)).ToList();
        }
    }
}
