﻿namespace Sereno.Admin.Host.Options.OAuth2
{
    public class OAuth2Options
    {
        public const string Section = "OAuth2";

        public bool IsEnabled { get; set; }
        public string Application { get; set; }
        public string Authority { get; set; }
        public string Scope { get; set; }
    }
}
