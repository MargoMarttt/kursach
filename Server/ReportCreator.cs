using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPConnectionAPI_C_sharp_
{
    static public class ReportCreator
    {
        public static string CreateReportAboutCarriers()
        {
            using (IDataView db = new DatabaseWrapper())
            {
                var carriers = db.FindCarsWhere(c => c != null);
                string report = "Состояние машин в ремонте на " + DateTime.Now + "\n\n";
                foreach (var item in carriers)
                {
                    report += "Название машины: " + item.Name + "\n";
                    report += "Учётный номер: " + item.VendorCode + "\n";
                    report += "Цена ремонта: " + item.Cost +  "\n";
                    report += "Оценка экспертов: " + item.TotalRate + "\n";
                    report += '\n';
                }
                return report;
            }
        }
    }
}
