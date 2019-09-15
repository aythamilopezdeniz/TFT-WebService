using Entities;
using NumbersTranslatorWebService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Text;

namespace NumbersTranslatorWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NumbersTranslatorWebService : INumbersTranslatorWebService
    {
        public List<List<string>> TranslateText(string text)
        {
            return new WebService().Convertion(text);
        }
    }
}