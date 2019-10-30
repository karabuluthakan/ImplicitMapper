using ImplicitMapper.Tests.Base;

namespace ImplicitMapper.Tests.DbEntity
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}