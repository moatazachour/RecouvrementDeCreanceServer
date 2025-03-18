using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.Acheteurs.Commands.AddAcheteurs
{
    public class AddAcheteursCommandHandler : IRequestHandler<AddAcheteursCommand, bool>
    {
        private readonly IAcheteurRepository _acheteurRepository;

        public AddAcheteursCommandHandler(IAcheteurRepository acheteurRepository)
        {
            _acheteurRepository = acheteurRepository;
        }

        public Task<bool> Handle(AddAcheteursCommand request, CancellationToken cancellationToken)
        {
            return _acheteurRepository.AddAcheteursAsync();
        }
    }
}
