namespace backend_challenge.Helpers
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
