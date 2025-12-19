# Word Hidden Powers - надстройка к Microsoft Office Word (Скрытые возможности Word)

Разарабатывается с помощью IDE Visual Studio 2022.

Для локального запуска LLM я использовал [**Ollama**](https://ollama.com/download), а также модель [**gpt-oss-20b**](https://ollama.com/library/gpt-oss) и получил хорошие результаты.

Советую изучить дополнительную информацию о модели [здесь](https://openai.com/ru-RU/index/introducing-gpt-oss/) и [здесь](https://cookbook.openai.com/articles/openai-harmony).

В приложении использован ряд .NET библиотек, включая  [**openai-dotnet**](https://github.com/openai/openai-dotnet.git).
[![NuGet stable version](https://img.shields.io/nuget/v/openai.svg)](https://www.nuget.org/packages/OpenAI)

Для использования иных платформ и языков программирования следует обратиться к репозиторию [**OpenAI**](https://github.com/openai).

## Описание проекта

Функции для обращения к **ollama** содержаться в классе `WordHiddenPowers\WordHiddenPowers\Services\OpenAIService.cs`;
Функция `getCurrentWeatherTool` в данном классе имеет ссылку на файл ресурса `get_we.json`,
который размещен в папке: `WordHiddenPowers\WordHiddenPowers\Repository\Data\get_we.json`.
Этот файл необходим для описания формата обращения к функции:

```json
{
  "type": "object",
  "properties": {
    "location": {
      "type": "string",
      "description": "текущее местоположение"
    },
    "unit": {
      "type": "string",
      "enum": [ "celsius", "fahrenheit" ],
      "description": "единицы измерения температуры"
    }
  },
  "required": [ "location" ]
}
```

Указанный файл размещен рядом с классом `TableCollection`, так как является заготовкой для разработки функции для обращения LLM к коллекции таблиц.

```c#
private static readonly ChatTool getCurrentWeatherTool = ChatTool.CreateFunctionTool(
	functionName: nameof(GetCurrentWeather),
	functionDescription: "Погода в указанном месте",
	functionParameters: BinaryData.FromBytes(Utils.Resource.GetBytesResource("WordHiddenPowers.Repository.Data.get_we.json"))
);
```

Клиент (в разработке) для обращения к **ollama** содержится в классе `WordHiddenPowers\WordHiddenPowers\LLMService\LLMClient.cs` (не обязательный, используется для отображения окна програсса).

## TODO (это планы для меня):

- Поиск по словам или выражениям в категории ии подкатегории.
- При автоматическом заполнении необходимо последующее отключение кнопок подтверждения в NodeListBox
- При редактировании регулярных выражений каретка не должна перемещаться в конец текста.
- Следует перепроверить разблокировку кнопки ОК в редакторе регулярных выражений.
- При вызове добавления записи из контекстного меню не видны ComboBox
- Необходимо протестировать в Windows 10 Pro 22H2, Microsoft Word 2019 MSO (16.0.10352.20042), 32-разрядная версия
Microsoft Office Standard 2019 Версия 1808 (сборка 10354.20022)