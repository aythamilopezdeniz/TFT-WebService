using System.Collections.Generic;
using System.ServiceModel;

namespace NumbersTranslatorWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface INumbersTranslatorWebService
    {
        [OperationContract]
        List<List<string>> TranslateText(string text);
    }
}
