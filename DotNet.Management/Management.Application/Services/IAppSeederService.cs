using Management.Domain;

namespace Management.Application
{
    public interface IAppSeederService: IScopedDependency
    {
        public Task SeedAsync();

        public void Test();
       
    }
}
