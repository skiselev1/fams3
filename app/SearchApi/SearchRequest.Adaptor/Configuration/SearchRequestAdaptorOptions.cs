﻿using System.Collections.Generic;

namespace SearchRequestAdaptor.Configuration
{
    /// <summary>
    /// Represents the Search Request Adaptor settings
    /// </summary>
    public class SearchRequestAdaptorOptions
    {
        public List<WebHookNotification> WebHooks { get; set; } = new List<WebHookNotification>();

        public SearchRequestAdaptorOptions AddWebHook(string name, string uri, string apikey=null)
        {
            WebHooks.Add(new WebHookNotification()
            {
                Name = name,
                Uri = uri,
                ApiKey = apikey
            });
            return this;
        }
    }

    public class WebHookNotification
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string ApiKey { get; set; }
    }
}
