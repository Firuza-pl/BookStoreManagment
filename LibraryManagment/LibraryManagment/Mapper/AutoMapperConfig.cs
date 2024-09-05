using System.Reflection;
using AutoMapper;


namespace LibraryManagment.Mapper;
   internal class AutoMapperConfig
{
    public static IMapper CreateMapper()
    {
        var profileType = typeof(Profile);

        var profiles = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => profileType.IsAssignableFrom(t)
                        && t.GetConstructor(Type.EmptyTypes) != null)
            .Select(Activator.CreateInstance)
            .Cast<Profile>();

        var config = new MapperConfiguration(
            c =>
            {
                c.CreateMap<DateTime, string>().ConvertUsing(dt => dt.ToString("dd/MM/yyyy"));
                foreach (var profile in profiles)
                {
                    c.AddProfile(profile);
                }
            });

        return config.CreateMapper();
    }
}
