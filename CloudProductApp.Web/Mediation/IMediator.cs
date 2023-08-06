using MediatR;

namespace CloudProductApp.Web.Mediation
{
  public interface IMediator
  {
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
  }

}
