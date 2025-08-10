using InfotecsWebAPI.Application.Common;
using MediatR;

namespace InfotecsWebAPI.Application.UseCases.Commands.FileUpload
{
    public class FileUploadCommand : IRequest<Result>
    {
        public string Filename { get; set; }
        public string Content { get; set; }
    }
}
