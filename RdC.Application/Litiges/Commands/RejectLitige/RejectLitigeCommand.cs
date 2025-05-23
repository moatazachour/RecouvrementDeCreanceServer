﻿using MediatR;

namespace RdC.Application.Litiges.Commands.RejectLitige
{
    public record RejectLitigeCommand(
        int LitigeID,
        int ResolutedByUserID) 
        : IRequest<bool>;
}
