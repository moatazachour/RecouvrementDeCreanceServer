using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Litiges;

namespace RdC.Application.Litiges.Commands.UploadLitigeJustificatifs
{
    internal sealed class UploadLitigeJustificatifsCommandHandler
        : IRequestHandler<UploadLitigeJustificatifsCommand, Unit>
    {
        private readonly ILitigeRepository _litigeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadLitigeJustificatifsCommandHandler(
            ILitigeRepository litigeRepository, 
            IUnitOfWork unitOfWork)
        {
            _litigeRepository = litigeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UploadLitigeJustificatifsCommand request, CancellationToken cancellationToken)
        {
            var litige = await _litigeRepository.GetByIdAsync(request.LitigeID);

            if (litige is null)
            {
                throw new Exception("Litige not found");
            }

            var litigeJustificatifsDirectory = Path.Combine(@"C:\RdC\LitigeJustificatifs");

            Directory.CreateDirectory(litigeJustificatifsDirectory);

            var folderPath = Path.Combine(
                litigeJustificatifsDirectory,
                litige.Facture.NumFacture,
                $"{litige.Id} - {litige.LitigeType.Name}");

            Directory.CreateDirectory(folderPath);

            foreach (var file in request.Files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.Content.CopyToAsync(stream);

                litige.AddJustificatif(file.FileName, filePath);
            }

            await _unitOfWork.CommitChangesAsync();

            return Unit.Value;
        }
    }
}
