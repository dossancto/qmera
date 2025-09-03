namespace Anv;
public static partial class AppEnv
{
    public static partial class DATABASES
    {
        public static partial class SQLITE
        {
            public static readonly AnvEnv DATASOURCE = new("DATABASES__SQLITE__DATASOURCE");
        }
    }
    public static readonly AnvEnv OTEL_SERVICE_NAME = new("OTEL_SERVICE_NAME");
    public static partial class TEST
    {
        public static partial class DATABASES
        {
            public static partial class SQLITE
            {
                public static readonly AnvEnv DATASOURCE = new("TEST__DATABASES__SQLITE__DATASOURCE");
            }
        }
    }
}
