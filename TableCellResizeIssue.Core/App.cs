using Cirrious.CrossCore.IoC;

namespace TableCellResize.Core
{
    using TableCellResizeIssue.Core.ViewModels;

    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            RegisterAppStart<FirstViewModel>();
        }
    }
}