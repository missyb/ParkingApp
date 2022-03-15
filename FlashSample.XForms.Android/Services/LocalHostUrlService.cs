using System;
using FlashSample.XForms.Services;

namespace FlashSample.XForms.Droid.Services
{
    public class LocalHostUrlService : ILocalHostUrlService
    {
        public string LocalHostUrl => "http://10.0.2.2:5000/";
    }
}
