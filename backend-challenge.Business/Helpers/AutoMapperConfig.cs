namespace backend_challenge.Business.Helpers
{
    public class AutoMapperConfig
    {
        public static Type[] RegisterMappings()
        {
            return new Type[]
            {
                typeof(EntityToDtoConfig)
            };
        }
    }
}
