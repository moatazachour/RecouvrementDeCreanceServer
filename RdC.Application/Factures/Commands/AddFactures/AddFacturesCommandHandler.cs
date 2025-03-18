using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.Factures.Commands.AddFactures
{
    public class AddFacturesCommandHandler : IRequestHandler<AddFacturesCommand, bool>
    {
        private readonly IFactureRepository _factureRepository;

        private readonly IAcheteurRepository _acheteurRepository;

        public AddFacturesCommandHandler(IFactureRepository factureRepository, IAcheteurRepository acheteurRepository)
        {
            _factureRepository = factureRepository;
            _acheteurRepository = acheteurRepository;
        }

        public async Task<bool> Handle(AddFacturesCommand request, CancellationToken cancellationToken)
        {
            bool refreshAcheteurs = await _acheteurRepository.AddAcheteursAsync();
            bool refreshFactures = await _factureRepository.AddFacturesAsync();

            return refreshAcheteurs && refreshFactures;
        }
    }
}
