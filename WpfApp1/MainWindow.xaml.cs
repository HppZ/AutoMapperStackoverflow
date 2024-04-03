using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            A a = new A();
            B b = new B();

            a.B = b;
            b.List.Add(a);

            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Profile1>();
            }));

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
}