using AutoMapper;

class Program
{
    static void Main(string[] args)
    {
        A a = new A();
        B b = new B();

        a.B = b;
        b.List.Add(a);

        var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<Profile1>(); }));

        var model = mapper.Map<AModel>(a);
    }
}

class A
{
    public B B { get; set; }
}

class B
{
    public List<A> List { get; set; }

    public B()
    {
        List = new List<A>();
    }
}

class AModel
{
    public BModel B { get; set; }
}

class BModel
{
    public List<AModel> List { get; set; }
}

class Profile1 : Profile
{
    public Profile1()
    {
        CreateMap<A, AModel>().ForMember(x => x.B, o => o.MapFrom<ValueResolver1>());
        CreateMap<B, BModel>();
    }
}

class ValueResolver1 : IValueResolver<A, AModel, BModel>
{
    public BModel Resolve(A source, AModel destination, BModel destMember, ResolutionContext context)
    {
        return context.Mapper.Map<BModel>(source.B);
    }
}