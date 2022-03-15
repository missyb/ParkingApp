using System;
using FlashSample.XForms.Services;

namespace FlashSample.XForms.iOS.Services
{
    public class LocalHostUrlService : ILocalHostUrlService
    {
        public string LocalHostUrl => "http://localhost:5000/";
    }
}
