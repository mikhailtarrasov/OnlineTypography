using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using NCron;
using Vera.Domain;

namespace Vera.CBR
{
    public class CurrenciesUpdater : CronJob
    {
        public override void Execute()
        {
            var xdoc = XDocument.Load("http://www.cbr.ru/scripts/XML_daily.asp").CreateReader();
            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));
            var valCurs = (ValCurs)serializer.Deserialize(xdoc);

            var db = new DatabaseContext();
            var currencies = db.Currencies;
            foreach (var currency in currencies)
            {
                if (currency.Name != "RUB")
                {
                    var valute = valCurs.Valute.FirstOrDefault(x => x.CharCode == currency.Name);
                    currency.Rate = Decimal.Parse(valute.Value) / valute.Nominal;
                }
            }
            db.SaveChanges();
        }
    }
}