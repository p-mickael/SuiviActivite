using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SuiviActivite.Business.Configuration
{
    public class ConfigManager
    {
        #region Singleton
        private static ConfigManager _instance;
        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigManager();
                }
                return _instance;
            }
        }
        #endregion

        private OfficeHours? _officeHours;
        public OfficeHours OfficeHours
        {
            get 
            {
                if (_officeHours == null)
                    LoadOfficeHours();

                return _officeHours.Value; 
            }
        }

        private ConfigManager()
        {
            LoadOfficeHours();
        }

        private void LoadOfficeHours()
        {
            string morningBeginStr = ConfigurationManager.AppSettings["morningBegin"];
            string morningEndStr = ConfigurationManager.AppSettings["morningEnd"];
            string afternoonBeginStr = ConfigurationManager.AppSettings["afternoonBegin"];
            string afternoonEndStr = ConfigurationManager.AppSettings["afternoonEnd"];

            DateTime parser;
            OfficeHours hours = new OfficeHours();

            if (DateTime.TryParse(morningBeginStr, out parser))
                hours.MorningBegin = parser;

            if (DateTime.TryParse(morningEndStr, out parser))
                hours.MorningEnd = parser;

            if (DateTime.TryParse(afternoonBeginStr, out parser))
                hours.AfternoonBegin = parser;

            if (DateTime.TryParse(afternoonEndStr, out parser))
                hours.AfternoonEnd = parser;

            _officeHours = hours;
        }

        public void SetNewOfficeHours(DateTime morningBegin, DateTime morningEnd, DateTime afternoonBegin, DateTime afternoonEnd)
        {
            OfficeHours hours = new OfficeHours();

            hours.MorningBegin = morningBegin;
            hours.MorningEnd = morningEnd;
            hours.AfternoonBegin = afternoonBegin;
            hours.AfternoonEnd = afternoonEnd;

            this._officeHours = hours;
            SaveOfficeHours();
        }

        private void SaveOfficeHours()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings["morningBegin"].Value = this._officeHours.Value.MorningBegin.ToString();
            config.AppSettings.Settings["morningEnd"].Value = this._officeHours.Value.MorningEnd.ToString();
            config.AppSettings.Settings["afternoonBegin"].Value = this._officeHours.Value.AfternoonBegin.ToString();
            config.AppSettings.Settings["afternoonEnd"].Value = this._officeHours.Value.AfternoonEnd.ToString();
            config.Save(ConfigurationSaveMode.Modified);

            Reload();
        }

        public void Reload()
        {
            this.LoadOfficeHours();
        }
    }
}
