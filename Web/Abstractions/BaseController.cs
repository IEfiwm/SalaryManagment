using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Infrastructure.Base.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Web.Abstractions
{
    public abstract class BaseController<T> : Controller
    {
        private IMediator _mediatorInstance;

        private IConfiguration _configurationInstance;

        private ILogger<T> _loggerInstance;

        private IViewRenderService _viewRenderInstance;

        private IMapper _mapperInstance;

        private INotyfService _notifyInstance;

        protected IHtmlLocalizer<SharedResource> _localizerInstance;

        protected IPermissionCommon _permissionCommonInstance;
    
        protected IPermissionCommon _permissionCommon => _permissionCommonInstance ??= HttpContext.RequestServices.GetService<IPermissionCommon>();

        protected IHtmlLocalizer<SharedResource> _localizer => _localizerInstance ??= HttpContext.RequestServices.GetService<IHtmlLocalizer<SharedResource>>();
      
        protected INotyfService _notify => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();

        protected IViewRenderService _viewRenderer => _viewRenderInstance ??= HttpContext.RequestServices.GetService<IViewRenderService>();

        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();

        protected IConfiguration _configuration => _configurationInstance ??= HttpContext.RequestServices.GetService<IConfiguration>();



    }
}