using System.ComponentModel;

namespace wooPrint.WindowsService
{
    [RunInstaller(true)]
    public partial class wooPrintServiceInstaller : System.Configuration.Install.Installer
    {
        public wooPrintServiceInstaller()
        {
            InitializeComponent();
        }
    }
}