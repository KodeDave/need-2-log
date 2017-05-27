using System;
using System.Reflection;
using System.Windows.Forms;

namespace N2L_Need_2_Log.gui
{
    /// <summary>
    /// Classe che definisce il comportamento del form FormAboutBox
    /// </summary>
    partial class FormAboutBox : Form
    {
        /// <summary>
        /// Costruttore della classe FormAboutBox
        /// </summary>
        public FormAboutBox()
        {
            InitializeComponent();
        }

        #region Funzioni di accesso attributo assembly
            private string AssemblyTitle
            {
                get
                {
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    if (attributes.Length > 0)
                    {
                        AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                        if (titleAttribute.Title != "")
                        {
                            return titleAttribute.Title;
                        }
                    }
                    return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                }
            }
            private string AssemblyVersion
            {
                get
                {
                    return Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
            }
            private string AssemblyDescription
            {
                get
                {
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                    if (attributes.Length == 0)
                    {
                        return "";
                    }
                    return ((AssemblyDescriptionAttribute)attributes[0]).Description;
                }
            }
            private string AssemblyProduct
            {
                get
                {
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                    if (attributes.Length == 0)
                    {
                        return "";
                    }
                    return ((AssemblyProductAttribute)attributes[0]).Product;
                }
            }
            private string AssemblyCopyright
            {
                get
                {
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                    if (attributes.Length == 0)
                    {
                        return "";
                    }
                    return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
                }
            }
            private string AssemblyCompany
            {
                get
                {
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                    if (attributes.Length == 0)
                    {
                        return "";
                    }
                    return ((AssemblyCompanyAttribute)attributes[0]).Company;
                }
            }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormAboutBox_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("Informazioni su {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Versione {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }
    }
}
