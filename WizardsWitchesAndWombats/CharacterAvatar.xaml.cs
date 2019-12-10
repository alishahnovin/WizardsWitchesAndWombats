using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WizardsWitchesAndWombats
{
    /// <summary>
    /// Interaction logic for Character.xaml
    /// </summary>
    public partial class CharacterAvatar : UserControl
    {
        public CharacterAvatar()
        {
            InitializeComponent();
        }

        private CharacterTypes _CharacterType = CharacterTypes.Wizard;
        public CharacterTypes CharacterType
        {
            get
            {
                return _CharacterType;
            }
            set
            {

                Wizard.Visibility = System.Windows.Visibility.Collapsed;
                Witch.Visibility = System.Windows.Visibility.Collapsed;
                Wombat.Visibility = System.Windows.Visibility.Collapsed;

                _CharacterType = value;
                switch (_CharacterType)
                {
                    case CharacterTypes.Wizard:
                        {
                            Wizard.Visibility = System.Windows.Visibility.Visible;
                            break;
                        }
                    case CharacterTypes.Witch:
                        {
                            Witch.Visibility = System.Windows.Visibility.Visible;
                            break;
                        }
                    case CharacterTypes.Wombat:
                        {
                            Wombat.Visibility = System.Windows.Visibility.Visible;
                            break;
                        }
                }
            }
        }
    }
}
