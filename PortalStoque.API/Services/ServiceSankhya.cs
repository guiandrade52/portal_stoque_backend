namespace PortalStoque.API.Services
{
    public class ServiceSankhya
    {
        private static SWServiceInvokerJson instance;
        public static SWServiceInvokerJson Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SWServiceInvokerJson(Properties.Settings.Default.SankhyaURL,
                                                        Properties.Settings.Default.SankhyaUser,
                                                        Properties.Settings.Default.SankhyaPassword);
                }
                return instance;
            }
        }

        private static SWServiceInvoker service;
        public static SWServiceInvoker Service
        {
            get
            {
                if (service == null)
                {
                    service = new SWServiceInvoker(Properties.Settings.Default.SankhyaURL,
                                                   Properties.Settings.Default.SankhyaUser,
                                                   Properties.Settings.Default.SankhyaPassword);
                }
                return service;
            }
        }
    }
}